using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuBackground : MonoBehaviour
{
    [SerializeField]
    Image[] BGImages;

    [SerializeField]
    Sprite[] BGSprites;

    int imageCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChangeCards());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ChangeCards()
    {
        while (true)
        {
            BGImages[imageCounter].sprite = BGSprites[Random.Range(0, BGSprites.Length)];
            imageCounter = (imageCounter + 1) % 4;
            yield return new WaitForSeconds(1.5f);
        }
    }
}
