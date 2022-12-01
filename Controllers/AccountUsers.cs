using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using LD_EC_PiaBackEnd.Entities;
using LD_EC_PiaBackEnd.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Org.BouncyCastle.Tsp;
using System.Linq;
using System.Collections.Generic;
using System;

namespace LD_EC_PiaBackEnd.Controllers
{
    [ApiController]
    [Route("api/account")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AccountUsers : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IConfiguration configuration;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IMapper mapper;
        private readonly ILogger<AccountUsers> logger;

        public AccountUsers(
            ApplicationDbContext dbContext, IConfiguration configuration, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IMapper mapper, ILogger<AccountUsers> logger)
        {
            this.dbContext = dbContext;
            this.configuration = configuration;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.mapper = mapper;
            this.logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("RegistrarUsuario")]
        public async Task<ActionResult<RespuestaAuthenticacion>> Registrar (CredencialsUsersDTOs credencials)
        {
            var user = new IdentityUser { UserName = credencials.email, Email = credencials.email };
            var result = await userManager.CreateAsync(user, credencials.password);

            if (result.Succeeded)
            {
                var token = await BuildToken(credencials);

                var playerdto = mapper.Map<PlayersCreationDTOs>(user);
                var playermap = mapper.Map<Players>(playerdto);

                dbContext.Add(playermap);
                await dbContext.SaveChangesAsync();

                logger.LogInformation("User creado y guardado con token");
                logger.LogInformation(token.ToString());
                return token;
            }
            else
            {
                logger.LogInformation("Error al crear user");
                return BadRequest(result.Errors);
            }
        }

        [AllowAnonymous]
        [HttpPost("IniciarSesion")]
        public async Task<ActionResult<RespuestaAuthenticacion>> login(CredencialsUsersDTOs credencials)
        {
            var result = await signInManager.PasswordSignInAsync(credencials.email, credencials.password, isPersistent: false, lockoutOnFailure: false);

            if(result.Succeeded)
            {
                logger.LogInformation("Inicio correcto");
                return await BuildToken(credencials);
            }
            else
            {
                logger.LogInformation("Credenciales invalidas");
                return BadRequest("Credenciales invalidas");
            }
        }

        [HttpGet("RenovarToken")]
        public async Task<ActionResult<RespuestaAuthenticacion>> renovar()
        {
            var emailclaim = HttpContext.User.Claims.Where(claim => claim.Type == "email").FirstOrDefault();
            var mail = emailclaim.Value;

            var credentials = new CredencialsUsersDTOs()
            {
                email = mail
            };
            return await BuildToken(credentials);
        }

        [HttpPost("HacerAdmin")]
        public async Task<ActionResult> HacerAdmin(EditarAdminDTOs editarAdmin)
        {
            var user = await userManager.FindByEmailAsync(editarAdmin.email);

            await userManager.AddClaimAsync(user, new Claim("Admin", "True"));

            return NoContent();
        }

        [HttpPost("QuitarAdmin")]
        public async Task<ActionResult>QuitarAdmin(EditarAdminDTOs editarAdmin)
        {
            var user = await userManager.FindByEmailAsync(editarAdmin.email);

            await userManager.RemoveClaimAsync(user, new Claim("Admin", "True"));

            return NoContent();
        }

        private async Task<RespuestaAuthenticacion> BuildToken(CredencialsUsersDTOs credencials)
        {
            var claims = new List<Claim>()
            {
                new Claim("email", credencials.email)
            };

            var user = await userManager.FindByEmailAsync(credencials.email);
            var dbclaims = await userManager.GetClaimsAsync(user);

            claims.AddRange(dbclaims);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["keyjwt"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expirate = DateTime.UtcNow.AddDays(5);

            var securitytoken = new JwtSecurityToken(issuer: null, audience: null, claims: claims, expires: expirate, signingCredentials: creds);

            return new RespuestaAuthenticacion()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(securitytoken),
                Caducidad = expirate
            };
        }

        
        
    }
}
