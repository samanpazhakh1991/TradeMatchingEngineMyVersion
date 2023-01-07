namespace TradeMatchingEngine
{
    public partial class StockMarketMatchEngine
    {
        class PreOpened : StockMarketState
        {
            public PreOpened(StockMarketMatchEngine stockMarketMatchEngine) : base(stockMarketMatchEngine)
            {
            }

            public override void Open()
            {
                
                StockMarketMatchEngine.state = new Opened(StockMarketMatchEngine);
                StockMarketMatchEngine.state.Code = MarcketState.Open;
                StockMarketMatchEngine.open();
            }

            public override void Close()
            {
                StockMarketMatchEngine.state = new Closed(StockMarketMatchEngine);
                StockMarketMatchEngine.state.Code = MarcketState.Close;
                StockMarketMatchEngine.close();
            }


        }
    }
}
