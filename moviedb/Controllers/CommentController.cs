using Microsoft.EntityFrameworkCore;
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
        public List<CommentViewModel> Get(int idMovie)
        {
            using (var context = new moviedbusersContext())
            {
                var movieComments =
                   from c in context.Comment
                   join u in context.Users on c.IdUser equals u.Id
                   where c.IdMovie == idMovie
                   select new CommentViewModel {
                       Id = c.Id,
                       Comment = c.Comment1,
                       IdMovie = c.IdMovie,
                       IdUser = c.IdUser,
                       UserName = u.UserName
                   };
                return movieComments.ToList();
            }
        }

        [Authorize]
        [HttpPost]
        [Route("api/comment")]
        public IHttpActionResult Post([FromBody]CommentViewModel commentObj)
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
                    Comment1 = commentObj.Comment,
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
        public IHttpActionResult PutRank(int id, [FromBody]CommentViewModel commentObj)
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
                    userComment.Comment1 = commentObj.Comment;
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
