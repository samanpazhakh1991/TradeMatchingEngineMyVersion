namespace TradeMatchingEngine
{
    public class Order
    {
        public Order()
        {
            Id = DateTime.Now.Ticks;
        }


        ~Order()
        {
            Console.WriteLine($"Order With ID: {Id} Destructed!!!");
        }
        public long Id { get; set; }

        public Side Side { get; set; }

        public int Price { get; set; }

        public int Amount { get; set; }
    }
}
