using AutoMapper;
using LD_EC_PiaBackEnd.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using LD_EC_PiaBackEnd.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace LD_EC_PiaBackEnd.Controllers
{
    [ApiController]
    [Route("api/Games")]
    [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
    public class GamesController: ControllerBase

    {
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;
        public GamesController(ApplicationDbContext dbContext, UserManager<IdentityUser> userManager, IMapper mapper, IConfiguration configuration)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
            this.mapper = mapper;
            this.configuration = configuration;
        }
        [AllowAnonymous]
        [Route("api/ListGames")]
        [HttpGet]
        public async Task<ActionResult<List<GetGamesDTOs>>> GetAll()
        {
            var lista = await dbContext.Games
                .Include(Games=> Games.player).ToListAsync();
            return mapper.Map<List<GetGamesDTOs>>(lista);
        }


        [HttpPost("RegistroNumero")]
        public async Task<ActionResult> Post(GamesCreationDTOs gamesCreationDTOs)
        {
            var emailClaim = HttpContext.User.Claims.Where(claim => claim.Type == "email").FirstOrDefault();
            if (emailClaim == null)
            {
                return Unauthorized("Porfavor inicie sesión para poder participar");
            }
            var email = emailClaim.Value;
            var user = await userManager.FindByNameAsync(email);

            if (gamesCreationDTOs.Numero_Loteria < 0 || gamesCreationDTOs.Numero_Loteria > 54)
                return BadRequest("Solo se aceptan numeros del 1 al 54");

            var gameDTO = mapper.Map<GamesDTOs>(gamesCreationDTOs);
            gameDTO.id_players = user.Id; 
            gameDTO.player = await dbContext.Players.FirstOrDefaultAsync(
                userPlayer => userPlayer.idUser == user.Id); 

            var Game = mapper.Map<Games>(gameDTO);

           
            var rifaDB = await dbContext.Rifas.FirstOrDefaultAsync(x => x.id == gamesCreationDTOs.idRifa); 
            if (rifaDB == null)
            {
                return BadRequest("La rifa no existe");
            }
            else
            {
                Game.rifa = rifaDB;
                var existeTarjeta = await dbContext.Games.AnyAsync(
                        GameRifa => GameRifa.Numero_Loteria == gamesCreationDTOs.Numero_Loteria
                        && GameRifa.id_Rifa == gamesCreationDTOs.idRifa
                    );
                if (!existeTarjeta)
                {

                    dbContext.Add(Game);
                    await dbContext.SaveChangesAsync();
                }
                else
                {
                    return BadRequest("El numero de loteria elegido ya esta en uso");
                }
            }

            return Ok("Numero de loteria registrado con exito");
        }

        [HttpDelete("{id:int}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> Delete(int id)
        {
            var exist = await dbContext.Games.AnyAsync(x => x.id_Game == id);
            if (!exist) return NotFound("Numero de participación invalido");
            dbContext.Remove(new Games
            {
                id_Game = id
            });
            await dbContext.SaveChangesAsync();
            return NoContent();
        }

    }

}
