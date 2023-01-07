using System.Collections.Generic;
using System.Linq;
using TradeMatchingEngine;
using Xunit;

namespace TestProject1
{
    public class StocketMarketEngineInOpenState
    {
        private StockMarketMatchEngine sut;
        public StocketMarketEngineInOpenState()
        {
            sut = new StockMarketMatchEngine();
            sut.PreOpen();
            sut.Open();
        }

        [Fact]
        [Trait("StockMarketMatchEngine", "Open")]
        public void StockMarketMatchEngine_FirstSellOrderEnters_MustEnqueue1SellOrder()
        {
            //Arrange


            var sellOrder = new Order()
            {
                
                Price = 100,
                Amount = 10,
                Side = Side.Sell
            };

            //Action
            sut.Trade(sellOrder.Amount, sellOrder.Price, sellOrder.Side);

            //Assert
            Assert.Equal(0, sut.TradeCount);
            Assert.Single(sut.Orders);
            Assert.Equal(0, sut.GetBuyOrderCount());
            Assert.Equal(1, sut.GetSellOrderCount());
        }

        [Trait("StockMarketMatchEngine", "Open")]
        [Fact]
        public void StockMarketMatchEngine_FirstBuyOrderEnters_MustEnqueue1BuyOrder()
        {
            //Arrange
            var buyOrder = new Order()
            {
                Id = 1,
                Price = 100,
                Amount = 10,
                Side = Side.Buy
            };

            //Action
            sut.Trade(buyOrder.Amount, buyOrder.Price, buyOrder.Side);

            //Assert
            Assert.Equal(0, sut.TradeCount);
            Assert.Single(sut.Orders);
            Assert.Equal(1, sut.GetBuyOrderCount());
            Assert.Equal(0, sut.GetSellOrderCount());
        }

        [Trait("StockMarketMatchEngine", "Open")]
        [Fact]
        public void StockMarketMatchEngine_SeveralSellOrderEnters_MustEnqueueAllOrder()
        {
            //Arrange
            var sellOrder1 = new Order()
            {
                
                Price = 100,
                Amount = 10,
                Side = Side.Sell
            };

            var sellOrder2 = new Order()
            {
          
                Price = 100,
                Amount = 10,
                Side = Side.Sell
            };

            var sellOrder3 = new Order()
            {
               
                Price = 110,
                Amount = 10,
                Side = Side.Sell
            };
            var sellOrder4 = new Order()
            {
            
                Price = 100,
                Amount = 10,
                Side = Side.Sell
            };
            var sellOrder5 = new Order()
            {
             
                Price = 120,
                Amount = 10,
                Side = Side.Sell
            };
            //Action
            sut.Trade(sellOrder1.Amount, sellOrder1.Price, sellOrder1.Side);
            sut.Trade(sellOrder2.Amount, sellOrder2.Price, sellOrder2.Side);
            sut.Trade(sellOrder3.Amount, sellOrder3.Price, sellOrder3.Side);
            sut.Trade(sellOrder4.Amount, sellOrder4.Price, sellOrder4.Side);
            sut.Trade(sellOrder5.Amount, sellOrder5.Price, sellOrder5.Side);

            //Assert
            Assert.Equal(0, sut.TradeCount);
            Assert.Equal(5, sut.Orders.Count);
            Assert.Equal(0, sut.GetBuyOrderCount());
            Assert.Equal(5, sut.GetSellOrderCount());
        }

        [Trait("StockMarketMatchEngine", "Open")]
        [Fact]
        public void StockMarketMatchEngine_SeveralBuyOrderEnters_MustEnqueueAllOrder()
        {
            //Arrange
            var buyOrder1 = new Order()
            {
               
                Price = 100,
                Amount = 10,
                Side = Side.Buy
            };

            var buyOrder2 = new Order()
            {
               
                Price = 100,
                Amount = 10,
                Side = Side.Buy
            };

            var buyOrder3 = new Order()
            {
               
                Price = 110,
                Amount = 10,
                Side = Side.Buy
            };
            var buyOrder4 = new Order()
            {
                
                Price = 100,
                Amount = 10,
                Side = Side.Buy
            };
            var buyOrder5 = new Order()
            {
                
                Price = 120,
                Amount = 10,
                Side = Side.Buy
            };
            //Action
            sut.Trade(buyOrder1.Amount, buyOrder1.Price, buyOrder1.Side);
            sut.Trade(buyOrder2.Amount, buyOrder2.Price, buyOrder2.Side);
            sut.Trade(buyOrder3.Amount, buyOrder3.Price, buyOrder3.Side);
            sut.Trade(buyOrder4.Amount, buyOrder4.Price, buyOrder4.Side);
            sut.Trade(buyOrder5.Amount, buyOrder5.Price, buyOrder5.Side);

            //Assert
            Assert.Equal(0, sut.TradeCount);
            Assert.Equal(5, sut.Orders.Count);
            Assert.Equal(5, sut.GetBuyOrderCount());
            Assert.Equal(0, sut.GetSellOrderCount());
        }

