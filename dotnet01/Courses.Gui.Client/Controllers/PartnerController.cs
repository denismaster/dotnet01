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

namespace Courses.Gui.Client.Controllers
{
    //[Authorize(Roles = "Admin, Manager, Default")]
    public class PartnerController : ApiController
    {
        private readonly IPartnerService partnerService;

        public PartnerController()
        {
        }

        public PartnerController(IPartnerService partnerService)
        {
            if (partnerService == null)
                throw new ArgumentNullException();
            this.partnerService = partnerService;
        }


        public PartnerViewModel Get(int id)
        {
            var partnerViewModel = partnerService.GetByID(id);
            return partnerViewModel;
        }

        public PartnerCollectionViewModel Get()
        {
            int? page = 1;
            int pageSize = 5;
            int currentPage = page ?? 1;
            var partners = partnerService.GetPartners(currentPage, pageSize);
            return partners;
        }

        public void Post(PartnerViewModel partnerViewModel)
        {
            partnerService.Edit(partnerViewModel);
        }
    }
}