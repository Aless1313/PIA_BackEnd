using System.ComponentModel.DataAnnotations;

namespace LD_EC_PiaBackEnd.DTOs
{
    public class EditarRifaDTOs
    {
        public int id_Rifa { get; set; }
        [StringLength(maximumLength: 80, ErrorMessage = "The {0} field can only be up to 100 characters")]
        public string nombre_Rifa { get; set; }
        public bool available_rifa { get; set; }
    }
}
