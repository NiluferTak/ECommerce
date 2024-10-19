using ECommerce.Entities.Items;

namespace ECommerce.Services
{
	public class SameSellerPromotion : IPromotion
	{
		private readonly decimal _discountPercentage = 0.10m;
		private const int SameSellerPromotionID = 9909;

		private int _promotionID;

		public int PromotionID
		{
			get => _promotionID;
			set
			{
				if (value != SameSellerPromotionID)
				{
					throw new ArgumentException($"PromotionID for SameSellerPromotion must be {SameSellerPromotionID}.");
				}
				_promotionID = value;
			}
		}

		public SameSellerPromotion()
		{
			PromotionID = SameSellerPromotionID;
		}

		public void  CalculatePromotion(Cart cart)
		{
			var sameSellerItems = cart.Items
			.Where(i => i.SellerID == cart.Items.First().SellerID && i is not VasItem)
			.ToList();

			if (sameSellerItems.Count == cart.Items.Count(i => i is not VasItem))
			{
				decimal discount = cart.TotalAmount * _discountPercentage; 
				cart.TotalDiscount += discount;
				cart.TotalAmount -= discount;
				cart.AppliedPromotionId = 9909; 
			}

		}
	}
}
