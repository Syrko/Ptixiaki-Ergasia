using System;
using System.Collections;
using UnityEngine;

public class Unit : Spawnable
{
    public static readonly float MOVE_DURATION = 1f;

    public void InitializeUnitPawn(string unitName, bool forPlayer)
    {
        UnitCardData data = UnitCardData.GetUnitDataFromName(unitName);
        cardName = data.CardName;
        cardCost = data.CardCost;
        originalCardCost = data.CardCost;
        cardText = data.CardText;
        cardType = data.CardType;
        attack = data.Attack;
        originalAttack = data.Attack;
        defense = data.Defense;
        originalDefense = data.Defense;
        maxHitPoints = data.MaxHitPoints;
        currentHP = maxHitPoints;
        attackPattern = data.AttackPattern;
        cardImage = data.CardImage;

        if (forPlayer)
            originalOwner = FindObjectOfType<HumanPlayer>();
        else
            originalOwner = FindObjectOfType<AIPlayer>();
        owner = originalOwner;

        cardMaterial = Resources.Load<Material>("Cards/Units/" + unitName + "/" + unitName);
        transform.Find("Image").GetComponent<MeshRenderer>().material = cardMaterial;

        InitializePawnUI();
    }

    public void Move(Vector2Int currentPos)
    {
        if (owner is HumanPlayer)
        {
            StartCoroutine(MovePlayerPawn(MOVE_DURATION, currentPos));
        }
        else if (owner is AIPlayer)
        {
            StartCoroutine(MoveAIPawn(MOVE_DURATION, currentPos));
        }
    }

    
    private void OnMouseDown()
    {
        SubjectUI.Notify(this.gameObject, new UIEvent(EventUICodes.CARD_INFO_CHANGED));
    }

    private IEnumerator MovePlayerPawn(float time, Vector2Int currentPos)
    {
        Vector3 startingPos = transform.position;
        Vector3 finalPos = BoardManager.TranslateCoordinates(currentPos.y, currentPos.x + 1, transform.position.y);

        float elapsedTime = 0;

        while (elapsedTime < time)
        {
            transform.position = Vector3.Lerp(startingPos, finalPos, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator MoveAIPawn(float time, Vector2Int currentPos)
    {
        Vector3 startingPos = transform.position;
        Vector3 finalPos = BoardManager.TranslateCoordinates(currentPos.y, currentPos.x - 1, transform.position.y);

        float elapsedTime = 0;

        while (elapsedTime < time)
        {
            transform.position = Vector3.Lerp(startingPos, finalPos, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
