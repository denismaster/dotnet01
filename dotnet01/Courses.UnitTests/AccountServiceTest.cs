using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Courses.Buisness;
using Courses.ViewModels;
using Courses.DAL;

namespace Courses.UnitTests
{
    [TestFixture]
    public class AccountServiceTest
    {
        [Test]
        public void GetAccountsTest()
        {
            IAccountService _accountService = new AccountService(new AccountRepository(), new Buisness.Filtering.AccountFilterFactory());
            AccountCollectionViewModel actual = _accountService.GetAccounts(1, 5);
            Assert.IsNotNull(actual);
        }

        [Test]
        public void GetByIDTest()
        {
            IAccountService _accountService = new AccountService(new AccountRepository(), new Buisness.Filtering.AccountFilterFactory());
            AccountViewModel _accountViewModel = GetAccountViewModel();
            _accountService.Add(_accountViewModel);
            AccountViewModel actual = _accountService.GetByID(1);
            Assert.IsNotNull(actual);
        }

        [Test]
        public void AddTest()
        {
            IAccountService _accountService = new AccountService(new AccountRepository(), new Buisness.Filtering.AccountFilterFactory());
            AccountViewModel _accountViewModel = GetAccountViewModel();
            _accountService.Add(_accountViewModel);
            Assert.IsNotNull(_accountService.GetByID(1));
        }

        [Test]
        public void EditTest()
        {
            IAccountService _accountService = new AccountService(new AccountRepository(), new Buisness.Filtering.AccountFilterFactory());
            AccountViewModel _accountViewModel = GetAccountViewModel();
            bool actual, expected = true;
            _accountService.Add(_accountViewModel);
            _accountViewModel.Password = "0a1b2c";
            _accountService.Edit(_accountViewModel);
            if (_accountService.GetByID(1).Password.CompareTo("0a1b2c") == 0)
            {
                actual = true;
            }
            else
            {
                actual = false;
            }
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void DeleteTest()
        {
            IAccountService _accountService = new AccountService(new AccountRepository(), new Buisness.Filtering.AccountFilterFactory());
            AccountViewModel _accountViewModel = GetAccountViewModel();
            _accountService.Add(_accountViewModel);
            _accountService.Delete(_accountViewModel);
            Assert.IsNull(_accountService.GetByID(1));
        }

        private AccountViewModel GetAccountViewModel()
        {
            AccountViewModel _accountViewModel = new AccountViewModel();
            _accountViewModel.Id = 1;
            _accountViewModel.Login = "test";
            _accountViewModel.Password = "testpassword";
            _accountViewModel.Email = "testc@tect.com";
            _accountViewModel.Role = "Default";
            return _accountViewModel;
        }
    }
}
