# Sliding Window Rate Limiter

**Problem:** A service needs to reject requests after a threshold within a time window.

**Solution:** A queue of timestamps implements a sliding-window policy with time and space proportional to retained requests.

```bash
dotnet run --project RateLimiter.csproj
```

**Limitations:** Single-threaded, in-memory demonstration. For real distributed APIs use thread-safe coordination and a shared store such as Redis.
