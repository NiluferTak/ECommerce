namespace ECommerce.Services
{
	public class PromotionManager : IPromotionManager
	{
		public IEnumerable<IPromotion> GetAvailablePromotions(Cart cart)
		{
			List<IPromotion> promotions = new List<IPromotion>
			{
				new SameSellerPromotion(),
				new CategoryPromotion(),
				new TotalPricePromotion()
			};

			return promotions;
		}

		//using strategy pattern to get the best promotion option , each promotion is interchangeable now.
		public void EvaluateAndApplyBestPromotion(Cart cart)
		{
			IEnumerable<IPromotion> promotions = GetAvailablePromotions(cart);
			decimal maxDiscount = 0;
			IPromotion bestPromotion = null;
			var promotionManager = new PromotionManager();
			var originalItems = cart.Items.Select(item => item.CreateNewInstance()).ToList(); 
			decimal originalTotalAmount = cart.TotalAmount;
			decimal originalTotalDiscount = cart.TotalDiscount;
			int? originalPromotionID = cart.AppliedPromotionId;

			foreach (var promotion in promotions)
			{
				promotion.CalculatePromotion(cart);
				decimal discount = cart.TotalDiscount;


				if (discount > maxDiscount)
				{
					maxDiscount = discount;
					bestPromotion = promotion;
				}

				if(discount == 0)
					cart.AppliedPromotionId = null;
				else
					cart.AppliedPromotionId = originalPromotionID;
				
				cart.TotalAmount = originalTotalAmount;
				cart.TotalDiscount = originalTotalDiscount;
				cart.Items.Clear();
				cart.Items.AddRange(originalItems.Select(item => item.CreateNewInstance()));
			}

			if (bestPromotion != null)
			{
				bestPromotion.CalculatePromotion(cart);
			}
		}
	}
}
