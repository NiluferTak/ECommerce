using Newtonsoft.Json;
using ECommerce.Entities.Items;
using ECommerce.Services;

namespace ECommerce.ConsoleCommands
{
	public class DisplayCartCommand : ICommand
	{
		public CommandResult Execute(Cart cart, Dictionary<string, object>? payload)
		{
			var cartInfo = new
			{
				items = cart.Items.Select(i => new
				{
					itemId = i.ItemID,
					categoryId = i.CategoryID,
					sellerId = i.SellerID,
					price = i.Price,
					quantity = i.Quantity,
					vasItems = (i as DefaultItem)?.GetVasItems().Select(vas => new
					{
						vasItemId = vas.ItemID,
						vasCategoryId = vas.CategoryID,
						vasSellerId = vas.SellerID,
						price = vas.Price,
						quantity = vas.Quantity
					}).ToList() 
				}).ToList(), 
				totalAmount = cart.TotalAmount,
				appliedPromotionId = cart.AppliedPromotionId,
				totalDiscount = cart.TotalDiscount
			};

			
			
			return new CommandResult(true, cartInfo);
		}
	}
}
