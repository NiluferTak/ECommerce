using NUnit.Framework;
using ECommerce.Entities.Items;
using ECommerce.Services;


namespace NUnitTests.Tests
{
    [TestFixture]
    public class PromotionTests
    {
        private Cart _cart;

        [SetUp]
        public void SetUp()
        {
            var promotionManager = new PromotionManager();
            _cart = new Cart(promotionManager);
        }

        [Test]
        [TestCase(1, 200, 2, 3003, 1, 40 ,9909)]// total 0 , sameseller 40,category 20
		[TestCase(2, 500, 1, 3004, 1, 250,1232)]// total 250 , sameseller 50,category 0
		[TestCase(2, 500, 1, 3003, 1, 250, 1232)]// total 250 , sameseller 50,category 25
		public void SameSellerAndTotalPricePromotion_ShouldApplyDiscount_ForOneItem(int itemID, decimal price, int quantity, int categoryId, int sellerId,decimal expectedDiscount , int expectedPromotionId)
        {
             
            var item1 = new DefaultItem( itemID,  price,  quantity,  categoryId, sellerId);
            _cart.AddItem(item1);
            
            Assert.That(expectedPromotionId == _cart.AppliedPromotionId && expectedDiscount == _cart.TotalDiscount);
        }

        [Test]
        public void TotalPricePromotion_ShouldApplyDiscount_BasedOnTotalAmount()
        {
            var item1 = new DefaultItem(1, 300, 1, 3003, 1);
            var item2 = new DefaultItem(2, 300, 1, 3003, 1);
            _cart.AddItem(item1);
            _cart.AddItem(item2);
            Assert.That(250m == _cart.TotalDiscount);
            Assert.That(350m == _cart.TotalAmount);
            Assert.That(1232 == _cart.AppliedPromotionId);
        }

        [Test]
        public void SameSellerPromotion_ShouldApplyDiscount_WhenAllItemsFromSameSeller()
        {

            var item1 = new DefaultItem(1, 100, 2, 3001, 1);
            var item2 = new DefaultItem(2, 50, 1, 3001, 1);
            _cart.AddItem(item1);
            _cart.AddItem(item2);

            Assert.That(25m == _cart.TotalDiscount);
            Assert.That(225m == _cart.TotalAmount);
            Assert.That(9909 == _cart.AppliedPromotionId);
        }

        [Test]
        public void NoPromotion_IsEligible()
        {
            
            var item1 = new DefaultItem(1, 100, 1, 3001, 1);
            var item2 = new DefaultItem(2, 200, 1, 3005, 2);
            _cart.AddItem(item1);
            _cart.AddItem(item2);

            Assert.That(0m == _cart.TotalDiscount);
            Assert.That(300m == _cart.TotalAmount);
            Assert.That(null == _cart.AppliedPromotionId);
        }

        [Test]
        public void PromotionCalculations_WithVasItems()
        {
			var defaultItem = new DefaultItem(1, 1000m, 1, 3004, 123); 

			var vasItem = new VasItem(2, 100m, 1, 3242, 5003); 
			defaultItem.AddVasItem(vasItem);

			_cart.AddItem(defaultItem);

			Assert.That(_cart.TotalAmount, Is.EqualTo(850)); 
			Assert.That(_cart.TotalDiscount, Is.EqualTo(250)); 
			Assert.That(1232 == _cart.AppliedPromotionId);
		}

        [Test]
        public void PromotionCalculations_WithDigitalItems_ShouldApplyDiscount_ForSameSellerPromotion()
        {
            var digitalItem = new DigitalItem(8, 2000m, 1, 1);
			var defaultItem = new DefaultItem(1, 1000m, 1, 3004, 1);
			var vasItem = new VasItem(2, 100m, 1, 3242, 5003);
			defaultItem.AddVasItem(vasItem);
			_cart.AddItem(digitalItem);
			_cart.AddItem(defaultItem);
			//sameseller 310 , total 250  ,category 0

			Assert.That(_cart.TotalAmount, Is.EqualTo(2790));
			Assert.That(_cart.TotalDiscount, Is.EqualTo(310));
			Assert.That(9909 == _cart.AppliedPromotionId);

		}
	}

    
}
