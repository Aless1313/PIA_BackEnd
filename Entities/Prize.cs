using System.ComponentModel.DataAnnotations;

namespace LD_EC_PiaBackEnd.Entities
{
    public class Prize
    {
        [Key]
        public int id_Prize { get; set; }

        [Required]
        public int id_rifa_prize { get; set; }
        public Rifa rifa { get; set; }

        [Required]
        public string name_prize { get; set;  }

        public bool available_prize { get; set; }

        public string description { get; set; }
    }
}
