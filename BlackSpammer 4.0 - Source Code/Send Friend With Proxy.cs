 public async Task spamFriend(String userId, List<String> proxy)
        {
            var random = new Random();
            try
            {
                HttpClientHandler handler = new HttpClientHandler();
                handler.PreAuthenticate = true;
                handler.UseProxy = true;
                var pri = proxy[random.Next(proxy.Count)].Split(':');

                var proxyy = new WebProxy(pri[0], int.Parse(pri[1]));

                handler.Proxy = proxyy;

                var http = new HttpClient(handler);
                foreach (String token in tokens)
                {
                    main.callbackLog("friend:: Sending friend (" + token + ") [ " + tokens.IndexOf(token) + " ] to " + userId + " (" + pri[0] + ":" + pri[1] + ")", ConsoleColor.Yellow);
                    try
                    {
                        main.friendBot(tokens.IndexOf(token), connected[token].CurrentUser.Id);
                    } catch(Exception)
                    {

                    }
                    var request = new HttpRequestMessage
                    {

                        RequestUri = new Uri("https://discordapp.com/api/v6/users/@me/relationships/" + userId),
                        Method = HttpMethod.Put,
                        Headers =
                {
                    { HttpRequestHeader.Authorization.ToString(), token },
                }

                    };
                    main.callbackLog("friend:: Sending friend req..", ConsoleColor.Yellow);
                    request.Content = new StringContent("{}", Encoding.UTF8, "application/json");
#pragma warning disable CS4014 // Non è possibile attendere la chiamata, pertanto l'esecuzione del metodo corrente continuerà prima del completamento della chiamata
                    Task.Run(async () =>
                    {
                        try
                        {

                            var c = await (await http.SendAsync(request)).Content.ReadAsStringAsync();
                            main.callbackLog("friend:: Friend (" + token + ") [ " + tokens.IndexOf(token) + " ] to " + userId + " (" + pri[0] + ":" + pri[1] + ") -> " + c, ConsoleColor.Cyan);

                        }
                        catch (Exception excc)
                        {
                            main.callbackLog("friend:: Friend (" + token + ") [ " + tokens.IndexOf(token) + " ] to " + userId + " (" + pri[0] + ":" + pri[1] + ") Error -> " + excc, ConsoleColor.Red);
                        }
                    });
#pragma warning restore CS4014 // Non è possibile attendere la chiamata, pertanto l'esecuzione del metodo corrente continuerà prima del completamento della chiamata
                    main.doneBot();

                }
            }
            catch (Exception exc)
            {
                main.callbackLog("friend:: Spam Error ->  " + exc);
            }
        }