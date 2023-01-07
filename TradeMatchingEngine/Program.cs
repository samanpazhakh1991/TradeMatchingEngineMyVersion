using TradeMatchingEngine;

//var stateNotifier = new StateNotification();
//var stateController = new StateController(new StockMarketMatchEngine());
//stateNotifier.MarketStateChanged += stateController.MarketState_HasChanged;

//var startTimeSpan = TimeSpan.Zero;
//var periodTimeSpan = TimeSpan.FromSeconds(10);

//var timer = new System.Threading.Timer((e) =>
//{
//    stateNotifier.ChangeStateMarket();
//}, null, startTimeSpan, periodTimeSpan);
var engine=new StockMarketMatchEngine();

engine.PreOpen();
engine.Open();
engine.Trade(50, 100, Side.Buy);
var order=new Order() { Amount=10,Price=100,Side=Side.Sell};

Console.WriteLine("Hello World");
