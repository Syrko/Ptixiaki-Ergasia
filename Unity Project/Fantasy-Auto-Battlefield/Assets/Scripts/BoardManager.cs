using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [Header("Board Variables")]
    [SerializeField, Tooltip("Width of the board in hexes")]
    int boardWidth = 5;
    [SerializeField, Tooltip("Depth of the board in hexes")]
    int boardDepth = 8;
    [Space(5)]
    [SerializeField, Tooltip("The height difference between hexes of different height steps")]
    float hexHeightModifier = 0.2f;
    [SerializeField, Tooltip("Delay between the hexes' reveal during board construction")]
    float hexRevealDelay = 0.2f;
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


    GameObject[,] boardArray;

	private void Start()
	{
        ConstructBoard();
	}

	private void ConstructBoard()
	{
        boardArray = new GameObject[boardDepth, boardWidth];
        List<TileType> tileTypePool = PopulateRandomTilesPool();
        InstantiateHexTiles(tileTypePool);
    }

    /// <summary>
	/// Populates board array by determining randomly the type of tiles of the board and 
    /// creating HexTile objects to fill the array
	/// </summary>
    private List<TileType> PopulateRandomTilesPool()
	{
        int nonBaseTilesCount = boardDepth * boardWidth - boardWidth * 2;
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

    private List<TileType> RandomizeTilePool(List<TileType> tileTypePool)
	{
        List<TileType> tempPool = new List<TileType>();
        for(int i = 0; i < boardWidth; i++)
		{
            tempPool.Add(TileType.Base);
        }

        // Shuffle the generated terrain types
        tileTypePool = tileTypePool.OrderBy(rng => Random.value).ToList();
        foreach(TileType type in tileTypePool)
		{
            tempPool.Add(type);
		}

        for (int i = 0; i < boardWidth; i++)
        {
            tempPool.Add(TileType.Base);
        }

        return tempPool;
    }

    private void InstantiateHexTiles(List<TileType> tileTypePool)
    {
        for (int d = 0; d < boardDepth; d++)
        {
            for (int w = 0; w < boardWidth; w++)
            {
                // Determine the position of the next hex in the 3D Scene's grid
                Vector3 nextHexPos;
                if (w % 2 == 0)
                {
                    nextHexPos = new Vector3(w * HexDimensions.R * 1.5f, 0, d * 2 * HexDimensions.r);
                }
                else
                {
                    nextHexPos = new Vector3(w * HexDimensions.R * 1.5f, 0, HexDimensions.r + 2 * d * HexDimensions.r);
                }

				switch (tileTypePool[0])
				{
					case TileType.Base:
                        boardArray[d, w] = Instantiate(baseTile, nextHexPos, Quaternion.identity);
						break;
					case TileType.Desert:
                        boardArray[d, w] = Instantiate(desertTile, nextHexPos, Quaternion.identity);
                        break;
					case TileType.Forest:
                        boardArray[d, w] = Instantiate(forestTile, nextHexPos, Quaternion.identity);
                        break;
					case TileType.Hills:
                        boardArray[d, w] = Instantiate(hillsTile, nextHexPos, Quaternion.identity);
                        break;
					case TileType.Lake:
                        boardArray[d, w] = Instantiate(lakeTile, nextHexPos, Quaternion.identity);
                        break;
					case TileType.Plains:
                        boardArray[d, w] = Instantiate(plainsTile, nextHexPos, Quaternion.identity);
                        break;
				}
                boardArray[d, w].GetComponent<HexTile>().Initialize();
                tileTypePool.RemoveAt(0);
            }
		}
	}

}
