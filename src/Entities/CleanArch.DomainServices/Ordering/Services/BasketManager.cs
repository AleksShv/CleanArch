using CleanArch.Entities;

namespace CleanArch.DomainServices.Ordering.Services;

public static class BasketManager
{
    public static void AddItem(this Basket basket, Guid productId, int quantity)
    {
        var item = basket.Items.Find(item => item.ProductId == productId);

        if (item is null)
        {
            item = new BasketItem
            {
                ProductId = productId,
                Quantity = quantity
            };
            basket.Items.Add(item);
        }

        else
        {
            item.Quantity = quantity;
        }
    }

    public static void RemoveItem(this Basket basket, Guid productId)
    {
        basket.Items.RemoveAll(item => item.ProductId == productId);
    }
}
