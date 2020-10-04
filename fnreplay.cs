using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using FortniteReplayReader;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Pastel;
using Unreal.Core.Models.Enums;

namespace vlexar
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection().AddLogging(loggingBuilder => loggingBuilder.AddConsole().SetMinimumLevel(LogLevel.Error));
            var provider = serviceCollection.BuildServiceProvider();
            var logger = provider.GetService<ILogger<Program>>();
            var desktopFolder = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            var replayFilesFolder = Path.Combine(desktopFolder, @"Replays");
            var replayFiles = Directory.EnumerateFiles(replayFilesFolder, "*.replay");
            var sw = new Stopwatch();
            var reader = new ReplayReader(logger, ParseMode.Full);
            long total = 0;
            foreach (var replayFile in replayFiles)
            {
                var count_player = 0;
                var count_bot = 0;
                var pPSN = 0;
                var pXBL = 0;
                var pWIN = 0;
                var pSWT = 0;
                var pIOS = 0;
                var pMAC = 0;
                var pAND = 0;
                var pMISC = 0;
                sw.Restart();
                try
                {
                    var replay = reader.ReadReplay(replayFile);
                    foreach (var playerData in replay.PlayerData)
                    {
                        if (playerData.EpicId is not null)
                        {
                            count_player++;
                            var playerFeedStuff = $"EpicId: {playerData.EpicId.ToString().ToLower()}, IsBot?: {playerData.IsBot}, Placement #: {playerData.Placement}, Platform: {playerData.Platform}, SeasonLevel: {playerData.SeasonLevelUIDisplay}, PlayerNumber: {playerData.PlayerNumber}, TeamIndex: {playerData.TeamIndex}, TeamKills: {playerData.TeamKills}, IsThePartyLeader?: {playerData.IsPartyLeader}, HasThankedBusDriver?!: {playerData.HasThankedBusDriver}, IsUsingStreamerMode?: {playerData.IsUsingStreamerMode},  IsUsingAnonymousMode?: {playerData.IsUsingAnonymousMode}, DC'ed?: {playerData.Disconnected}, PlayerKills: {playerData.Kills}, Death Time: {playerData.DeathTime}, Reboot Counter: {playerData.RebootCounter}, CurrentWeapon (code): {playerData.CurrentWeapon}";
                            Console.WriteLine(playerFeedStuff.Pastel(Color.FromArgb(255, 165, 0)));
                            Console.WriteLine($" ");
                            Console.WriteLine($"--------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                            var playerDataCosmetics = $"PlayerContrail: {playerData.Cosmetics.SkyDiveContrail}, Pickaxe: {playerData.Cosmetics.Pickaxe}, PlayerCharacter: {playerData.Cosmetics.Character}, Parts: {playerData.Cosmetics.Parts}, Glider: {playerData.Cosmetics.Glider}";
                            Console.WriteLine(playerDataCosmetics.Pastel(Color.FromArgb(255, 165, 0)));
                            switch (playerData.Platform)
                            {
                                case "PSN":
                                    pPSN++;
                                    break;
                                case "XBL":
                                    pXBL++;
                                    break;
                                case "WIN":
                                    pWIN++;
                                    break;
                                case "SWT":
                                    pSWT++;
                                    break;
                                case "IOS":
                                    pIOS++;
                                    break;
                                case "MAC":
                                    pMAC++;
                                    break;
                                case "AND":
                                    pAND++;
                                    break;
                                default:
                                    pMISC++;
                                    break;
                            }
                        }

                        if (playerData.BotId is not null)
                        {
                            count_bot++;
                        }
                    }

                    foreach (var cunt in replay.KillFeed)
                    {
                        if (cunt.PlayerName is not null && cunt.FinisherOrDownerName is not null)
                        {
                            var pName = cunt?.PlayerName.ToLower();
                            var whoKilled = cunt?.FinisherOrDownerName.ToLower();
                            var killFeedStuffCunt = $"Player: {pName}, IsBot: {cunt.PlayerIsBot}, IsDowned: {cunt.IsDowned}, DeathType (Code): {cunt.DeathCause}, DeathLocation: [{cunt.DeathLocation}], WhoKilled?: {whoKilled}, WasFinisherOrDownerAFknBot?: {cunt.FinisherOrDownerIsBot}, TimeAlive: {cunt.ReplicatedWorldTimeSeconds}, Was Revived?: {cunt.IsRevived}";
                            Console.WriteLine($" ");
                            Console.WriteLine(killFeedStuffCunt.Pastel(Color.FromArgb(219, 112, 147)));
                        }
                    }

                    /* Start game data stuff */

                    Console.WriteLine($" ");
                    var gameDataStuff = $" MapInfo: {replay.GameData.MapInfo}, TotalBots: {replay.GameData.TotalBots}, TotalTeams: {replay.GameData.TotalTeams}, WinningTeamID: {replay.GameData.WinningTeam}, TotalPlayerStructures: {replay.GameData.TotalPlayerStructures}, SafeZoneStartTime: {replay.GameData.SafeZonesStartTime}, MatchEndTime: {replay.GameData.MatchEndTime}";
                    Console.WriteLine(gameDataStuff.Pastel(Color.FromArgb(175, 238, 238)));
                    Console.WriteLine($" ");

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }

                string totalCumulativePlayerPlatform = $"PSN: {pPSN}, XBL: {pXBL}, WIN: {pWIN}, SWT: {pSWT}, IOS: {pIOS}, MAC: {pMAC}, AND: {pAND}, MISC: {pMISC}";
                string totalCumulativePlayer = $"Humans: {count_player}, Bots: {count_bot}";
                Console.WriteLine(totalCumulativePlayer.Pastel(Color.FromArgb(0, 250, 154)));
                Console.WriteLine(totalCumulativePlayerPlatform.Pastel(Color.FromArgb(0, 255, 127)));
                Console.WriteLine($"========================================================================================================".Pastel(Color.FromArgb(124, 252, 0)));
                sw.Stop();
                Console.WriteLine($"TotalProgramRuntime: {total / 1000} seconds ----".Pastel(Color.FromArgb(75, 0, 130)));
                Console.WriteLine($"---- {replayFile} : done in {sw.ElapsedMilliseconds} milliseconds ----".Pastel(Color.FromArgb(75, 0, 130)));
                total += sw.ElapsedMilliseconds;
            }

            Console.WriteLine($"TOTALS: {total / 1000} seconds ----".Pastel(Color.FromArgb(255, 68, 0)));
            /* Leave these lines for debug
                   * Console.WriteLine($"TOTAL FROM ALL REPLAY FILES ----  Humans: {count_player}, Bots: {count_bot}");
                   * Console.WriteLine($"TOTAL FOR ALL REPLAY FILES ---- PSN: {pPSN}, XBL: {pXBL}, WIN: {pWIN}, SWT: {pSWT}, IOS: {pIOS}, MAC: {pMAC}, AND: {pAND}, MISC: {pMISC}");
                  */
            Console.WriteLine($"========================================================================================================".Pastel(Color.FromArgb(124, 252, 0)));
            Console.WriteLine($"DONE - press any key to end. VERSION: 1601697301".Pastel(Color.FromArgb(255, 68, 0)));
            Console.ReadLine();
        }
    }
}
