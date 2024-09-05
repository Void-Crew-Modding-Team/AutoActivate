using Gameplay.Quests;
using HarmonyLib;
using Photon.Pun;
using System.Reflection;

namespace AutoActivate
{
    [HarmonyPatch(typeof(GalaxyMapUIController), "Setup")]
    internal class Patch
    {
        private static FieldInfo _endlessQuestinfo = AccessTools.Field(typeof(GalaxyMapUIController), "_endlessQuest");
        static void Postfix(GalaxyMapUIController __instance)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                if ((EndlessQuest)_endlessQuestinfo.GetValue(__instance) == null)
                {
                    return;
                }
                HubQuestManager.Instance.SelectQuest(((EndlessQuest)_endlessQuestinfo.GetValue(__instance)).QuestParameters);
            }
        }
    }
}
