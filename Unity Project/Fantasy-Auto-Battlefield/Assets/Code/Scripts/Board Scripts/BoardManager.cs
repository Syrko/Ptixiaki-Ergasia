using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    BoardGenerator boardGenerator;
    private void Awake()
    {
        boardGenerator = GetComponentInParent<BoardGenerator>();
    }

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
