using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CNPM.Migrations
{
    /// <inheritdoc />
    public partial class @new : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    TenDangNhap = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MatKhau = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "DayHoc",
                columns: table => new
                {
                    IdGiaSu = table.Column<int>(type: "int", nullable: false),
                    IdKhoaHoc = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "GiaSu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    TenDangNhap = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MatKhau = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    HoTen = table.Column<string>(type: "NVARCHAR(50)", nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KhuVuc = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "HocVien",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    TenDangNhap = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MatKhau = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    HoTen = table.Column<string>(type: "NVARCHAR(50)", nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    KhoiLop = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "HopDong",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    IdHocVien = table.Column<int>(type: "int", nullable: false),
                    IdGiaSu = table.Column<int>(type: "int", nullable: false),
                    IdKhoaHoc = table.Column<int>(type: "int", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "IdCounter",
                columns: table => new
                {
                    TableName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CurrentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "KhoaHoc",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    TenKhoaHoc = table.Column<string>(type: "NVARCHAR(50)", nullable: false),
                    TenMonHoc = table.Column<string>(type: "NVARCHAR(50)", nullable: false),
                    KhoiLop = table.Column<int>(type: "int", nullable: false),
                    SoBuoi = table.Column<int>(type: "int", nullable: false),
                    HocPhi = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "TGBGiaSu",
                columns: table => new
                {
                    IdGiaSu = table.Column<int>(type: "int", nullable: false),
                    TDiemBatDau = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TDiemKetThuc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "TGBHocVien",
                columns: table => new
                {
                    IdHocVien = table.Column<int>(type: "int", nullable: false),
                    TDiemBatDau = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TDiemKetThuc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "TGBHopDong",
                columns: table => new
                {
                    IdHopDong = table.Column<int>(type: "int", nullable: false),
                    TDiemBatDau = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TDiemKetThuc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admin");

            migrationBuilder.DropTable(
                name: "DayHoc");

            migrationBuilder.DropTable(
                name: "GiaSu");

            migrationBuilder.DropTable(
                name: "HocVien");

            migrationBuilder.DropTable(
                name: "HopDong");

            migrationBuilder.DropTable(
                name: "IdCounter");

            migrationBuilder.DropTable(
                name: "KhoaHoc");

            migrationBuilder.DropTable(
                name: "TGBGiaSu");

            migrationBuilder.DropTable(
                name: "TGBHocVien");

            migrationBuilder.DropTable(
                name: "TGBHopDong");
        }
    }
}
