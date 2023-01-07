namespace TradeMatchingEngine
{
    public partial class StockMarketMatchEngine
    {
        class Closed : StockMarketState
        {
            public Closed(StockMarketMatchEngine stockMarketMatchEngine) : base(stockMarketMatchEngine)
            {
            }

            public override void PreOpen()
            {

                StockMarketMatchEngine.state = new PreOpened(StockMarketMatchEngine);
                StockMarketMatchEngine.state.Code = MarcketState.PreOpen;
                StockMarketMatchEngine.preOpen();
            }
        }
    }
}
