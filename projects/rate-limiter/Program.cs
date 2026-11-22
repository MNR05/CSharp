namespace RateLimiter;

public sealed class SlidingWindowLimiter
{
    private readonly int _maxRequests;
    private readonly TimeSpan _window;
    private readonly Queue<DateTimeOffset> _timestamps = new();

    public SlidingWindowLimiter(int maxRequests, TimeSpan window)
    {
        if (maxRequests < 1) throw new ArgumentOutOfRangeException(nameof(maxRequests));
        if (window <= TimeSpan.Zero) throw new ArgumentOutOfRangeException(nameof(window));
        _maxRequests = maxRequests;
        _window = window;
    }

    public bool TryAcquire(DateTimeOffset now)
    {
        while (_timestamps.Count > 0 && now - _timestamps.Peek() >= _window)
            _timestamps.Dequeue();
        if (_timestamps.Count >= _maxRequests) return false;
        _timestamps.Enqueue(now);
        return true;
    }
}

public static class Program
{
    public static void Main()
    {
        var limiter = new SlidingWindowLimiter(2, TimeSpan.FromSeconds(10));
        var now = DateTimeOffset.UtcNow;
        Console.WriteLine($"Request 1: {limiter.TryAcquire(now)}");
        Console.WriteLine($"Request 2: {limiter.TryAcquire(now)}");
        Console.WriteLine($"Request 3: {limiter.TryAcquire(now)}");
        Console.WriteLine($"After 10 seconds: {limiter.TryAcquire(now.AddSeconds(10))}");
    }
}
