using ECommerce.Services;

namespace ECommerce.ConsoleCommands
{
	public class RemoveItemCommand : ICommand
	{
		public CommandResult Execute(Cart cart, Dictionary<string, object>? payload)
		{
			var itemId = Convert.ToInt32(payload["itemId"]);
			var item = cart.Items.FirstOrDefault(item => item.ItemID == itemId);

			if(item == null)
			{
				return new CommandResult(false, "The item not found");
			}

			var res = cart.RemoveItem(item);
			return new CommandResult(res.result, res.message);
		}
	}
}
