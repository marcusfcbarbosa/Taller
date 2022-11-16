using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Taller.Core.Identity;
using Taller.Identity.Api.Models;
using Taller.Web.Models;
using Taller.Web.Services;

namespace Taller.Web.Controllers
{
    public class HomeController : MainController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAuthService _authenticationService;
        private IAspNetUser _aspNetUser;
        private readonly ICarService _carService;
        public HomeController(IAuthService authenticationService,
                              IAspNetUser aspNetUser,
                              ICarService carService)
        {
            _authenticationService = authenticationService;
            _aspNetUser = aspNetUser;
            _carService = carService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(UserLogin userLogin, string returnUrl = null)
        {
            if (!ModelState.IsValid) return View(userLogin);
            var result = await _authenticationService.Login(userLogin);
            if (!ResponseHasErrors(result.ResponseResult))
            {
                await LogIn(result);
                if (string.IsNullOrEmpty(returnUrl))
                    return RedirectToAction(actionName: "taller", controllerName: "Home");
                return LocalRedirect(returnUrl);
            }
            return View(userLogin);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(UserRegistration userRegister)
        {
            if (!ModelState.IsValid) return View(userRegister);
            var result = await _authenticationService.Register(userRegister);
            if (ResponseHasErrors(result.ResponseResult)) return View(userRegister);
            await LogIn(result);
            return RedirectToAction(actionName: "taller", controllerName: "Home");
        }

        [HttpGet]
        [Route("register")]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpGet]
        [Route("taller")]
        [Authorize]
        public async Task<IActionResult> Taller([FromQuery] int ps = 8, [FromQuery] int page = 1, [FromQuery] string q = null)
        {
            return View(new ListCarModel
            {
                aspNetUser = _aspNetUser,
                cars = await _carService.GetAllCarsPaged(ps, page, q)
            });
        }

        [HttpGet]
        [Route("delete/{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
             await _carService.Delete(id);
            return RedirectToAction(actionName: "taller", controllerName: "Home");

        }




        [HttpGet]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }


        #region "Private Methods"
        private async Task LogIn(UserResponseLogin userAnswersLogin)
        {
            var token = GetFormattedToken(userAnswersLogin.AccessToken);
            var claims = new List<Claim>();
            claims.Add(new Claim("JWT", userAnswersLogin.AccessToken));
            claims.AddRange(token.Claims);
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60),
                IsPersistent = true
            };
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }
        private static JwtSecurityToken GetFormattedToken(string jwtToken)
        {
            return new JwtSecurityTokenHandler().ReadToken(jwtToken) as JwtSecurityToken;
        }
        #endregion
    }
}
