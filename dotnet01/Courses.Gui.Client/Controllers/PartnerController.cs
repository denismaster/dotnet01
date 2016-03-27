using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Courses.DAL;
using Courses.ViewModels;
using Courses.Buisness.Services;
using Courses.Buisness.Filtering;
using System.Web.Http.Results;

namespace Courses.Gui.Client.Controllers
{
    //[Authorize(Roles = "Admin, Manager, Default")]
    public class PartnerController : ApiController
    {
        private readonly IPartnerService partnerService;

        public PartnerController(IPartnerService partnerService)
        {
            if (partnerService == null)
                throw new ArgumentNullException();
            this.partnerService = partnerService;
        }

        public JsonResult<PartnerViewModel> GetPartner(int id)
        {
            var partnerViewModel = partnerService.GetByID(id);
            return Json(partnerViewModel);
        }

        public JsonResult<IEnumerable<PartnerViewModel>> GetPartners()
        {
            var partners = partnerService.GetIEnumerablePartnersCollection();
            return Json(partners);
        }
    }
}