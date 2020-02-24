using moviedb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace moviedb.Controllers
{
    public class CommentController : ApiController
    {
        // GET: api/Comment
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [Authorize]
        [HttpGet]
        [Route("api/comments")]
        public List<Comment> Get(int idMovie)
        {
            using (var context = new moviedbusersContext())
            {
                return context.Comment.Where(r => r.IdMovie.Equals(idMovie)).ToList();
            }
        }

        [Authorize]
        [HttpPost]
        [Route("api/comment")]
        public IHttpActionResult Post([FromBody]Comment commentObj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            using (var context = new moviedbusersContext())
            {
                context.Comment.Add(new Comment()
                {
                    IdUser = commentObj.IdUser,
                    IdMovie = commentObj.IdMovie,
                    Comment1 = commentObj.Comment1,
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now
                });
                context.SaveChanges();
                return Ok();
            }
        }

        [Authorize]
        [HttpPut]
        [Route("api/comment")]
        public IHttpActionResult PutRank(int id, [FromBody]Comment commentObj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            using (var context = new moviedbusersContext())
            {
                var userComment = context.Comment.Where(r => r.Id == id).FirstOrDefault();

                if (userComment != null)
                {
                    userComment.Comment1 = commentObj.Comment1;
                    userComment.DateUpdated = DateTime.Now;

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
        [Route("api/comment")]
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid Comment id");

            using (var context = new moviedbusersContext())
            {
                var userComment = context.Comment.Where(r => r.Id == id).FirstOrDefault();

                context.Comment.Remove(userComment);
                context.SaveChanges();
            }

            return Ok();
        }
    }
}
