namespace TradeMatchingEngine
{
    public partial class StockMarketMatchEngine
    {
        class StockMarketState : IStockMarketMatchEngine
        {
            public MarcketState Code;

            protected StockMarketMatchEngine StockMarketMatchEngine;

            public StockMarketState(StockMarketMatchEngine stockMarketMatchEngine)
            {
                this.StockMarketMatchEngine = stockMarketMatchEngine;
            }
            public virtual void Close()
            {
                throw new NotImplementedException();
            }
          
            public virtual void Open()
            {
                throw new NotImplementedException();
            }

            public virtual void PreOpen()
            {
                throw new NotImplementedException();
            }
        }
        
    }
}