        [Trait("StockMarketMatchEngine", "Open")]
        [Fact]
        public void StockMarketMatchEngine_SeveralBuyAndSellOrderEnters_MustEnqueueAllOrder()
        {
            //Arrange
            var buyOrder1 = new Order()
            {
                
                Price = 100,
                Amount = 10,
                Side = Side.Buy
            };

            var buyOrder2 = new Order()
            {
                
                Price = 110,
                Amount = 5,
                Side = Side.Buy
            };

            var sellOrder1 = new Order()
            {
               
                Price = 120,
                Amount = 10,
                Side = Side.Sell
            };
            var sellOrder2 = new Order()
            {
                
                Price = 125,
                Amount = 10,
                Side = Side.Sell
            };
            var sellOrder3 = new Order()
            {
                
                Price = 120,
                Amount = 10,
                Side = Side.Sell
            };
            //Action
            sut.Trade(buyOrder1.Amount, buyOrder1.Price, buyOrder1.Side);
            sut.Trade(sellOrder1.Amount, sellOrder1.Price, sellOrder1.Side);
            sut.Trade(buyOrder2.Amount, buyOrder2.Price, buyOrder2.Side);
            sut.Trade(sellOrder2.Amount, sellOrder2.Price, sellOrder2.Side);
            sut.Trade(sellOrder3.Amount, sellOrder3.Price, sellOrder3.Side);

            //Assert
            Assert.Equal(0, sut.TradeCount);
            Assert.Equal(5, sut.Orders.Count);
            Assert.Equal(2, sut.GetBuyOrderCount());
            Assert.Equal(3, sut.GetSellOrderCount());
        }

        [Trait("StockMarketMatchEngine", "Open")]
        [Fact]
        public void StockMarketMatchEngine_SellEntersFirstBuyOrderEntersWithSamePriceAndAmount_MustExecute1Trade()
        {
            //Arrange
            var buyOrder = new Order()
            {
                
                Price = 100,
                Amount = 5,
                Side = Side.Buy
            };
            var sellOrder = new Order()
            {
                
                Price = 100,
                Amount = 5,
                Side = Side.Sell
            };

            sut.Trade(buyOrder.Amount, buyOrder.Price, buyOrder.Side);

            //Action
            sut.Trade(sellOrder.Amount, sellOrder.Price, sellOrder.Side);

            //Assert
            Assert.Equal(1, sut.TradeCount);
            Assert.Empty(sut.Orders);
            Assert.Equal(0, sut.GetBuyOrderCount());
            Assert.Equal(0, sut.GetSellOrderCount());
        }

        [Trait("StockMarketMatchEngine", "Open")]
        [Fact]
        public void StockMarketMatchEngine_BuyEntersFirstSellOrderEntersWithSamePriceAndAmount_MustExecute1Trade()
        {
            //Arrange
            var buyOrder = new Order()
            {
               
                Price = 100,
                Amount = 5,
                Side = Side.Buy
            };
            var sellOrder = new Order()
            {
               
                Price = 100,
                Amount = 5,
                Side = Side.Sell
            };

            sut.Trade(sellOrder.Amount, sellOrder.Price, sellOrder.Side);


            //Action
            sut.Trade(buyOrder.Amount, buyOrder.Price, buyOrder.Side);
            //Assert
            Assert.Equal(1, sut.TradeCount);
            Assert.Empty(sut.Orders);
            Assert.Equal(0, sut.GetBuyOrderCount());
            Assert.Equal(0, sut.GetSellOrderCount());
        }

