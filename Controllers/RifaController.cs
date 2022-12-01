using AutoMapper;
using LD_EC_PiaBackEnd.DTOs;
using LD_EC_PiaBackEnd.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LD_EC_PiaBackEnd.Controllers
{
    [Route("api/Rifa")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RifaController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;
        public RifaController(ApplicationDbContext dbContext, UserManager<IdentityUser> userManager, IMapper mapper, IConfiguration configuration)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
            this.mapper = mapper;
            this.configuration = configuration;
        }

        [AllowAnonymous]
        [HttpGet("MostrarRifa")]
        public async Task<ActionResult<List<GetRifaDTO>>> GetLista()
        {
            var RifaInfo = await dbContext.Rifas
                .Include(rifa => rifa.ListPrize)
                .Include(rifa => rifa.Games)
                .ToListAsync();
            if (RifaInfo.Count == 0)
            {
                return NotFound();
            }
            return mapper.Map<List<GetRifaDTO>>(RifaInfo);
        }
        [AllowAnonymous]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<GetRifaDTO>> GetbyId(int id)
        {
            var RifaInfo = await dbContext.Rifas.FirstOrDefaultAsync(x => x.id_Rifa == id);

            if (RifaInfo == null)
            {
                return NotFound();
            }
            return mapper.Map<GetRifaDTO>(RifaInfo);
        }
        [AllowAnonymous]
        [HttpGet("SearchName")]
        public async Task<ActionResult<List<GetRifaDTO>>> GetByNombre(string nombre)
        {
            var RifaInfo = await dbContext.Rifas.Where(x => x.nombre_Rifa.Contains(nombre)).ToListAsync();

            if (RifaInfo.Count == 0)
            {
                return NotFound();
            }
            return mapper.Map<List<GetRifaDTO>>(RifaInfo);
        }

        [HttpPost("CrearRifa")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> Post(RifaCreationDTOs rifaCreationDTOs)
        {
            var emailClaim = HttpContext.User.Claims.Where(claim => claim.Type == "email").FirstOrDefault();
            var email = emailClaim.Value;
            var user = await userManager.FindByNameAsync(email);
            var rifaDTO = mapper.Map<RifaDTOs>(rifaCreationDTOs);
            var rifa = mapper.Map<Rifa>(rifaDTO);

            rifa.available_rifa = false;

            dbContext.Add(rifa);
            await dbContext.SaveChangesAsync();
            return Ok("Rifa creada");
        }

        [HttpPatch("{idRifa:int}", Name = "Vigencia")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> Vigencia(int idRifa, JsonPatchDocument<RifaPatchDTO> rifaPatch)
        {
            if (rifaPatch == null) return BadRequest();
            var rifaInfo = await dbContext.Rifas.FirstOrDefaultAsync(x => x.id_Rifa == idRifa);
            if (rifaInfo == null) return NotFound();

            var rifaDTO = mapper.Map<RifaPatchDTO>(rifaInfo);
            
            rifaPatch.ApplyTo(rifaDTO, ModelState); 

            var isOk = TryValidateModel(rifaDTO);
            if (!isOk) return BadRequest(ModelState);

            mapper.Map(rifaDTO, rifaInfo);
            await dbContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost("Prize")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> PostPrize(PrizeCreationDTOs prizeDTOs)
        {
            var idRifa = prizeDTOs.idRifa;

            if (prizeDTOs == null) return BadRequest();
            var rifaInfo = await dbContext.Rifas
                .Include(rifa => rifa.ListPrize)
                .FirstOrDefaultAsync(x => x.id_Rifa == idRifa);
            if (rifaInfo == null) return NotFound("No existe esa rifa");

            var prizeDTO = mapper.Map<PrizeDTO>(prizeDTOs);

            var isOk = TryValidateModel(prizeDTO);
            if (!isOk) return BadRequest();

            var prize = mapper.Map<Prize>(prizeDTO);
            prize.available_prize = true;
            prize.rifa = rifaInfo;

            var aux = true;
            if (rifaInfo.ListPrize.Count == 0)
            {
                rifaInfo.available_rifa = true;
            }
            else
            {
                aux = false;
                for (int i = 0; i < rifaInfo.ListPrize.Count; i++)
                {
                    if (rifaInfo.ListPrize[i].available_prize == true)
                    {
                        aux = true;
                        break;
                    }
                }
            }
            if (!aux)
            {
                return BadRequest("No se solicitan más premios");
            }

            dbContext.Add(prize);
            await dbContext.SaveChangesAsync();
            return Ok("Su premio ha sido agregado, su numero de rifa es " + idRifa);
        }

        [HttpDelete("{idRifa:int}", Name = "EliminarRifa")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> Delete(int idRifa)
        {
            var exist = await dbContext.Rifas.AnyAsync(x => x.id_Rifa == idRifa);
            if (!exist) return NotFound("Sin coincidencias");
            dbContext.Remove(new Rifa
            {
                id_Rifa = idRifa
            });
            await dbContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("EditarRifa")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<ActionResult> Put(EditarRifaDTOs editarRifaDTOs)
        {
            var rifaInfo = await dbContext.Rifas
                .Include(rifa => rifa.Games).Include(rifa => rifa.ListPrize)
                .FirstOrDefaultAsync(rifa => rifa.id_Rifa == editarRifaDTOs.id);
            if (rifaInfo == null)
            {
                return NotFound("No existe una rifa con ese id");
            }
            var UpdateRifa = mapper.Map(editarRifaDTOs, rifaInfo);

            dbContext.Update(UpdateRifa);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [AllowAnonymous]
        [HttpGet("Cartas")]
        public async Task<ActionResult<List<Card>>> GetDisponibles(int idRifa)
        {
            var elegidas = await dbContext.Games.Where(x => x.id_Rifa == idRifa).ToListAsync();
            if (elegidas.Count == 0)
            {
                return NotFound("No hay jugadores");
            }

            List<int> NoDisponibles = new List<int>();
            foreach (var registro in elegidas)
            {
                NoDisponibles.Add(registro.Numero_Loteria);
            }

            CartasInfo Listado = new CartasInfo();
            List<Card> Cartas = Listado.GetCartas();

            List<int> CardsInt = new List<int>();
            foreach (var card in Cartas)
            {
                CardsInt.Add(card.id);
            }

            List<Card> Disponibles = new List<Card>();
            List<int> DisponiblesInt = new List<int>();

            foreach (var numeroLoteria in CardsInt)
            {
                if (!NoDisponibles.Contains(numeroLoteria))
                {
                    DisponiblesInt.Add(numeroLoteria);
                }
            }

            foreach (var id in DisponiblesInt)
            {
                foreach (var card in Cartas)
                {
                    if (card.id == id)
                        Disponibles.Add(card);
                }
            }
            return Disponibles;
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpGet("Ganador")]
        public async Task<ActionResult<WinnerCard>> Winner(int idRifa)
        {
            var rifaInfo = await dbContext.Rifas.Where(x => x.id_Rifa == idRifa).FirstOrDefaultAsync();
            if (rifaInfo == null) return BadRequest();
            var games = await dbContext.Games.Where(x => x.id_Rifa == idRifa).ToListAsync();
            if (games.Count() == 0) return BadRequest();
            var prizeInfo = await dbContext.Prizes.Where(x => x.id_rifa_prize == idRifa && x.available_prize == true).ToListAsync();

            var random = new Random();
            var ganador = games.OrderBy(x => random.Next()).Take(1).FirstOrDefault();
            var Ganador = prizeInfo.Last();
            Ganador.available_prize = false;

            dbContext.Prizes.Update(Ganador);

            await dbContext.SaveChangesAsync();

            var player = await dbContext.Players.Where(x => x.idUser == ganador.id_Player).FirstOrDefaultAsync();

            CartaInfo Lista = new CartaInfo();
            List<Card> Cartas = Lista.GetCartas();

            Card winnerCard = Cartas[Ganador.id_rifa_prize];
            GetPrizeDTOs prize = new GetPrizeDTOs();
            prize.name = Ganador.name_prize;
            prize.available = true;
            prize.description = Ganador.description;

            WinnerCard winner = new WinnerCard(player.email_players, idRifa, rifaInfo.nombre_Rifa, winnerCard, prize);

            return winner;
        }
    }
}
