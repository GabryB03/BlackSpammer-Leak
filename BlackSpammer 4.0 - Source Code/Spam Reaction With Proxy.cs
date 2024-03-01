   public async Task spamReactionProxy(String channelId, String messageId, String emoji, List<String> proxies)
        {
            try
            {
                ulong idd = Convert.ToUInt64(messageId);
                ulong iddd = Convert.ToUInt64(channelId);
            }
            catch (Exception)
            {


                main.callbackLog("reaction::  Spam reaction in " + channelId + ":" + messageId + "  (" + emoji + ") -> Unknown error", ConsoleColor.Red);

            }
            ulong mid = Convert.ToUInt64(messageId);
            ulong chid = Convert.ToUInt64(channelId);
            try
            {
                var random = new Random();
                foreach (String token in tokens)
                {
                    var randomProxyy = proxies[random.Next(proxies.Count)];
                    int indexOf = tokens.IndexOf(token);
                    try
                    {
                        main.reactionBotSet(tokens.IndexOf(token), connected[token].CurrentUser.Id);
                    } catch (Exception)
                    {

                    }
                    main.callbackLog("reaction:: [" + randomProxyy + "] Spamming reaction (Bot= " + indexOf + ")  in " + channelId + ":" + messageId + " with " + token + " (" + emoji + ")", ConsoleColor.Yellow);

                    if(emoji.Contains(":"))
                    {

                        main.callbackLog("reaction:: Emote Found.", ConsoleColor.Green);
                        main.callbackLog("reaction:: (" + indexOf + ") [" + randomProxyy + "] Emote -> " + emoji);

                        aggiungiEmote(channelId, messageId, emoji, token, new WebProxy(randomProxyy.Split(':')[0], int.Parse(randomProxyy.Split(':')[1])));

                        main.doneBot();
                        main.callbackLog("reaction:: Emote() # Bot [" + randomProxyy + "] (" + indexOf + ") -> Request sent.", ConsoleColor.Yellow);

                    }
                    else
                    {
                        var emo = new Emoji(emoji);

                        main.callbackLog("reaction:: (" + indexOf + ") [" + randomProxyy + "] Emoji -> " + emo.Name);

                        aggiungiReazione(channelId, messageId, emo, token, new WebProxy(randomProxyy.Split(':')[0], int.Parse(randomProxyy.Split(':')[1])));

                        main.doneBot();
                        main.callbackLog("reaction:: Bot [" + randomProxyy + "] (" + indexOf + ") -> Request sent.", ConsoleColor.Yellow);
                    }

                }

            }
            catch (Exception)
            {

                main.callbackLog("reaction::  Spam reaction in " + channelId + ":" + messageId + "  (" + emoji + ") -> Unknown error", ConsoleColor.Red);

            }
        }