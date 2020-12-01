using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameMode { SelectTerritory, SelectAdjacent };
public enum PlayerTurn { Attack, Defense };

public class GameController : MonoBehaviour
{
    
    public GameMode gameMode;
    public GameObject selectedSpace;
    private GameObject[] currentAdjSpaces;

    private static GameController sharedController;
    public static GameController SharedController
    {
        get
        {
            if(sharedController == null)
            {
                GameObject go = new GameObject("GameController");
                go.AddComponent<GameController>();
            }
            return sharedController;
        }

    }

    
    public PlayerTurn playerTurn;
    // Start is called before the first frame update

    private void Awake()
    {
        sharedController = this;
    }

    void Start()
    {

        sharedController = this;

        //print("Begin");
        gameMode = GameMode.SelectTerritory;
        playerTurn = PlayerTurn.Attack;
        selectedSpace = null;

    }

    // Update is called once per frame

    private bool didStartParticles = false;
    private bool didStopParticles = false;
    void Update()
    {
        switch(gameMode)
        {
            case GameMode.SelectTerritory:

                didStartParticles = false; //reset bool

                if (selectedSpace != null && !didStopParticles)
                {
                    didStopParticles = true;
                    selectedSpace = null;
                    foreach (GameObject space in currentAdjSpaces)
                    {
                        space.GetComponent<TerritoryHandler>().stopParticle();
                    }
                }
                break;
            case GameMode.SelectAdjacent:

                didStopParticles = false; //reset bool

                if (selectedSpace != null && !didStartParticles)
                {
                    didStartParticles = true;
                    print("should start particles");
                    currentAdjSpaces = selectedSpace.GetComponent<TerritoryHandler>().adjacentTerritories;
                    foreach (GameObject space in selectedSpace.GetComponent<TerritoryHandler>().adjacentTerritories)
                    {
                        space.GetComponent<TerritoryHandler>().playParticle();
                    }
                }
                break;
        }
    }
}