        [Trait("StockMarketMatchEngine", "Open")]
        [Fact]
        public void StockMarketMatchEngine_BuyEntersFirstSellOrderEntersWithHigherAmount_MustExecute1TradeAndEnqueue1Sell()
        {
            //Arrange
            var buyOrder = new Order()
            {
                
                Price = 100,
                Amount = 5,
                Side = Side.Buy
            };
            var sellOrder = new Order()
            {
              
                Price = 100,
                Amount = 10,
                Side = Side.Sell
            };


            sut.Trade(buyOrder.Amount, buyOrder.Price, buyOrder.Side);

            //Action
            sut.Trade(sellOrder.Amount, sellOrder.Price, sellOrder.Side);

            //Assert
            Assert.Equal(1, sut.TradeCount);
            Assert.Single(sut.Orders);
            Assert.Equal(0, sut.GetBuyOrderCount());
            Assert.Equal(1, sut.GetSellOrderCount());
        }

        [Trait("StockMarketMatchEngine", "Open")]
        [Fact]
        public void StockMarketMatchEngine_SellEntersFirstBuyOrderEntersWithHigherAmount_MustExecute1TradeAndEnqueue1Buy()
        {
            //Arrange
            var buyOrder = new Order()
            {
              
                Price = 100,
                Amount = 15,
                Side = Side.Buy
            };
            var sellOrder = new Order()
            {
              
                Price = 100,
                Amount = 10,
                Side = Side.Sell
            };


            sut.Trade(sellOrder.Amount, sellOrder.Price, sellOrder.Side);


            //Action
            sut.Trade(buyOrder.Amount, buyOrder.Price, buyOrder.Side);

            //Assert
            Assert.Equal(1, sut.TradeCount);
            Assert.Single(sut.Orders);
            Assert.Equal(1, sut.GetBuyOrderCount());
            Assert.Equal(0, sut.GetSellOrderCount());
        }

        [Trait("StockMarketMatchEngine", "Open")]
        [Fact]
        public void StockMarketMatchEngine_BuyEntersFirstSellOrderEntersWithLowerAmount_MustExecute1TradeAndEnqueue1Buy()
        {
            //Arrange
            var buyOrder = new Order()
            {
               
                Price = 100,
                Amount = 15,
                Side = Side.Buy
            };
            var sellOrder = new Order()
            {
               
                Price = 100,
                Amount = 10,
                Side = Side.Sell
            };


            sut.Trade(buyOrder.Amount, buyOrder.Price, buyOrder.Side);


            //Action
            sut.Trade(sellOrder.Amount, sellOrder.Price, sellOrder.Side);

            //Assert
            Assert.Equal(1, sut.TradeCount);
            Assert.Single(sut.Orders);
            Assert.Equal(1, sut.GetBuyOrderCount());
            Assert.Equal(0, sut.GetSellOrderCount());
        }

        [Trait("StockMarketMatchEngine", "Open")]
        [Fact]
        public void StockMarketMatchEngine_SellEntersFirstSBuyOrderEntersWithLowerAmount_MustExecute1TradeAndEnqueue1Sell()
        {
            //Arrange
            var buyOrder = new Order()
            {
               
                Price = 100,
                Amount = 10,
                Side = Side.Buy
            };
            var sellOrder = new Order()
            {
                
                Price = 100,
                Amount = 15,
                Side = Side.Sell
            };


            sut.Trade(sellOrder.Amount, sellOrder.Price, sellOrder.Side);


            //Action
            sut.Trade(buyOrder.Amount, buyOrder.Price, buyOrder.Side);

            //Assert
            Assert.Equal(1, sut.TradeCount);
            Assert.Single(sut.Orders);
            Assert.Equal(0, sut.GetBuyOrderCount());
            Assert.Equal(1, sut.GetSellOrderCount());
        }

