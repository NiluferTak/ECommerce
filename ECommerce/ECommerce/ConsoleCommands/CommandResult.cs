namespace ECommerce.ConsoleCommands
{
	public class CommandResult
	{
		public bool Result { get; private set; }
		public object Message { get; private set; }

		public CommandResult(bool result, object message)
		{
			Result = result;
			Message = message;
		}
	}
}
