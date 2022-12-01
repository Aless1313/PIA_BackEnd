using LD_EC_PiaBackEnd.Entities;
using System.ComponentModel.DataAnnotations;

namespace LD_EC_PiaBackEnd.DTOs
{
    public class PrizeDTO
    {
        [Required]
        public int id_Rifa { get; set; }
        public Rifa rifa { get; set; }
        [Required]
        public string name_prize { get; set; }
        public string description { get; set; }
        public bool available { get; set; }
    }
}
