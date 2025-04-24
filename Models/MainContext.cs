using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Cnpm.Models.Object;

public class MainContext : DbContext
{
    public required DbSet<DayHoc> DayHoc { get; set; } = null!;
    public required DbSet<IdCounter> IdCounter { get; set; } = null!;
    public required DbSet<GiaSu> GiaSu { get; set; } = null!;
    public required DbSet<HocVien> HocVien { get; set; } = null!;
    public required DbSet<HopDong> HopDong { get; set; } = null!;
    public required DbSet<KhoaHoc> KhoaHoc { get; set; } = null!;
    public required DbSet<TGBGiaSu> TGBGiaSu { get; set; } = null!;
    public required DbSet<TGBHocVien> TGBHocVien { get; set; } = null!;
    public required DbSet<TGBHopDong> TGBHopDong { get; set; } = null!;
    public required DbSet<Admin> Admin { get; set; } = null!;


    public MainContext() { }

    public MainContext(DbContextOptions<MainContext> options)
        : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string conn_string = new SqlConnectionStringBuilder
        {
            DataSource = "PC",// Thay bằng tên server trên máy của bạn, ví dụ @"PC\SQLEXPRESS"
            InitialCatalog = "DbCnpm", // Tên của database
            IntegratedSecurity = true,
            TrustServerCertificate = true,
            ConnectTimeout = 60,
        }.ConnectionString;
        optionsBuilder.UseSqlServer(conn_string);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure entities with primary keys but tell EF not to generate values
        
        // DayHoc doesn't need a key as it's likely a join table
        modelBuilder.Entity<DayHoc>().HasNoKey();
        
        // Configure IdCounter with TableName as primary key but don't let EF generate values
        modelBuilder.Entity<IdCounter>()
            .HasKey(e => e.TableName);
        
        // Configure entities with primary keys but specify that values are not database generated
        modelBuilder.Entity<GiaSu>()
            .HasKey(e => e.Id)
            .HasAnnotation("DatabaseGenerated", "None"); // Don't auto-generate keys
            
        modelBuilder.Entity<HocVien>()
            .HasKey(e => e.Id)
            .HasAnnotation("DatabaseGenerated", "None"); // Don't auto-generate keys
            
        modelBuilder.Entity<HopDong>()
            .HasKey(e => e.Id)
            .HasAnnotation("DatabaseGenerated", "None"); // Don't auto-generate keys
            
        modelBuilder.Entity<KhoaHoc>()
            .HasKey(e => e.Id)
            .HasAnnotation("DatabaseGenerated", "None"); // Don't auto-generate keys
            
        modelBuilder.Entity<Admin>()
            .HasKey(e => e.Id)
            .HasAnnotation("DatabaseGenerated", "None"); // Don't auto-generate keys
            
        // Time-based entities likely don't need keys or are join tables
        modelBuilder.Entity<TGBGiaSu>().HasNoKey();
        modelBuilder.Entity<TGBHocVien>().HasNoKey();
        modelBuilder.Entity<TGBHopDong>().HasNoKey();
    }
}