        [Trait("StockMarketMatchEngine", "Open")]
        [Fact]
        public void StockMarketMatchEngine_SeveralSellsEntersFirstSBuyOrderEntersWithCoverOfAllAmount_MustExecute2TradeAndClearAllQueues()
        {
            //Arrange
            var sellOrder1 = new Order()
            {
                
                Price = 100,
                Amount = 8,
                Side = Side.Sell
            };
            var sellOrder2 = new Order()
            {
                
                Price = 100,
                Amount = 12,
                Side = Side.Sell
            };

            var buyOrder = new Order()
            {
                
                Price = 100,
                Amount = 20,
                Side = Side.Buy
            };


            sut.Trade(sellOrder1.Amount, sellOrder1.Price, sellOrder1.Side);
            sut.Trade(sellOrder2.Amount, sellOrder2.Price, sellOrder2.Side);


            //Action
            sut.Trade(buyOrder.Amount, buyOrder.Price, buyOrder.Side);

            //Assert
            Assert.Equal(2, sut.TradeCount);
            Assert.Empty(sut.Orders);
            Assert.Equal(0, sut.GetBuyOrderCount());
            Assert.Equal(0, sut.GetSellOrderCount());
        }

        [Trait("StockMarketMatchEngine", "Open")]
        [Fact]
        public void StockMarketMatchEngine_SeveralBuysEntersFirstSSellOrderEntersWithCoverOfAllAmount_MustExecute2TradeAndClearAllQueues()
        {
            //Arrange
            var buyOrder1 = new Order()
            {
              
                Price = 100,
                Amount = 8,
                Side = Side.Buy
            };
            var buyOrder2 = new Order()
            {
               
                Price = 100,
                Amount = 12,
                Side = Side.Buy
            };

            var sellOrder = new Order()
            {
               
                Price = 100,
                Amount = 20,
                Side = Side.Sell
            };


            sut.Trade(buyOrder1.Amount, buyOrder1.Price, buyOrder1.Side);
            sut.Trade(buyOrder2.Amount, buyOrder2.Price, buyOrder2.Side);


            //Action
            sut.Trade(sellOrder.Amount, sellOrder.Price, sellOrder.Side);

            //Assert
            Assert.Equal(2, sut.TradeCount);
            Assert.Empty(sut.Orders);
            Assert.Equal(0, sut.GetBuyOrderCount());
            Assert.Equal(0, sut.GetSellOrderCount());
        }

        [Trait("StockMarketMatchEngine", "Open")]
        [Fact]
        public void StockMarketMatchEngine_SeveralSellsEntersFirstSBuyOrderEntersWithCoverOf1AndHalfOfSecondSell_MustExecute2TradeAndEnQueueSecondSell()
        {
            //Arrange
            var sellOrder1 = new Order()
            {
            
                Price = 100,
                Amount = 10,
                Side = Side.Sell
            };
            var sellOrder2 = new Order()
            {
                
                Price = 100,
                Amount = 5,
                Side = Side.Sell
            };

            var sellOrder3 = new Order()
            {
               
                Price = 100,
                Amount = 8,
                Side = Side.Sell
            };
            var buyOrder = new Order()
            {
               
                Price = 100,
                Amount = 13,
                Side = Side.Buy
            };

            sut.Trade(sellOrder1.Amount, sellOrder1.Price, sellOrder1.Side);
            sut.Trade(sellOrder2.Amount, sellOrder2.Price, sellOrder2.Side);
            sut.Trade(sellOrder3.Amount, sellOrder3.Price, sellOrder3.Side);




            //Action
            sut.Trade(buyOrder.Amount, buyOrder.Price, buyOrder.Side);

            //Assert
            Assert.Equal(2, sut.TradeCount);
            Assert.Equal(2, sut.Orders.Count);
            Assert.Equal(0, sut.GetBuyOrderCount());
            Assert.Equal(2, sut.GetSellOrderCount());
        }

        [Trait("StockMarketMatchEngine", "Open")]
        [Fact]
        public void StockMarketMatchEngine_SeveralBuysEntersFirstSellOrderEntersWithCoverOf1AndHalfOfSecondBuy_MustExecute2TradeAndEnQueueSecondBuy()
        {
            //Arrange
            var buyOrder1 = new Order()
            {
                
                Price = 100,
                Amount = 10,
                Side = Side.Buy
            };
            var buyOrder2 = new Order()
            {
                
                Price = 100,
                Amount = 5,
                Side = Side.Buy
            };

            var buyOrder3 = new Order()
            {
               
                Price = 100,
                Amount = 8,
                Side = Side.Buy
            };
            var sellOrder = new Order()
            {
               
                Price = 100,
                Amount = 13,
                Side = Side.Sell
            };

            sut.Trade(buyOrder1.Amount, buyOrder1.Price, buyOrder1.Side);
            sut.Trade(buyOrder2.Amount, buyOrder2.Price, buyOrder2.Side);
            sut.Trade(buyOrder3.Amount, buyOrder3.Price, buyOrder3.Side);




            //Action
            sut.Trade(sellOrder.Amount, sellOrder.Price, sellOrder.Side);

            //Assert
            Assert.Equal(2, sut.TradeCount);
            Assert.Equal(2, sut.Orders.Count);
            Assert.Equal(2, sut.GetBuyOrderCount());
            Assert.Equal(0, sut.GetSellOrderCount());
        }

