using Courses.Buisness.Partner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Courses.Gui.Manager.Controllers
{
    public class PartnerController: Controller
    {
        private readonly IPartnerService partnerService;

        public PartnerController(IPartnerService partnerService)
        {
            if (partnerService == null)
                throw new ArgumentNullException();
            this.partnerService = partnerService;
        }
        
    }
}