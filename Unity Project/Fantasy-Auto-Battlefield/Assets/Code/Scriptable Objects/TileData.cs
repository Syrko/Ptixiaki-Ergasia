using UnityEngine;

/// <summary>
/// Used for storing data about the game's different tile kinds.
/// To create new data right-click in the unity editor and use the menu
/// </summary>
[CreateAssetMenu(fileName = "TileData", menuName = "Tile Data", order = 51)]
public class TileData : ScriptableObject
{
    [SerializeField]
    TileType tileType;
    [SerializeField, TextArea(15, 20)]
    string tileDescription;
    [SerializeField]
    Sprite tileImage;

    public TileType TileType { get => tileType; set => tileType = value; }
    public string TileDescription { get => tileDescription; set => tileDescription = value; }
	public Sprite TileImage { get => tileImage; set => tileImage = value; }
}
