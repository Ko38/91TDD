using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;

namespace StaticFactoryCoupling
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestShippingByStore_Seven_1_Order_Family_2_Orders()
        {
            //arrange
            var target = new ShipService();

            var orders = new List<Order>
            {
                new Order{ StoreType= StoreType.Seven, Id=1},
                new Order{ StoreType= StoreType.Family, Id=2},
                new Order{ StoreType= StoreType.Family, Id=3},
            };

			IStoreService sevenService = Substitute.For<IStoreService>();
			IStoreService familyService = Substitute.For<IStoreService>();
			//var simpleFactory = new MockSimpleFactory
			//{
			//	SevenService = sevenService,
			//	FamilyService = familyService
			//};
			SimpleFactory.SetSevenService(sevenService);
			SimpleFactory.SetFamilyService(familyService);

			//act
			target.ShippingByStore(orders);

			//todo, assert
			//ShipService should invoke SevenService once and FamilyService twice
			sevenService.Received(1).Ship(Arg.Is<Order>(x => x.StoreType == StoreType.Seven));
			familyService.Received(2).Ship(Arg.Is<Order>(x => x.StoreType == StoreType.Family));
		}
    }

    public enum StoreType
    {
        /// <summary>
        /// 7-11
        /// </summary>
        Seven = 0,

        /// <summary>
        /// 全家
        /// </summary>
        Family = 1
    }

    public class ShipService
    {
        public void ShippingByStore(List<Order> orders)
        {
            foreach (var order in orders)
            {
                // simple factory pattern implementation
                IStoreService storeService = SimpleFactory.GetStoreService(order);
                storeService.Ship(order);
            }
        }
    }

	internal class MockSimpleFactory : SimpleFactory
	{
		internal IStoreService SevenService
		{
			set
			{
				sevenService = value;
			}
		}

		internal IStoreService FamilyService
		{
			set
			{
				familyService = value;
			}
		}
	}

    internal class SimpleFactory
    {
		protected static IStoreService sevenService = new SevenService();
		protected static IStoreService familyService = new FamilyService();
		internal static void SetSevenService(IStoreService service)
		{
			sevenService = service;
		}

		internal static void SetFamilyService(IStoreService service)
		{
			familyService = service;
		}
		internal static IStoreService GetStoreService(Order order)
        {
            if (order.StoreType == StoreType.Family)
            {
                return familyService;
            }
            else
            {
                return sevenService;
            }
        }
    }

    public class Order
    {
        public StoreType StoreType { get; set; }
        public int Id { get; set; }
        public int Amount { get; set; }
    }

    internal class SevenService : IStoreService
    {
        public void Ship(Order order)
        {
            // seven web service
            var client = new HttpClient();
            client.PostAsync("http://api.seven.com/Order", order, new JsonMediaTypeFormatter());
        }
    }

    internal class FamilyService : IStoreService
    {
        public void Ship(Order order)
        {
            // family web service
            var client = new HttpClient();
            client.PostAsync("http://api.family.com/Order", order, new JsonMediaTypeFormatter());
        }
    }

    public interface IStoreService
    {
        void Ship(Order order);
    }
}