        [Trait("StockMarketMatchEngine", "Open")]
        [Fact]
        public void StockMarketMatchEngine_SeveralSellsEntersFirstBuyOrderEntersWithHigherAmount_MustExecute3TradeAndEnQueue1Buy()
        {
            //Arrange
            var sellOrder1 = new Order()
            {
               
                Price = 100,
                Amount = 5,
                Side = Side.Sell
            };
            var sellOrder2 = new Order()
            {
               
                Price = 100,
                Amount = 2,
                Side = Side.Sell
            };

            var sellOrder3 = new Order()
            {
                
                Price = 100,
                Amount = 8,
                Side = Side.Sell
            };
            var buyOrder = new Order()
            {
                Price = 100,
                Amount = 17,
                Side = Side.Buy
            };

            sut.Trade(sellOrder1.Amount, sellOrder1.Price, sellOrder1.Side);
            sut.Trade(sellOrder2.Amount, sellOrder2.Price, sellOrder2.Side);
            sut.Trade(sellOrder3.Amount, sellOrder3.Price, sellOrder3.Side);




            //Action
            sut.Trade(buyOrder.Amount, buyOrder.Price, buyOrder.Side);

            //Assert
            Assert.Equal(3, sut.TradeCount);
            Assert.Single(sut.Orders);
            Assert.Equal(1, sut.GetBuyOrderCount());
            Assert.Equal(0, sut.GetSellOrderCount());
        }

        [Trait("StockMarketMatchEngine", "Open")]
        [Fact]
        public void StockMarketMatchEngine_SeveralBuysEntersFirstSellOrderEntersWithHigherAmount_MustExecute3TradeAndEnQueue1Sell()
        {
            //Arrange
            var buyOrder1 = new Order()
            {
            
                Price = 100,
                Amount = 5,
                Side = Side.Buy
            };
            var buyOrder2 = new Order()
            {
               
                Price = 100,
                Amount = 2,
                Side = Side.Buy
            };

            var buyOrder3 = new Order()
            {
             
                Price = 100,
                Amount = 8,
                Side = Side.Buy
            };
            var sellOrder = new Order()
            {
              
                Price = 100,
                Amount = 17,
                Side = Side.Sell
            };

            sut.Trade(buyOrder1.Amount, buyOrder1.Price, buyOrder1.Side);
            sut.Trade(buyOrder2.Amount, buyOrder2.Price, buyOrder2.Side);
            sut.Trade(buyOrder3.Amount, buyOrder3.Price, buyOrder3.Side);




            //Action
            sut.Trade(sellOrder.Amount, sellOrder.Price, sellOrder.Side);

            //Assert
            Assert.Equal(3, sut.TradeCount);
            Assert.Single(sut.Orders);
            Assert.Equal(0, sut.GetBuyOrderCount());
            Assert.Equal(1, sut.GetSellOrderCount());
        }

