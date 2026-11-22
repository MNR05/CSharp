using InventoryTracker;
using RateLimiter;

static void Check(bool condition, string message)
{
    if (!condition) throw new Exception("Test failed: " + message);
}

var stock = new StockItem("XYZ", 5);
stock.Receive(3);
Check(stock.Quantity == 8, "stock received");
Check(stock.Dispatch(6) && stock.Quantity == 2, "dispatch valid");
Check(!stock.Dispatch(3) && stock.Quantity == 2, "insufficient stock unchanged");

var window = new SlidingWindowLimiter(2, TimeSpan.FromSeconds(10));
var t = DateTimeOffset.Parse("2026-01-01T00:00:00Z");
Check(window.TryAcquire(t), "request one");
Check(window.TryAcquire(t), "request two");
Check(!window.TryAcquire(t.AddSeconds(1)), "third request blocked");
Check(window.TryAcquire(t.AddSeconds(10)), "expired request allowed");

Console.WriteLine("All C# smoke tests passed.");
