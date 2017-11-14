using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Database.Interfaces.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SimpleWorkTimeTracker.Extensions;
using SimpleWorkTimeTracker.Models;
using SimpleWorkTimeTracker.Models.AccountViewModels;
using SimpleWorkTimeTracker.Services;

namespace SimpleWorkTimeTracker.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly IAuthentication _authentication;

        private readonly IPersonQueryRepository _dbPersonQuery;

        public AccountController(IAuthentication authentication, IPersonQueryRepository dbPersonQuery)
        {
            _authentication = authentication;
            _dbPersonQuery = dbPersonQuery;
        }
        
        [TempData]
        public string ErrorMessage { get; set; }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var authenticationResult = await _authentication.AuthenticateAsync(model.Email, model.Password);
                if (authenticationResult.Success)
                {
                    // Get the details required for the claims
                    var dbUser = await _dbPersonQuery.GetDetailsRequiredForClaimsAsync(model.Email);
                    // Add claims
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.GivenName, dbUser.Firstname),
                        new Claim(ClaimTypes.Surname, dbUser.Lastname),
                        new Claim(ClaimTypes.Email, dbUser.Email),
                        new Claim(ClaimTypes.Name, $"{dbUser.Firstname} {dbUser.Lastname}"),
                        new Claim(ClaimTypes.NameIdentifier, dbUser.Email),
                        new Claim(ClaimTypes.PrimarySid, dbUser.Id.ToString())
                    };
                    var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));
                    await HttpContext.SignInAsync(claimsPrincipal);

                    //_logger.LogInformation($"{model.Email} logged in.");
                    return RedirectToLocal(returnUrl);
                }

                ModelState.AddModelError(string.Empty, authenticationResult.Message);
                return View(model);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            //await _signInManager.SignOutAsync();
            //_logger.LogInformation("User logged out.");
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }



        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        #endregion
    }
}
