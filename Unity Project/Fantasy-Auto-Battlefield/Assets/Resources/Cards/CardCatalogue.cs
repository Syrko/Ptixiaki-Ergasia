using UnityEngine;
using System;
using System.Reflection;

public class CardCatalogue
{
    // ============ Units ============
    public static CardCatalogueEntry Soldier = new CardCatalogueEntry("Soldier", CardType.Unit);
    public static CardCatalogueEntry Crabby = new CardCatalogueEntry("Crabby", CardType.Unit);

    // ========== Buildings ==========
    public static CardCatalogueEntry Gate = new CardCatalogueEntry("Gate", CardType.Building);

    // =========== SPELLS ============
    public static CardCatalogueEntry Strengthen = new CardCatalogueEntry("Strengthen", CardType.Spell);

    public static CardType? GetType(string cardName)
    {
        CardCatalogueEntry entry = typeof(CardCatalogue).GetField(cardName).GetValue(null) as CardCatalogueEntry;
        if (entry.CardName == cardName)
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


