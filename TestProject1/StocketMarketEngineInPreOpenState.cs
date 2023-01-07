using TradeMatchingEngine;
using Xunit;

namespace TestProject1
{
    public class StocketMarketEngineInPreOpenState
    {
        private StockMarketMatchEngine sut;
        public StocketMarketEngineInPreOpenState()
        {
            sut = new StockMarketMatchEngine();
            sut.PreOpen();
        }

        [Fact]
        public void StockMarketMatchEngine_1BuyOrderEnters_MustEnQueueToPreOrderQueue()
        {
        
            //Arrange
            var buyOrder = new Order()
            {
            
                Amount = 5,
                Price = 100,
                Side = Side.Buy
            };

            //Action
            sut.Trade(buyOrder.Amount,buyOrder.Price,buyOrder.Side);    

            //Assert

            Assert.Equal(0,sut.TradeCount);
            Assert.Single(sut.Orders);
            Assert.Single(sut.GetPreOrderQueue());
            Assert.Equal(0,sut.GetBuyOrderCount());
            Assert.Equal(0,sut.GetSellOrderCount());


        }

        [Fact]
        public void StockMarketMatchEngine_1SellOrderEnters_MustEnQueueToPreOrderQueue()
        {
            //Arrange
            var sellOrder = new Order()
            {
               
                Amount = 5,
                Price = 100,
                Side = Side.Sell
            };

            //Action
            sut.Trade(sellOrder.Amount,sellOrder.Price,sellOrder.Side);

            //Assert

            Assert.Equal(0, sut.TradeCount);
            Assert.Single(sut.Orders);
            Assert.Single(sut.GetPreOrderQueue());
            Assert.Equal(0, sut.GetBuyOrderCount());
            Assert.Equal(0, sut.GetSellOrderCount());


        }

        [Fact]
        public void StockMarketMatchEngine_MultipleSellOrderEnters_MustEnQueueToPreOrderQueue()
        {
            //Arrange
            var sellOrder1 = new Order()
            {
               
                Amount = 5,
                Price = 100,
                Side = Side.Sell
            };
            var sellOrder2 = new Order()
            {
                
                Amount = 5,
                Price = 100,
                Side = Side.Sell
            };
            var sellOrder3 = new Order()
            {
               
                Amount = 5,
                Price = 100,
                Side = Side.Sell
            };
            var sellOrder4 = new Order()
            {
             
                Amount = 5,
                Price = 100,
                Side = Side.Sell
            };
            //Action
            sut.Trade(sellOrder1.Amount, sellOrder1.Price, sellOrder1.Side);
            sut.Trade(sellOrder2.Amount, sellOrder2.Price, sellOrder2.Side);
            sut.Trade(sellOrder3.Amount, sellOrder3.Price, sellOrder3.Side);
            sut.Trade(sellOrder4.Amount, sellOrder4.Price, sellOrder4.Side);

            //Assert

            Assert.Equal(0, sut.TradeCount);
            Assert.Equal(4, sut.Orders.Count);
            Assert.Equal(4, sut.GetPreOrderQueue().Count);
            Assert.Equal(0, sut.GetBuyOrderCount());
            Assert.Equal(0, sut.GetSellOrderCount());


        }

        [Fact]
        public void StockMarketMatchEngine_MultipleBuyOrderEnters_MustEnQueueToPreOrderQueue()
        {
            //Arrange
            var buyOrder1 = new Order()
            {
                
                Amount = 5,
                Price = 100,
                Side = Side.Buy
            };
            var buyOrder2 = new Order()
            {
                
                Amount = 5,
                Price = 100,
                Side = Side.Buy
            };
            var buyOrder3 = new Order()
            {
                
                Amount = 5,
                Price = 100,
                Side = Side.Buy
            };
            var buyOrder4 = new Order()
            {
               
                Amount = 5,
                Price = 100,
                Side = Side.Buy
            };
            //Action
            sut.Trade(buyOrder1.Amount, buyOrder1.Price, buyOrder1.Side);
            sut.Trade(buyOrder2.Amount, buyOrder2.Price, buyOrder2.Side);
            sut.Trade(buyOrder3.Amount, buyOrder3.Price, buyOrder3.Side);
            sut.Trade(buyOrder4.Amount, buyOrder4.Price, buyOrder4.Side);

            //Assert

            Assert.Equal(0, sut.TradeCount);
            Assert.Equal(4, sut.Orders.Count);
            Assert.Equal(4, sut.GetPreOrderQueue().Count);
            Assert.Equal(0, sut.GetBuyOrderCount());
            Assert.Equal(0, sut.GetSellOrderCount());


        }

        [Fact]
        public void StockMarketMatchEngine_MultipleBuyAndSellOrderEnters_MustEnQueueToPreOrderQueue()
        {
            //Arrange
            var buyOrder1 = new Order()
            {
                
                Amount = 5,
                Price = 100,
                Side = Side.Buy
            };
            var buyOrder2 = new Order()
            {
                
                Amount = 5,
                Price = 100,
                Side = Side.Buy
            };
            var buyOrder3 = new Order()
            {
             
                Amount = 5,
                Price = 100,
                Side = Side.Buy
            };
            var buyOrder4 = new Order()
            {
               
                Amount = 5,
                Price = 100,
                Side = Side.Buy
            };
            var sellOrder1 = new Order()
            {
               
                Amount = 5,
                Price = 100,
                Side = Side.Sell
            };
            var sellOrder2 = new Order()
            {
               
                Amount = 5,
                Price = 100,
                Side = Side.Sell
            };
            var sellOrder3 = new Order()
            {
                Amount = 5,
                Price = 100,
                Side = Side.Sell
            };
            var sellOrder4 = new Order()
            {
                
                Amount = 5,
                Price = 100,
                Side = Side.Sell
            };
            //Action
            sut.Trade(buyOrder1.Amount, buyOrder1.Price, buyOrder1.Side);
            sut.Trade(buyOrder2.Amount, buyOrder2.Price, buyOrder2.Side);
            sut.Trade(buyOrder3.Amount, buyOrder3.Price, buyOrder3.Side);
            sut.Trade(buyOrder4.Amount, buyOrder4.Price, buyOrder4.Side);
            sut.Trade(sellOrder1.Amount, sellOrder1.Price, sellOrder1.Side);
            sut.Trade(sellOrder2.Amount, sellOrder2.Price, sellOrder2.Side);
            sut.Trade(sellOrder3.Amount, sellOrder3.Price, sellOrder3.Side);
            sut.Trade(sellOrder4.Amount, sellOrder4.Price, sellOrder4.Side);
            //Assert

            Assert.Equal(0, sut.TradeCount);
            Assert.Equal(8, sut.Orders.Count);
            Assert.Equal(8, sut.GetPreOrderQueue().Count);
            Assert.Equal(0, sut.GetBuyOrderCount());
            Assert.Equal(0, sut.GetSellOrderCount());


        }
    }
}
