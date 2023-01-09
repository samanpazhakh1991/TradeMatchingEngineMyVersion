namespace TradeMatchingEngine
{
    public class TradeInfo
    {
        public TradeInfo()
        {
            TradeId = DateTime.Now.Ticks;
        }
        public long TradeId { get; set; }
        public long BuyOrderId { get; set; }
        public long SellOrderId { get; set; }
        public int Amount { get; set; }
        public int Price { get; set; }
    }
}