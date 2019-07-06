# reCAPTCHA-CSharp
reCAPTCHA Example for C#

Quick Start
---

### Create API Key and Secret

https://www.google.com/recaptcha/intro/v3.html

### Client side

```js
<script src='@string.Format("https://www.google.com/recaptcha/api.js?render={0}", key)'></script>
<script>
    grecaptcha.ready(function () {
        grecaptcha.execute('@key', { action: 'example' }).then(function (token) {
            document.getElementById("response-token").value = token;
        });
    });
</script>
```

### Server side

```csharp
var service = new ReCaptchaService(secret);
var response = await service.VerifyAsync(token, ipAddress);

/*
 * Response JSON
 * 
 {  
     "success":true,
     "score":0.9, // Human 1.0 → 0.0 BOT
     "action":"example",
     "challenge_ts":"2019-07-06T05:13:35Z",
     "hostname":"example.com",
     "error-codes":null
 }
 */
```

Official Documents
---
https://developers.google.com/recaptcha/docs/v3
