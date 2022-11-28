namespace LD_EC_PiaBackEnd.Entities
{
    public class Card
    {
        public int id { get; set; }
        public string name { get; set; }

        public Card(int id, string name)
        {
            this.id = id;
            this.name = name;
           
        }
    }
}
