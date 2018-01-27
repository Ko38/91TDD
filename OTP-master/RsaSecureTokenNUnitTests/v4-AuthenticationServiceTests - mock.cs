using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using RsaSecureToken;
using NUnit.Framework;

namespace RsaSecureToken.Tests
{
    [TestFixture]
    public class AuthenticationServiceTests
    {
        [Test]
        public void IsValidTest_只有驗證Authentication合法或非法Nunit()
        {
            //arrange
            IProfile profile = Substitute.For<IProfile>();
            profile.GetPassword("Joey").Returns("91");

            IToken token = Substitute.For<IToken>();
            token.GetRandom("Joey").Returns("abc");

            ILog log = Substitute.For<ILog>();
            AuthenticationService target = new AuthenticationService(profile, token, log);
            string account = "Joey";
            string password = "wrong password";
            // 正確的 password 應為 "91abc"

            //act
            bool actual;
            actual = target.IsValid(account, password);

			// assert
			log.Received().Save(
				Arg.Is<string>(x => x.Contains("Joey") && x.Contains("login failed"))
				);
			//Assert.IsFalse(actual);
		}

        [Test]
        public void IsValidTest_如何驗證當非法登入時有正確紀錄logNunit()
        {
			// 試著使用 stub object 的 ReturnsForAnyArgs() 方法
			//例如：profile.GetPassword("").ReturnsForAnyArgs("91"); // 不管GetPassword()傳入任何參數，都要回傳 "91"

			// step 1: arrange, 建立 mock object
			// ILog log = Substitute.For<ILog>();

			// step 2: act

			// step 3: assert, mock object 是否有正確互動
			//log.Received(1).Save("account:Joey try to login failed"); //Received(1) 可以簡化成 Received()
			IProfile profile = Substitute.For<IProfile>();
			profile.GetPassword("Joey").Returns("91");

			IToken token = Substitute.For<IToken>();
			token.GetRandom("Joey").Returns("abc");

			ILog log = Substitute.For<ILog>();
			AuthenticationService target = new AuthenticationService(profile, token, log);
			string account = "Joey";
			string password = "91abc";
			// 正確的 password 應為 "91abc"

			//act
			bool actual;
			actual = target.IsValid(account, password);

			log.DidNotReceiveWithAnyArgs().Save("");
			// assert
		}        
    }
}