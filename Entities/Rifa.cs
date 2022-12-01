using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LD_EC_PiaBackEnd.Entities
{
    public class Rifa
    {
        public int id { get; set; }

        [StringLength(maximumLength:50, ErrorMessage = "El campo excede de caracteres")]
        public string nombre_Rifa { get; set; }

        public bool available_rifa { get; set; }

        public List<Games> Games { get; set; }

        public List<Prize> ListPrize { get; set; }
    }
}
