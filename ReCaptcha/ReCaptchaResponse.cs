using System;
using System.Net;
using System.Runtime.Serialization;

namespace ReCaptcha
{
    public enum ReCaptchaResponseStatus
    {
        Success = 0,
        Failed = 1
    }

    public enum ReCaptchaErrorCode
    {
        [EnumMember(Value = "missing-input-secret")]
        MissingInputSecret,

        [EnumMember(Value = "invalid-input-secret")]
        InvalidInputSecret,

        [EnumMember(Value = "missing-input-response")]
        MissingInputResponse,

        [EnumMember(Value = "invalid-input-response")]
        InvalidInputResponse,

        [EnumMember(Value = "bad-request")]
        BadRequest,

        [EnumMember(Value = "timeout-or-duplicate")]
        TimeoutOrDuplicate
    }

    public class ReCaptchaResponse
    {
        /// <summary>
        /// ReCaptcha API Response Status
        /// </summary>
        [IgnoreDataMember]
        public ReCaptchaResponseStatus ResponseStatus { get; set; } = ReCaptchaResponseStatus.Success;

        /// <summary>
        /// ReCaptcha API Response Status Code
        /// </summary>
        [IgnoreDataMember]
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;

        /// <summary>
        /// whether this request was a valid reCAPTCHA token for your site
        /// </summary>
        [DataMember(Name = "success")]
        public bool Success { get; set; }

        /// <summary>
        /// the score for this request (0.0 - 1.0)
        /// </summary>
        [DataMember(Name = "score")]
        public float Score { get; set; }

        /// <summary>
        /// the action name for this request (important to verify)
        /// </summary>
        [DataMember(Name = "action")]
        public string Action { get; set; }

        /// <summary>
        /// timestamp of the challenge load (ISO format yyyy-MM-dd'T'HH:mm:ssZZ)
        /// </summary>
        [DataMember(Name = "challenge_ts")]
        public DateTime? ChallengeTimestamp { get; set; }

        /// <summary>
        /// the hostname of the site where the reCAPTCHA was solved
        /// </summary>
        [DataMember(Name = "hostname")]
        public string HostName { get; set; }

        /// <summary>
        /// missing-input-secret	The secret parameter is missing.
        /// invalid-input-secret	The secret parameter is invalid or malformed.
        /// missing-input-response	The response parameter is missing.
        /// invalid-input-response	The response parameter is invalid or malformed.
        /// bad-request	The request is invalid or malformed.
        /// timeout-or-duplicate	The response is no longer valid: either is too old or has been used previously.
        /// </summary>
        [DataMember(Name = "error-codes")]
        public ReCaptchaErrorCode[] ErrorCodes { get; set; }
    }
}
