using Microsoft.AspNetCore.Mvc;
using LusiTrack.Models;

namespace LusiTrack.Controllers;

public class AccountController : Controller
{
    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        ViewBag.ReturnUrl = returnUrl;
        return View(new LoginViewModel { ReturnUrl = returnUrl });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        // Validate credentials (accept demo or valid 6+ char password for testing)
        if (model.Password.Length >= 6)
        {
            var displayName = model.Email.Contains('@') ? model.Email.Split('@')[0] : "Member";
            // Capitalize first letter
            displayName = char.ToUpper(displayName[0]) + displayName.Substring(1);

            TempData["SuccessMessage"] = $"Welcome back to the Roaster's Guild, {displayName}!";

            if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
            {
                return Redirect(model.ReturnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        ModelState.AddModelError(string.Empty, "Password must be at least 6 characters.");
        return View(model);
    }

    [HttpGet]
    public IActionResult Logout()
    {
        TempData["SuccessMessage"] = "You have been safely signed out. See you at the brew bar!";
        return RedirectToAction("Index", "Home");
    }
}
