namespace ECommerce.Entities.Items
{
	public class DigitalItem : IItem
	{
		
		private const int MaxQuantity = 5;
		private const int DigitalCategoryID = 7889;

		private int _categoryID;
		private int _quantity;

		public int ItemID { get; set; }
		public int SellerID { get; set; }
		public decimal Price { get; set; }
		public int Quantity
		{
			get => _quantity;
			set
			{
				if (value > MaxQuantity)
				{
					throw new ArgumentException($"Quantity cannot exceed {MaxQuantity} for DigitalItem.");
				}
				_quantity = value;
			}
		}
		
		public int CategoryID
		{
			get => _categoryID;
			private set
			{
				if (value != DigitalCategoryID)
				{
					throw new ArgumentException($"CategoryID for DigitalItem must be {DigitalCategoryID}.");
				}
				_categoryID = value;
			}
		}

		
		public DigitalItem(int itemID, decimal price, int quantity, int sellerID)
		{
			if (price <= 0 || quantity <= 0)
			{
				throw new ArgumentException("Price and quantity must be greater than zero.");
			}

			Price = price;
			SellerID = sellerID;
			Quantity = quantity; 
			CategoryID = DigitalCategoryID;  
			ItemID	= itemID;
		}

		public IItem CreateNewInstance()
		{
			return new DigitalItem(ItemID, Price, Quantity, SellerID);
		} 

		public decimal CalculatePrice()
		{
			return Price * Quantity;
		}
	}


}
