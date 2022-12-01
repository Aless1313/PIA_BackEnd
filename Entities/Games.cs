using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LD_EC_PiaBackEnd.Entities
{
    public class Games : IValidatableObject
    {
        [Key]
        public int id_Game { get; set; }

        [Required]
        public string id_players { get; set; }

        public Players player { get; set; }

        [Required]
        public int id_Rifa { get; set; }
        public Rifa rifa { get; set; }
        public int Numero_Loteria { get; set; }
        public bool Winner { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(Numero_Loteria < 1 || Numero_Loteria > 54)
            {
                yield return new ValidationResult("Solo se pueden elegir numeros del 1 al 54", new string[] { nameof(Numero_Loteria) });
            }
            throw new NotImplementedException();
        }
    }
}
