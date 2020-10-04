## CHANGELOG


-------------

  
**Version Revision 1601697301 (03/10/2020 03:55Z):**  
  
-Cleaned up code some more  
-Begin replay.GameData models  
  
**Version Revision 1601683989 (03/10/2020 12:13Z):**  
  
-Removed (useless) parameters like playerData.Level, as the SeasonLevel is shown in another variable.  
-Code cleanups, switch to using C#9 syntax for null-checking pattern matching and other optimisations.  
-Use (tested and works, but still in beta) - trimming unused assemblies during publish for the portable .exe which reduces the size by ~10MB. Let me know if there are any issues here.  
  
**Version Revision 1601637265 (02/10/2020 11:14Z):**  
  
-Switched to using [unix timestamps][1] for version instead of local date  
-Added color to organise things a bit better - killfeed is in pinkish-red; playerData & cosmetics is in orange-yellow, etc. [Preview picture here..][2]  
-Moved variables for better memory management inside loop, minor code cleanups.  
  
**Version Revision 30/09/2020 08:36:**  
  
-Killfeed overhaul, will now only show events that actually "matter"; all null-references removed (KillFeed will always have a player/bot's ID and no empty/null)  
-Lowercase for all ID's to make cross-referencing them on online-facing API's easier  
-Minor cleanups  
  
  
**Version Revision 30/09/2020 01:31:**  
  
-Initial push to the CDN - implemented playerData, some cosmetics. TODO: playerData -> bMovement (playerData.bMovement)  
-Killfeed implemented with all data. TODO: Cleanup (done in revision 30/09/2020 08:38)  

[1]: https://unixtimestamp.com
[2]: https://dfwk.online/vlexar/fnreplay/color-fnreplay-1601637265.PNG
