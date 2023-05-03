using System;
using API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;

namespace API.Data
{
    public class DataContext : IdentityDbContext<AppUser,AppRole, int,
      IdentityUserClaim<int>, AppUserRole, IdentityUserLogin<int>,
      IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContext;
        public DataContext(DbContextOptions<DataContext> options, IConfiguration config,
        IHttpContextAccessor httpContext) : base(options)
        {
            _httpContext = httpContext;
            _config = config;
        }
    public DbSet<Country> Countries { get; set; }
    public DbSet<UserType> UserTypes { get; set; }
    public DbSet<Bank> Banks { get; set; }
    // public DbSet<SchoolTraineeShip> SchoolTraineeShips { get; set; }
    public DbSet<Doc> Docs { get; set; }
    public DbSet<DocType> DocTypes { get; set; }
    public DbSet<Capability> Capabilities { get; set; }
    public DbSet<Connection> Connections { get; set; }
    public DbSet<EmailType> EmailTypes { get; set; }
    public DbSet<Email> Emails { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Menu> Menus { get; set; }
    public DbSet<MenuItem> MenuItems { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<Photo> Photos { get; set; }
    public DbSet<SmsType> SmsTypes { get; set; }
    public DbSet<DeadLine> DeadLines { get; set; }
    public DbSet<Discount> Discounts { get; set; }
    public DbSet<DiscountType> DiscountTypes { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductType> ProductTypes { get; set; }
    public DbSet<RegFeeType> RegFeeTypes { get; set; }
    public DbSet<Periodicity> Periodicities { get; set; }
    public DbSet<EmailTemplate> EmailTemplates { get; set; }
    public DbSet<EmailCategory> EmailCategories{ get; set; }
    public DbSet<Fee> Fees{ get; set; }
    public DbSet<FinOp> FinOps{ get; set; }
    public DbSet<BankAccount> BankAccounts{ get; set; }
    public DbSet<CashDesk> CashDesks{ get; set; }
    public DbSet<PaymentType> PaymentTypes{ get; set; }
    public DbSet<Cheque> Cheques{ get; set; }
    public DbSet<FinOpType> FinOpTypes{ get; set; }
    public DbSet<Invoice> Invoices{ get; set; }
    public DbSet<OrderLine> OrderLines{ get; set; }
    public DbSet<Entities.Order> Orders{ get; set; }
    public DbSet<OrderHistory> OrderHistories{ get; set; }
    public DbSet<OrderLineDeadline> OrderLineDeadlines{ get; set; }
    public DbSet<OrderType> OrderTypes{ get; set; }
    public DbSet<OrderLineHistory> OrderLineHistories{ get; set; }
    public DbSet<OrderLineReceipt> OrderLineReceipts{ get; set; }
    public DbSet<OrderLineRegFee> OrderLineRegFees{ get; set; }
    public DbSet<Receipt> Receipts{ get; set; }
    public DbSet<HaKaDocClient> HaKaDocClients{ get; set; }
    public DbSet<Store> Stores{ get; set; }
    public DbSet<StoreUser> StoreUsers{ get; set; }
    public DbSet<MaritalStatus> MaritalStatuses{ get; set; }
    public DbSet<Customer> Customers{ get; set; }
    public DbSet<City> Cities{ get; set; }
    public DbSet<ProductGroup> ProductGroups{ get; set; }
    public DbSet<Category> Categories{ get; set; }
    public DbSet<Feature> Features{ get; set; }
    public DbSet<InventOp> InventOps{ get; set; }
    public DbSet<InventOpType> InventOpTypes{ get; set; }
    public DbSet<StockHistory> StockHistories{ get; set; }
    public DbSet<StockHistoryAction> StockHistoryActions{ get; set; }
    public DbSet<StockMvt> StockMvts{ get; set; }
    public DbSet<StockMvtInventOp> StockMvtInventOps{ get; set; }
    public DbSet<StoreProduct> StoreProducts{ get; set; }
    public DbSet<SubProduct> SubProducts{ get; set; }
    public DbSet<SubProductFeature> SubProductFeatures{ get; set; }
    public DbSet<Entities.Type> Types{ get; set; }
    public DbSet<District> Districts{ get; set; }
    public DbSet<SubProductSN> SubProductSNs{ get; set; }
    public DbSet<InventOpSubProductSN> InventOpSubProductSNs{ get; set; }
    public DbSet<CustomerCode> CustomerCodes{ get; set; }
    public DbSet<InvoiceOrderLine> InvoiceOrderLines{ get; set; }
    public DbSet<InvoiceTemplate> InvoiceTemplates{ get; set; }
    public DbSet<OrderLineSubProductSN> OrderLineSubProductSNs{ get; set; }
    public DbSet<NotificationType> NotificationTypes{ get; set; }
    public DbSet<Notification> Notifications{ get; set; }


    
   
       // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     string subdomain = "EducNotes";
    //     //To get subdomain
    //     string[] fullAddress = _httpContext.HttpContext?.Request?.Headers?["Host"].ToString()?.Split('.');
    //     if (fullAddress != null)
    //     {
    //         subdomain = fullAddress[0].ToLower();
    //         if (subdomain == "localhost:5000" || subdomain == "test2")
    //         {
    //             subdomain = "educnotes";
    //         }
    //         else if (subdomain == "test1" || subdomain == "www" || subdomain == "educnotes")
    //         {
    //             subdomain = "demo";
    //         }
    //     }
    //     string tenantConnString = string.Format(_config.GetConnectionString("DefaultConnection"), $"{subdomain}");
    //     optionsBuilder.UseSqlServer(tenantConnString);
    //     base.OnConfiguring(optionsBuilder);
    // }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        
        base.OnModelCreating (builder);

        builder.Entity<AppUserRole> (userRole => {
          userRole.HasKey (ur => new { ur.UserId, ur.RoleId });

          userRole.HasOne (ur => ur.Role)
            .WithMany (r => r.UserRoles)
            .HasForeignKey (ur => ur.RoleId)
            .IsRequired ();

          userRole.HasOne (ur => ur.User)
            .WithMany (r => r.UserRoles)
            .HasForeignKey (ur => ur.UserId)
            .IsRequired ();
        });

      

        builder.Entity<Message> ()
          .HasOne (u => u.Sender)
          .WithMany (u => u.MessagesSent)
          .OnDelete (DeleteBehavior.Restrict);

        builder.Entity<Message> ()
          .HasOne (u => u.Recipient)
          .WithMany (u => u.MessagesReceived)
          .OnDelete (DeleteBehavior.Restrict);

        builder.Entity<Email> ()
          .HasOne (u => u.Sender)
          .WithMany (u => u.EmailsSent)
          .OnDelete (DeleteBehavior.Restrict);

        builder.Entity<Email> ()
          .HasOne (u => u.Recipient)
          .WithMany (u => u.EmailsReceived)
          .OnDelete (DeleteBehavior.Restrict);

        builder.Entity<Photo>().HasQueryFilter(p => p.IsApproved);
       // builder.Entity<AppUser>().HasQueryFilter(u => u.Active == 1);


        builder.Entity<AppUser>()
           .HasMany(ur => ur.UserRoles)
           .WithOne(u => u.User)
           .HasForeignKey(ur => ur.UserId)
           .IsRequired();

        builder.Entity<AppRole>()
            .HasMany(ur => ur.UserRoles) 
            .WithOne(u => u.Role)
            .HasForeignKey(ur => ur.RoleId)
            .IsRequired();

        builder.ApplyUtcDateTimeConverter();

           builder.Entity<Category>().HasQueryFilter(p => p.Active);
           builder.Entity<Product>().HasQueryFilter(p => p.Active);
           builder.Entity<SubProduct>().HasQueryFilter(p => p.Active);
           builder.Entity<SubProductSN>().HasQueryFilter(p => p.Active);
           builder.Entity<InventOp>().HasQueryFilter(p => p.Active);
           builder.Entity<InventOpSubProductSN>().HasQueryFilter(p => p.Active);
           builder.Entity<StoreProduct>().HasQueryFilter(p => p.Active);
    }
}

public static class UtcDateAnnotation
{
    private const String IsUtcAnnotation = "IsUtc";
    private static readonly ValueConverter<DateTime, DateTime> UtcConverter =
      new ValueConverter<DateTime, DateTime>(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

    private static readonly ValueConverter<DateTime?, DateTime?> UtcNullableConverter =
      new ValueConverter<DateTime?, DateTime?>(v => v, v => v == null ? v : DateTime.SpecifyKind(v.Value, DateTimeKind.Utc));

    public static PropertyBuilder<TProperty> IsUtc<TProperty>(this PropertyBuilder<TProperty> builder, Boolean isUtc = true) =>
      builder.HasAnnotation(IsUtcAnnotation, isUtc);

    public static Boolean IsUtc(this IMutableProperty property) =>
      ((Boolean?)property.FindAnnotation(IsUtcAnnotation)?.Value) ?? true;

    /// <summary>
    /// Make sure this is called after configuring all your entities.
    /// </summary>
    public static void ApplyUtcDateTimeConverter(this ModelBuilder builder)
    {
        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            foreach (var property in entityType.GetProperties())
            {
                if (!property.IsUtc())
                {
                    continue;
                }

                if (property.ClrType == typeof(DateTime))
                {
                    property.SetValueConverter(UtcConverter);
                }

                if (property.ClrType == typeof(DateTime?))
                {
                    property.SetValueConverter(UtcNullableConverter);
                }
            }
        }
    }
}
}