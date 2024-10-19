namespace ECommerce.Services
{
	public class TotalPricePromotion : IPromotion
	{
		
		private const int TotalPricePromotionID = 1232;

		private int _promotionID;

		public int PromotionID
		{
			get => _promotionID;
			set
			{
				if (value != TotalPricePromotionID)
				{
					throw new ArgumentException($"PromotionID for TotalPricePromotion must be {TotalPricePromotionID}.");
				}
				_promotionID = value;
			}
		}

		public TotalPricePromotion()
		{
			PromotionID = TotalPricePromotionID;
		}

		public void CalculatePromotion(Cart cart)
		{
			if (cart.TotalAmount >= 500 && cart.TotalAmount < 5000)
			{
				cart.TotalDiscount += 250; 
				cart.TotalAmount -= 250;
			}
			else if (cart.TotalAmount >= 5000 && cart.TotalAmount < 10000)
			{
				cart.TotalDiscount += 500; 
				cart.TotalAmount -= 500;
				
			}
			else if (cart.TotalAmount >= 10000 && cart.TotalAmount < 50000)
			{
				cart.TotalDiscount += 1000; 
				cart.TotalAmount -= 1000;
				
			}
			else if (cart.TotalAmount >= 50000)
			{
				cart.TotalDiscount += 2000; 
				cart.TotalAmount -= 2000;
				
			}

			cart.AppliedPromotionId = TotalPricePromotionID;
		}
	}
}
