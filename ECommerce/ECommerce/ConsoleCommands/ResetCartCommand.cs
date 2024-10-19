using ECommerce.Services;

namespace ECommerce.ConsoleCommands
{
	public class ResetCartCommand : ICommand
	{
		public CommandResult Execute(Cart cart, Dictionary<string, object>? payload)
		{
			cart.ResetCart();
			return new CommandResult(true,"The cart is reset.");
		}
	}
}
