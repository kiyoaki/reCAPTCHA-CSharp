using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace ReCaptcha.ExampleWebsite.Pages
{
    public class IndexModel : PageModel
    {
        private HttpContext httpContext;

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public string Token { get; set; }

            public string ApiResponse { get; set; }

            public string IpAddress { get; set; }
        }

        public IndexModel(IHttpContextAccessor httpContextAccessor)
        {
            httpContext = httpContextAccessor.HttpContext;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            Input.IpAddress = httpContext?.Connection?.RemoteIpAddress?.MapToIPv4()?.ToString();

            var service = new ReCaptchaService(Environment.GetEnvironmentVariable("RECAPTCHA_SECRET"));
            var response = await service.VerifyAsync(Input.Token, Input.IpAddress);
            Input.ApiResponse = Utf8Json.JsonSerializer.ToJsonString(response);
            return Page();
        }
    }
}