        [Fact]
        [Trait("StockMarketMatchEngine", "Open")]
        public void StockMarketMatchEngine_SeveralSellsEntersFirstBuyOrderEntersWithLowerAmount_MustExecute1TradeAndEnQueueFirstSell()
        {
            //Arrange
            var sellOrder1 = new Order()
            {
               
                Price = 100,
                Amount = 5,
                Side = Side.Sell
            };
            var sellOrder2 = new Order()
            {
              
                Price = 100,
                Amount = 2,
                Side = Side.Sell
            };

            var sellOrder3 = new Order()
            {
              
                Price = 100,
                Amount = 8,
                Side = Side.Sell
            };
            var buyOrder = new Order()
            {
               
                Price = 100,
                Amount = 4,
                Side = Side.Buy
            };

            sut.Trade(sellOrder1.Amount, sellOrder1.Price, sellOrder1.Side);
            sut.Trade(sellOrder2.Amount, sellOrder2.Price, sellOrder2.Side);
            sut.Trade(sellOrder3.Amount, sellOrder3.Price, sellOrder3.Side);




            //Action
            sut.Trade(buyOrder.Amount, buyOrder.Price, buyOrder.Side);

            //Assert
            Assert.Equal(1, sut.TradeCount);
            Assert.Equal(3, sut.Orders.Count);
            Assert.Equal(0, sut.GetBuyOrderCount());
            Assert.Equal(3, sut.GetSellOrderCount());
        }

        [Fact]
        [Trait("StockMarketMatchEngine", "Open")]
        public void StockMarketMatchEngine_SeveralBuysEntersFirstSellOrderEntersWithLowerAmount_MustExecute1TradeAndEnQueueFirstBuy()
        {
            //Arrange
            var buyOrder1 = new Order()
            {
              
                Price = 100,
                Amount = 5,
                Side = Side.Buy
            };
            var buyOrder2 = new Order()
            {
               
                Price = 100,
                Amount = 2,
                Side = Side.Buy
            };

            var buyOrder3 = new Order()
            {
               
                Price = 100,
                Amount = 8,
                Side = Side.Buy
            };
            var sellOrder = new Order()
            {
               
                Price = 100,
                Amount = 4,
                Side = Side.Sell
            };

            sut.Trade(buyOrder1.Amount, buyOrder1.Price, buyOrder1.Side);
            sut.Trade(buyOrder2.Amount, buyOrder2.Price, buyOrder2.Side);
            sut.Trade(buyOrder3.Amount, buyOrder3.Price, buyOrder3.Side);




            //Action
            sut.Trade(sellOrder.Amount, sellOrder.Price, sellOrder.Side);

            //Assert
            Assert.Equal(1, sut.TradeCount);
            Assert.Equal(3, sut.Orders.Count);
            Assert.Equal(3, sut.GetBuyOrderCount());
            Assert.Equal(0, sut.GetSellOrderCount());
        }

        [Trait("StockMarketMatchEngine", "Open")]
        [Fact]
        public void StockMarketMatchEngine_SeveralSellsEntersWithDefferPriceFirstBuyOrderEntersWithLowerAmount_MustExecute1TradeAndEnQueueFirstSell()
        {
            //Arrange
            var sellOrder1 = new Order()
            {
               
                Price = 100,
                Amount = 5,
                Side = Side.Sell
            };
            var sellOrder2 = new Order()
            {
              
                Price = 100,
                Amount = 2,
                Side = Side.Sell
            };

            var sellOrder3 = new Order()
            {
               
                Price = 100,
                Amount = 8,
                Side = Side.Sell
            };
            var sellOrder4 = new Order()
            {
               
                Price = 100,
                Amount = 8,
                Side = Side.Sell
            };
            var sellOrder5 = new Order()
            {
                
                Price = 100,
                Amount = 8,
                Side = Side.Sell
            };

            var buyOrder = new Order()
            {
                Price = 100,
                Amount = 4,
                Side = Side.Buy
            };

            sut.Trade(sellOrder1.Amount, sellOrder1.Price, sellOrder1.Side);
            sut.Trade(sellOrder2.Amount, sellOrder2.Price, sellOrder2.Side);
            sut.Trade(sellOrder3.Amount, sellOrder3.Price, sellOrder3.Side);
            sut.Trade(sellOrder4.Amount, sellOrder4.Price, sellOrder4.Side);
            sut.Trade(sellOrder5.Amount, sellOrder5.Price, sellOrder5.Side);




            //Action
            sut.Trade(buyOrder.Amount, buyOrder.Price, buyOrder.Side);

            //Assert
            Assert.Equal(1, sut.TradeCount);
            Assert.Equal(5, sut.Orders.Count);
            Assert.Equal(0, sut.GetBuyOrderCount());
            Assert.Equal(5, sut.GetSellOrderCount());
        }

