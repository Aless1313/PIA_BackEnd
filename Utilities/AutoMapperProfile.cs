using AutoMapper;
using Microsoft.AspNetCore.Identity;
using LD_EC_PiaBackEnd.DTOs;
using LD_EC_PiaBackEnd.Entities;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace LD_EC_PiaBackEnd.Utilities
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<IdentityUser, PlayersCreationDTOs>()
                .ForMember(dto => dto.email_players,
                    opciones => opciones.MapFrom(identityUser => identityUser.Email))
                .ForMember(dto => dto.id_players,
                    opciones => opciones.MapFrom(identityUser => identityUser.Id))
                .ForMember(dto => dto.user,
                    opciones => opciones.MapFrom(identityUser => identityUser));
            CreateMap<PlayersCreationDTOs, Players>();

            CreateMap<RifaCreationDTOs, RifaDTOs>();
            CreateMap<RifaDTOs, Rifa>();
            CreateMap<Rifa, GetRifaDTO>()
                .ForMember(getrifaDTO => getrifaDTO.prizes, opciones => opciones.MapFrom(RTGRDTO))
                .ForMember(getrifaDTO => getrifaDTO.games, opciones => opciones.MapFrom(RTGRDTOP));

            CreateMap<Rifa, RifaPatchDTO>().ReverseMap();
            CreateMap<EditarRifaDTOs, Rifa>();
            //PREMIOS
            CreateMap<PrizeCreationDTOs, PrizeDTO>();
            CreateMap<PrizeDTO, Prize>();
            //PARTICIPACIONES
            CreateMap<GamesCreationDTOs, GamesDTOs>();
            CreateMap<GamesDTOs, Games>().ReverseMap();
            CreateMap<Games, GetGamesDTOs>()
                .ForMember(GetPlayDTO => GetPlayDTO.id, opciones => opciones.MapFrom(Part => Part.id_Game))
                .ForMember(GetPlayDTO => GetPlayDTO.id_players, opciones => opciones.MapFrom(Part => Part.id_Player));
        }

        private List<GetPrizeDTOs> RTGRDTO(Rifa rifa, GetRifaDTO getRifaDTO)
        {
            var ListGet = new List<GetPrizeDTOs>();
            if (rifa.ListPrize == null) return ListGet;

            foreach (Prize prize in rifa.ListPrize)
            {
                var prizeDTO = getpremiodto(prize);
                ListGet.Add(prizeDTO);
            }
            return ListGet;
        }

        private GetPrizeDTOs getpremiodto(Prize prize)
        {
            var getpremio = new GetPrizeDTOs();
            getpremio.description = prize.description;
            getpremio.name = prize.name_prize;
            getpremio.available= prize.available_prize;

            return getpremio;
        }

        private List<GetGamesDTOs> RTGRDTOP(Rifa rifa, GetRifaDTO getRifaDTO)
        {
            var ListGames = new List<GetGamesDTOs>();
            if(rifa.Games== null) return ListGames;

            foreach(Games games in rifa.Games)
            {
                var Getgames = gamestogetplayers(games);
                ListGames.Add(Getgames);
            }
            return ListGames;
        }

        private GetGamesDTOs gamestogetplayers( Games games)
        {
            var getgames = new GetGamesDTOs();

            getgames.id = games.id_Game;
            getgames.id_players = games.id_Player;
            getgames.idRifa = games.id_Rifa;
            getgames.noLoteria = games.Numero_Loteria;
            getgames.Winner = games.Winner;

            return getgames;
        }
    }
}
