using HarmonyLib;
using System;

namespace SearchFixes.HarmonyPatches
{
    [HarmonyPatch(typeof(BeatmapLevelFilterModel))]
    [HarmonyPatch("LevelContainsText", MethodType.Normal)]
    internal class LevelContainsTextPatch
    {
        private static bool Prefix(IPreviewBeatmapLevel beatmapLevel, string[] searchTexts, ref bool __result)
        {
            int l_Words = 0;
            int l_Matches = 0;

            string l_SongName = beatmapLevel.songName.Replace(" ", "").Replace("_", "");
            string l_SongSubName = beatmapLevel.songSubName.Replace(" ", "").Replace("_", "");
            string l_SongAuthorName = beatmapLevel.songAuthorName.Replace(" ", "").Replace("_", "");
            string l_LevelAuthorName = beatmapLevel.levelAuthorName.Replace(" ", "").Replace("_", "");


            for (int l_I = 0; l_I < searchTexts.Length; l_I++)
            {
                if (!string.IsNullOrWhiteSpace(searchTexts[l_I]))
                {
                    l_Words++;
                    string l_SearchTerm = searchTexts[l_I].Replace("_", "");

                    if (
                        l_LevelAuthorName.IndexOf(l_SearchTerm, 0, StringComparison.InvariantCultureIgnoreCase) != -1 ||
                        l_SongName.IndexOf(l_SearchTerm, 0, StringComparison.InvariantCultureIgnoreCase) != -1 ||
                        l_SongSubName.IndexOf(l_SearchTerm, 0, StringComparison.InvariantCultureIgnoreCase) != -1 ||
                        l_SongAuthorName.IndexOf(l_SearchTerm, 0, StringComparison.InvariantCultureIgnoreCase) != -1)
                    {
                        l_Matches++;
                    }
                }
            }
            
            if (l_Matches == l_Words && l_Matches != 0)
            {
                __result = true;
            }
            else
            {
                __result = false;
            }

            return false;
        }
    }
}