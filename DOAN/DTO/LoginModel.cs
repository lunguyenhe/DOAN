using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace DOAN.DTO
{
	public class LoginModel
	{
		[Required]
		//	[DataType(DataType.EmailAddress)]
		[JsonProperty("username")]
		public string Username { get; set; }

		[Required]
		[JsonProperty("password")]
		public string Password { get; set; }
	}
}

