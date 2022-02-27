using UnityEngine;
using System;
using System.Reflection;

public class CardCatalogue
{
    // ============ Units ============
    public static CardCatalogueEntry Soldier = new CardCatalogueEntry("Soldier", CardType.Unit);
    public static CardCatalogueEntry Crabby = new CardCatalogueEntry("Crabby", CardType.Unit);
    public static CardCatalogueEntry Mad_Slug = new CardCatalogueEntry("Mad_Slug", CardType.Unit);
    public static CardCatalogueEntry Yeti = new CardCatalogueEntry("Yeti", CardType.Unit);
    public static CardCatalogueEntry Egg_Thief = new CardCatalogueEntry("Egg_Thief", CardType.Unit);
    public static CardCatalogueEntry Summoner = new CardCatalogueEntry("Summoner", CardType.Unit);
    public static CardCatalogueEntry The_Seventh_Demon = new CardCatalogueEntry("The_Seventh_Demon", CardType.Unit);
    public static CardCatalogueEntry Ghoul = new CardCatalogueEntry("Ghoul", CardType.Unit);
    public static CardCatalogueEntry Egg = new CardCatalogueEntry("Egg", CardType.Unit);
    public static CardCatalogueEntry Healing_Sheep = new CardCatalogueEntry("Healing_Sheep", CardType.Unit);
    public static CardCatalogueEntry Strong_Birb = new CardCatalogueEntry("Strong_Birb", CardType.Unit);
    public static CardCatalogueEntry Little_Imp = new CardCatalogueEntry("Little_Imp", CardType.Unit);
    public static CardCatalogueEntry Attack_Bot = new CardCatalogueEntry("Attack_Bot", CardType.Unit);
    public static CardCatalogueEntry Fishercat = new CardCatalogueEntry("Fishercat", CardType.Unit);
    public static CardCatalogueEntry The_Great_Golem = new CardCatalogueEntry("The_Great_Golem", CardType.Unit);

    // ========== Buildings ==========
    public static CardCatalogueEntry Gate = new CardCatalogueEntry("Gate", CardType.Building);
    public static CardCatalogueEntry Magical_Well = new CardCatalogueEntry("Magical_Well", CardType.Building);
    public static CardCatalogueEntry Guard_Tower = new CardCatalogueEntry("Guard_Tower", CardType.Building);
    public static CardCatalogueEntry Cursed_Ruins = new CardCatalogueEntry("Cursed_Ruins", CardType.Building);

    // =========== SPELLS ============
    public static CardCatalogueEntry Honey_Strength = new CardCatalogueEntry("Honey_Strength", CardType.Spell);
    public static CardCatalogueEntry Magical_Conduit = new CardCatalogueEntry("Magical_Conduit", CardType.Spell);
    public static CardCatalogueEntry Disintegrate = new CardCatalogueEntry("Disintegrate", CardType.Spell);
    public static CardCatalogueEntry Cataclysm = new CardCatalogueEntry("Cataclysm", CardType.Spell);
    public static CardCatalogueEntry Revitalize = new CardCatalogueEntry("Revitalize", CardType.Spell);
    public static CardCatalogueEntry Intoxicate = new CardCatalogueEntry("Intoxicate", CardType.Spell);
    public static CardCatalogueEntry Mask_Of_Control = new CardCatalogueEntry("Mask_Of_Control", CardType.Spell);
    public static CardCatalogueEntry Knowledge_Is_Power = new CardCatalogueEntry("Knowledge_Is_Power", CardType.Spell);

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


