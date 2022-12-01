using LD_EC_PiaBackEnd.DTOs;
namespace LD_EC_PiaBackEnd.Entities
{
    public class WinnerCard
    {
        public string email { get; set; }
        public int id_rifa { get; set; }
        public string nameRifa { get; set; }
        public Card winnerCard { get; set; }
        public GetPrizeDTOs prize { get; set; }

        public WinnerCard(string email, int idRifa, string nameRifa, Card winnercard, GetPrizeDTOs prize)
        {
            this.email = email;
            this.id_rifa = idRifa;
            this.nameRifa = nameRifa;
            this.winnerCard = winnercard;
            this.prize = prize;
        }
    }
}
