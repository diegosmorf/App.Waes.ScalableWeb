using System.Web.Http;
using App.Waes.ScalableWeb.Application.Contract;

namespace App.Waes.ScalableWeb.WebApi.Controllers
{
    [RoutePrefix("v1/diff")]
    public class DocumentContentController : ApiController
    {
        private readonly IDocumentContentServiceApplication applicationService;

        public DocumentContentController(IDocumentContentServiceApplication applicationService)
        {
            this.applicationService = applicationService;
        }

        [Route("{id:int}/right")]
        public IHttpActionResult SetRightContent(int id, [FromBody] string content)
        {
            applicationService.Right(id, content);
            return Ok();
        }

        [Route("{id:int}/left")]
        public IHttpActionResult SetLeftContent(int id, [FromBody] string content)
        {
            applicationService.Left(id, content);
            return Ok();
        }

        [Route("{id:int}")]
        public IHttpActionResult GetDifference(int id)
        {
            var result = applicationService.Diff(id);
            return Ok(result);
        }
    }
}