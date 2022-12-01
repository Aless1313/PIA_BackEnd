using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace LD_EC_PiaBackEnd.DTOs
{
    public class PlayersCreationDTOs
    {
        [EmailAddress]
        public string email_players { get; set; }
        public string idUser { get; set; }
        public IdentityUser user { get; set; }
    }
}
