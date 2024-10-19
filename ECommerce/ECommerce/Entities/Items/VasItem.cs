namespace ECommerce.Entities.Items
{
	public class VasItem : IItem
	{
		private const int MaxQuantity = 5;
		private const int VasItemCategoryID = 3242;
		private const int VasItemSellerID = 5003;

		private int _categoryID;
		private int _quantity;
		private int _sellerID;

		public int ItemID { get; set; }

		public decimal Price { get; set; }
		
		public int SellerID
		{
			get => _sellerID;
			set
			{
				if (value != VasItemSellerID)
				{
					throw new ArgumentException($"SellerID for VasItem must be {VasItemSellerID}.");
				}
				_sellerID = value;
			}
		}
		public int Quantity
		{
			get => _quantity;
			set
			{
				if (value > MaxQuantity)
				{
					throw new ArgumentException($"Quantity cannot exceed {MaxQuantity} for VasItem.");
				}
				_quantity = value;
			}
		}

		public int CategoryID
		{
			get => _categoryID;
			private set
			{
				if (value != VasItemCategoryID)
				{
					throw new ArgumentException($"CategoryID for VasItem must be {VasItemCategoryID}.");
				}
				_categoryID = value;
			}
		}

		public decimal CalculatePrice()
		{
			return Price * Quantity;
		}

		public VasItem(int itemID, decimal price, int quantity, int categoryID,int sellerID)
		{
			if (price <= 0 || quantity <= 0)
			{
				throw new ArgumentException("Price and quantity must be greater than zero.");
			}

			Price = price;
			Quantity = quantity;
			CategoryID = categoryID; 
			SellerID = sellerID; 
			ItemID	= itemID;
		}

		//Factory design pattern
		public IItem CreateNewInstance()
		{
			return new VasItem(ItemID, Price, Quantity, VasItemCategoryID,VasItemSellerID);
		}
	}
}
