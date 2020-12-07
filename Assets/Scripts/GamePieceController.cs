using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePieceController : MonoBehaviour
{
    // Start is called before the first frame update

    public Sprite cowboySprt;
    public Sprite ghostSprt;
    public Sprite priestessSprt;
    public Sprite rebelSprt;
    public Sprite wandererSprt;
    public Sprite berserkerSprt;
    public Sprite guardSprt;
    public Sprite builderSprt;
    public Sprite swordsmanSprt;

    public Sprite sprt0;
    public Sprite sprt1;
    public Sprite sprt2;
    public Sprite sprt3;
    public Sprite sprt4;
    public Sprite sprt5;
    public Sprite sprt6;
    public Sprite sprt7;
    public Sprite sprt8;
    public Sprite sprt9;
    public Sprite sprt10;
    public Sprite sprt10p;

    private int population;

    private PlayerTurn owner;
    private GameController controller;

    private GameObject[] chipSprites = new GameObject[5];
    private GameObject[] slotNumbers = new GameObject[5];

    private CharacterClassBase[] pieces = new CharacterClassBase[5]; //keeps track of what
    private int[] numInSlot = new int[] { 0, 0, 0, 0, 0 }; //keeps track of how many

    private void Awake()
    {
        //numInSlot = new int[] { 0, 0, 0, 0, 0 };
        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

        population = 0;
        owner = controller.playerTurn;
        int i = 0;
        foreach (Transform child in transform)
        {
            //print(child.gameObject.name);
            chipSprites[i] = child.gameObject;
            slotNumbers[i] = child.transform.GetChild(0).gameObject;
            i++;
        }
        setupNumbers();
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
        updateSlotNumbers();
    }

    public void populateSlot(CharacterClassBase piece)
    {
        //print("222" + switchNum);

        int slot = population;

        for(int i = 0; i < pieces.Length; i++)
        {
            if (pieces[i] == piece)
            {
                numInSlot[i] += 1;
                print("return");
                return;
            }
        }
        print(population);

        pieces[slot] = piece;

        if (population < 5)
        {
            //print(slot);
            numInSlot[slot] += 1;
        }
        
        switch (piece.getName())
        {
            case "Cowboy":
                //print("333");
                chipSprites[slot].GetComponent<SpriteRenderer>().sprite = cowboySprt;
                break;
            case "Ghost":
                chipSprites[slot].GetComponent<SpriteRenderer>().sprite = ghostSprt;
                break;
            case "Priestess":
                chipSprites[slot].GetComponent<SpriteRenderer>().sprite = priestessSprt;
                break;
            case "Rebel":
                chipSprites[slot].GetComponent<SpriteRenderer>().sprite = rebelSprt;
                break;
            case "Wanderer":
                chipSprites[slot].GetComponent<SpriteRenderer>().sprite = wandererSprt;
                break;
            case "Berserker":
                chipSprites[slot].GetComponent<SpriteRenderer>().sprite = berserkerSprt;
                break;
            case "Guard":
                print("g");
                chipSprites[slot].GetComponent<SpriteRenderer>().sprite = guardSprt;
                break;
            case "Builder":
                print("b");
                chipSprites[slot].GetComponent<SpriteRenderer>().sprite = builderSprt;
                break;
            case "Swordsman":
                print("s");
                chipSprites[slot].GetComponent<SpriteRenderer>().sprite = swordsmanSprt;
                break;
            default:
                break;
        }

        population++;
    }

    private void setupNumbers()
    {

        int i = 0;
        foreach (GameObject slot in slotNumbers)
        {
            SpriteRenderer sprtRend = slot.GetComponent<SpriteRenderer>();
            switch (numInSlot[i])
            {
                case 0:
                    sprtRend.sprite = sprt0;
                    break;
                case 1:
                    sprtRend.sprite = sprt1;
                    break;
                case 2:
                    sprtRend.sprite = sprt2;
                    break;
                case 3:
                    sprtRend.sprite = sprt3;
                    break;
                case 4:
                    sprtRend.sprite = sprt4;
                    break;
                case 5:
                    sprtRend.sprite = sprt5;
                    break;
                case 6:
                    sprtRend.sprite = sprt6;
                    break;
                case 7:
                    sprtRend.sprite = sprt7;
                    break;
                case 8:
                    sprtRend.sprite = sprt8;
                    break;
                case 9:
                    sprtRend.sprite = sprt9;
                    break;
                case 10:
                    sprtRend.sprite = sprt10;
                    break;
                default: //>10
                    sprtRend.sprite = sprt10p;
                    break;
            }
            i++;
        }
        

    }

    private bool updateComplete = true;
    private void updateSlotNumbers()
    {
        if (updateComplete) //make sure it does not spam int i = 0;
        {
            updateComplete = false;
            setupNumbers();
            updateComplete = true;
        }
    }
}
