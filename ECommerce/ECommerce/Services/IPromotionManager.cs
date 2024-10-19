namespace ECommerce.Services
{
	public interface IPromotionManager
	{
		IEnumerable<IPromotion> GetAvailablePromotions(Cart cart);
		void EvaluateAndApplyBestPromotion(Cart cart);
	}
}
