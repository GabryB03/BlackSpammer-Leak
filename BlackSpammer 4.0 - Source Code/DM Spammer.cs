        public async void dmUserSpam(String message, String userId, List<String> proxies)
        {
            userdmm = true;
            var random = new Random();
            //token, recipient
            var recipients = new Dictionary<string, string>();

#pragma warning disable CS4014 // Non è possibile attendere la chiamata, pertanto l'esecuzione del metodo corrente continuerà prima del completamento della chiamata
           Task.Run(async () =>
            {
                try
                {

                    foreach (String token in tokens)
                    {
                        var proxyy = proxies[random.Next(proxies.Count)];
                        var wproxy = new WebProxy(proxyy.Split(':')[0], int.Parse(proxyy.Split(':')[1]));
                        main.callbackLog("dm:: token position (" + tokens.IndexOf(token) + ") (" + proxyy + ") [" + token + "] Creating DM with " + userId + "..", ConsoleColor.Cyan);
                        Task.Run(async () =>
                        {
                            string recipient = await createDM(token, userId, wproxy, 5000);
                            if (recipient == null || recipient == "")
                            {
                                main.callbackLog("dm:: token position (" + tokens.IndexOf(token) + ") (" + proxyy + ") [" + token + "] DM Fail -> Recipient is null. Removing..", ConsoleColor.Red);
                                recipients.Add(token, "");
                            }
                            else
                            {
                                try
                                {
                                    string regToken = token;
                                    var handler = new HttpClientHandler();
                                    handler.UseProxy = true;
                                    handler.Proxy = wproxy;
                                    var http = new HttpClient(handler);
                                    string messageJson = "";
                                    messageJson = "{\"content\":\"Spam powered by BlackSpammer: " + message + "\"}";
                                    main.callbackLog("dm_spam:: token (" + regToken + ") spam in dm, sending json -> " + messageJson, ConsoleColor.Green);
                                    var str_con = new StringContent(messageJson, Encoding.UTF8, "application/json");

                                    var authReq = new HttpRequestMessage
                                    {

                                        RequestUri = new Uri("https://discord.com/api/v6/channels/" + recipient + "/messages"),
                                        Content = str_con,
                                        Headers = {
                            { HttpRequestHeader.ContentType.ToString(), "application/json" },
                                                    { HttpRequestHeader.Authorization.ToString(), regToken }
                    },
                                        Method = HttpMethod.Post

                                    };
                                    Task.Run(async () =>
                                    {
                                        var e = await http.SendAsync(authReq);
                                        var content = await e.Content.ReadAsStringAsync();
                                        main.callbackLog("dm_spam:: token (" + regToken + ") spam in dm -> " + content, ConsoleColor.Cyan);
                                        main.callbackLog("dm_spam:: Proxy (token -> " + regToken + ") is " + proxyy, ConsoleColor.DarkCyan);
                                    });
                                }
                                catch (Exception exc)
                                {
                                    main.callbackLog("dm_spam:: token (" + token + ") spam in dm error -> Proxy dead or error.", ConsoleColor.Red);
                                }
                                recipients.Add(token, recipient);                        
                            }
                        });
                        continue;

                    }
                }
                catch (Exception exc)
                {
                    main.callbackLog("dm:: Unknown error -> " + exc, ConsoleColor.Red);
                }

            });
#pragma warning restore CS4014 // Non è possibile attendere la chiamata, pertanto l'esecuzione del metodo corrente continuerà prima del completamento della chiamata

        }