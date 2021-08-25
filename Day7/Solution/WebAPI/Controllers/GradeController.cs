﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.SqlClient;
using Model.Grade;
using System.Threading.Tasks;
using Service.Common;

namespace WebAPI.Controllers
{
    public class GradeController : ApiController
    {
        public IService GradeService { get; set; }
        public GradeController(IService service) => GradeService = service;
        public class RESTGrade
        {
            public int id { get; set; }
            public string name { get; set; }
        }
        public RESTGrade GradeToREST(Grade domainGrade)
        {

            RESTGrade RestGrade = new RESTGrade
            {
                id = domainGrade.id,
                name = domainGrade.name
            };

            return RestGrade;
        }

        public SqlConnection connection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;");

        // GET api/student/{id}
        [HttpGet]
        [Route("api/grade/{id}")]
        public async Task<HttpResponseMessage> Get(int id)
        {

            List<RESTGrade> GradeList = (await GradeService.GetGrade(id)).ConvertAll(GradeToREST);

            string combinedString = "";

            if (GradeList.Capacity == 0)
            {
                HttpResponseMessage Msg = Request.CreateResponse(HttpStatusCode.BadRequest, "No grade for given id.");
                return Msg;
            }
            else
            {
                foreach (RESTGrade x in GradeList)
                {
                    combinedString += String.Format("Grade ID: {0}, Grade Name: {1}", x.id, x.name);
                }
                HttpResponseMessage Msg = Request.CreateResponse(HttpStatusCode.OK, combinedString);
                return Msg;
            }
        }

        [HttpGet]
        [Route("api/grades/sort_by{order}={sort}/pageination={page}/where{atribute}={filter}")]
        public async Task<HttpResponseMessage> GetAll(string order, string sort, int page, string atribute, string filter)
        {

            List<RESTGrade> GradeList = (await GradeService.GetAllGrades(order, sort, page, atribute, filter)).ConvertAll(GradeToREST);

            string combinedString = "";

            if (GradeList.Capacity == 0)
            {
                HttpResponseMessage Msg = Request.CreateResponse(HttpStatusCode.BadRequest, "No grade for given id.");
                return Msg;
            }
            else
            {
                foreach (RESTGrade x in GradeList)
                {
                    combinedString += String.Format("Grade ID: {0}, Grade Name: {1}    ", x.id, x.name);
                }
                HttpResponseMessage Msg = Request.CreateResponse(HttpStatusCode.OK, combinedString);
                return Msg;
            }
        }
        // POST api/student/{id}
        [HttpPost]
        [Route("api/grade/{id}")]
        public async Task<HttpResponseMessage> Post(int id, [FromBody] string value)
        {
            RESTGrade returnGrade = GradeToREST(await GradeService.PostGrade(id, value));

            if (returnGrade.id == 0 && returnGrade.name == "")
            {
                HttpResponseMessage MsgBad = Request.CreateResponse(HttpStatusCode.BadRequest, "Such grade already exists.");
                return MsgBad;
            }

            string returnString = "Grade ID: " + returnGrade.id + ", Grade Name:" + returnGrade.name;

            HttpResponseMessage MsgGood = Request.CreateResponse(HttpStatusCode.Created, returnString);
            return MsgGood;
        }

        // PUT api/student/5
        [HttpPut]
        [Route("api/grade/{id}")]
        public async Task<HttpResponseMessage> Put(int id, [FromBody] string value)
        {
            string combinedString = await GradeService.PutGrade(id, value);

            if (combinedString == "")
            {
                HttpResponseMessage MsgBad = Request.CreateResponse(HttpStatusCode.NotFound, "Grade with given id does not exist.");
                return MsgBad;
            }

            HttpResponseMessage Msg = Request.CreateResponse(HttpStatusCode.OK, combinedString);
            return Msg;
        }
        // DELETE api/student/5
        [HttpDelete]
        [Route("api/grade/{id}")]
        public async Task<HttpResponseMessage> Delete(int id)
        {
            string returnString = await GradeService.DeleteGrade(id);

            if (returnString == "")
            {
                HttpResponseMessage MsgBad = Request.CreateResponse(HttpStatusCode.NotFound, "Grade with given id does not exist or has Student references.");
                return MsgBad;
            }

            HttpResponseMessage MsgGood = Request.CreateResponse(HttpStatusCode.OK, returnString);
            return MsgGood;
        }
    }
}
