using System.ComponentModel.DataAnnotations;

namespace LD_EC_PiaBackEnd.DTOs
{
    public class EditarAdminDTOs
    {
        [Required]
        [EmailAddress]
        public string email { get; set; }
    }
}
