 public async void aggiungiReazione(String channelId, String messageId, Emoji emoji, String token)
        {

            // 
            var http = new HttpClient();
            {
                var request = new HttpRequestMessage
                {

                    RequestUri = new Uri("https://discordapp.com/api/v6/channels/" + channelId + "/messages/" + messageId + "/reactions/" + emoji.Name + "/@me"),
                    Method = HttpMethod.Put,
                    Headers =
                {
                    { HttpRequestHeader.Authorization.ToString(), token },
                }

                };
                main.callbackLog("reaction:: @TOKEN=" + token + " Channel: " + channelId + "..", ConsoleColor.Cyan);
                try
                {
                    await http.SendAsync(request);
                }
                catch (Exception excc)
                {
                    main.callbackLog("reaction:: @TOKEN= " + token + "Error: " + excc, ConsoleColor.Red);
                }
            }

        }