using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
            var service = new ReCaptchaService(Environment.GetEnvironmentVariable("RECAPTCHA_KEY"), Environment.GetEnvironmentVariable("RECAPTCHA_SECRET"));
            var response = await service.VerifyAsync(Input.Token, httpContext.Connection?.RemoteIpAddress?.ToString());
            Input.ApiResponse = Utf8Json.JsonSerializer.ToJsonString(response);
            return Page();
        }
    }
}
