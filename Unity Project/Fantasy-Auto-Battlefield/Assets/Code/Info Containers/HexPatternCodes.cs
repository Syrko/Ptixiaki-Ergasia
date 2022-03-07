using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enumeration of the different combinations of hexes that make up a targeting pattern for a card.
/// F: Front
/// B: Back
/// L: Left
/// R: Right
/// Values need to be separated by underscores.
/// </summary>
public enum HexPatternCodes
{
    EMPTY,
    FF,
    FF_BL_BB_BR,
    FF_BL_BR,
    FL_FF_FR,
    FL_FF_FR_BL_BB_BR,
    FL_FF_FR_BL_BR,
    FL_FR_BL_BR
}
/// <summary>
/// <c>HexPattern</c> contains static methods that decode the hex pattern codes and return the suitable sprite or return the affcted hexes.
/// </summary>
public class HexPattern
{
    // Created a static instance of the class that will contain the paths to the necessary sprites
    private static HexPattern hexPatternInstance = new HexPattern();

    /// <summary>
    /// Returns the associated sprite for the hex pattern code you provide
    /// </summary>
    public static Sprite getHexPatternSprite(HexPatternCodes code)
    {
        return hexPatternInstance.hexPatternSprites[code];
    }

    /// <summary>
    /// Return the individual hexes in the hex pattern code you provide
    /// </summary>
    /// <returns>Returns an array of strings that contain the hex code broken down (e.g [FF, BB])</returns>
    public static string[] GetHexPatternInStrings(HexPatternCodes code)
    {
        if(code == HexPatternCodes.EMPTY)
        {
            return null;
        }

        return code.ToString().Split('_');
    }

    private Dictionary<HexPatternCodes, Sprite> hexPatternSprites = new Dictionary<HexPatternCodes, Sprite>();
    HexPattern()
    {
        hexPatternSprites.Add(HexPatternCodes.EMPTY, Resources.Load<Sprite>("HexPatternSprites/Empty-Hex-Pattern"));
        hexPatternSprites.Add(HexPatternCodes.FF, Resources.Load<Sprite>("HexPatternSprites/FF"));
        hexPatternSprites.Add(HexPatternCodes.FF_BL_BB_BR, Resources.Load<Sprite>("HexPatternSprites/FF-BL-BB-BR"));
        hexPatternSprites.Add(HexPatternCodes.FF_BL_BR, Resources.Load<Sprite>("HexPatternSprites/FF-BL-BR"));
        hexPatternSprites.Add(HexPatternCodes.FL_FF_FR, Resources.Load<Sprite>("HexPatternSprites/FL-FF-FR"));
        hexPatternSprites.Add(HexPatternCodes.FL_FF_FR_BL_BB_BR, Resources.Load<Sprite>("HexPatternSprites/FL-FF-FR-BL-BB-BR"));
        hexPatternSprites.Add(HexPatternCodes.FL_FF_FR_BL_BR, Resources.Load<Sprite>("HexPatternSprites/FL-FF-FR-BL-BR"));
        hexPatternSprites.Add(HexPatternCodes.FL_FR_BL_BR, Resources.Load<Sprite>("HexPatternSprites/FL-FR-BL-BR"));        
    }
}
