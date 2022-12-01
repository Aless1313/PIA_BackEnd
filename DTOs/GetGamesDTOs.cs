using LD_EC_PiaBackEnd.Entities;
using System.ComponentModel.DataAnnotations;

namespace LD_EC_PiaBackEnd.DTOs
{
    public class GetGamesDTOs
    {
        public int id { get; set; }
        public string id_players { get; set; }
        public int id_Rifa { get; set; }
        public int Numero_Loteria { get; set; }
        public bool Winner { get; set; }
    }
}
