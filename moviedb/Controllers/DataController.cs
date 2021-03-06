﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Security.Claims;
using moviedb.Models;

namespace moviedb.Controllers
{
    public class DataController : ApiController
    {

        [AllowAnonymous]
        [HttpGet]
        [Route("api/data/public")]
        public IHttpActionResult Get()
        {
            return Ok("Now server time is: " + DateTime.Now.ToString());
        }


        [Authorize]
        [HttpGet]
        [Route("api/data/authenticate")]
        public IHttpActionResult GetForAuthenticate()
        {
            var identity = (ClaimsIdentity)User.Identity;
            string userId = identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            int userIdInt = 0;
            if (!string.IsNullOrEmpty(userId)) {
                userIdInt = int.Parse(userId);
            }
            bool isNew = false;

            using (var context = new moviedbusersContext())
            {
                if (!context.Users.Any(o => o.Id == int.Parse(userId))) {
                    context.Users.Add(new Users()
                    {
                        Id = userIdInt,
                        UserName = identity.Name
                    });
                    context.SaveChanges();
                    isNew = true;
                }
            }

            return Ok(new { UserId = userIdInt, UserName = identity.Name , NewUser = isNew});
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        [Route("api/data/authorize")]
        public IHttpActionResult GetForAdmin()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var roles = identity.Claims
                        .Where(c => c.Type == ClaimTypes.Role)
                        .Select(c => c.Value);
            return Ok("Hello " + identity.Name + " Role: " + string.Join(",", roles.ToList()));
        }
    }
}
