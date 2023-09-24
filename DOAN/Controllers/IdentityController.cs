
using AutoMapper;
using DOAN.DTO;
using DOAN.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PRN231API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class IdentityController : ControllerBase
	{
		IConfiguration _configuration;

		public IdentityController(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		[HttpPost]
		public IActionResult RequiredToken(LoginModel request)
		{
			using(DOANContext context =new DOANContext())
			{
				//Include(s => s.Customers)
				Account acc = context.Accounts.FirstOrDefault(u => u.Email == request.Username && u.Password == request.Password);
				if (acc == null)
				{
					return Unauthorized("Username or Password incorrect!");
				}
				JwtSecurityToken token = GenerateSecurityToken(acc);
				var config = new MapperConfiguration(config => config.AddProfile(new MappingProfile()));
				var mapper = config.CreateMapper();
				return Ok(new TokenResponse
				{
					AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
					User = acc
				});
				//return Ok(acc);
			}
				
			
			

			

		}
		private JwtSecurityToken GenerateSecurityToken(Account acc)
		{
			string role = acc.Role.ToString();

			var authClaims = new List<Claim>
			{	 
					new Claim(ClaimTypes.Email, acc.Email),		
				new Claim(ClaimTypes.Role, role),
			};

			var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

			var token = new JwtSecurityToken(issuer: _configuration["JWT:ValidIssuer"],
					audience: _configuration["JWT:ValidAudience"],
					expires: DateTime.Now.AddDays(1),
					claims: authClaims,
					signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)); ;

			return token;
		}
		
		public class TokenResponse
		{
			public string AccessToken { get; set; }
			public Account User { get; set; }
		}
	}
}
