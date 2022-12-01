using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LD_EC_PiaBackEnd.Entities
{
    public class Players
    {
        [Key]
        public int id_players { get; set; }

        [EmailAddress]
        public string email_players { get; set; }
        public string idUser { get; set; }
        public IdentityUser user { get; set; }
        public List<Games> GameList_players { get; set; }

    }
}
