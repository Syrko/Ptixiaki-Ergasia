public struct CardCatalog
{
    public const string Soldier = "Soldier";
    public const string Gate = "Gate";

    public static CardType? GetType(string CardName)
    {
        switch (CardName)
        {
            case Soldier:
                return CardType.Unit;
            case Gate:
                return CardType.Building;
            default:
                return null;
        }
    }
}


