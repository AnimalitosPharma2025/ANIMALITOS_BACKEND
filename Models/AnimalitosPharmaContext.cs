using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ANIMALITOS_PHARMA_API.Models;

public partial class AnimalitosPharmaContext : DbContext
{
    private readonly bool _useRetryLogic;
    public AnimalitosPharmaContext(bool useRetryLogic)
    {
        _useRetryLogic = useRetryLogic;
    }

    public AnimalitosPharmaContext(DbContextOptions<AnimalitosPharmaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AddressBook> AddressBooks { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Credit> Credits { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<InventoryItem> InventoryItems { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductLot> ProductLots { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<RolPermission> RolPermissions { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserPermission> UserPermissions { get; set; }

    public virtual DbSet<Vendor> Vendors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured) return;
        var connectionString = "workstation id=ANIMALITOS_PHARMA.mssql.somee.com;packet size=4096;user id=smarredondo_SQLLogin_1;pwd=h1et2z4bd4;data source=ANIMALITOS_PHARMA.mssql.somee.com;persist security info=False;initial catalog=ANIMALITOS_PHARMA;TrustServerCertificate=True";
        if (_useRetryLogic)
        {
            optionsBuilder.UseSqlServer(
                connectionString,
                options => options.EnableRetryOnFailure());
        }
        else
        {
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("workstation id=ANIMALITOS_PHARMA.mssql.somee.com;packet size=4096;user id=smarredondo_SQLLogin_1;pwd=h1et2z4bd4;data source=ANIMALITOS_PHARMA.mssql.somee.com;persist security info=False;initial catalog=ANIMALITOS_PHARMA;TrustServerCertificate=True");


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AddressBook>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ADDRESS___3214EC27C8F1C858");

            entity.ToTable("ADDRESS_BOOK", "dbo");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Direction)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("DIRECTION");
            entity.Property(e => e.Email)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("PHONE");
            entity.Property(e => e.Rfc)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("RFC");
            entity.Property(e => e.StatusId).HasColumnName("STATUS_ID");

            entity.HasOne(d => d.Status).WithMany(p => p.AddressBooks)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ADDRESS_STATUS");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CLIENTS__3214EC27426B762F");

            entity.ToTable("CLIENTS", "dbo");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AddressId).HasColumnName("ADDRESS_ID");
            entity.Property(e => e.CreditLimit).HasColumnName("CREDIT_LIMIT");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("NAME");
            entity.Property(e => e.StatusId).HasColumnName("STATUS_ID");

            entity.HasOne(d => d.Address).WithMany(p => p.Clients)
                .HasForeignKey(d => d.AddressId)
                .HasConstraintName("FK_CLIENTS_ADDRESS");

            entity.HasOne(d => d.Status).WithMany(p => p.Clients)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CLIENTS_STATUS");
        });

        modelBuilder.Entity<Credit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CREDITS__3214EC2730CD2BC0");

            entity.ToTable("CREDITS", "dbo");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ExpirationDate)
                .HasColumnType("datetime")
                .HasColumnName("EXPIRATION_DATE");
            entity.Property(e => e.PurchaseDate)
                .HasColumnType("datetime")
                .HasColumnName("PURCHASE_DATE");
            entity.Property(e => e.SaleId).HasColumnName("SALE_ID");
            entity.Property(e => e.StatusId).HasColumnName("STATUS_ID");

            entity.HasOne(d => d.Sale).WithMany(p => p.Credits)
                .HasForeignKey(d => d.SaleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CREDITS_SALES");

            entity.HasOne(d => d.Status).WithMany(p => p.Credits)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CREDITS_STATUS");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__EMPLOYEE__3214EC2798F56873");

