using System;
using System.Collections.Generic;

namespace ANIMALITOS_PHARMA_API.Models;

public partial class Status
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<AddressBook> AddressBooks { get; set; } = new List<AddressBook>();

    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();

    public virtual ICollection<CreditPayment> CreditPayments { get; set; } = new List<CreditPayment>();

    public virtual ICollection<Credit> Credits { get; set; } = new List<Credit>();

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<InventoryItem> InventoryItems { get; set; } = new List<InventoryItem>();

    public virtual ICollection<Load> Loads { get; set; } = new List<Load>();

    public virtual ICollection<LoadsContent> LoadsContents { get; set; } = new List<LoadsContent>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<NotificationsUser> NotificationsUsers { get; set; } = new List<NotificationsUser>();

    public virtual ICollection<Permission> Permissions { get; set; } = new List<Permission>();

    public virtual ICollection<ProductLot> ProductLots { get; set; } = new List<ProductLot>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual ICollection<RolPermission> RolPermissions { get; set; } = new List<RolPermission>();

    public virtual ICollection<Rol> Rols { get; set; } = new List<Rol>();

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();

    public virtual ICollection<UserPermission> UserPermissions { get; set; } = new List<UserPermission>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();

    public virtual ICollection<Vendor> Vendors { get; set; } = new List<Vendor>();
}
