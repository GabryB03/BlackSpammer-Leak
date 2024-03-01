  public async Task joinServerProxy(String invito, String token, WebProxy proxy)
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.PreAuthenticate = true;
            handler.UseProxy = true;
            handler.Proxy = proxy;
            HttpClient http;
            try
            {
                String invitoCorretto = invito.Replace("https://discord.gg/", "").Replace("https://discord.com/", "").Replace("https://discord.com/invite/", "").Replace("https://discordapp.com/invite/", "").Replace(" ", "");

                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri("https://discord.com/api/v6/invite/" + invitoCorretto),
                    Method = HttpMethod.Post,
                    Headers = {
        { HttpRequestHeader.Authorization.ToString(), token },
        { HttpRequestHeader.ContentType.ToString(), "multipart/mixed" },
        },
                };
                try
                {
                    main.setInviteCurrent(tokens.IndexOf(token), connected[token].CurrentUser.Id);
                } catch (Exception)
                {

                }
                main.callbackLog("join:: (" + proxy.Address.Host + ":" + proxy.Address.Port + ") Joining into " + invito + " with token " + token + " (" + proxy.Address.Host + ":" + proxy.Address.Port + ")", ConsoleColor.Cyan);
                if (!main.MultiThread23)
                {
#pragma warning disable CS4014 // Non è possibile attendere la chiamata, pertanto l'esecuzione del metodo corrente continuerà prima del completamento della chiamata
                    Task.Run(async () =>
                    {
                        if (main.fst)
                        {
                            http = new HttpClient();
                        }
                        else
                        {
                            http = new HttpClient(handler);
                        }
                        var tt = await http.SendAsync(request);
                        var ttt = await tt.Content.ReadAsStringAsync();
                        ConsoleColor cl = ConsoleColor.Green;
                        if (ttt.Contains("error code: 1015") || ttt.Contains("rate"))
                        {
                            cl = ConsoleColor.DarkYellow;
                            main.callbackLog("join:: Join (" + proxy.Address.Host + ":" + proxy.Address.Port + ") [" + invitoCorretto + "] @token=" + token + " (" + proxy.Address.Host + ":" + proxy.Address.Port + ") -> This proxy is currently rate limited (" + ttt + ")", cl);
                            return;
                        }
                        var tttt = ttt.ToLower();
                        if (tttt.Contains("unauthorized") || tttt.Contains("verify") || tttt.Contains("rate") || tttt.Contains("banned"))
                        {
                            cl = ConsoleColor.Red;

                        }
                        if (tttt.Contains("new_member"))
                        {
                            cl = ConsoleColor.Green;
                        }
                        main.callbackLog("join:: (" + proxy.Address.Host + ":" + proxy.Address.Port + ") Join [" + invitoCorretto + "] @token=" + token + " (" + proxy.Address.Host + ":" + proxy.Address.Port + ") -> " + ttt, cl);
                    });
                } else
                {

                    new Thread(p =>
                    {


                        if (main.fst)
                        {
                            http = new HttpClient();
                        }
                        else
                        {
                            http = new HttpClient(handler);
                        }
                        var tt = http.SendAsync(request).Result;
                        var ttt = tt.Content.ReadAsStringAsync().Result;
                        ConsoleColor cl = ConsoleColor.Green;
                        if (ttt.Contains("error code: 1015") || ttt.Contains("rate"))
                        {
                            cl = ConsoleColor.DarkYellow;
                            main.callbackLog("join:: Join (" + proxy.Address.Host + ":" + proxy.Address.Port + ") [" + invitoCorretto + "] @token=" + token + " (" + proxy.Address.Host + ":" + proxy.Address.Port + ") -> This proxy is currently rate limited (" + ttt + ")", cl);
                            return;
                        }
                        var tttt = ttt.ToLower();
                        if (tttt.Contains("unauthorized") || tttt.Contains("verify") || tttt.Contains("rate") || tttt.Contains("banned"))
                        {
                            cl = ConsoleColor.Red;

                        }
                        if (tttt.Contains("new_member"))
                        {
                            cl = ConsoleColor.Green;
                        }
                        main.callbackLog("join:: (" + proxy.Address.Host + ":" + proxy.Address.Port + ") Join [" + invitoCorretto + "] @token=" + token + " (" + proxy.Address.Host + ":" + proxy.Address.Port + ") -> " + ttt, cl);

                    }).Start();

                }
                
#pragma warning restore CS4014 // Non è possibile attendere la chiamata, pertanto l'esecuzione del metodo corrente continuerà prima del completamento della chiamata
                main.doneInvite();
            }
            catch (Exception)
            {
                main.callbackLog("join:: Join Fail [" + invito + "] @token=" + token + " (" + proxy.Address.Host + ":" + proxy.Address.Port + ") -> Proxy dead or error", ConsoleColor.Red);
            }
        }
