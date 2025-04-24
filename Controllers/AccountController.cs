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
}