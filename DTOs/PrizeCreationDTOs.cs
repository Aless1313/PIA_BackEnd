using System.ComponentModel.DataAnnotations;

namespace LD_EC_PiaBackEnd.DTOs
{
    public class PrizeCreationDTOs
    {
        [Required]
        public int id_Rifa { get; set; }
        [Required]
        [StringLength(99)]
        public string name_prize { get; set; }
        [Required]
        [StringLength(99)]
        public string description { get; set; }
    }
}
