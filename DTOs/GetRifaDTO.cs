using LD_EC_PiaBackEnd.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Policy;

namespace LD_EC_PiaBackEnd.DTOs
{
    public class GetRifaDTO
    {
        public int id_getrifa { get; set; }
        public string name { get; set; }
        public bool available { get; set; }
        public List<GetGamesDTOs> games { get; set; }
        public List<GetPrizeDTOs> prizes { get; set; }
    }
}
