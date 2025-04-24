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
        modelBuilder.Entity<DayHoc>().HasNoKey();
        modelBuilder.Entity<IdCounter>().HasNoKey();
        modelBuilder.Entity<GiaSu>().HasNoKey();
        modelBuilder.Entity<HocVien>().HasNoKey();
        modelBuilder.Entity<HopDong>().HasNoKey();
        modelBuilder.Entity<KhoaHoc>().HasNoKey();
        modelBuilder.Entity<TGBGiaSu>().HasNoKey();
        modelBuilder.Entity<TGBHocVien>().HasNoKey();
        modelBuilder.Entity<TGBHopDong>().HasNoKey();
        modelBuilder.Entity<Admin>().HasNoKey();
    }
}