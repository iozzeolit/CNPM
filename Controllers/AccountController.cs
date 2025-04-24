using System.Security.Claims;
using Cnpm.Models;
using Cnpm.Models.Object;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cnpm.Controllers;

public class AccountController : Controller
{
    private readonly MainContext _context;

    public AccountController(MainContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;

        if (ModelState.IsValid)
        {
            bool isAuthenticated = false;
            ClaimsIdentity identity = null;

            // Check for tutor credentials
            GiaSu? giaSu = await _context.GiaSu
                .FirstOrDefaultAsync(x => x.TenDangNhap == model.TenDangNhap);

            if (giaSu != null && PasswordHasher.VerifyPassword(model.MatKhau, giaSu.MatKhau))
            {
                // User is a tutor
                identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, giaSu.HoTen),
                    new Claim(ClaimTypes.NameIdentifier, giaSu.Id.ToString()),
                    new Claim(ClaimTypes.Role, "GiaSu")
                }, CookieAuthenticationDefaults.AuthenticationScheme);
                
                isAuthenticated = true;
            }

            // Check for student credentials if not already authenticated
            if (!isAuthenticated)
            {
                HocVien? hocVien = await _context.HocVien
                    .FirstOrDefaultAsync(x => x.TenDangNhap == model.TenDangNhap);

                if (hocVien != null && PasswordHasher.VerifyPassword(model.MatKhau, hocVien.MatKhau))
                {
                    // User is a student
                    identity = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Name, hocVien.HoTen),
                        new Claim(ClaimTypes.NameIdentifier, hocVien.Id.ToString()),
                        new Claim(ClaimTypes.Role, "HocVien")
                    }, CookieAuthenticationDefaults.AuthenticationScheme);
                    
                    isAuthenticated = true;
                }
            }

            // Check for admin credentials if not already authenticated
            if (!isAuthenticated)
            {
                Admin? admin = await _context.Admin
                    .FirstOrDefaultAsync(x => x.TenDangNhap == model.TenDangNhap);

                if (admin != null && PasswordHasher.VerifyPassword(model.MatKhau, admin.MatKhau))
                {
                    // User is an admin
                    identity = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, admin.Id.ToString()),
                        new Claim(ClaimTypes.Role, "Admin"),
                        new Claim(ClaimTypes.Name, "Quản trị viên")
                    }, CookieAuthenticationDefaults.AuthenticationScheme);
                    
                    isAuthenticated = true;
                }
            }

            if (isAuthenticated)
            {
                var principal = new ClaimsPrincipal(identity);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = model.RememberMe,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddDays(7)
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    principal,
                    authProperties);

                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError(string.Empty, "Tên đăng nhập hoặc mật khẩu không hợp lệ.");
        }

        return View(model);
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }
    
    [HttpGet]
    public IActionResult StudentRegister()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> StudentRegister(StudentRegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            // Check if username already exists (for any user type)
            var existingStudent = await _context.HocVien
                .FirstOrDefaultAsync(x => x.TenDangNhap == model.TenDangNhap);

            var existingTutor = await _context.GiaSu
                .FirstOrDefaultAsync(x => x.TenDangNhap == model.TenDangNhap);

            var existingAdmin = await _context.Admin
                .FirstOrDefaultAsync(x => x.TenDangNhap == model.TenDangNhap);

            if (existingStudent != null || existingTutor != null || existingAdmin != null)
            {
                ModelState.AddModelError("TenDangNhap", "Tên đăng nhập đã tồn tại trong hệ thống");
                return View(model);
            }

            // Get and update next available ID for student using raw SQL to avoid tracking issues
            int nextStudentId = await GetNextIdFromCounter("HocVien");
                
            // Create the new student account with the ID from IdCounter
            var newStudent = new HocVien
            {
                Id = nextStudentId,
                TenDangNhap = model.TenDangNhap,
                MatKhau = PasswordHasher.HashPassword(model.MatKhau),
                HoTen = model.HoTen,
                NgaySinh = model.NgaySinh,
                DiaChi = model.DiaChi,
                KhoiLop = model.KhoiLop
            };

            _context.Add(newStudent);
            await _context.SaveChangesAsync();

            // Automatically log in the new student
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, newStudent.HoTen),
                new Claim(ClaimTypes.NameIdentifier, newStudent.Id.ToString()),
                new Claim(ClaimTypes.Role, "HocVien")
            }, CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(7)
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                authProperties);

            return RedirectToAction("Index", "Home");
        }

        return View(model);
    }

    // Helper method to get next ID without entity tracking issues
    private async Task<int> GetNextIdFromCounter(string tableName)
    {
        // Try to get current ID
        var counter = await _context.IdCounter
            .FirstOrDefaultAsync(x => x.TableName == tableName);
            
        int nextId = 1; // Default starting ID
        
        if (counter != null)
        {
            // Use the current ID
            nextId = counter.CurrentId;
            
            // Update the ID for next use
            counter.CurrentId = nextId + 1;
            _context.SaveChanges();
        }
        else
        {
            // Create a new IdCounter
            counter = new IdCounter
            {
                TableName = tableName,
                CurrentId = 2  // Next ID will be 2
            };
            
            _context.IdCounter.Add(counter);
            _context.SaveChanges();
        }
        
        return nextId;
    }
}