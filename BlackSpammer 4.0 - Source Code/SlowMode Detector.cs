   if (main.SelectedAttack.Contains("SlowMode"))
                                {
                                    if (!main.SelectedAttack.Contains(" "))
                                    {
                                        main.callbackLog("slow:: Slowmode is enabled with default value. Awaiting timeout: 100 milliseconds", ConsoleColor.Magenta);
                                        await Task.Run(async () => Thread.Sleep(100));
                                    }
                                    else
                                    {

                                        var ee = main.SelectedAttack.Split(' ');
                                        var ii = ee[1];
                                        var iii = int.Parse(ii);
                                        main.callbackLog("slow:: Slowmode is enabled with a custom value. Awaiting timeout: " + iii + " milliseconds.", ConsoleColor.Magenta);
                                        await Task.Run(async () => Thread.Sleep(iii));
                                    }

                                }