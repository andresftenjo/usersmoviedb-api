using moviedb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace moviedb.Controllers
{
    public class RankController : ApiController
    {
        // GET: api/Rank
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [Authorize]
        [HttpGet]
        [Route("api/rank")]
        public Rank Get(int idUser, int idMovie)
        {
            using (var context = new moviedbusersContext()) {
                return context.Rank.Where(r => r.IdUser.Equals(idUser) && r.IdMovie.Equals(idMovie)).FirstOrDefault();
            }
        }

        [Authorize]
        [HttpPost]
        [Route("api/rank")]
        public IHttpActionResult Post([FromBody]Rank rankObj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            using (var context = new moviedbusersContext())
            {
                context.Rank.Add(new Rank() { 
                    IdUser = rankObj.IdUser,
                    IdMovie = rankObj.IdMovie,
                    Rank1 =  rankObj.Rank1,
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now
                });
                context.SaveChanges();
                return Ok();
            }
        }

        [Authorize]
        [HttpPut]
        [Route("api/rank")]
        public IHttpActionResult PutRank(int id, [FromBody]Rank rankObj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            using (var context = new moviedbusersContext())
            {
                var userRank = context.Rank.Where(r => r.Id == id).FirstOrDefault();

                if (userRank != null)
                {
                    userRank.Rank1 = rankObj.Rank1;
                    userRank.DateUpdated = DateTime.Now;

                    context.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
                return Ok();
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("api/rank")]
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid Rank id");

            using (var context = new moviedbusersContext())
            {
                var userRank = context.Rank.Where(r => r.Id == id).FirstOrDefault();

                context.Rank.Remove(userRank);
                context.SaveChanges();
            }

            return Ok();
        }
    }
}
