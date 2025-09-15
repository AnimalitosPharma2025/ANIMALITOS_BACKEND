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

    public virtual DbSet<CreditPayment> CreditPayments { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<InventoryItem> InventoryItems { get; set; }

    public virtual DbSet<Load> Loads { get; set; }

    public virtual DbSet<LoadsContent> LoadsContents { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<NotificationsUser> NotificationsUsers { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductLot> ProductLots { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<RolPermission> RolPermissions { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    public virtual DbSet<SaleItem> SaleItems { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserPermission> UserPermissions { get; set; }

    public virtual DbSet<UserRol> UserRols { get; set; }

    public virtual DbSet<Vendor> Vendors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=SQL1004.site4now.net;Database=db_abac9a_animalitospharma;User Id=db_abac9a_animalitospharma_admin;Password=QEdTsFa2TE92y8zc;TrustServerCertificate=True;MultipleActiveResultSets=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AddressBook>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ADDRESS___3214EC27CEE03FFA");

            entity.ToTable("ADDRESS_BOOK");

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
            entity.HasKey(e => e.Id).HasName("PK__CLIENTS__3214EC2752E8D72B");

            entity.ToTable("CLIENTS");

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
            entity.HasKey(e => e.Id).HasName("PK__CREDITS__3214EC274B3985A0");

            entity.ToTable("CREDITS");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AuthorizeCredit).HasColumnName("AUTHORIZE_CREDIT");
            entity.Property(e => e.ClientId).HasColumnName("CLIENT_ID");
            entity.Property(e => e.EmployeeId).HasColumnName("EMPLOYEE_ID");
            entity.Property(e => e.ExpirationDate)
                .HasColumnType("datetime")
                .HasColumnName("EXPIRATION_DATE");
            entity.Property(e => e.IsSale)
                .HasDefaultValue(false)
                .HasColumnName("IS_SALE");
            entity.Property(e => e.PurchaseDate)
                .HasColumnType("datetime")
                .HasColumnName("PURCHASE_DATE");
            entity.Property(e => e.StatusId).HasColumnName("STATUS_ID");
            entity.Property(e => e.TotalDebt).HasColumnName("TOTAL_DEBT");

            entity.HasOne(d => d.Client).WithMany(p => p.Credits)
                .HasForeignKey(d => d.ClientId)
                .HasConstraintName("FK_CREDITS_CLIENTS");

            entity.HasOne(d => d.Employee).WithMany(p => p.Credits)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_CREDITS_EMPLOYEES");

            entity.HasOne(d => d.Status).WithMany(p => p.Credits)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CREDITS_STATUS");
        });

        modelBuilder.Entity<CreditPayment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CREDIT_P__3214EC2753DAB4E9");

            entity.ToTable("CREDIT_PAYMENTS");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreditId).HasColumnName("CREDIT_ID");
            entity.Property(e => e.EmployeeId).HasColumnName("EMPLOYEE_ID");
            entity.Property(e => e.PaymentAmount).HasColumnName("PAYMENT_AMOUNT");
            entity.Property(e => e.PaymentDate)
                .HasColumnType("datetime")
                .HasColumnName("PAYMENT_DATE");
            entity.Property(e => e.StatusId).HasColumnName("STATUS_ID");

            entity.HasOne(d => d.Employee).WithMany(p => p.CreditPayments)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_CREDIT_PAYMENTS_EMPLOYEES");

            entity.HasOne(d => d.Status).WithMany(p => p.CreditPayments)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("FK_CREDITS_PAYMENT_STATUS");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__EMPLOYEE__3214EC2766FDAF7F");

            entity.ToTable("EMPLOYEES");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AddressId).HasColumnName("ADDRESS_ID");
            entity.Property(e => e.DailyAmount).HasColumnName("DAILY_AMOUNT");
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
            entity.HasKey(e => e.Id).HasName("PK__INVENTOR__3214EC2767320ED1");

            entity.ToTable("INVENTORY_ITEM");

            entity.Property(e => e.Id).HasColumnName("ID");
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

        modelBuilder.Entity<Load>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LOADS__3214EC27E89BCB6E");

            entity.ToTable("LOADS");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("CREATED_DATE");
            entity.Property(e => e.EmployeeId).HasColumnName("EMPLOYEE_ID");
            entity.Property(e => e.LoadValue).HasColumnName("LOAD_VALUE");
            entity.Property(e => e.StatusId).HasColumnName("STATUS_ID");

            entity.HasOne(d => d.Employee).WithMany(p => p.Loads)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_LOADS_EMPLOYEES");

            entity.HasOne(d => d.Status).WithMany(p => p.Loads)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("FK_LOADS_STATUS");
        });

        modelBuilder.Entity<LoadsContent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LOADS_CO__3214EC27CA4D0629");

            entity.ToTable("LOADS_CONTENT");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.InventoryId).HasColumnName("INVENTORY_ID");
            entity.Property(e => e.LoadId).HasColumnName("LOAD_ID");
            entity.Property(e => e.StatusId).HasColumnName("STATUS_ID");

            entity.HasOne(d => d.Inventory).WithMany(p => p.LoadsContents)
                .HasForeignKey(d => d.InventoryId)
                .HasConstraintName("FK_LOADS_CONTENT_INVENTORY");

            entity.HasOne(d => d.Load).WithMany(p => p.LoadsContents)
                .HasForeignKey(d => d.LoadId)
                .HasConstraintName("FK_LOADS_CONTENT_LOAD");

            entity.HasOne(d => d.Status).WithMany(p => p.LoadsContents)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("FK_LOADS_CONTENT_STATUS");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__NOTIFICA__3214EC2724136920");

            entity.ToTable("NOTIFICATIONS");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Message)
                .IsUnicode(false)
                .HasColumnName("MESSAGE");
            entity.Property(e => e.StatusId).HasColumnName("STATUS_ID");
            entity.Property(e => e.Title)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("TITLE");

            entity.HasOne(d => d.Status).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("FK_NOTIFICATIONS_STATUS");
        });

        modelBuilder.Entity<NotificationsUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__NOTIFICA__3214EC27EC79CE34");

            entity.ToTable("NOTIFICATIONS_USER");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.IsRead).HasColumnName("IS_READ");
            entity.Property(e => e.NotificationId).HasColumnName("NOTIFICATION_ID");
            entity.Property(e => e.StatusId).HasColumnName("STATUS_ID");
            entity.Property(e => e.UserId).HasColumnName("USER_ID");

            entity.HasOne(d => d.Notification).WithMany(p => p.NotificationsUsers)
                .HasForeignKey(d => d.NotificationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NOTIFICATIONS_USER_NOTIFICATIONS");

            entity.HasOne(d => d.Status).WithMany(p => p.NotificationsUsers)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("FK_NOTIFICATIONS_USER_STATUS");

            entity.HasOne(d => d.User).WithMany(p => p.NotificationsUsers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_NOTIFICATIONS_USER_USER");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PERMISSI__3214EC2757E5CCF9");

            entity.ToTable("PERMISSION");

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
            entity.HasKey(e => e.Id).HasName("PK__PRODUCT__3214EC276E4FB5BE");

            entity.ToTable("PRODUCT");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Category)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("CATEGORY");
            entity.Property(e => e.Code)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("CODE");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.Discount)
                .HasDefaultValue(0.0)
                .HasColumnName("DISCOUNT");
            entity.Property(e => e.ImageUrl)
                .IsUnicode(false)
                .HasColumnName("IMAGE_URL");
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
            entity.HasKey(e => e.Id).HasName("PK__PRODUCT___3214EC27DC74C3B1");

            entity.ToTable("PRODUCT_LOT");

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
            entity.HasKey(e => e.Id).HasName("PK__ROL__3214EC27BC3BAA01");

            entity.ToTable("ROL");

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
            entity.HasKey(e => e.Id).HasName("PK__ROL_PERM__3214EC2731E245DA");

            entity.ToTable("ROL_PERMISSION");

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
            entity.HasKey(e => e.Id).HasName("PK__SALES__3214EC2727041B3A");

            entity.ToTable("SALES");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ClientId).HasColumnName("CLIENT_ID");
            entity.Property(e => e.EmployeeId).HasColumnName("EMPLOYEE_ID");
            entity.Property(e => e.PurchaseDate)
                .HasColumnType("datetime")
                .HasColumnName("PURCHASE_DATE");
            entity.Property(e => e.StatusId).HasColumnName("STATUS_ID");
            entity.Property(e => e.Total)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("TOTAL");
        });

        modelBuilder.Entity<SaleItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SALE_ITE__3214EC27A9BB0A0B");

            entity.ToTable("SALE_ITEMS");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Discount)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("DISCOUNT");
            entity.Property(e => e.InventoryId).HasColumnName("INVENTORY_ID");
            entity.Property(e => e.SaleId).HasColumnName("SALE_ID");
            entity.Property(e => e.UnitPrice)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("UNIT_PRICE");

            entity.HasOne(d => d.Inventory).WithMany(p => p.SaleItems)
                .HasForeignKey(d => d.InventoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SALE_ITEM__INVEN__3493CFA7");

            entity.HasOne(d => d.Sale).WithMany(p => p.SaleItems)
                .HasForeignKey(d => d.SaleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SALE_ITEM__SALE___339FAB6E");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__STATUS__3214EC274BF1D45F");

            entity.ToTable("STATUS");

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
            entity.HasKey(e => e.Id).HasName("PK__USER__3214EC27F66C3CF9");

            entity.ToTable("USER");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.EmployeeId).HasColumnName("EMPLOYEE_ID");
            entity.Property(e => e.ImageUrl)
                .IsUnicode(false)
                .HasColumnName("IMAGE_URL");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
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
            entity.HasKey(e => e.Id).HasName("PK__USER_PER__3214EC2732B1CBA2");

            entity.ToTable("USER_PERMISSION");

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

        modelBuilder.Entity<UserRol>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("USER_ROL");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.RolId).HasColumnName("ROL_ID");
            entity.Property(e => e.StatusId).HasColumnName("STATUS_ID");
            entity.Property(e => e.UserId).HasColumnName("USER_ID");

            entity.HasOne(d => d.Rol).WithMany()
                .HasForeignKey(d => d.RolId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_USER_ROL_ROL");

            entity.HasOne(d => d.Status).WithMany()
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_USER_ROL_STATUS");

            entity.HasOne(d => d.User).WithMany()
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_USER_ROL_USER");
        });

        modelBuilder.Entity<Vendor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__VENDOR__3214EC27B586201D");

            entity.ToTable("VENDOR");

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
