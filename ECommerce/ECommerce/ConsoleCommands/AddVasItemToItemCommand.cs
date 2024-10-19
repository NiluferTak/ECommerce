using ECommerce.Entities.Items;
using ECommerce.Services;

namespace ECommerce.ConsoleCommands
{
	public class AddVasItemToItemCommand : ICommand
	{
		public CommandResult Execute(Cart cart, Dictionary<string, object>? payload)
		{
			var itemId = Convert.ToInt32(payload["itemId"]);
			var categoryId = Convert.ToInt32(payload["vasCategoryId"]);
			var sellerId = Convert.ToInt32(payload["vasSellerId"]);
			var price = Convert.ToDecimal(payload["price"]);
			var quantity = Convert.ToInt32(payload["quantity"]);

			var defaultItem = cart.Items.FirstOrDefault(item => item.ItemID == itemId) as DefaultItem;
			if(defaultItem == null)
			{
				return new CommandResult(false, "The item not found");
			}
			var vasitem = new VasItem(itemId,price, quantity, categoryId,sellerId);

			return defaultItem.AddVasItem(vasitem);

		}
	}
}
