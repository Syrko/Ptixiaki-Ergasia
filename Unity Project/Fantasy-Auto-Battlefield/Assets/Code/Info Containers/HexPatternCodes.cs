using System;
using System.Collections.Generic;
using UnityEngine;

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

public class HexPattern
{
    private static HexPattern hexPatternInstance = new HexPattern();
    public static Sprite getHexPatternSprite(HexPatternCodes code)
    {
        return hexPatternInstance.hexPatternSprites[code];
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
