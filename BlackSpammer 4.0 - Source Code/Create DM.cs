 public async Task<String> createDM(String token, String targetUserId, WebProxy webProxy, int timeout = 5000)
        {

            try
            {
                HttpClientHandler handler = new HttpClientHandler();
                handler.PreAuthenticate = true;
                handler.UseProxy = true;
                handler.Proxy = webProxy;
                var http = new HttpClient(handler);

                http.Timeout = TimeSpan.FromMilliseconds(timeout);

                main.callbackLog("dm:: POST https://discord.com/api/v6/users/@me/channels [..]", ConsoleColor.Yellow);
                main.callbackLog("dm:: (" + webProxy.Address.Host + ":" + webProxy.Address.Port + ") [token=" + token + "] Creating DM with " + targetUserId + "..", ConsoleColor.DarkYellow);
                var requestOpen = new HttpRequestMessage
                {

                    RequestUri = new Uri("https://discord.com/api/v6/users/@me/channels"),
                    Method = HttpMethod.Post,
                    Headers =
                      {
                    { HttpRequestHeader.Authorization.ToString(), token },
                    { HttpRequestHeader.ContentType.ToString(), "application/json" }
                    },

                };
                requestOpen.Content = new StringContent("{\"recipient_id\": " + targetUserId + "}", Encoding.UTF8, "application/json");
                // requestOpen.Headers.Add("Content-Length", requestOpen.Content.ReadAsStringAsync().Result.Length.ToString());
                var requestOpenResponse = await http.SendAsync(requestOpen);
                var responseStr = await requestOpenResponse.Content.ReadAsStringAsync();
                if(responseStr.Contains("401") || responseStr.Contains("403") || responseStr.Contains("500") || responseStr.Contains("verify") || responseStr.Contains("400"))
                {
                    main.callbackLog("dm:: (" + webProxy.Address.Host + ":" + webProxy.Address.Port + ") [token=" + token + "] DM Create Error -> " + responseStr, ConsoleColor.Red);
                    return "";
                }
                dynamic jss = JObject.Parse(responseStr);
                var channelId = jss.id;
                main.callbackLog("dm:: (" + webProxy.Address.Host + ":" + webProxy.Address.Port + ") [token=" + token + "] Dm Created (" + channelId + ") -> " + responseStr, ConsoleColor.Green);
                return channelId;

            }
            catch (Exception exc)
            {
                main.callbackLog("dm:: (" + webProxy.Address.Host + ":" + webProxy.Address.Port + ") [token=" + token + "] DM Create Fail -> Proxy dead or error.", ConsoleColor.Red);
                return "";
            }

        }