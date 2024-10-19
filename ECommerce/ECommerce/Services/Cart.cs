using ECommerce.Entities.Items;

namespace ECommerce.Services
{
	public class Cart
	{
		private readonly IPromotionManager _promotionManager;
		private readonly List<IItem> items = new List<IItem>();
		private const int MaxUniqueItems = 10;  
		private const int MaxTotalProducts = 30;  
		private const decimal MaxTotalAmount = 500_000m;
		public decimal TotalAmount { get;  set; }
		public decimal TotalDiscount { get;  set; }
		public int? AppliedPromotionId { get;  set; }

		public List<IItem> Items => items;

		

		public Cart(IPromotionManager promotionManager)
		{
			_promotionManager = promotionManager;
		}

		public void ResetCart()
		{
			items.Clear();
			TotalAmount = 0m;
			TotalDiscount = 0m;
			AppliedPromotionId = null;
		}

		public (bool result, string message) AddItem(IItem item)
		{
			
			int newTotalItemCount = items.Sum(i => i.Quantity) + item.Quantity;

			List<int> uniqueItems = items.Where(i => i is not VasItem).Select(i => i.ItemID).Distinct().ToList();
			bool itemTypeExists = uniqueItems.Contains(item.ItemID);

			
			if (newTotalItemCount > MaxTotalProducts)
			{
				return (false,$"The total number of products cannot exceed {MaxTotalProducts}");
			}

			if(uniqueItems.Count == MaxUniqueItems && !itemTypeExists)
			{
				return (false, $"The total number of unique products (excluding VasItems) cannot exceed {MaxUniqueItems}");
			}

			
			items.Add(item);
			CalculateNewTotal();
			_promotionManager.EvaluateAndApplyBestPromotion(this);

			

			if(TotalAmount > MaxTotalAmount)
			{
				items.Remove(item);
				CalculateNewTotal();
				_promotionManager.EvaluateAndApplyBestPromotion(this);

				return (false, $"The total amount (including vas items) of the Cart cannot exceed {MaxTotalAmount} TL");
			}

			
			return (true, "Item added successfully");


		}

		public (bool result, string message) RemoveItem(IItem item)
		{
			if (!items.Remove(item))
			{
				return (false, "Item not found in the cart.");
			}

			// Recalculate totals and promotions since an item object is removed
			CalculateNewTotal();
			_promotionManager.EvaluateAndApplyBestPromotion(this);

			return (true, "Item removed successfully.");
		}


		public void CalculateNewTotal()
		{
			TotalAmount = items.Sum(i => i.CalculatePrice());
			TotalDiscount = 0m;

		}



		

	}
}
