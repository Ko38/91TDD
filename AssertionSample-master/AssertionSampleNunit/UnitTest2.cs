using NUnit.Framework;
using System;

namespace AssertionSample
{
    [TestFixture]
    public class AssertExceptionSample
    {
        [Test]
        public void Divide_positiveNunit()
        {
            var calculator = new Calculator();
            var actual = calculator.Divide(5, 2);
            Assert.AreEqual(2.5m, actual);
        }

        [Test]
		public void Divide_ZeroNunit()
        {
            var calculator = new Calculator();
			//var actual = calculator.Divide(5, 0);

			//how to assert expected exception?
			YouShallNotPassException ex = Assert.Throws<YouShallNotPassException>(() =>
			{
				calculator.Divide(5, 0);
			});
			Assert.AreEqual(0, ex.Number);
		}
	}

    public class Calculator
    {
        public decimal Divide(decimal first, decimal second)
        {
            if (second == 0)
            {
                throw new YouShallNotPassException() { Number = second };
            }
            return first / second;
        }
    }

	public class YouShallNotPassException : Exception
	{
		public decimal Number { get; internal set; }
	}
}