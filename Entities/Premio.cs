using System.ComponentModel.DataAnnotations;

namespace LD_EC_PiaBackEnd.Entities
{
    public class Premio
    {
        public int id_Premio { get; set; }

        [Required]
        public int id_rifa_premio { get; set; }
        public Rifa rifa { get; set; }

        [Required]
        public string nombre_premio { get; set;  }

        public bool available_premio { get; set; }
    }
}
