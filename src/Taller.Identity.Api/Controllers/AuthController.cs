using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Taller.Core.Controllers;
using Taller.Core.Identity;

namespace Taller.Identity.Api.Controllers
{
    [Route("api/identity")]
    public class AuthController : MainController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppSettings _appSettings;
        public AuthController(SignInManager<IdentityUser> signInManager,
                             UserManager<IdentityUser> userManager,
                             IOptions<AppSettings> appSettings)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _appSettings = appSettings.Value;
        }
        [HttpPost("new-account")]
        public async Task<ActionResult> Registrar(UsuarioRegistro usuarioRegistro)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            var user = new IdentityUser
            {
                UserName = usuarioRegistro.Email,
                Email = usuarioRegistro.Email,
                EmailConfirmed = true
            };
            var result = await _userManager.CreateAsync(user, usuarioRegistro.Senha);
            if (result.Succeeded)
            {
                return CustomResponse(await GerarJwt(usuarioRegistro.Email));
            }
            result.Errors.ForEach(x => AdicionarErroProcessamento(x.Description));
            return CustomResponse();
        }

    }
}
