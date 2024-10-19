using ECommerce.Entities.Items;

namespace ECommerce.Services
{
	public class CategoryPromotion : IPromotion
	{
		private readonly int _itemCategoryId = 3003;
		private readonly decimal _discountPercentage = 0.05m; 

		private const int CategoryPromotionID = 5676;

		private int _promotionID;

		public int PromotionID
		{
			get => _promotionID;
			set
			{
				if (value != CategoryPromotionID)
				{
					throw new ArgumentException($"PromotionID for CategoryPromotion must be {CategoryPromotionID}.");
				}
				_promotionID = value;
			}
		}

        public CategoryPromotion()
        {
            PromotionID = CategoryPromotionID;
        }

		public void CalculatePromotion(Cart cart)
		{
			var applicableItems = cart.Items.Where(i => i.CategoryID == _itemCategoryId).ToList(); 

            foreach (var item in applicableItems)
            {
				decimal discount = item.CalculatePrice() * _discountPercentage;
				cart.TotalDiscount += discount;
				cart.TotalAmount -= discount;
			}

			if(applicableItems.Count > 0)
			{
				cart.AppliedPromotionId = _promotionID;
			}

        }
    }
}
