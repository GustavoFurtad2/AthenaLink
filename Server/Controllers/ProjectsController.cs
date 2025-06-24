using Shared.Models;
using Server.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProjectsController : ControllerBase
	{
		private readonly ProjectsService _service;

		public ProjectsController(ProjectsService service)
		{
			_service = service;
		}

		[HttpPost("new")]
		public IActionResult New()
		{
			Project project = _service.Create();

			return Ok(new { token = project.Token });
		}

		[HttpGet("{token}/main.js")]
		public IActionResult GetMainJs(string token)
		{
			Project project = _service.Get(token);

			if (project == null)
			{
				return NotFound();
			}

			_service.MarkAsUpdated(token);

			return Content(project.Code, "application/javascript");
		}

		[HttpPost("{token}/main.js")]
		public async Task<IActionResult> UploadMainJs(string token)
		{
			var body = await new StreamReader(Request.Body).ReadToEndAsync();

			if (!_service.UpdateCode(token, body))
			{
				return NotFound();
			}

			return Ok();
		}

		[HttpPost("{token}/version")]
		public IActionResult GetVersion(string token)
		{
			Project project = _service.Get(token);

			if (project == null)
			{
				return NotFound();
			}

			return Ok(new { version = project.Version });
		}

		[HttpGet("{token}/should-update")]
		public IActionResult ShouldUpdate(string token)
		{
			Project project = _service.Get(token);

			if (project == null)
			{
				return NotFound();
			}

			return Ok(new { update = project.UpdateRequested });
		}
	}
}
