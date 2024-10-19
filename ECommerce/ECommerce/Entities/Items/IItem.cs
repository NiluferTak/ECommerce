namespace ECommerce.Entities.Items
{
	public interface IItem
	{
		public int ItemID { get; set; }
		public decimal Price { get; set; }
		public int Quantity { get; }
		public int SellerID { get; }
		public int CategoryID { get;  }

		IItem CreateNewInstance();

		decimal CalculatePrice();
	}





}
