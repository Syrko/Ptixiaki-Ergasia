using UnityEngine;
using System;

public class CardCatalogue : MonoBehaviour
{
    // ============ Units ============
    public static CardCatalogueEntry Soldier = new CardCatalogueEntry("Soldier", CardType.Unit);
    public static CardCatalogueEntry Crabby = new CardCatalogueEntry("Crabby", CardType.Unit);

    // ========== Buildings ==========
    public static CardCatalogueEntry Gate = new CardCatalogueEntry("Gate", CardType.Building);

    // =========== SPELLS ============
    public static CardCatalogueEntry Strengthen = new CardCatalogueEntry("Strengthen", CardType.Spell);

    private void Awake()
    {
        instance = this;
    }

    private static CardCatalogue instance;
    public static CardType? GetType(string cardName)
    {
        CardCatalogueEntry entry = instance.GetType().GetProperty(cardName).GetValue(instance) as CardCatalogueEntry;
        if(entry.CardName == cardName)
        {
            return entry.CardType;
        }
        else
        {
            return null;
        }
    }

    public class CardCatalogueEntry
    {
        private string cardName;
        private CardType cardType;

        public string CardName { get => cardName; }
        public CardType CardType { get => cardType; }

        public CardCatalogueEntry(string cardName, CardType cardType)
        {
            this.cardName = cardName;
            this.cardType = cardType;
        }
    }
}


