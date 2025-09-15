namespace ANIMALITOS_PHARMA_API.Contract.DTO
{
    public class ClientDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal CreditLimit { get; set; }

        public int StatusId { get; set; }
        public AddressDTO? Address { get; set; }
        public List<CreditDTO> Credits { get; set; } = new();
    }

    public class AddressDTO
    {
        public int Id { get; set; }
        public string? Direction { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Rfc { get; set; }
    }

    public class CreditDTO
    {
        public int Id { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public decimal TotalDebt { get; set; }
        public int StatusId { get; set; }
        public List<PaymentDTO> Payments { get; set; } = new();
    }

    public class EmployeeDTO
    {
        public int? Id { get; set; }
        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;
    }

    public class PaymentDTO
    {
        public int? Id { get; set; }
        public decimal PaymentAmount { get; set; }
        public DateTime PaymentDate { get; set; }
        public EmployeeDTO? Employee { get; set; }
    }

    public class LotDto
    {
        public int LotId { get; set; }
        public DateTime DateReceipt { get; set; }
        public DateTime Expiration { get; set; }
        public int StatusId { get; set; }
        public int Quantity { get; set; }
    }

    public class ProductWithLotsDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public double UnitPrice { get; set; }
        public List<LotDto> Lots { get; set; } = new();
    }

    public class UserNotificationDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Message { get; set; }
        public bool IsRead { get; set; }
    }
}
