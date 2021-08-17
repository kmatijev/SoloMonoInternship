using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BasicWebAPI.Controllers
{

    public class Student
    {
        public static Dictionary<int, string> Data = new Dictionary<int, string>();
    }

    public class StudentController : ApiController
    {

        // GET api/student/{id}
        [HttpGet]
        [Route("api/student/{id}")]
        public HttpResponseMessage Get(int id)
        {
            if (Student.Data.ContainsKey(id))
            {
                HttpResponseMessage Msg = Request.CreateResponse(HttpStatusCode.OK, Student.Data[id]);
                return Msg;
            }
            else
            {
                HttpResponseMessage Msg = Request.CreateResponse(HttpStatusCode.NotFound, id);
                return Msg;
            }
        }
        // POST api/student/{id}
        [HttpPost]
        [Route("api/Student/{id}")]
        public HttpResponseMessage Post(int id, [FromBody] string value)
        {
            if (!Student.Data.ContainsKey(id))
            {
                Student.Data.Add(id, value);
                HttpResponseMessage Msg = Request.CreateResponse(HttpStatusCode.OK, Student.Data[id]);
                return Msg;
            }
            else
            {
                HttpResponseMessage Msg = Request.CreateResponse(HttpStatusCode.Conflict, id);
                return Msg;
            }
        }

        [HttpPut]
        [Route("api/Student/{id}")]
        // PUT api/Student/5
        public HttpResponseMessage Put(int id, [FromBody] string value)
        {
            Student.Data[id] = value;
            HttpResponseMessage Msg = Request.CreateResponse(HttpStatusCode.OK, Student.Data[id]);

            return Msg;
        }
        // DELETE api/Student/5
        [HttpDelete]
        [Route("api/Student/{id}")]
        public HttpResponseMessage Delete(int id)
        {
            if (Student.Data.ContainsKey(id))
            {
                Student.Data.Remove(id);
                HttpResponseMessage Msg = Request.CreateResponse(HttpStatusCode.OK, id);
                return Msg;
            }
            else
            {
                HttpResponseMessage Msg = Request.CreateResponse(HttpStatusCode.NotFound, id);
                return Msg;
            }
        }
    }
}