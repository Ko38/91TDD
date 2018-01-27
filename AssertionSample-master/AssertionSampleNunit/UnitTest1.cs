
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;

namespace AssertionSample
{
    [TestFixture]
    public class AssertionSample
    {
        private CustomerRepo customerRepo = new CustomerRepo();

		private bool AreEqual(Customer a, Customer b)
		{
			if (a.Order == b.Order)
				return a.Age == b.Age && a.Birthday == b.Birthday && a.Id == b.Id;

			return a.Age == b.Age && a.Birthday == b.Birthday && a.Id == b.Id && OrderAreEqual(a.Order, b.Order);

		}

		private bool OrderAreEqual(Order a, Order b)
		{
			return a.Id == b.Id && a.Price == b.Price;
		}

		public class Comparer : IEqualityComparer<Customer>
		{
			public bool Equals(Customer a, Customer b)
			{
				if (a.Order == b.Order)
					return a.Age == b.Age && a.Birthday == b.Birthday && a.Id == b.Id;
				return a.Age == b.Age && a.Birthday == b.Birthday && a.Id == b.Id && a.Order.Id == b.Order.Id && a.Order.Price == b.Order.Price;
			} 

			public int GetHashCode(Customer inst)
			{
				return inst.GetHashCode();
			}
		}
		private Comparer comparer = new Comparer();

		[Test]
		public void CompareCustomerNunit()
		{
			var actual = customerRepo.Get();
			var expect = new Customer()
			{
				Id = 2,
				Age = 18,
				Birthday = new DateTime(1990, 1, 26)
			};

			//Assert.IsTrue(AreEqual(expect, actual));
			Assert.That(actual, Is.EqualTo(expect).Using(comparer));
			//Assert.AreEqual(expect, actual);
		}

        [Test]
        public void CompareCustomerListNunit()
        {
            var actual = customerRepo.GetAll();
			var expect = new List<Customer>
			{
				new Customer()
				{
					Id=3,
					Age=20,
					Birthday = new DateTime(1993,1,2)
				},

				new Customer()
				{
					Id=4,
					Age=21,
					Birthday = new DateTime(1993,1,3)
				},
			};
			//how to assert customers?
			Assert.Multiple(() =>
			{
				//Assert.IsTrue(AreEqual(expect[0], actual[0]));
				//Assert.IsTrue(AreEqual(expect[1], actual[1]));
				Assert.That(actual[0], Is.EqualTo(expect[0]).Using(comparer));
				Assert.That(actual[1], Is.EqualTo(expect[1]).Using(comparer));
			});
			
		}

		[Test]
        public void CompareComposedCustomerNunit()
        {
            var actual = customerRepo.GetComposedCustomer();
			var expect = new Customer()
			{
				Age = 30,
				Id = 11,
				Birthday = new DateTime(1999, 9, 9),
				Order = new Order { Id = 19, Price = 91 },
			};
			//how to assert composed customer?
			Assert.That(actual, Is.EqualTo(expect).Using(comparer));
		}

		[Test]
        public void PartialCompare_Customer_Birthday_And_Order_PriceNunit()
        {
            var actual = customerRepo.GetComposedCustomer();

            var expected = new Customer()
            {
                Birthday = new DateTime(1999, 9, 9),
                Order = new Order { Price = 91 },
            };

			//how to assert actual is equal to expected?
			//Assert.IsTrue(AreEqual(expected, actual));
			Assert.That(actual, Is.EqualTo(expected).Using(comparer));
		}
	}

    public class CustomerRepo
    {
        public Customer Get()
        {
            return new Customer
            {
                Id = 2,
                Age = 18,
                Birthday = new DateTime(1990, 1, 26)
            };
        }

        public List<Customer> GetAll()
        {
            return new List<Customer>
            {
                new Customer()
                {
                    Id=3,
                    Age=20,
                    Birthday = new DateTime(1993,1,2)
                },

                new Customer()
                {
                    Id=4,
                    Age=21,
                    Birthday = new DateTime(1993,1,3)
                },
            };
        }

        public Customer GetComposedCustomer()
        {
            return new Customer()
            {
                Age = 30,
                Id = 11,
                Birthday = new DateTime(1999, 9, 9),
                Order = new Order { Id = 19, Price = 91 },
            };
        }
    }

    public class Order
    {
        public int Id { get; set; }
        public int Price { get; set; }
    }

    public class Customer
    {
        public int Id { get; set; }
        public int Age { get; set; }
        public DateTime Birthday { get; set; }
        public Order Order { get; set; }
    }
}