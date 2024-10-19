using ECommerce.Services;

namespace ECommerce.ConsoleCommands
{
	public interface ICommand
	{
		CommandResult Execute(Cart cart, Dictionary<string, object>? payload);
	}
}
