using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // ================================
    // Testing Variables
    // --------------------------------
    List<string> testingDeck = new List<string> { CardNames.Soldier, CardNames.Soldier, CardNames.Gate };
    int maxHP = 10;
    int maxMana = 10;
    int maxHandSize = 5;
    // ================================

    HumanPlayer player;
    HumanPlayer opponent; // TODO change type to AIPlayer

    private void Start()
    {
        Initialize();
    }

    void Initialize()
    {
        player = new HumanPlayer(testingDeck, maxHP, maxHandSize, maxMana);
        opponent = new HumanPlayer(testingDeck, maxHP, maxHandSize, maxMana);
    }

    void SwapInitiative()
    {

    }
}
