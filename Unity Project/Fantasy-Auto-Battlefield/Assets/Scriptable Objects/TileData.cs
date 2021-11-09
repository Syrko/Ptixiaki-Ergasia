using UnityEngine;

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
