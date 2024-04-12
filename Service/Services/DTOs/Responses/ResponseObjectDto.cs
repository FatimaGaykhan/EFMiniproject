using System;
namespace Service.Services.DTOs.Responses
{
	public class ResponseObjectDto
	{
		public int StatusCode { get; set; }
		public string Message { get; set; }

		public ResponseObjectDto()
		{
		}
	}
}

