    List<String> msgLines = new List<String>(main.GetMessageBox().Lines);
                                            if (msgLines.Count != 1)
                                            {
                                                main.callbackLog("spam:: [ " + nextx + " ]  Found multiple lines.", ConsoleColor.Yellow);
                                                string mmr = "";
                                                foreach (String line in msgLines)
                                                {
                                                    mmr = mmr + " \\u000d" + line;
                                                }
                                                msg = mmr;
                                                main.callbackLog("spam:: [ " + nextx + " ]  Spamming: " + mmr, ConsoleColor.Yellow);
                                            }
                                            string messageJson = "";
                                            if (main.embed)
                                            {
                                                messageJson = "{\"embed\": { \"title\":\"BlackSpammer 4.569 Dev Build\", \"description\":\"" + msg + "\" } }";
                                            }
                                            else
                                            {
                                                if (tts)
                                                {
                                                    messageJson = "{\"content\":\"" + msg + "\", \"tts\": true}";
                                                }
                                                else
                                                {
                                                    messageJson = "{\"content\":\"" + msg + "\"}";
                                                }
                                            }
                                            main.callbackLog("spam:: [ " + nextx + " ] token (" + indexOf + ") spam in " + channelId + " sending json -> " + messageJson, ConsoleColor.Green);
                                            var str_con = new StringContent(messageJson, Encoding.UTF8, "application/json");

                                            var authReq = new HttpRequestMessage
                                            {

                                                RequestUri = new Uri("https://discord.com/api/v6/channels/" + channelId + "/messages"),
                                                Content = str_con,
                                                Headers = {
                                            { HttpRequestHeader.ContentType.ToString(), "application/json" },
                                            { HttpRequestHeader.Authorization.ToString(), token }
                                            },
                                                Method = HttpMethod.Post

                                            };
                                            var e = await http.SendAsync(authReq);
                                            var content = await e.Content.ReadAsStringAsync();
                                            if (content.ToLower().Contains("rate"))
                                            {
                                                main.callbackLog("spam:: [ " + nextx + " ]  token (" + indexOf + ") spam in " + channelId + " -> Proxy is currently rate limited" + content, ConsoleColor.DarkYellow);
                                            }
                                            if (content.Contains("Missing"))
                                            {
                                                main.callbackLog("spam:: [ " + nextx + " ]  token (" + indexOf + ") spam in " + channelId + " -> Missing Access (Banned or not joined)" + content, ConsoleColor.DarkYellow);
                                            }
                                            if (content.Contains("You need to verify your account in order to perform this action"))
                                            {
                                                main.callbackLog("spam:: [ " + nextx + " ]  token (" + indexOf + ") spam in " + channelId + " -> Dead Token (Unverified)" + content, ConsoleColor.DarkYellow);
                                            }
                                            if (content.Contains("Unauthorized"))
                                            {
                                                main.callbackLog("spam:: [ " + nextx + " ]  token (" + indexOf + ") spam in " + channelId + " -> Dead Token (401/Invalid)" + content, ConsoleColor.Red);
                                            }
                                            if (content.Contains("slowmode"))
                                            {
                                                main.callbackLog("spam:: [ " + nextx + " ]  token (" + indexOf + ") spam in " + channelId + " -> Warning: SlowMode" + content, ConsoleColor.DarkYellow);
                                            }
                                            if (content.Contains("Bad Request"))
                                            {
                                                main.callbackLog("spam:: [ " + nextx + " ]  token (" + indexOf + ") spam in " + channelId + " -> Invalid Message (Please change it in attack manager)." + content, ConsoleColor.DarkYellow);
                                            }
                                            main.callbackLog("spam:: [ " + nextx + " ]  token (" + indexOf + ") spam in " + channelId + " -> " + content, ConsoleColor.Cyan);
                                            main.callbackLog("spam:: [ " + nextx + " ]  Proxy (token -> " + indexOf + ") is " + nextx, ConsoleColor.DarkCyan);
                                        }
                                        catch (Exception exc)
                                        {
                                            if (("" + exc).ToLower().Contains("task"))
                                            {


                                            }
                                            else
                                            {

                                                if (("" + exc).ToLower().Contains("socket"))
                                                {
                                                    main.callbackLog("spam:: [ " + nextx + " ]  token (" + indexOf + ") spam in " + channelId + " error -> { " + nextx + " :: PROXY FAIL }", ConsoleColor.Red);
                                                }
                                                else
                                                {
                                                    main.callbackLog("spam:: [ " + nextx + " ]  token (" + indexOf + ") spam in " + channelId + " error -> " + exc, ConsoleColor.Red);
                                                }

                                            }
                                        }

                                    });