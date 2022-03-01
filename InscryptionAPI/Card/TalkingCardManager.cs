using System;
using System.Collections.ObjectModel;
using System.Reflection;
using APIPlugin;
using DiskCardGame;
using HarmonyLib;

namespace InscryptionAPI.Card
{
    public class TalkingCardManager {
        private static readonly ObservableCollection<string> NewTalkingCards = new();

        public static ObservableCollection<string> GetNewTalkingCards()
        {
            return NewTalkingCards;
        }

        public static void Add(string name)
        {
            NewTalkingCards.Add(name);
        }
    }

    [HarmonyPatch(typeof(TalkingCardChooser), "Awake")]
    public class TalkingCardPatch
    {
        public static bool Prefix(ref TalkingCardChooser __instance)
        {

            DiskCardGame.Card c = __instance.gameObject.GetComponentInChildren<DiskCardGame.Card>();
            if (TalkingCardManager.GetNewTalkingCards().Contains(c.Info.name))
            {
                MethodInfo info = c.GetType().GetMethod("AddPermanentBehaviour")
                    .MakeGenericMethod(typeof(GenericPaperTalkingCard));
                info.Invoke(c, new object[] { });
                return false;
            }
            else return true;
        }
    }

    [HarmonyPatch(typeof(CardTriggerHandler), "GetType", typeof(string))]
    public class TriggerTypePatch
    {
        public static bool Prefix(ref Type __result, string typeName)
        {
            if (typeName == typeof(GenericPaperTalkingCard).Name)
            {
                __result = typeof(GenericPaperTalkingCard);
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
