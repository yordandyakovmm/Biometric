using Facebook;
using Newtonsoft.Json.Linq;
using BAuth.DAL;
using BAuth.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Threading;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web.Script.Serialization;
using System.Device.Location;

namespace BAuth.Controllers
{


    public class ClaimController : BaseController
    {

        [HttpGet]
        [Route("обезщетение-при-полет/{category}")]
        public ActionResult RegisterClaim(string category)
        {
            ViewBag.category = category;
            return View("RegisterClaim");
        }

       
        [Route("обезщетение-при-полет")]
        public ActionResult ClaimSpliter(string category)
        {
            return View("ClaimSpliter");
        }

        

        [HttpPost]
        [Authorize(Roles = "admin,temp")]
        [Route("обезщетение-списък/{id}")]
        public ActionResult ClaimUpdate(Guid id)
        {
            Claim claim = null;
            using (BAuthDBContext dc = new BAuthDBContext())
            {
                claim = dc.Claims.Where(c => c.ClaimId == id).SingleOrDefault();

                var password = Request.Form["password"];
                var newUserBD = new User()
                {
                    UserId = claim.Email,
                    FirstName = claim.FirstName,
                    LastName = claim.LastName,
                    Email = claim.Email,
                    password = GetHash(password),
                    PictureUrl = "",
                    CreateDate = DateTime.Now,
                    Role = "user"
                };

                dc.Users.Add(newUserBD);
                claim.UserId = newUserBD.UserId;
                dc.SaveChanges();

            }

            return Redirect($"/обезщетение-списък/{claim.ClaimId}");

        }

        



    }
}