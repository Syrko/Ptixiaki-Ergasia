using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    protected int frontline;
    protected bool hasInitiative;

    protected int maxHP;
    protected int currentHP;

    public bool HasInitiative { get => hasInitiative; set => hasInitiative = value; }
    public int Frontline { get => frontline; }
    public int CurrentHP { get => currentHP; }

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
