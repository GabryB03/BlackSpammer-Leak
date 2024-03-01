public async Task spamFriend(String userId)
        {
            try
            {
                var http = new HttpClient();
                foreach (String token in tokens)
                {
                    main.callbackLog("(FRIENDSPAM) Sending friend (" + token + ") to " + userId);
                    var request = new HttpRequestMessage
                    {

                        RequestUri = new Uri("https://discordapp.com/api/v6/users/@me/relationships/" + userId),
                        Method = HttpMethod.Put,
                        Headers =
                {
                    { HttpRequestHeader.Authorization.ToString(), token },
                }

                    };
                    main.callbackLog("(FRIENDSPAM) Sending..");
                    request.Content = new StringContent("{}", Encoding.UTF8, "application/json");
                http.SendAsync(request);

                    main.callbackLog("(FRIENDSPAM) Sent.");
                }
            }
            catch (Exception exc)
            {
                main.callbackLog("(FRIENDSPAM) Error:: " + exc);
            }
        }