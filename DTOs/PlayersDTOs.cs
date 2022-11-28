using System.ComponentModel.DataAnnotations;
using LD_EC_PiaBackEnd.Entities;

namespace LD_EC_PiaBackEnd.DTOs
{
    public class PlayersDTOs
    {
        public int id { get; set; }
        [Required]
        public string id_players { get; set; }
        public Players players { get; set; }
        [Required]
        public int idRifa { get; set; }
        public int noLoteria { get; set; }
    }
}