            entity.ToTable("EMPLOYEES", "dbo");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AddressId).HasColumnName("ADDRESS_ID");
            entity.Property(e => e.LastName)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("LAST_NAME");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("NAME");
            entity.Property(e => e.Rol)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ROL");
            entity.Property(e => e.StatusId).HasColumnName("STATUS_ID");

            entity.HasOne(d => d.Address).WithMany(p => p.Employees)
                .HasForeignKey(d => d.AddressId)
                .HasConstraintName("FK_EMPLOYEES_ADDRESS");

            entity.HasOne(d => d.Status).WithMany(p => p.Employees)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EMPLOYEES_STATUS");
        });

        modelBuilder.Entity<InventoryItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__INVENTOR__3214EC2701FC66AC");

            entity.ToTable("INVENTORY_ITEM", "dbo");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Code)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("CODE");
            entity.Property(e => e.EmployeeId).HasColumnName("EMPLOYEE_ID");
            entity.Property(e => e.ProductId).HasColumnName("PRODUCT_ID");
            entity.Property(e => e.ProductLotId).HasColumnName("PRODUCT_LOT_ID");
            entity.Property(e => e.StatusId).HasColumnName("STATUS_ID");

            entity.HasOne(d => d.Employee).WithMany(p => p.InventoryItems)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_INVENTORY_EMPLOYEE");

            entity.HasOne(d => d.Status).WithMany(p => p.InventoryItems)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_INVENTORY_STATUS");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PERMISSI__3214EC27EA8AC4C9");

            entity.ToTable("PERMISSION", "dbo");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Description)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.Name)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("NAME");
            entity.Property(e => e.StatusId).HasColumnName("STATUS_ID");

            entity.HasOne(d => d.Status).WithMany(p => p.Permissions)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PERMISSION_STATUS");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PRODUCT__3214EC27DE84CC1A");

            entity.ToTable("PRODUCT", "dbo");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Category)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("CATEGORY");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.Name)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("NAME");
            entity.Property(e => e.PurchasePrice).HasColumnName("PURCHASE_PRICE");
            entity.Property(e => e.StatusId).HasColumnName("STATUS_ID");
            entity.Property(e => e.UnitPrice).HasColumnName("UNIT_PRICE");
            entity.Property(e => e.VendorId).HasColumnName("VENDOR_ID");

            entity.HasOne(d => d.Status).WithMany(p => p.Products)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PRODUCT_STATUS");

            entity.HasOne(d => d.Vendor).WithMany(p => p.Products)
                .HasForeignKey(d => d.VendorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PRODUCT_VENDOR");
        });

        modelBuilder.Entity<ProductLot>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PRODUCT___3214EC2799AB258C");

            entity.ToTable("PRODUCT_LOT", "dbo");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DateReceipt)
                .HasColumnType("datetime")
                .HasColumnName("DATE_RECEIPT");
            entity.Property(e => e.Expiration)
                .HasColumnType("datetime")
                .HasColumnName("EXPIRATION");
            entity.Property(e => e.StatusId).HasColumnName("STATUS_ID");

            entity.HasOne(d => d.Status).WithMany(p => p.ProductLots)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PRODUCT_LOT_STATUS");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ROL__3214EC27C94DE3AF");

            entity.ToTable("ROL", "dbo");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Description)
                .HasMaxLength(35)
                .IsUnicode(false)
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.Name)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("NAME");
            entity.Property(e => e.StatusId).HasColumnName("STATUS_ID");

            entity.HasOne(d => d.Status).WithMany(p => p.Rols)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ROL_STATUS");
        });

        modelBuilder.Entity<RolPermission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ROL_PERM__3214EC279BCB854C");

            entity.ToTable("ROL_PERMISSION", "dbo");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.PermissionId).HasColumnName("PERMISSION_ID");
            entity.Property(e => e.RolId).HasColumnName("ROL_ID");
            entity.Property(e => e.StatusId).HasColumnName("STATUS_ID");

            entity.HasOne(d => d.Permission).WithMany(p => p.RolPermissionPermissions)
                .HasForeignKey(d => d.PermissionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ROL_PERMISSION_PERMISSION");

            entity.HasOne(d => d.Rol).WithMany(p => p.RolPermissionRols)
                .HasForeignKey(d => d.RolId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ROL_PERMISSION_ROL");

            entity.HasOne(d => d.Status).WithMany(p => p.RolPermissions)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ROL_PERMISSION_STATUS");
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SALES__3214EC274D1A547C");

            entity.ToTable("SALES", "dbo");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ClientId).HasColumnName("CLIENT_ID");
            entity.Property(e => e.EmployeeId).HasColumnName("EMPLOYEE_ID");
            entity.Property(e => e.InventoryId).HasColumnName("INVENTORY_ID");
            entity.Property(e => e.PurchaseDate)
                .HasColumnType("datetime")
                .HasColumnName("PURCHASE_DATE");
            entity.Property(e => e.StatusId).HasColumnName("STATUS_ID");

            entity.HasOne(d => d.Client).WithMany(p => p.Sales)
                .HasForeignKey(d => d.ClientId)
                .HasConstraintName("FK_SALES_CLIENTS");

            entity.HasOne(d => d.Employee).WithMany(p => p.Sales)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SALES_EMPLOYEES");

            entity.HasOne(d => d.Inventory).WithMany(p => p.Sales)
                .HasForeignKey(d => d.InventoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SALES_INVENTORY");

            entity.HasOne(d => d.Status).WithMany(p => p.Sales)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SALES_STATUS");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__STATUS__3214EC274342BAE0");

            entity.ToTable("STATUS", "dbo");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Description)
                .HasMaxLength(35)
                .IsUnicode(false)
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.Name)
                .HasMaxLength(55)
                .IsUnicode(false)
                .HasColumnName("NAME");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__USER__3214EC27285B130A");

            entity.ToTable("USER", "dbo");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.EmployeeId).HasColumnName("EMPLOYEE_ID");
            entity.Property(e => e.Password)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("PASSWORD");
            entity.Property(e => e.StatusId).HasColumnName("STATUS_ID");
            entity.Property(e => e.Username)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("USERNAME");

            entity.HasOne(d => d.Employee).WithMany(p => p.Users)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_USER_EMPLOYEES");

            entity.HasOne(d => d.Status).WithMany(p => p.Users)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_USER_STATUS");
        });

        modelBuilder.Entity<UserPermission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__USER_PER__3214EC27770FF754");

            entity.ToTable("USER_PERMISSION", "dbo");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.PermissionId).HasColumnName("PERMISSION_ID");
            entity.Property(e => e.StatusId).HasColumnName("STATUS_ID");
            entity.Property(e => e.UserId).HasColumnName("USER_ID");

            entity.HasOne(d => d.Permission).WithMany(p => p.UserPermissions)
                .HasForeignKey(d => d.PermissionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_USER_PERMISSION_PERMISSION");

            entity.HasOne(d => d.Status).WithMany(p => p.UserPermissions)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_USER_PERMISSION_STATUS");

            entity.HasOne(d => d.User).WithMany(p => p.UserPermissions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_USER_PERMISSION_USER");
        });

        modelBuilder.Entity<Vendor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__VENDOR__3214EC27554228E2");

            entity.ToTable("VENDOR", "dbo");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AddressId).HasColumnName("ADDRESS_ID");
            entity.Property(e => e.Name)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("NAME");
            entity.Property(e => e.StatusId).HasColumnName("STATUS_ID");

            entity.HasOne(d => d.Address).WithMany(p => p.Vendors)
                .HasForeignKey(d => d.AddressId)
                .HasConstraintName("FK_VENDOR_ADDRESS");

            entity.HasOne(d => d.Status).WithMany(p => p.Vendors)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VENDOR_STATUS");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
