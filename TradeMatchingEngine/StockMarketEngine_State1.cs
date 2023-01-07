using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeMatchingEngine
{
    public partial class StockMarketMatchEngine
    {
        public class StockMarketEngine_State1 : IStockMarketMatchEngine
        {
            protected StockMarketMatchEngine StockMarketEngine;
            public StockMarketEngine_State1(StockMarketMatchEngine stockMarketEngine)
            {
                this.StockMarketEngine = stockMarketEngine;
            }

            public virtual void Close()
            {
                throw new NotImplementedException();
            }

            public virtual void Enqueue(int price, int amount, Side side)
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
