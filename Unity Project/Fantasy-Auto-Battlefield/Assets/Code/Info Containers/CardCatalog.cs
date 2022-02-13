public struct CardCatalog
{
    public const string Soldier = "Soldier";
    public const string Gate = "Gate";
    public const string Strengthen = "Strengthen";

    public static CardType? GetType(string CardName)
    {
        switch (CardName)
        {
            case Soldier:
                return CardType.Unit;
            case Gate:
                return CardType.Building;
                case Strengthen:
                return CardType.Spell;
            default:
                return null;
        }
    }
}


