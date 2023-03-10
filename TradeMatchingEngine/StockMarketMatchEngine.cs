namespace TradeMatchingEngine
{
    public partial class StockMarketMatchEngine : IStockMarketMatchEngine
    {
        #region PrivateField
        private readonly PriorityQueue<Order, Order> SellOrderQueue;
        private readonly PriorityQueue<Order, Order> BuyOrderQueue;
        private readonly Queue<Order> preOrderQueue;
        private StockMarketState state;
        public MarcketState State => state.Code;

        #endregion

        public StockMarketMatchEngine()
        {
            this.SellOrderQueue = new PriorityQueue<Order, Order>(new ModifiedOrderPriorityMin());
            this.BuyOrderQueue = new PriorityQueue<Order, Order>(new ModifiedOrderPriorityMax());
            this.Orders = new List<Order>();
            preOrderQueue = new Queue<Order>();
            state = new Closed(this);
        }

        #region Properties
        public List<Order> Orders { get; set; }
        public int TradeCount { get; set; }
        #endregion

        #region Public Method
        public void Trade(int amount, int price, Side side)
        {
            var order = new Order() { Price = price, Amount = amount, Side = side };
            switch (this.State)
            {
                case MarcketState.Close:
                    throw new Exception("This Stock Has been Closed");
                    

                case MarcketState.Open:
                    Orders.Add(order);
                    switch (order.Side)
                    {
                        case Side.Sell:
                            Sell(order);
                            break;
                        case Side.Buy:
                            Buy(order);
                            break;
                        default:
                            break;
                    }
                    break;

                case MarcketState.PreOpen:
                    preOrderQueue.Enqueue(order);
                    Orders.Add(order);
                    break;
                default:
                    break;
            }

        }
        public PriorityQueue<Order, Order> GetSellOrderQueue()
        {
            return SellOrderQueue;
        }

        public Queue<Order> GetPreOrderQueue()
        {
            return preOrderQueue;
        }

        public PriorityQueue<Order, Order> GetBuyOrderQueue()
        {
            return BuyOrderQueue;
        }

        public int GetBuyOrderCount()
        {
            return BuyOrderQueue.Count;
        }

        public int GetSellOrderCount()
        {
            return SellOrderQueue.Count;
        }

        public void Close()
        {
            state.Close();
        }

        public void PreOpen()
        {
            state.PreOpen();
        }

        public void Open()
        {
            state.Open();
        }


        #endregion

        #region Private Method
        private void Trade()
        {
            if (preOrderQueue.Count > 0)
            {
                int cnt = preOrderQueue.Count;
                for (int i = 0; i < cnt; i++)
                {
                    var preOrderdOrder = (preOrderQueue.Peek());

                    if (preOrderdOrder.Side == Side.Buy)
                    {
                        Buy(preOrderdOrder);
                    }
                    else if (preOrderdOrder.Side == Side.Sell)
                    {
                        Sell(preOrderdOrder);
                    }

                    preOrderQueue.Dequeue();
                }
            }
        }

        private void open()
        {
            this.Trade();
        }

        private void close()
        {
            ClearQueues();
        }

        private void preOpen()
        {

        }

        private void ClearQueues()
        {
            Orders.Clear();
            preOrderQueue.Clear();
            BuyOrderQueue.Clear();
            SellOrderQueue.Clear();
        }

        private void Buy(Order order)
        {
            var sellAvailble = SellOrderQueue.Count;
            if (sellAvailble > 0)
            {
                var sell = SellOrderQueue.Peek();

                if (order.Price >= sell.Price)
                {
                    if (order.Amount == sell.Amount)
                    {
                        TradeCount++;
                        SellOrderQueue.Dequeue();
                        Orders.Remove(order);
                        Orders.Remove(sell);
                    }
                    if (order.Amount > sell.Amount)
                    {
                        TradeCount++;
                        SellOrderQueue.Dequeue();

                        Orders.Remove(sell);

                        order.Amount = order.Amount - sell.Amount;
                        Orders.Where(o => o.Id == order.Id).First().Amount = order.Amount;

                        while (order.Amount > 0)
                        {
                            if (SellOrderQueue.Count > 0)
                            {

                                var nextSell = SellOrderQueue.Peek();
                                if (nextSell.Price > order.Price)
                                {
                                    if (BuyOrderQueue.Count > 0)
                                    {
                                        if (BuyOrderQueue.Peek().Id == order.Id)
                                        {
                                            BuyOrderQueue.Dequeue();
                                            BuyOrderQueue.Enqueue(order, order);
                                        }
                                        else
                                        {
                                            BuyOrderQueue.Enqueue(order, order);
                                        }
                                    }
                                    else
                                    {
                                        BuyOrderQueue.Enqueue(order, order);
                                    }
                                    break;
                                }
                                if (order.Amount == nextSell.Amount)
                                {
                                    TradeCount++;
                                    Orders.Remove(nextSell);
                                    Orders.Remove(order);
                                    SellOrderQueue.Dequeue();
                                    break;
                                }

                                else if (order.Amount < nextSell.Amount)
                                {
                                    TradeCount++;
                                    Orders.Remove(order);
                                    nextSell.Amount = nextSell.Amount - order.Amount;
                                    var sellUpdate = Orders.Where(o => o.Id == nextSell.Id).First();
                                    sellUpdate.Amount = nextSell.Amount - order.Amount;
                                    order.Amount = 0;
                                    SellOrderQueue.Dequeue();
                                    SellOrderQueue.Enqueue(nextSell, nextSell);
                                    if (BuyOrderQueue.Count > 0)
                                    {
                                        if (BuyOrderQueue.Peek().Id == order.Id)
                                        {
                                            BuyOrderQueue.Dequeue();
                                        }
                                    }

                                }

                                else if (order.Amount > nextSell.Amount)
                                {
                                    TradeCount++;
                                    Orders.Remove(nextSell);
                                    SellOrderQueue.Dequeue();
                                    order.Amount = order.Amount - nextSell.Amount;
                                    if (BuyOrderQueue.Count > 0)
                                    {
                                        if (BuyOrderQueue.Peek().Id == order.Id)
                                        {
                                            BuyOrderQueue.Dequeue();
                                            BuyOrderQueue.Enqueue(order, order);
                                        }
                                        else
                                        {
                                            BuyOrderQueue.Enqueue(order, order);
                                        }
                                    }
                                    else
                                    {
                                        BuyOrderQueue.Enqueue(order, order);
                                    }

                                }
                            }
                            else
                            {
                                if (BuyOrderQueue.Count > 0)
                                {
                                    if (BuyOrderQueue.Peek().Id == order.Id)
                                    {
                                        BuyOrderQueue.Dequeue();
                                        BuyOrderQueue.Enqueue(order, order);
                                    }
                                    else
                                    {
                                        BuyOrderQueue.Enqueue(order, order);
                                    }
                                }
                                else
                                {
                                    BuyOrderQueue.Enqueue(order, order);
                                }
                                break;
                            }
                        }
                    }

                    else if (order.Amount < sell.Amount)
                    {
                        TradeCount++;
                        Orders.Remove(order);
                        sell.Amount = sell.Amount - order.Amount;
                        var sellUpdate = Orders.Where(o => o.Id == sell.Id).First();
                        sellUpdate.Amount = sell.Amount - order.Amount;

                        SellOrderQueue.Dequeue();
                        SellOrderQueue.Enqueue(sell, sell);

                    }
                }
                else
                {

                    BuyOrderQueue.Enqueue(order, order);

                }
            }
            else
            {
                BuyOrderQueue.Enqueue(order, order);
            }
        }
        private void Sell(Order order)
        {
            var buyAvailble = BuyOrderQueue.Count;

            if (buyAvailble > 0)
            {

                var buy = BuyOrderQueue.Peek();
                if (order.Price <= buy.Price)
                {

                    if (order.Amount == buy.Amount)
                    {


                        TradeCount++;
                        Orders.Remove(buy);
                        Orders.Remove(order);
                        BuyOrderQueue.Dequeue();

                    }
                    if (order.Amount > buy.Amount)
                    {
                        TradeCount++;
                        BuyOrderQueue.Dequeue();
                        Orders.Remove(buy);
                        order.Amount = order.Amount - buy.Amount;
                        Orders.Where(o => o.Id == order.Id).First().Amount = order.Amount;

                        while (order.Amount > 0)
                        {
                            if (BuyOrderQueue.Count > 0)
                            {

                                var nextBuy = BuyOrderQueue.Peek();
                                if (nextBuy.Price < order.Price)
                                {
                                    if (SellOrderQueue.Count > 0)
                                    {
                                        if (SellOrderQueue.Peek().Id == order.Id)
                                        {
                                            SellOrderQueue.Dequeue();
                                            SellOrderQueue.Enqueue(order, order);
                                        }
                                        else
                                        {
                                            SellOrderQueue.Enqueue(order, order);
                                        }

                                    }
                                    else
                                    {
                                        SellOrderQueue.Enqueue(order, order);
                                    }
                                    break;
                                }
                                if (order.Amount == nextBuy.Amount)
                                {
                                    TradeCount++;
                                    Orders.Remove(nextBuy);
                                    Orders.Remove(order);
                                    BuyOrderQueue.Dequeue();
                                    break;
                                }

                                else if (order.Amount < nextBuy.Amount)
                                {
                                    TradeCount++;
                                    Orders.Remove(order);
                                    nextBuy.Amount = nextBuy.Amount - order.Amount;
                                    var buyUpdate = Orders.Where(o => o.Id == nextBuy.Id).First();
                                    buyUpdate.Amount = nextBuy.Amount;
                                    order.Amount = 0;
                                    BuyOrderQueue.Dequeue();
                                    BuyOrderQueue.Enqueue(nextBuy, nextBuy);


                                }

                                else if (order.Amount > nextBuy.Amount)
                                {
                                    TradeCount++;
                                    Orders.Remove(nextBuy);
                                    BuyOrderQueue.Dequeue();
                                    order.Amount = order.Amount - nextBuy.Amount;

                                    if (SellOrderQueue.Count > 0)
                                    {
                                        if (SellOrderQueue.Peek().Id == order.Id)
                                        {
                                            SellOrderQueue.Dequeue();
                                            SellOrderQueue.Enqueue(order, order);
                                        }
                                        else
                                        {
                                            SellOrderQueue.Enqueue(order, order);
                                        }
                                    }
                                    else
                                    {
                                        SellOrderQueue.Enqueue(order, order);
                                    }

                                }
                            }
                            else
                            {
                                if (SellOrderQueue.Count > 0)
                                {
                                    if (SellOrderQueue.Peek().Id == order.Id)
                                    {
                                        SellOrderQueue.Dequeue();
                                        SellOrderQueue.Enqueue(order, order);
                                    }
                                    else
                                    {
                                        SellOrderQueue.Enqueue(order, order);
                                    }
                                }
                                else
                                {
                                    SellOrderQueue.Enqueue(order, order);
                                }
                                break;
                            }
                        }
                    }
                    else if (order.Amount < buy.Amount)
                    {
                        TradeCount++;
                        Orders.Remove(order);
                        buy.Amount = buy.Amount - order.Amount;
                        var sellUpdate = Orders.Where(o => o.Id == buy.Id).First();
                        sellUpdate.Amount = buy.Amount - order.Amount;


                        BuyOrderQueue.Dequeue();
                        BuyOrderQueue.Enqueue(buy, buy);
                        if (SellOrderQueue.Count > 0)
                        {
                            if (SellOrderQueue.Peek().Id == order.Id)
                            {
                                SellOrderQueue.Dequeue();
                            }
                        }
                    }


                }
                else
                {
                    SellOrderQueue.Enqueue(order, order);
                }
            }
            else
            {
                SellOrderQueue.Enqueue(order, order);
            }
        }
        #endregion


    }
}
