using DiskCardGame;
using InscryptionAPI.Dialogue;
using System.Collections.Generic;

namespace InscryptionAPI.Card;
public static class TalkingCardExtensions
{
    public enum DialogueTrigger
    {
        OnDrawn,
        OnDrawnFallback,
        OnPlayFromHand,
        OnAttacked,
        OnBecomeSelectablePositive,
        OnBecomeSelectableNegative,
        OnSacrificed,
        OnSelectedForCardMerge,
        OnSelectedForCardRemove,
        OnSelectedForDeckTrial,
        OnDiscoveredInExploration,
        OnDrawnSpecialOpponent
    }

    public static PaperTalkingCardBuilder CreatePaperTalkingCard(this CardInfo info)
    {
        if (!info.SpecialAbilities.Contains(SpecialTriggeredAbility.TalkingCardChooser))
        {
            info.specialAbilities.Add(SpecialTriggeredAbility.TalkingCardChooser);
        }
        TalkingCardManager.Add(info.name);
        return new PaperTalkingCardBuilder(info);
    }

    public static CardInfo Build(this PaperTalkingCardBuilder talkingCard)
    {
        return talkingCard.info;
    }

    public static DialogueBuilder<PaperTalkingCardBuilder> CreateDialogueLine(this PaperTalkingCardBuilder talkingCard, DialogueTrigger trigger)
    {
        string id = talkingCard.AddDialogueToDictionary(trigger);
        return new DialogueBuilder<PaperTalkingCardBuilder>(talkingCard, id);
    }

    public static DialogueBuilder<PaperTalkingCardBuilder> CreateSpecialOpponentDialogueLines(this PaperTalkingCardBuilder talkingCard, Opponent.Type opponent)
    {
        string id = talkingCard.AddDialogueToSpecialOpponentDialogues(opponent);
        return new DialogueBuilder<PaperTalkingCardBuilder>(talkingCard, id);
    }
}

