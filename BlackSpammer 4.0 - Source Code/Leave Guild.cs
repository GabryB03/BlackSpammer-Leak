   public async Task leaveGuild(String guildId)
        {
            try
            {
                var http = new HttpClient();
                foreach (String token in tokens)
                {
                    var request = new HttpRequestMessage
                    {

                        RequestUri = new Uri("https://discordapp.com/api/v6/users/@me/guilds/" + guildId),
                        Method = HttpMethod.Delete,
                        Headers =
                {
                    { HttpRequestHeader.Authorization.ToString(), token },
                }

                    };
                    main.callbackLog("(LEAVE) " + token + " from " + guildId + "");
                    await http.SendAsync(request);
                    main.callbackLog("(LEAVE) Done. Ready [" + token + "]");
                }
            }
            catch (Exception)
            {
                throw new Exception("Unable to parse guild id or invaild user");
            }

        }