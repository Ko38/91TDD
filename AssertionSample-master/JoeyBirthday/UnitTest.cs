using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;

namespace JoeyBirthday
{
	[TestFixture]
    public class UnitTest
	{
		[Test]
		public void Today_Is_Joey_Birthday()
		{
			var expected = "Happy Birthday";
			Mock<JoeyBirthday> mock = new Mock<JoeyBirthday>();
			mock.Protected().SetupGet<DateTime>("TodayDateTime").Returns(new DateTime(1990, 11, 28));
			Assert.AreEqual(expected, mock.Object.IsTodayJoeyBirthday());
		}

		[Test]
		public void Today_Is_Joey_Birthday1()
		{
			var expected = "Happy Birthday";
			JoeyBirthday joeyBirthday = new JoeyBirthday(new DateTime(1990, 11, 28));
			Assert.AreEqual(expected, joeyBirthday.IsTodayJoeyBirthday());
		}

		[Test]
		public void Today_Is_Joey_Birthday2()
		{
			var expected = "Happy Birthday";
			var joeyBirthday = new MockJoeyBirthday();
			joeyBirthday.SetToday(new DateTime(1990, 11, 28));
			Assert.AreEqual(expected, joeyBirthday.IsTodayJoeyBirthday());
		}

		[Test]
		public void Today_Is_Not_Joey_Birthday()
		{
			var expected = "No, Sorry";
			JoeyBirthday joeyBirthday = new JoeyBirthday();
			Assert.AreEqual(expected, joeyBirthday.IsTodayJoeyBirthday());
		}

		[Test]
		public void Today_Is_Not_Joey_Birthday1()
		{
			var expected = "No, Sorry";
			MockJoeyBirthday joeyBirthday = new MockJoeyBirthday();
			joeyBirthday.SetToday(new DateTime(1990, 11, 29));
			Assert.AreEqual(expected, joeyBirthday.IsTodayJoeyBirthday());
		}
	}

	public class Employee
	{
		
	}

	public class MockJoeyBirthday : JoeyBirthday
	{
		private DateTime _today;
		public void SetToday(DateTime today)
		{
			_today = today;
		}
		protected override DateTime TodayDateTime => _today;
	}

	public class JoeyBirthday
	{
		private DateTime JoeysBirthday = new DateTime(1990, 11, 28);
		private readonly DateTime? _today;
		public JoeyBirthday() {}
		public JoeyBirthday(DateTime today)
		{
			_today = today;
		}

		public static JoeyBirthday CreateJoeyBirthday()
		{
			return new JoeyBirthday();
		}
		protected virtual DateTime TodayDateTime
		{
			get
			{
				return _today ?? DateTime.Today;
			}
		}
		public string IsTodayJoeyBirthday()
		{
			return IsJoeyBirthday(TodayDateTime);
		}
		private string IsJoeyBirthday(DateTime date)
		{
			return JoeysBirthday.Date == date.Date ? "Happy Birthday" : "No, Sorry";

		}
	}
}
