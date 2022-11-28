using System.ComponentModel.DataAnnotations;

namespace LD_EC_PiaBackEnd.DTOs
{
    public class EditarRifa
    {
        public int id { get; set; }
        [StringLength(maximumLength: 100, ErrorMessage = "The {0} field can only be up to 100 characters")]
        public string nombre { get; set; }
        public bool vigente { get; set; }
    }
}
