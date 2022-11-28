using LD_EC_PiaBackEnd.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace LD_EC_PiaBackEnd.DTOs
{
    public class RifaCreationDTOs
    {
        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(maximumLength: 100, ErrorMessage = "The {0} field can only be up to 100 characters")]
        [PrimeraLetraMayuscula]
        public string name { get; set; }
    }
}
