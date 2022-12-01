using LD_EC_PiaBackEnd.Entities;
using System.Collections.Generic;

namespace LD_EC_PiaBackEnd.Entities
{
    public class CartasInfo
    {
        public CartasInfo() { }

        public List<Card> GetCartas()
        {
            return this.Cartas;
        }

        public List<Card> Cartas = new List<Card> {
            new Card(1,"ElGallo"),
            new Card(2,"ElDiablo"),
            new Card(3,"LaDama"),
            new Card(4,"ElCatrín"),
            new Card(5,"ElParaguas"),
            new Card(6,"LaSirena"),
            new Card(7,"LaEscalera"),
            new Card(8,"LaBotella"),
            new Card(9,"ElBarril"),
            new Card(10,"ElÁrbol"),
            new Card(11,"ElMelón"),
            new Card(12,"ElValiente"),
            new Card(13,"ElGorrito"),
            new Card(14,"LaMuerte"),
            new Card(15,"LaPera"),
            new Card(16,"LaBandera"),
            new Card(17,"ElBandolón"),
            new Card(18,"ElVioloncello"),
            new Card(19,"LaGarza"),
            new Card(20,"ElPájaro"),
            new Card(21,"LaMano"),
            new Card(22,"LaBota"),
            new Card(23,"LaLuna"),
            new Card(24,"ElCotorro"),
            new Card(25,"ElBorracho"),
            new Card(26,"ElNegrito"),
            new Card(27,"ElCorazón"),
            new Card(28,"LaSandía"),
            new Card(29,"ElTambor"),
            new Card(30,"ElCamarón"),
            new Card(31,"LasJaras"),
            new Card(32,"ElMúsico"),
            new Card(33,"LaAraña"),
            new Card(34,"ElSoldado"),
            new Card(35,"LaEstrella"),
            new Card(36,"ElCazo"),
            new Card(37,"ElMundo"),
            new Card(38,"ElApache"),
            new Card(39,"ElNopal"),
            new Card(40,"ElAlacrán"),
            new Card(41,"LaRosa"),
            new Card(42,"LaCalavera"),
            new Card(43,"LaCampana"),
            new Card(44,"ElCantarito"),
            new Card(45,"ElVenado"),
            new Card(46,"ElSol"),
            new Card(47,"LaCorona"),
            new Card(48,"LaChalupa"),
            new Card(49,"ElPino"),
            new Card(50,"ElPescado"),
            new Card(51,"LaPalma"),
            new Card(52,"LaMaceta"),
            new Card(53,"ElArpa"),
            new Card(54,"LaRana")
        };
    }
}