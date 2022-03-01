using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace InscryptionAPI.Dialogue;

public class DialogueManager
{
    // TODO: Add editing of existing dialogue
    public static Dictionary<string, DialogueEvent> NewDialogues = new();

    public static void AddAll(Dictionary<string, DialogueEvent> dialogueEvents)
    {
        foreach (KeyValuePair<string, DialogueEvent> pair in dialogueEvents)
        {
            NewDialogues.Add(pair.Key, pair.Value);
        }
    }

    public static void Add(string id, DialogueEvent dialogueEvent)
    {
        NewDialogues.Add(id, dialogueEvent);
    }

    [HarmonyPatch(typeof(DialogueDataUtil.DialogueData), "GetEvent", typeof(string))]
    public class DialogueUtilPatch
    {
        public static bool Prefix(ref DialogueEvent __result, string id)
        {
            DialogueEvent dialogueEvent;
            if (id != null && NewDialogues.TryGetValue(id, out dialogueEvent))
            {
                __result = dialogueEvent;
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
