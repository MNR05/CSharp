# Inventory Tracker

**Problem:** Stock movements need consistent validation to prevent negative balances.

**Solution:** Encapsulated stock item with receive and dispatch operations, non-negative stock, checked overflow and explicit return values for insufficient quantity.

```bash
dotnet run --project InventoryTracker.csproj
```

This is a domain-logic demo, not a database-backed multi-user inventory system. Concurrency and persistence would be required for production.
