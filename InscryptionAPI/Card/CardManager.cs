using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using DiskCardGame;
using HarmonyLib;
using UnityEngine;

namespace InscryptionAPI.Card;

[HarmonyPatch]
public static class CardManager
{
    public static readonly ReadOnlyCollection<CardInfo> BaseGameCards = new(Resources.LoadAll<CardInfo>("Data/Cards"));
    private static readonly ObservableCollection<CardInfo> NewCards = new();
    
    public static event Func<List<CardInfo>, List<CardInfo>> ModifyCardList;

    public static void SyncCardList()
    {
        var cards = BaseGameCards.Concat(NewCards).Select(x => CardLoader.Clone(x)).ToList();
        //var cards = BaseGameCards.Concat(NewCards).ToList();
        AllCardsCopy = ModifyCardList?.Invoke(cards) ?? cards;
    }

    static CardManager()
    {
        InscryptionAPIPlugin.ScriptableObjectLoaderLoad += static type =>
        {
            if (type == typeof(CardInfo))
            {
                ScriptableObjectLoader<CardInfo>.allData = AllCardsCopy;
            }
        };
        NewCards.CollectionChanged += static (_, _) =>
        {
            SyncCardList();
        };
    }

    public static List<CardInfo> AllCardsCopy { get; private set; } = BaseGameCards.ToList();

    public static void Add(CardInfo newCard) { if (!NewCards.Contains(newCard)) NewCards.Add(newCard); }
    public static void Remove(CardInfo card) => NewCards.Remove(card);

    public static CardInfo New(string name, string displayName, int attack, int health, string description = default(string), bool addToPool = true)
    {
        CardInfo retval = ScriptableObject.CreateInstance<CardInfo>();
        retval.name = name;
        retval.SetBasic(displayName, attack, health, description);

        // Go ahead and add this as well
        if (addToPool)
            Add(retval);

        return retval;
    }
}