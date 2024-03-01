   public async Task joinServer(String invito, String token) {
            var http = new HttpClient();
            main.callbackLog("(JOIN) Received joinServer Request.");
            try
            {
                String invitoCorretto = invito.Replace("https://discord.gg/", "");

                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri("https://discordapp.com/api/v6/invite/" + invitoCorretto),
                    Method = HttpMethod.Post,
                    Headers = {
        { HttpRequestHeader.Authorization.ToString(), token },
        { HttpRequestHeader.ContentType.ToString(), "multipart/mixed" },
        },
                };

                main.callbackLog("(JOIN) Joining into " + invito + " with token " + token);
                http.SendAsync(request);
                main.callbackLog("(JOIN) Done. Ready (" + token + ")");
            } catch (Exception)
            {
                main.callbackLog("(JOIN) An error has occurred with token: " + token);
            }
        }