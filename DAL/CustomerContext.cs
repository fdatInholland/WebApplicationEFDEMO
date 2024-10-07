using Bogus;
using DAL.Faker;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DAL
{
    public class CustomerContext : DbContext
    {
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Invoice> Invoice { get; set; }
        public DbSet<InvoiceLine> InvoicesLine { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<EmployeeRole> EmployeeRole { get; set; }

        public CustomerContext()
        {

        }

        public CustomerContext(DbContextOptions<CustomerContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);

                optionsBuilder
           .UseSqlServer(connectionString)
           .LogTo(Console.WriteLine);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //  modelBuilder.ApplyConfiguration(new CustomerConfiguration());

            modelBuilder.Entity<Customer>()
            .HasKey(c => c.CustomerID);

            modelBuilder.Entity<Invoice>()
                .HasKey(i => i.InvoiceID);

            modelBuilder.Entity<InvoiceLine>()
                .HasKey(il => il.InvoiceLineID);

            modelBuilder.Entity<Invoice>()
                .HasMany(i => i.InvoiceLines)
                .WithOne(il => il.Invoice)
                .HasForeignKey(il => il.InvoiceID);

            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Invoices)
                .WithOne(i => i.customer)
                .HasForeignKey(i => i.CustomerID);

            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            int custid = 1;
            int invoiceid = 1;
            int invoiceline = 1;

            var InvoiceLineFaker = new Faker<InvoiceLine>()
                                .RuleFor(c => c.InvoiceLineID, _ => invoiceline++)
                                .RuleFor(c => c.Productname, f => f.PickRandom<FakeProduct>().ToString());

            var invoiceFaker = new Faker<Invoice>()
                            .RuleFor(c => c.InvoiceID, _ => invoiceid++)
                            .RuleFor(i => i.InvoiceDate, f => f.Date.Past(1))
                            .RuleFor(i => i.InvoiceID, _ => invoiceid++);

            var customerFaker = new Faker<Customer>()
                       .RuleFor(c => c.CustomerID, _ => custid++)
                       .RuleFor(c => c.FirstName, f => f.Name.FirstName())
                       .RuleFor(c => c.LastName, f => f.Name.LastName())
                       .RuleFor(c => c.Age, f => f.Random.Int(18, 95));

            var customers = customerFaker.Generate(100);
            modelBuilder.Entity<Customer>().HasData(customers);

            var invoices = new List<Invoice>();

            Random rnd = new Random();

            foreach (Customer customer in customers)
            {
                for (int i = 0; i < rnd.Next(1, 25); i++)
                {
                    var invoice = invoiceFaker.Generate();
                    invoice.CustomerID = customer.CustomerID;
                    invoices.Add(invoice);
                }
            }

            modelBuilder.Entity<Invoice>().HasData(invoices);

            var invoiceLines = new List<InvoiceLine>();

            foreach (Invoice invoice in invoices)
            {
                for (int i = 0; i < 3; i++)
                {
                    var line = InvoiceLineFaker.Generate();
                    line.InvoiceID = invoice.InvoiceID;
                    invoiceLines.Add(line);
                }
            }

            modelBuilder.Entity<InvoiceLine>().HasData(invoiceLines);

            base.OnModelCreating(modelBuilder);
        }
    }
}

