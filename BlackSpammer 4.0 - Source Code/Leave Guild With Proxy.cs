  public async void leaveGuild(String guildId, List<String> proxies)
        {
            
            try
            {
                var random = new Random();
                var randomProxy = proxies[random.Next(proxies.Count)];
                var http = new HttpClient();
                foreach (String token in tokens)
                {
                    try {
                        var request = new HttpRequestMessage
                        {

                            RequestUri = new Uri("https://discordapp.com/api/v6/users/@me/guilds/" + guildId),
                            Method = HttpMethod.Delete,
                            Headers =
                {
                    { HttpRequestHeader.Authorization.ToString(), token },
                }

                        };
                        main.callbackLog("leave:: (" + randomProxy + ") @TOKEN= " + token + " from " + guildId + " using proxy..");
                        if (!main.MultiThread23)
                        {
#pragma warning disable CS4014 // Non è possibile attendere la chiamata, pertanto l'esecuzione del metodo corrente continuerà prima del completamento della chiamata
                            Task.Run(async () =>
                            {
                                var eeee = await http.SendAsync(request);
                                var uu = await eeee.Content.ReadAsStringAsync();
                                main.callbackLog("leave:: (" + randomProxy + ") @TOKEN= " + token + " from " + guildId + " -> " + uu, ConsoleColor.Yellow);
                                if (uu == "")
                                {
                                    main.callbackLog("leave:: (" + randomProxy + ") @TOKEN= " + token + " from " + guildId + " -> Successfully leaved." + uu, ConsoleColor.Green);
                                }
                            });
                        } else
                        {

                            new Thread(p =>
                            {

                                var eeee = http.SendAsync(request).Result;
                                var uu = eeee.Content.ReadAsStringAsync().Result;
                                main.callbackLog("leave:: (" + randomProxy + ") @TOKEN= " + token + " from " + guildId + " -> " + uu, ConsoleColor.Yellow);
                                if (uu == "")
                                {
                                    main.callbackLog("leave:: (" + randomProxy + ") @TOKEN= " + token + " from " + guildId + " -> Successfully leaved." + uu, ConsoleColor.Green);
                                }

                            }).Start();

                        }
#pragma warning restore CS4014 // Non è possibile attendere la chiamata, pertanto l'esecuzione del metodo corrente continuerà prima del completamento della chiamata
                    } catch(Exception uue)
                    {
                        main.callbackLog("leave:: (" + randomProxy + ") @TOKEN= " + token + " from " + guildId + " -> ERROR: " + uue, ConsoleColor.Red);
                    }
                    }
            }
            catch (Exception exc)
            {
                main.callbackLog("leave:: Leave Error: " + exc, ConsoleColor.Red);
            }

        }