using ECommerce.Entities.Items;
using ECommerce.Services;

namespace ECommerce.ConsoleCommands
{
	public class AddItemCommand : ICommand
	{
		public CommandResult Execute(Cart cart, Dictionary<string, object>? payload)
		{
			var itemId = Convert.ToInt32(payload["itemId"]);
			var categoryId = Convert.ToInt32(payload["categoryId"]);
			var sellerId = Convert.ToInt32(payload["sellerId"]);
			var price = Convert.ToDecimal(payload["price"]);
			var quantity = Convert.ToInt32(payload["quantity"]);

			IItem item;
			if (categoryId == 7889) 
			{
				item = new DigitalItem(itemId, price, quantity, sellerId);
			}
			else 
			{
				item = new DefaultItem(itemId, price, quantity, categoryId, sellerId);
			}

			var (result, message) = cart.AddItem(item);
			CommandResult c = new CommandResult(result, message);
			return c;


		}
	}
}
