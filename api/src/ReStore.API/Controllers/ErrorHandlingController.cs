using Microsoft.AspNetCore.Mvc;

namespace ReStore.API.Controllers;

public class ErrorHandlingController : BaseController
{
        [HttpGet("not-found")]
        public ActionResult GetNotFound() => NotFound();

        [HttpGet("bad-request")]
        public ActionResult GetBadRequest() => BadRequest(new ProblemDetails { Title = "Bir Bad Request hatasıyla karşılaştınız!" });

        [HttpGet("unauthorised")]
        public ActionResult GetUnauthorised() => Unauthorized();

        [HttpGet("server-error")]
        public ActionResult GetServerError() => throw new Exception("This is a server error");

        [HttpGet("validation-error")]
        public ActionResult GetValidationError()
        {
                ModelState.AddModelError("Problem1", "İlk hata için gösterim:");
                ModelState.AddModelError("Problem2", "İkinci hata için gösterim:");
                return ValidationProblem();
        }
}
