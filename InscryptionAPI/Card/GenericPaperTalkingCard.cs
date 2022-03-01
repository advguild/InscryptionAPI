using DiskCardGame;
using System;
using System.Collections.Generic;
using System.Text;
using static InscryptionAPI.Card.TalkingCardExtensions;

namespace InscryptionAPI.Card
{

    internal class GenericPaperTalkingCard : PaperTalkingCard
    {
        internal static Dictionary<string, Dictionary<DialogueTrigger, string>> DialogueIds = new();
        internal static Dictionary<string, Dictionary<Opponent.Type, string>> SpecialOpponentDialogues = new();

        internal string Name => base.Card.Info.name;

        public static DialogueEvent.Speaker DefaultSpeaker => DialogueEvent.Speaker.Single;

        public override DialogueEvent.Speaker SpeakerType => DefaultSpeaker;

        public override string OnDrawnDialogueId => GetDialogueFor(Name, DialogueTrigger.OnDrawn);

        public override string OnDrawnFallbackDialogueId => GetDialogueFor(Name, DialogueTrigger.OnDrawnFallback);

        public override string OnPlayFromHandDialogueId => GetDialogueFor(Name, DialogueTrigger.OnPlayFromHand);

        public override string OnAttackedDialogueId => GetDialogueFor(Name, DialogueTrigger.OnAttacked);

        public override string OnBecomeSelectablePositiveDialogueId => GetDialogueFor(Name, DialogueTrigger.OnBecomeSelectablePositive);

        public override string OnBecomeSelectableNegativeDialogueId => GetDialogueFor(Name, DialogueTrigger.OnBecomeSelectableNegative);

        public override string OnSacrificedDialogueId => GetDialogueFor(Name, DialogueTrigger.OnSacrificed);

        public override string OnSelectedForCardMergeDialogueId => GetDialogueFor(Name, DialogueTrigger.OnSelectedForCardMerge);

        public override string OnSelectedForCardRemoveDialogueId => GetDialogueFor(Name, DialogueTrigger.OnSelectedForCardRemove);

        public override string OnSelectedForDeckTrialDialogueId => GetDialogueFor(Name, DialogueTrigger.OnSelectedForDeckTrial);

        public override string OnDiscoveredInExplorationDialogueId => GetDialogueFor(Name, DialogueTrigger.OnDiscoveredInExploration);

        public override Dictionary<Opponent.Type, string> OnDrawnSpecialOpponentDialogueIds => GetSpecialOpponentDialoguesFor(Name);

        private string GetDialogueFor(string name, DialogueTrigger trigger)
        {
            Dictionary<DialogueTrigger, string> dict;
            if (DialogueIds.TryGetValue(name, out dict))
            {
                string id;
                if (dict.TryGetValue(trigger, out id)) {
                    return id;
                }
                return "";
            }
            return "";
        }
        private Dictionary<Opponent.Type, string> GetSpecialOpponentDialoguesFor(string name)
        {
            Dictionary<Opponent.Type, string> dict;
            if (SpecialOpponentDialogues.TryGetValue(name, out dict))
            {
                return dict;
            }
            return new Dictionary<Opponent.Type, string>();
        }
    }
}
