using LD_EC_PiaBackEnd.Entities;
using LD_EC_PiaBackEnd.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace LD_EC_PiaBackEnd.DTOs
{
    public class RifaPatchDTO
    {
        [PrimeraLetraMayuscula]
        public string? name { get; set; }   
        public bool available { get; set; }
    }
}
