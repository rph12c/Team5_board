using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePieceController : MonoBehaviour
{
    // Start is called before the first frame update

    public Sprite cowboySprt;
    public Sprite ghostSprt;
    public Sprite priestessSprt;
    public Sprite rebelSprt;
    public Sprite wandererSprt;

    private int population;

    private GameObject[] chipSprites = new GameObject[5];

    private void Awake()
    {
        population = 0;
        print("zzz");
        int i = 0;
        foreach (Transform child in transform)
        {
            //print(child.gameObject.name);
            chipSprites[i] = child.gameObject;
            i++;
        }
    }

    void Start()
    {
        
        //chipSprites[0].GetComponent<SpriteRenderer>().sprite = wandererSprt;
        //chipSprites[1].GetComponent<SpriteRenderer>().sprite = cowboySprt;
        //chipSprites[2].GetComponent<SpriteRenderer>().sprite = priestessSprt;
        //chipSprites[3].GetComponent<SpriteRenderer>().sprite = rebelSprt;
        //chipSprites[4].GetComponent<SpriteRenderer>().sprite = ghostSprt;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void populateSlot(int switchNum)
    {
        print("222" + switchNum);
        int slot = population;
        print(population);

        switch (switchNum)
        {
            case 0:
                print("333");
                chipSprites[slot].GetComponent<SpriteRenderer>().sprite = cowboySprt;
                break;
            case 1:
                chipSprites[slot].GetComponent<SpriteRenderer>().sprite = ghostSprt;
                break;
            case 2:
                chipSprites[slot].GetComponent<SpriteRenderer>().sprite = priestessSprt;
                break;
            case 3:
                chipSprites[slot].GetComponent<SpriteRenderer>().sprite = rebelSprt;
                break;
            case 4:
                chipSprites[slot].GetComponent<SpriteRenderer>().sprite = wandererSprt;
                break;
            default:
                break;
        }

        population++;
    }
}
