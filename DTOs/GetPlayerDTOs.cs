using LD_EC_PiaBackEnd.Entities;
using System.ComponentModel.DataAnnotations;

namespace LD_EC_PiaBackEnd.DTOs
{
    public class GetPlayerDTOs
    {
        public int id { get; set; }
        public string id_players { get; set; }
        public int idRifa { get; set; }
        public int noLoteria { get; set; }
        public bool Winner { get; set; }
    }
}
