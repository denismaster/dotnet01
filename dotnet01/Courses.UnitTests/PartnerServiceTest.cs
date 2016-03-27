using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Courses.Buisness;
using Courses.DAL;
using Courses.Buisness.Filtering;
using Courses.ViewModels;
using Courses.Buisness.Services;

namespace Courses.UnitTests
{
    [TestFixture]
    public class PartnerServiceTest
    {
        [Test]
        public void GetPartnersTest()
        {
            IPartnerService _partnerService = new PatherService(new PartnerRepository(), new AccountRepository(), new PartnerFilterFactory());
            PartnerCollectionViewModel actual = _partnerService.GetPartners(1, 5);
            Assert.IsNotNull(actual);
        }

        [Test]
        public void GetIEnumerablePartnersCollectionTest()
        {
            IPartnerService _partnerService = new PatherService(new PartnerRepository(), new AccountRepository(), new PartnerFilterFactory());
            IEnumerable<PartnerViewModel> actual = _partnerService.GetIEnumerablePartnersCollection();
            Assert.IsNotNull(actual);
        }

        [Test]
        public void GetByIDTest()
        {
            IPartnerService _partnerService = new PatherService(new PartnerRepository(), new AccountRepository(), new PartnerFilterFactory());
            PartnerViewModel _partnerViewModel = GetPartnerViewModel();
            _partnerService.Add(_partnerViewModel);
            Assert.IsNotNull(_partnerService.GetByID(1));
        }

        [Test]
        public void GetPartnerWithMenegersTest()
        {
            IPartnerService _partnerService = new PatherService(new PartnerRepository(), new AccountRepository(), new PartnerFilterFactory());
            PartnerViewModelForAddEditView actual = _partnerService.GetPartnerWithMenegers(null);
            Assert.IsNotNull(actual);
        }

        [Test]
        public void AddTest()
        {
            IPartnerService _partnerService = new PatherService(new PartnerRepository(), new AccountRepository(), new PartnerFilterFactory());
            PartnerViewModel _partnerViewModel = GetPartnerViewModel();
            _partnerService.Add(_partnerViewModel);
            Assert.IsNotNull(_partnerService.GetByID(1));
        }

        [Test]
        public void EditTest()
        {
            IPartnerService _partnerService = new PatherService(new PartnerRepository(), new AccountRepository(), new PartnerFilterFactory());
            PartnerViewModel _partnerViewModel = GetPartnerViewModel();
            _partnerService.Add(_partnerViewModel);
            _partnerViewModel.Address = "otherAddress";
            _partnerService.Edit(_partnerViewModel);
            bool actual, expected = true;
            if (_partnerService.GetByID(1).Address.CompareTo("otherAddress") == 0)
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
            IPartnerService _partnerService = new PatherService(new PartnerRepository(), new AccountRepository(), new PartnerFilterFactory());
            PartnerViewModel _partnerViewModel = GetPartnerViewModel();
            _partnerService.Add(_partnerViewModel);
            _partnerService.Delete(_partnerViewModel);
            Assert.IsNull(_partnerService.GetByID(1));
        }

        private PartnerViewModel GetPartnerViewModel()
        {
            PartnerViewModel _partnerViewModel = new PartnerViewModel();
            _partnerViewModel.Address = "testAddress";
            _partnerViewModel.Contact = "testContact";
            _partnerViewModel.Email = "test@test.com";
            _partnerViewModel.Name = "testName";
            _partnerViewModel.Phone = "1234567890";
            _partnerViewModel.Id = 1;
            _partnerViewModel.CreatedDate = new DateTime(1, 1, 1);
            _partnerViewModel.UpdatedDate = new DateTime(1, 1, 1);
            return _partnerViewModel;
        }
    }
}
