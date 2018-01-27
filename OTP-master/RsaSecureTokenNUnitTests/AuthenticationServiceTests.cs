using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RsaSecureToken;
using NUnit.Framework;

namespace RsaSecureToken.Tests
{
    [TestFixture]
    public class AuthenticationServiceTests
    {
        [Test]
        public void IsValidTestNunit()
        {
            var target = new AuthenticationService(new FakeProfile(), new FakeToken());

			var actual = target.IsValid("joey", "91000000");

            //always failed
            Assert.IsTrue(actual);                       
        }

		public class FakeProfile : IProfile
		{
			public string GetPassword(string account)
			{
				if (account == "joey")
					return "91";
				return "";
			}
		}

		public class FakeToken : IToken
		{

			public string GetRandom(string account)
			{
				if (account == "joey")
					return "000000";
				return "";
			}
		}
	}
}