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
        //private readonly UserManager<ApplicationUser> _userManager;
        //private readonly SignInManager<ApplicationUser> _signInManager;
        //private readonly IEmailSender _emailSender;
       // private readonly ILogger _logger;

        private readonly IAuthentication _authentication;

        private readonly IAuthenticationQueryRepository _dbAuthenticationQuery;

        public AccountController(
            //ILogger logger, 
            IAuthentication authentication, IAuthenticationQueryRepository dbAuthenticationQuery)
        {
            //_logger = logger;
            _authentication = authentication;
            _dbAuthenticationQuery = dbAuthenticationQuery;
        }

        //public AccountController(
        //    UserManager<ApplicationUser> userManager,
        //    SignInManager<ApplicationUser> signInManager,
        //    IEmailSender emailSender,
        //    ILogger<AccountController> logger)
        //{
        //    _userManager = userManager;
        //    _signInManager = signInManager;
        //    _emailSender = emailSender;
        //    _logger = logger;
        //}

        [TempData]
        public string ErrorMessage { get; set; }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync();

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
                    var dbUser = await _dbAuthenticationQuery.GetDetailsRequiredForClaimsAsync(model.Email);
                    // Add claims
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.GivenName, dbUser.Firstname),
                        new Claim(ClaimTypes.Surname, dbUser.Lastname),
                        new Claim(ClaimTypes.Email, dbUser.Email),
                        new Claim(ClaimTypes.Name, $"{dbUser.Firstname} {dbUser.Lastname}"),
                        new Claim(ClaimTypes.NameIdentifier, dbUser.Email)
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

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Lockout()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                //var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                //var result = await _userManager.CreateAsync(user, model.Password);
                //if (result.Succeeded)
                //{
                //    _logger.LogInformation("User created a new account with password.");

                //    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                //    var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
                //    await _emailSender.SendEmailConfirmationAsync(model.Email, callbackUrl);

                //    await _signInManager.SignInAsync(user, isPersistent: false);
                //    _logger.LogInformation("User created a new account with password.");
                //    return RedirectToLocal(returnUrl);
                //}
                //AddErrors(result);
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
