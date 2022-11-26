namespace MarketStore.Models
{
    public class jointable
    {


        public Store store { get; set; }
        public Product product { get; set; }
        public Order2 order { get; set; }

        public double total { get; set; }
        public Productorder productorder { get; set; }

    }
}
