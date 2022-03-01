using DiskCardGame;
using InscryptionAPI.Card;
using System;
using System.Collections.Generic;
using System.Text;

namespace InscryptionAPI.Dialogue;
public static class DialogueExtensions
{
    public static DialogueLineSetBuilder<DialogueBuilder<T>> CreateMainLineSet<T>(this DialogueBuilder<T> dialogueBuilder)
    {
        DialogueEvent.LineSet lineSet = new DialogueEvent.LineSet();
        dialogueBuilder.evt.mainLines = lineSet;
        return new DialogueLineSetBuilder<DialogueBuilder<T>>(dialogueBuilder, lineSet);
    }

    public static DialogueLineSetsBuilder<DialogueBuilder<T>> CreateRepeatLineSets<T>(this DialogueBuilder<T> dialogueBuilder)
    {
        List<DialogueEvent.LineSet> lineSet = new List<DialogueEvent.LineSet>();
        dialogueBuilder.evt.repeatLines = lineSet;
        return new DialogueLineSetsBuilder<DialogueBuilder<T>>(dialogueBuilder, lineSet);
    }

    public static DialogueBuilder<T> SetMaxRepeatsBehaviour<T>(this DialogueBuilder<T> dialogueBuilder, DialogueEvent.MaxRepeatsBehaviour behaviour)
    {
        dialogueBuilder.evt.maxRepeatsBehaviour = behaviour;
        return dialogueBuilder;
    }

    public static DialogueLineSetBuilder<DialogueLineSetsBuilder<T>> CreateLineSet<T>(this DialogueLineSetsBuilder<T> dialogueLineSetBuilder)
    {
        DialogueEvent.LineSet lineSet = new DialogueEvent.LineSet();
        dialogueLineSetBuilder.lineSets.Add(lineSet);
        return new DialogueLineSetBuilder<DialogueLineSetsBuilder<T>>(dialogueLineSetBuilder, lineSet);
    }

    public static DialogueLineSetBuilder<T> AddLine<T>(this DialogueLineSetBuilder<T> dialogueLinesBuilder, string text,
        TextDisplayer.LetterAnimation letterAnimation = TextDisplayer.LetterAnimation.None,
        Emotion emotion = Emotion.Neutral,
        P03AnimationController.Face p03Face = P03AnimationController.Face.Default, int speakerIndex = 0,
        string specialInstruction = null, StoryEvent storyCondition = 0, bool storyConditionMustBeMet = false)
    {
        dialogueLinesBuilder.lineSet.lines.Add(new DialogueEvent.Line()
        {
            text = text,
            letterAnimation = letterAnimation,
            emotion = emotion,
            p03Face = p03Face,
            speakerIndex = speakerIndex,
            specialInstruction = specialInstruction,
            storyCondition = storyCondition,
            storyConditionMustBeMet = storyConditionMustBeMet
        });
        return dialogueLinesBuilder;
    }

    public static T Build<T>(this DialogueBuilder<T> dialogueBuilder)
    {
        DialogueManager.Add(dialogueBuilder.evt.id, dialogueBuilder.evt);
        return dialogueBuilder.rv;
    }

    public static T Build<T>(this DialogueLineSetsBuilder<T> dialogueLineSetsBuilder)
    {
        return dialogueLineSetsBuilder.rv;
    }

    public static T Build<T>(this DialogueLineSetBuilder<T> dialogueLineSetBuilder)
    {
        return dialogueLineSetBuilder.rv;
    }
}

public class DialogueBuilder<T>
{
    string id;
    internal T rv;
    internal DialogueEvent evt;

    public DialogueBuilder(T rv, string id)
    {
        this.id = id;
        this.rv = rv;
        evt = new DialogueEvent();
        evt.id = id;
        // TODO: Multi-speaker dialogue support
        evt.speakers = new List<DialogueEvent.Speaker>() { GenericPaperTalkingCard.DefaultSpeaker };
    }

}

public class DialogueLineSetsBuilder<T>
{
    internal T rv;
    internal List<DialogueEvent.LineSet> lineSets;

    public DialogueLineSetsBuilder(T rv, List<DialogueEvent.LineSet> lineSets)
    {
        this.rv = rv;
        this.lineSets = lineSets;
    }
}

public class DialogueLineSetBuilder<T>
{
    internal T rv;
    internal DialogueEvent.LineSet lineSet;

    public DialogueLineSetBuilder(T rv, DialogueEvent.LineSet lineSet)
    {
        this.rv = rv;
        this.lineSet = lineSet;
    }
}
