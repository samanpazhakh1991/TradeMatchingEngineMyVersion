namespace TradeMatchingEngine
{
    public partial class StockMarketMatchEngine
    {
        class Opened : StockMarketState
        {
            public Opened(StockMarketMatchEngine stockMarketMatchEngine) : base(stockMarketMatchEngine)
            {
            }
            public override void PreOpen()
            {
                StockMarketMatchEngine.state = new PreOpened(StockMarketMatchEngine);
                StockMarketMatchEngine.state.Code = MarcketState.PreOpen;
                Code = MarcketState.PreOpen;
                StockMarketMatchEngine.preOpen();
            }
            

        }
    }
}
