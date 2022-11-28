using System.ComponentModel.DataAnnotations;

namespace LD_EC_PiaBackEnd.DTOs
{
    public class EditarAdmin
    {
        [Required]
        [EmailAddress]
        public string email { get; set; }
    }
}
