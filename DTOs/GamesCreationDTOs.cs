using System.ComponentModel.DataAnnotations;
using LD_EC_PiaBackEnd.Entities;

namespace LD_EC_PiaBackEnd.DTOs
{
    public class GamesCreationDTOs
    {
        [Required]
        public int idRifa { get; set; }
        [Required]
        public int Numero_Loteria { get; set; }
    }
}
