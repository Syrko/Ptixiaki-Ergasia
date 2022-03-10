using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class BoardGenerator : MonoBehaviour
{
    private GameObject[,] board = null;
    public GameObject[,] Board { get { return board; } }

    [Header("Board Variables")]
    [SerializeField, Tooltip("Width of the board in hexes")]
    int boardWidth = 5;
    [SerializeField, Tooltip("Depth of the board in hexes")]
    int boardDepth = 8;
    [Space(10)]

    [Header("Terrain Percentages - must add up to 100")]
    [SerializeField]
    float desertPercentage = 10f;
    [SerializeField]
    float hillsPercentage = 20f;
    [SerializeField]
    float lakePercentage = 10f;
    [SerializeField]
    float forestPercentage = 35f;
    [SerializeField]
    float plainsPercentage = 25f;
    [Space(10)]

    [Header("Hex Tiles Prefabs")]
    [SerializeField] private GameObject baseTile;
    [SerializeField] private GameObject desertTile;
    [SerializeField] private GameObject forestTile;
    [SerializeField] private GameObject hillsTile;
    [SerializeField] private GameObject lakeTile;
    [SerializeField] private GameObject plainsTile;

    public int BoardWidth { get => boardWidth; }
    public int BoardDepth { get => boardDepth; }

    private void Awake()
    {
        ConstructBoard();
    }

    /// <summary>
    /// Constructs the board used in the current game session
    /// </summary>
    private void ConstructBoard()
    {
        board = new GameObject[BoardDepth, BoardWidth];
        List<TileType> tileTypePool = PopulateRandomTilesPool();
        InstantiateHexTiles(tileTypePool);
    }

    /// <summary>
	/// Populates board array by determining randomly the type of tiles of the board and 
    /// creating HexTile objects to fill the array
	/// </summary>
    private List<TileType> PopulateRandomTilesPool()
    {
        int nonBaseTilesCount = BoardDepth * BoardWidth - BoardWidth * 2;
        List<TileType> tileTypePool = new List<TileType>(nonBaseTilesCount);

        // Determine the number of hexes for each type of terrain based on the given percentages
        int desertCount = Mathf.FloorToInt(tileTypePool.Capacity * desertPercentage / 100f);
        int hillsCount = Mathf.FloorToInt(tileTypePool.Capacity * hillsPercentage / 100f);
        int lakeCount = Mathf.FloorToInt(tileTypePool.Capacity * lakePercentage / 100f);
        int forestCount = Mathf.FloorToInt(tileTypePool.Capacity * forestPercentage / 100f);
        int plainsCount = nonBaseTilesCount - desertCount - hillsCount - lakeCount - forestCount;

        // Fill the pool with the number of tiles of each type as determined above in a random order
        // except for the base tiles
        FillTilePool(tileTypePool, desertCount, TileType.Desert);
        FillTilePool(tileTypePool, hillsCount, TileType.Hills);
        FillTilePool(tileTypePool, lakeCount, TileType.Lake);
        FillTilePool(tileTypePool, forestCount, TileType.Forest);
        FillTilePool(tileTypePool, plainsCount, TileType.Plains);

        return RandomizeTilePool(tileTypePool);
    }

    /// <summary>
	/// Generates the required count of terrain types for the given type and adds them to a list
	/// </summary>
	/// <param name="tileTypePool">Empty list of Tile Types to return the result</param>
	/// <param name="tileTypeCount">Count of the required Tile Type</param>
	/// <param name="type">The required Tile Type</param>
	private void FillTilePool(List<TileType> tileTypePool, int tileTypeCount, TileType type)
    {
        for (int c = 0; c < tileTypeCount; c++)
        {
            tileTypePool.Add(type);
        }
    }

    /// <summary>
    /// Shuffles the pool of hex tiles available for the board's creation
    /// </summary>
    private List<TileType> RandomizeTilePool(List<TileType> tileTypePool)
    {
        List<TileType> tempPool = new List<TileType>();
        for (int i = 0; i < BoardWidth; i++)
        {
            tempPool.Add(TileType.Base);
        }

        // Shuffle the generated terrain types
        tileTypePool = tileTypePool.OrderBy(rng => Random.value).ToList();
        foreach (TileType type in tileTypePool)
        {
            tempPool.Add(type);
        }

        for (int i = 0; i < BoardWidth; i++)
        {
            tempPool.Add(TileType.Base);
        }

        return tempPool;
    }

    /// <summary>
    /// Instantiates the gameobjects used to represent the 3D board
    /// </summary>
    private void InstantiateHexTiles(List<TileType> tileTypePool)
    {
        for (int d = 0; d < BoardDepth; d++)
        {
            for (int w = 0; w < BoardWidth; w++)
            {
                // Determine the position of the next hex in the 3D Scene's grid
                Vector3 nextHexPos = BoardManager.TranslateCoordinates(w, d, 0);

                switch (tileTypePool[0])
                {
                    case TileType.Base:
                        board[d, w] = Instantiate(baseTile, nextHexPos, Quaternion.identity);
                        break;
                    case TileType.Desert:
                        board[d, w] = Instantiate(desertTile, nextHexPos, Quaternion.identity);
                        break;
                    case TileType.Forest:
                        board[d, w] = Instantiate(forestTile, nextHexPos, Quaternion.identity);
                        break;
                    case TileType.Hills:
                        board[d, w] = Instantiate(hillsTile, nextHexPos, Quaternion.identity);
                        break;
                    case TileType.Lake:
                        board[d, w] = Instantiate(lakeTile, nextHexPos, Quaternion.identity);
                        break;
                    case TileType.Plains:
                        board[d, w] = Instantiate(plainsTile, nextHexPos, Quaternion.identity);
                        break;
                }
                board[d, w].GetComponent<HexTile>().InitializeCoords(d, w);
                tileTypePool.RemoveAt(0);
            }
        }
    }
}
