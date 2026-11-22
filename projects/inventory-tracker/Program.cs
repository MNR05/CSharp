namespace InventoryTracker;

public sealed class StockItem
{
    public string Sku { get; }
    public int Quantity { get; private set; }

    public StockItem(string sku, int initialQuantity)
    {
        if (string.IsNullOrWhiteSpace(sku)) throw new ArgumentException("SKU is required", nameof(sku));
        if (initialQuantity < 0) throw new ArgumentOutOfRangeException(nameof(initialQuantity));
        Sku = sku.Trim();
        Quantity = initialQuantity;
    }

    public void Receive(int quantity)
    {
        if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity));
        Quantity = checked(Quantity + quantity);
    }

    public bool Dispatch(int quantity)
    {
        if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity));
        if (quantity > Quantity) return false;
        Quantity -= quantity;
        return true;
    }
}

public static class Program
{
    public static void Main()
    {
        var item = new StockItem("SKU-100", 10);
        item.Receive(5);
        Console.WriteLine($"Stock after delivery: {item.Quantity}");
        Console.WriteLine($"Dispatch 7: {item.Dispatch(7)}");
        Console.WriteLine($"Remaining stock: {item.Quantity}");
    }
}
