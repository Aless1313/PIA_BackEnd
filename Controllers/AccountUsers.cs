using AutoMapper;
using LD_EC_PiaBackEnd.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using LD_EC_PiaBackEnd.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

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

        
    }
}
