using System;

namespace LD_EC_PiaBackEnd.DTOs
{
    public class RespuestaAuthenticacion
    {
        public string Token { get; set; }
        public DateTime Caducidad { get; set; }
    }
}
