using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <c>Player</c> is a monobeahaviour from which the <c>HumanPlayer</c> and <c>AIPlayer</c> inherit.
/// It contains some shared functionality such as the taking of damage.
/// </summary>
public class Player : MonoBehaviour
{
    protected int frontline;
    protected bool hasInitiative;

    protected int maxHP;
    protected int currentHP;

    public bool HasInitiative { get => hasInitiative; set => hasInitiative = value; }
    public int Frontline { get => frontline; }
    public int CurrentHP { get => currentHP; }

    /// <summary>
    /// The player takes the given amount of damage.
    /// It also checks for the end of the game, by checking if at least one of the players reaches 0 hitpoints.
    /// </summary>
    public void TakeDamage(int amount)
    {
        currentHP -= amount;
        if (currentHP <= 0)
        {
            currentHP = 0;
            FindObjectOfType<GameManager>().EndGame();
        }

        // Update UI
        if (this is HumanPlayer)
        {
            SubjectUI.Notify(this.gameObject, new UIEvent(EventUICodes.PLAYER_HP_CHANGED, currentHP.ToString()));
        }
        else if (this is AIPlayer)
        {
            SubjectUI.Notify(this.gameObject, new UIEvent(EventUICodes.OPPONENT_HP_CHANGED, currentHP.ToString()));
        }
    }

    /// <summary>
    /// The player heals the given amount of hitpoints up to the maximum
    /// </summary>
    public void HealSelf(int amount)
    {
        currentHP += amount;
        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }

        // Update UI
        if (this is HumanPlayer)
        {
            SubjectUI.Notify(this.gameObject, new UIEvent(EventUICodes.PLAYER_HP_CHANGED, currentHP.ToString()));
        }
        else if (this is AIPlayer)
        {
            SubjectUI.Notify(this.gameObject, new UIEvent(EventUICodes.OPPONENT_HP_CHANGED, currentHP.ToString()));
        }
    }
}
