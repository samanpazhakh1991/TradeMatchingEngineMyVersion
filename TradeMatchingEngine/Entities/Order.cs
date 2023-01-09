namespace TradeMatchingEngine
{
    public class Order
    {


        ~Order()
        {
            Console.WriteLine($"Order With ID: {Id} Destructed!!!");
        }
        public long Id { get; set; }

        public Side Side { get; set; }

        public int Price { get; set; }

        public int Amount { get; set; }
        public bool HasCompleted
        {
            get
            {
                if (Amount <= 0) return true;

                return false;
            }
        }
    }
}
