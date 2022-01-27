using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BoardUI : MonoBehaviour
{
    [SerializeField]
    GameObject opponentHP;
    [SerializeField]
    GameObject playerHP;
    [SerializeField]
    GameObject playerDeckCounter;
    [SerializeField]
    GameObject initiativeToken;

    Vector3 humanPos = new Vector3(-2f, -0.3f, 0.5f);
    Vector3 AIPos = new Vector3(-2f, -0.3f, 12.5f);

    public TextMeshPro OpponentHP { get => opponentHP.GetComponent<TextMeshPro>(); }
    public TextMeshPro PlayerHP { get => playerHP.GetComponent<TextMeshPro>(); }
    public TextMeshPro PlayerDeckCounter { get => playerDeckCounter.GetComponent<TextMeshPro>(); }
    public GameObject InitiativeToken { get => initiativeToken; }

    public void MoveInitiativeToPlayer(bool isHuman)
    {
        StartCoroutine(SmoothLerp(2f, isHuman));
    }

    private IEnumerator SmoothLerp(float time, bool isHuman)
    {
        Vector3 startingPos;
        Vector3 finalPos;

        if (isHuman)
        {
            startingPos = AIPos;
            finalPos = humanPos;
        }
        else
        {
            startingPos = humanPos;
            finalPos = AIPos;
        }

        float elapsedTime = 0;

        while (elapsedTime < time)
        {
            initiativeToken.transform.position = Vector3.Lerp(startingPos, finalPos, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
