using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataAccess;
using Dto;

namespace WebUI.Controllers
{
    public class AutomateController : ApiController
    {
        private readonly IRepository _repository;

        public AutomateController()
        {
            _repository = new Repository();
        }

        [HttpPost]
        public HttpResponseMessage Post(ClientStatistics clientStatistics)
        {
            try
            {
                if ((!ModelState.IsValid) || (clientStatistics == null))
                {
                    return Request.CreateResponse(HttpStatusCode.ExpectationFailed);
                }

                _repository.AddUserStatistics(
                    new UserStatistic
                    {
                        AgresivityRate = clientStatistics.AgresivityRate,
                        EmailAddress = clientStatistics.EmailAddress,
                        DateTime = DateTime.UtcNow,
                        Location = clientStatistics.Location
                    });

                return Request.CreateResponse(HttpStatusCode.Created);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed);
            }
        }
    }
}