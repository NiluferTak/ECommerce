namespace ECommerce.Services
{
	public interface IPromotion
	{
		public int PromotionID { get; }

		public void CalculatePromotion(Cart cart);

	}
}
