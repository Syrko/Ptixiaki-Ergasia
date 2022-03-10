using System.Linq;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Provides access to the Board Generator and a static method to translate coordinates of array into 
/// coordinates of 3D board.
/// </summary>
public class BoardManager : MonoBehaviour
{
    BoardGenerator boardGenerator;
    private void Awake()
    {
        boardGenerator = GetComponentInParent<BoardGenerator>();
    }

    /// <summary>
    /// Takes the (x, y) coordinates of a hextile on the game board array and translates them into
    /// a Vector3 as coordinates for the 3D board
    /// </summary>
    /// <param name="x">The width dimension of the hex's coordinates</param>
    /// <param name="y">The depth dimension of the hex's coordinates</param>
    /// <param name="height">The height of the hex on the 3D world</param>
    /// <returns>A Vector3 used by a gameobject to be placed on the 3D board</returns>
    public static Vector3 TranslateCoordinates(int x, int y, float height)
    {
        Vector3 coords;
        if (x % 2 == 0)
        {
            coords = new Vector3(x * HexDimensions.R * 1.5f, height, y * 2 * HexDimensions.r);
        }
        else
        {
            coords = new Vector3(x * HexDimensions.R * 1.5f, height, HexDimensions.r + 2 * y * HexDimensions.r);
        }
        return coords;
    }
}
