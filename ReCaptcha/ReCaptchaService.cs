﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Utf8Json;

namespace ReCaptcha
{
    public class ReCaptchaService
    {
        private const string ResponseHeaderKey = "g-recaptcha-response";
        private static readonly Uri BaseUri = new Uri("https://www.google.com/recaptcha/api/siteverify");
        private static readonly MediaTypeHeaderValue MediaType = MediaTypeHeaderValue.Parse("application/json; charset=utf-8");
        private readonly string key;
        private readonly string secret;

        public static HttpClient HttpClient { get; set; } = new HttpClient
        {
            BaseAddress = BaseUri,
            Timeout = TimeSpan.FromSeconds(60)
        };

        public ReCaptchaService(string key, string secret)
        {
            this.key = key;
            this.secret = secret;
        }

        public async Task<ReCaptchaResponse> VerifyAsync(string responseToken, string remoteIp, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(responseToken)) throw new ArgumentException(nameof(responseToken));

            var form = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "response", responseToken },
                { "secret", secret },
                { "remoteip", remoteIp },
            });

            var response = await HttpClient.PostAsync(BaseUri, form, cancellationToken).ConfigureAwait(false);
            try
            {
                var responseJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return JsonSerializer.Deserialize<ReCaptchaResponse>(responseJson);
            }
            catch
            {
                return new ReCaptchaResponse
                {
                    ResponseStatus = ReCaptchaResponseStatus.Failed,
                    StatusCode = response.StatusCode
                };
            }
        }
    }
}
