using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DOAN.Models;
using Microsoft.AspNetCore.Authorization;

namespace DOAN.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		[HttpGet("login/google")]
		public IActionResult GoogleLogin()
		{
			var properties = new AuthenticationProperties
			{
				RedirectUri = Url.Action(nameof(GoogleCallback))
			};

			return Challenge(properties, GoogleDefaults.AuthenticationScheme);
		}


		[HttpGet]
		[Authorize]
		public IActionResult Get()
		{
			try
			{
				using (DOANContext context = new DOANContext())
				{
					var data = context.Accounts.ToList();
					if (data == null)
					{
						return NotFound();
					}
					return Ok(data);
				}
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}

		}
		[HttpGet("callback/google")]
		public async Task<IActionResult> GoogleCallback()
		{
			var authenticateResult = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);

			// Xử lý thông tin người dùng đã xác thực từ authenticateResult

			// Redirect hoặc trả về thông tin người dùng

			return Ok();
		}
	}
}
