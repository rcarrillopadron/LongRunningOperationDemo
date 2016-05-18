using System.Web.Http;

namespace TheWebApi.Controllers
{
    public class LongRunningController : ApiController
    {
        [HttpPost, Route("api/start")]
        public IHttpActionResult Start()
        {
            WebApiApplication.Cancel = false;
            WebApiApplication.SessionProxy.StartProcess();
            return Ok("Started");
        }

        [HttpPost, Route("api/stop")]
        public IHttpActionResult Cancel()
        {
            WebApiApplication.Cancel = true;
            return Ok("Stopped");
        }

        [HttpGet, Route("api/state")]
        public IHttpActionResult GetState()
        {
            return Ok(new {State = WebApiApplication.CurrentState});
        }
    }
}
