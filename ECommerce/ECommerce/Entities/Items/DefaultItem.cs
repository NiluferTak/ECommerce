using ECommerce.ConsoleCommands;

namespace ECommerce.Entities.Items
{
	public class DefaultItem : IItem
	{
		private List<IItem> vasItems = new List<IItem>();


		public decimal Price { get; set; }

		// no restrictions for these properties 
		public int Quantity { get; set; }
		public int SellerID { get; set; }
		public int CategoryID { get; set; }

		public int ItemID { get; set; }

		public DefaultItem(int itemID,decimal price, int quantity, int categoryId, int sellerId)
		{
			if (price <= 0 || quantity <= 0)
			{
				throw new ArgumentException("Price and quantity must be greater than zero.");
			}

			Price = price;
			Quantity = quantity;
			CategoryID = categoryId;
			SellerID = sellerId;
			ItemID = itemID;
		}

		//Factory design pattern
		public IItem CreateNewInstance()
		{
			return new DefaultItem(ItemID, Price, Quantity, CategoryID, SellerID);
		}

		// Composite design pattern for sub-items of defaultitems aka vasitems
		public CommandResult AddVasItem(VasItem vasItem)
		{
			if (vasItem.Price > this.Price)
			{
				return new CommandResult(false, "VasItem price cannot be higher than the DefaultItem price.");
			}

			if (vasItems.Count >= 3)
			{
				return new CommandResult(false, "Cannot add more than 3 VasItems to a DefaultItem.");
			}

			// Category restriction iff adding vasitem to a defaultitem
			if (CategoryID != 1001 && CategoryID != 3004)
			{
				return new CommandResult(false, "VasItems can only be added to DefaultItems in the Furniture (1001) or Electronics (3004) categories.");
			}

			vasItems.Add(vasItem);
			return new CommandResult(true, "VasItem added successfully.");
		}

		
		public void RemoveVasItem(VasItem vasItem)
		{
			vasItems.Remove(vasItem);
		}

		
		public decimal CalculatePrice()
		{
			decimal totalPrice = Price * Quantity;

			foreach (var vasItem in vasItems)
			{
				totalPrice += vasItem.Price * vasItem.Quantity;
			}

			return totalPrice;
		}

		public IReadOnlyList<IItem> GetVasItems()
		{
			return new List<IItem>(vasItems);
		}

	}

}