        [Fact]
        [Trait("StockMarketMatchEngine", "Open")]
        public void StockMarketMatchEngine_SeveralBuysEntersWithDefferPriceFirstSellOrderEntersWithLowerAmount_MustExecute1TradeAndEnQueueFirstBuy()
        {
            //Arrange
            var buyOrder1 = new Order()
            {
              
                Price = 100,
                Amount = 5,
                Side = Side.Buy
            };
            var buyOrder2 = new Order()
            {
                
                Price = 100,
                Amount = 2,
                Side = Side.Buy
            };

            var buyOrder3 = new Order()
            {
               
                Price = 100,
                Amount = 8,
                Side = Side.Buy
            };
            var buyOrder4 = new Order()
            {
             
                Price = 100,
                Amount = 8,
                Side = Side.Buy
            };
            var buyOrder5 = new Order()
            {
                
                Price = 100,
                Amount = 8,
                Side = Side.Buy
            };

            var sellOrder = new Order()
            {
                
                Price = 100,
                Amount = 4,
                Side = Side.Sell
            };

            sut.Trade(buyOrder1.Amount, buyOrder1.Price, buyOrder1.Side);
            sut.Trade(buyOrder2.Amount, buyOrder2.Price, buyOrder2.Side);
            sut.Trade(buyOrder3.Amount, buyOrder3.Price, buyOrder3.Side);
            sut.Trade(buyOrder4.Amount, buyOrder4.Price, buyOrder4.Side);
            sut.Trade(buyOrder5.Amount, buyOrder5.Price, buyOrder5.Side);




            //Action
            sut.Trade(sellOrder.Amount, sellOrder.Price, sellOrder.Side);

            //Assert
            Assert.Equal(1, sut.TradeCount);
            Assert.Equal(5, sut.Orders.Count);
            Assert.Equal(5, sut.GetBuyOrderCount());
            Assert.Equal(0, sut.GetSellOrderCount());
        }

        [Fact]
        [Trait("StockMarketMatchEngine", "Open")]
        public void StockMarketMatchEngine_SeveralBuysWithDefferPriceEntersFirstSellOrderEntersWithHigherAmount_MustExecute1TradeAndEnQueueFirstSell()
        {
            //Arrange
            var buyOrder1 = new Order()
            {
              
                Price = 100,
                Amount = 5,
                Side = Side.Buy
            };
            var buyOrder2 = new Order()
            {
              
                Price = 90,
                Amount = 2,
                Side = Side.Buy
            };


            var sellOrder = new Order()
            {
               
                Price = 100,
                Amount = 6,
                Side = Side.Sell
            };

            sut.Trade(buyOrder1.Amount, buyOrder1.Price, buyOrder1.Side);
            sut.Trade(buyOrder2.Amount, buyOrder2.Price, buyOrder2.Side);

            //Action
            sut.Trade(sellOrder.Amount, sellOrder.Price, sellOrder.Side);

            //Assert
            Assert.Equal(1, sut.TradeCount);
            Assert.Equal(2, sut.Orders.Count);
            Assert.Equal(1, sut.GetBuyOrderCount());
            Assert.Equal(1, sut.GetSellOrderCount());
        }

        [Fact]
        [Trait("StockMarketMatchEngine", "Open")]
        public void StockMarketMatchEngine_SeveralSellsWithDefferPriceEntersFirstBuyOrderEntersWithHigherAmount_MustExecute1TradeAndEnQueueFirstBuy()
        {
            //Arrange
            var sellOrder1 = new Order()
            {
              
                Price = 100,
                Amount = 5,
                Side = Side.Sell
            };
            var sellOrder2 = new Order()
            {
              
                Price = 110,
                Amount = 2,
                Side = Side.Sell
            };


            var buyOrder = new Order()
            {
               
                Price = 100,
                Amount = 6,
                Side = Side.Buy
            };

            sut.Trade(sellOrder1.Amount, sellOrder1.Price, sellOrder1.Side);
            sut.Trade(sellOrder2.Amount, sellOrder2.Price, sellOrder2.Side);

            //Action
            sut.Trade(buyOrder.Amount, buyOrder.Price, buyOrder.Side);

            //Assert
            Assert.Equal(1, sut.TradeCount);
            Assert.Equal(2, sut.Orders.Count);
            Assert.Equal(1, sut.GetBuyOrderCount());
            Assert.Equal(1, sut.GetSellOrderCount());
        }
    }
}