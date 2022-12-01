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
            new Card(1,"Carta #1 El Gallo"),
            new Card(2,"Carta #2 El Diablo"),
            new Card(3,"Carta #3 La Dama"),
            new Card(4,"Carta #4 El Catrín"),
            new Card(5,"Carta #5 El Paraguas"),
            new Card(6,"Carta #6 La Sirena"),
            new Card(7,"Carta #7 La Escalera"),
            new Card(8,"Carta #8 La Botella"),
            new Card(9,"Carta #9 El Barril"),
            new Card(10,"Carta #10 El Árbol"),
            new Card(11,"Carta #11 El Melón"),
            new Card(12,"Carta #12 El Valiente"),
            new Card(13,"Carta #13 El Gorrito"),
            new Card(14,"Carta #14 La Muerte"),
            new Card(15,"Carta #15 La Pera"),
            new Card(16,"Carta #16 La Bandera"),
            new Card(17,"Carta #17 El Bandolón"),
            new Card(18,"Carta #18 El Violoncello"),
            new Card(19,"Carta #19 La Garza"),
            new Card(20,"Carta #20 El Pájaro"),
            new Card(21,"Carta #21 La Mano"),
            new Card(22,"Carta #22 La Bota"),
            new Card(23,"Carta #23 La Luna"),
            new Card(24,"Carta #24 El Cotorro"),
            new Card(25,"Carta #25 El Borracho"),
            new Card(26,"Carta #26 El Negrito"),
            new Card(27,"Carta #27 El Corazón"),
            new Card(28,"Carta #28 La Sandía"),
            new Card(29,"Carta #29 El Tambor"),
            new Card(30,"Carta #30 El Camarón"),
            new Card(31,"Carta #31 Las Jaras"),
            new Card(32,"Carta #32 El Músico"),
            new Card(33,"Carta #33 La Araña"),
            new Card(34,"Carta #34 El Soldado"),
            new Card(35,"Carta #35 La Estrella"),
            new Card(36,"Carta #36 El Cazo"),
            new Card(37,"Carta #37 El Mundo"),
            new Card(38,"Carta #38 El Apache"),
            new Card(39,"Carta #39 El Nopal"),
            new Card(40,"Carta #40 El Alacrán"),
            new Card(41,"Carta #41 La Rosa"),
            new Card(42,"Carta #42 La Calavera"),
            new Card(43,"Carta #43 La Campana"),
            new Card(44,"Carta #44 El Cantarito"),
            new Card(45,"Carta #45 El Venado"),
            new Card(46,"Carta #46 El Sol"),
            new Card(47,"Carta #47 La Corona"),
            new Card(48,"Carta #48 La Chalupa"),
            new Card(49,"Carta #49 El Pino"),
            new Card(50,"Carta #50 El Pescado"),
            new Card(51,"Carta #51 La Palma"),
            new Card(52,"Carta #52 La Maceta"),
            new Card(53,"Carta #53 El Arpa"),
            new Card(54,"Carta #54 La Rana")
        };
    }
}