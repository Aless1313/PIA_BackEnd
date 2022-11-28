using LD_EC_PiaBackEnd.Entities;
using System.ComponentModel.DataAnnotations;

namespace LD_EC_PiaBackEnd.DTOs
{
    public class Prize
    {
        [Required]
        public int idRifa { get; set; }
        public Rifa rifa { get; set; }
        [Required]
        public string name { get; set; }
        public string description { get; set; }
        public bool available { get; set; }
    }
}
