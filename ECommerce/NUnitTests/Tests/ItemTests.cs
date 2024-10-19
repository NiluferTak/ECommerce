using NUnit.Framework;
using ECommerce.Entities.Items;




namespace NUnitTests.Tests
{
    [TestFixture]
    public class ItemTests
    {

        [Test]
        [TestCase(1001, 100, 50, 1, 150)]
        [TestCase(3004, 100, 20, 1, 120)]
        public void AddVasItem_ShouldAddVasItem_WhenValid(int categoryId, decimal defaultItemPrice, decimal vasItemPrice, int vasItemQuantity, decimal expectedPrice)
        {
            var defaultItem = new DefaultItem(1, defaultItemPrice, 1, categoryId, 1);
            var vasItem = new VasItem(2, vasItemPrice, vasItemQuantity,3242,5003);

            defaultItem.AddVasItem(vasItem);
            var totalPrice = defaultItem.CalculatePrice();

            Assert.That(expectedPrice, Is.EqualTo(totalPrice));
        }


        [Test]
        [TestCase(1001, 100, 120, 1)]
        public void AddVasItem_ShouldThrowException_WhenVasItemPriceHigherThanDefaultItem(int categoryId, decimal defaultItemPrice, decimal vasItemPrice, int vasItemQuantity)
        {
         
            var defaultItem = new DefaultItem(1, defaultItemPrice, 1, categoryId, 1);
            var vasItem = new VasItem(2, vasItemPrice, vasItemQuantity, 3242, 5003);

            var res = defaultItem.AddVasItem(vasItem);
			Assert.That(res.Result, Is.False);
			Assert.That(res.Message, Is.EqualTo("VasItem price cannot be higher than the DefaultItem price."));

		}

           
		[Test]
        [TestCase(5000, 100, 50, 1)] 
        public void AddVasItem_ShouldThrowException_WhenCategoryIsInvalid(int categoryId, decimal defaultItemPrice, decimal vasItemPrice, int vasItemQuantity)
        {
            var defaultItem = new DefaultItem(1, defaultItemPrice, 1, categoryId, 1);
            var vasItem = new VasItem(2, vasItemPrice, vasItemQuantity, 3242, 5003);

			var res = defaultItem.AddVasItem(vasItem);
			Assert.That(res.Result, Is.False);
			Assert.That(res.Message, Is.EqualTo("VasItems can only be added to DefaultItems in the Furniture (1001) or Electronics (3004) categories."));
			
        }


        [Test]
        [TestCase(1001, 100, 20, 1, 30, 1, 150)]
        public void AddVasItem_ShouldCalculatePriceCorrectly_WithMultipleVasItems(int categoryId, decimal defaultItemPrice, decimal vasItem1Price, int vasItem1Quantity, decimal vasItem2Price, int vasItem2Quantity, decimal expectedPrice)
        {
            var defaultItem = new DefaultItem(1, defaultItemPrice, 1, categoryId, 1);
            var vasItem1 = new VasItem(2, vasItem1Price, vasItem1Quantity, 3242, 5003);
            var vasItem2 = new VasItem(3, vasItem2Price, vasItem2Quantity, 3242, 5003);

            defaultItem.AddVasItem(vasItem1);
            defaultItem.AddVasItem(vasItem2);
            var totalPrice = defaultItem.CalculatePrice();

            Assert.That(expectedPrice, Is.EqualTo(totalPrice));
        }
    }
}
