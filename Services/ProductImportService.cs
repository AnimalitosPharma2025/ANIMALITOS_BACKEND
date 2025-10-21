using ANIMALITOS_PHARMA_API.Accessors.Util.StatusEnumerable;
using ANIMALITOS_PHARMA_API.Models;
using ClosedXML.Excel;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;

namespace ANIMALITOS_PHARMA_API.Services
{
    public class ProductImportService
    {
        private readonly AnimalitosPharmaContext _context;
        private static readonly Random _random = new();

        public ProductImportService(AnimalitosPharmaContext context)
        {
            _context = context;
        }

        public async Task ImportProductsAsync(Stream fileStream)
        {
            using var workbook = new XLWorkbook(fileStream);
            var worksheet = workbook.Worksheets.First();

            var rows = worksheet.RowsUsed().Skip(1); // Saltar encabezados

            foreach (var row in rows)
            {
                string name = row.Cell(1).GetString().Trim();
                string description = row.Cell(2).GetString().Trim();
                string categoryName = row.Cell(3).GetString().Trim();
                decimal purchasePrice = ParseDecimal(row.Cell(4));
                decimal unitPrice = ParseDecimal(row.Cell(5));
                string vendorName = row.Cell(6).GetString().Trim();
                double discount = ParseDouble(row.Cell(7));

                if (string.IsNullOrWhiteSpace(name))
                    continue; // saltar filas vacías

                // Buscar proveedor existente o crear uno nuevo
                var vendor = await _context.Vendors
                    .FirstOrDefaultAsync(v => v.Name.ToLower() == vendorName.ToLower());

                if (vendor == null)
                {
                    vendor = new Vendor
                    {
                        Name = vendorName,
                        CreatedAt = DateTime.Now
                    };

                    _context.Vendors.Add(vendor);
                    await _context.SaveChangesAsync(); // guardar para obtener el ID
                }

                // Buscar categoría (si tienes tabla de categorías)
                var category = await _context.Categories
                    .FirstOrDefaultAsync(c => c.Name.ToLower() == categoryName.ToLower());

                if (category == null)
                {
                    category = new Category { Name = categoryName };
                    _context.Categories.Add(category);
                    await _context.SaveChangesAsync();
                }

                // Buscar producto existente (por nombre o código)
                var existingProduct = await _context.Products
                    .Include(p => p.Vendor)
                    .FirstOrDefaultAsync(p => p.Name.ToLower() == name.ToLower());

                if (existingProduct != null)
                {
                    // Solo actualizamos si algo cambió
                    bool updated = false;

                    if (existingProduct.Description != description)
                    {
                        existingProduct.Description = description;
                        updated = true;
                    }

                    if (existingProduct.CategoryId != category.Id)
                    {
                        existingProduct.CategoryId = category.Id;
                        updated = true;
                    }

                    if (existingProduct.PurchasePrice != purchasePrice)
                    {
                        existingProduct.PurchasePrice = purchasePrice;
                        updated = true;
                    }

                    if (existingProduct.UnitPrice != unitPrice)
                    {
                        existingProduct.UnitPrice = unitPrice;
                        updated = true;
                    }

                    if (existingProduct.Discount != discount)
                    {
                        existingProduct.Discount = discount;
                        updated = true;
                    }

                    if (existingProduct.VendorId != vendor.Id)
                    {
                        existingProduct.VendorId = vendor.Id;
                        updated = true;
                    }

                    if (updated)
                    {
                        existingProduct.UpdatedAt = DateTime.Now;
                        _context.Products.Update(existingProduct);
                    }
                }
                else
                {
                    // Crear nuevo producto
                    string code = GenerateProductCode(categoryName, name);

                    var newProduct = new Product
                    {
                        Name = name,
                        Description = description,
                        CategoryId = category.Id,
                        PurchasePrice = purchasePrice,
                        UnitPrice = unitPrice,
                        VendorId = vendor.Id,
                        Discount = discount,
                        Code = code,
                        CreatedAt = DateTime.Now
                    };

                    _context.Products.Add(newProduct);
                }
            }

            await _context.SaveChangesAsync();
        }

        public static string GenerateProductCode(string categoryName, string productName, int sequence = 0)
        {
            string prefix = Regex.Replace(categoryName.ToUpper(), "[^A-Z0-9]", "");
            prefix = prefix.Length >= 3 ? prefix.Substring(0, 3) : prefix.PadRight(3, 'X');

            string productInitial = string.IsNullOrEmpty(productName)
                ? "X"
                : productName.ToUpper()[0].ToString();

            string datePart = DateTime.Now.ToString("yyMMdd");

            string randomPart = _random.Next(1000, 9999).ToString();

            string guidPart = Guid.NewGuid().ToString("N").Substring(0, 2).ToUpper();
            return $"{prefix}{productInitial}-{datePart}-{guidPart}";
        }

        private decimal ParseDecimal(IXLCell cell)
        {
            if (cell == null || cell.IsEmpty()) return 0m;

            if (cell.DataType == XLDataType.Number)
                return (decimal)cell.GetDouble();

            if (decimal.TryParse(cell.GetString().Trim(), out decimal value))
                return value;

            return 0m;
        }

        private double ParseDouble(IXLCell cell)
        {
            if (cell == null || cell.IsEmpty()) return 0d;

            if (cell.DataType == XLDataType.Number)
                return cell.GetDouble();

            if (double.TryParse(cell.GetString().Trim(), out double value))
                return value;

            return 0d;
        }

    }
}
