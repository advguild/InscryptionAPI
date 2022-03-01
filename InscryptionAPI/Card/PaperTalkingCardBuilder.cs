using DiskCardGame;

namespace InscryptionAPI.Card
{
    public class PaperTalkingCardBuilder
    {
        internal GenericPaperTalkingCard talkingCard;
        internal string name;
        internal CardInfo info;

        public PaperTalkingCardBuilder(CardInfo info)
        {
            talkingCard = new GenericPaperTalkingCard();
            name = info.name;
            this.info = info;
            GenericPaperTalkingCard.DialogueIds.Add(name, new Dictionary<TalkingCardExtensions.DialogueTrigger, string>());
            GenericPaperTalkingCard.SpecialOpponentDialogues.Add(name, new Dictionary<Opponent.Type, string>());
        }

        internal string AddDialogueToDictionary(TalkingCardExtensions.DialogueTrigger trigger)
        {
            string id = InscryptionAPIPlugin.ModGUID + "/" + name + "_" + trigger.ToString();
            GenericPaperTalkingCard.DialogueIds[name].Add(trigger, id);
            return id;
        }

        internal string AddDialogueToSpecialOpponentDialogues(Opponent.Type opponent)
        {
            string id = InscryptionAPIPlugin.ModGUID + "/" + name + "_" + opponent.ToString();
            GenericPaperTalkingCard.SpecialOpponentDialogues[name].Add(opponent, id);
            return id;
        }
    }
}