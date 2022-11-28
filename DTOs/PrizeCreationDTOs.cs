using System.ComponentModel.DataAnnotations;

namespace LD_EC_PiaBackEnd.DTOs
{
    public class PrizeCreationDTOs
    {
        [Required]
        public int idRifa { get; set; }
        [Required]
        [StringLength(99)]
        public string name { get; set; }
        [Required]
        [StringLength(99)]
        public string description { get; set; }
    }
}
