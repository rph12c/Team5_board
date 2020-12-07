using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Select first spot, Select spot next to first spot
public enum GameMode { SelectTerritory, SelectAdjacent};

//Placing piece on board, Selecting own space, Selecting space to attack
public enum Action { Placement, SelectOwn, AttackSetup, DefenseSetup};

public enum PlayerTurn { Attack, Defense};

public enum DefensePlacement { Troop, Trap};

public enum TerritoryHolder { Attack, Defense, Neutral};

public class GameController : MonoBehaviour
{
    
    public GameMode gameMode;
    public Action action;
    public GameObject selectedSpace;

    private TerritoryHandler spawnSpace;

    private GameObject[] currentAdjSpaces;
    private GameObject playerTurnSign;

    private GameObject canvas;
    private GameObject nextUpSignPanel;
    private GameObject nextUpSignSpriteObject;
    private Image nextPieceSprite;
    private GameObject turnsLeftSign;

    private int turnsLeft;

    public CharacterClassBase onBoardPiece;
    

    private bool defenseDidSetTraps = false;

    //private static GameController sharedController;
    //public static GameController SharedController
    //{
    //    get
    //    {
    //        if(sharedController == null)
    //        {
    //            GameObject go = new GameObject("GameController");
    //            go.AddComponent<GameController>();
    //        }
    //        return sharedController;
    //    }

    //}

    
    public PlayerTurn playerTurn;
    public DefensePlacement defensePlacement;
    // Start is called before the first frame update

    private void Awake()
    {
        //sharedController = this;
    }

    void Start()
    {

        //sharedController = this;

        print("Begin");
        gameMode = GameMode.SelectTerritory;
        action = Action.DefenseSetup;
        playerTurn = PlayerTurn.Defense;
        selectedSpace = null;
        playerTurnSign = GameObject.Find("PlayersTurnSign");
        canvas = GameObject.Find("UICanvas");

        nextUpSignPanel = canvas.transform.GetChild(2).gameObject;
        nextUpSignSpriteObject = nextUpSignPanel.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject;
        nextPieceSprite = nextUpSignSpriteObject.GetComponent<Image>();
        turnsLeftSign = canvas.transform.GetChild(1).gameObject;

        nextUpSignPanel.SetActive(false);

        turnsLeft = 20;
        updateTurnsSign();

        defenseTroopPlacement = 0;
        defenseTrapPlacement = 0;
        attackSpecialistNum = 0;
        hasGuard = false;
        hasBerserker = false;

    }

    private void updateTurnsSign()
    {
        turnsLeftSign.transform.GetChild(0).gameObject.GetComponent<Text>().text = turnsLeft.ToString();
    }

    // Update is called once per frame

    private bool didStartParticles = false;
    private bool didStopParticles = false;
    private bool didShowPlayerTurnSign = false;

    private bool isFinishedShowingSign = false;
    private bool finishedSetup = false;
    private bool hasPickedNext = false;

    void Update()
    {

        //HANDLE PIECE MOVING
        if (isMovingPiece)
        {
            GameObject piece = this.selectedSpace.GetComponent<TerritoryHandler>().getPopulatedPiece();
            piece.transform.position = Vector3.Lerp(piece.transform.position, newPiecePosition, Time.deltaTime * 4);
        }

        //END HANDLE PIECE MOVING

        //HANDLE TURN SIGN
        switch (playerTurn)
        {
            case PlayerTurn.Attack:
                if (!didShowPlayerTurnSign)
                {
                    presentTurnSign(PlayerTurn.Attack);
                }
                break;
            case PlayerTurn.Defense:
                if (!didShowPlayerTurnSign)
                {
                    print("updateSign");
                    if (finishedSetup)
                    {
                        turnsLeft--; //finished one whole turn
                        updateTurnsSign();
                    }
                    presentTurnSign(PlayerTurn.Defense);
                }
                break;
        }
        //END HANDLE TURN SIGN

        if (isFinishedShowingSign)
        {
            //SETUP
            if (action == Action.DefenseSetup)
            {
                if (!hasPickedNext)
                {
                    populateUpNext();
                }
                nextUpSignPanel.SetActive(true);
                //print("Defender, choose your five territories");
                if (defensePlacement == DefensePlacement.Troop)
                {
                    //print(defenseTroopPlacement);
                    if (defenseTroopPlacement == 5)
                    {
                        print("toTraps");
                        defensePlacement = DefensePlacement.Trap;
                    }
                } else if (defensePlacement == DefensePlacement.Trap)
                {
                    if (defenseTrapPlacement == 3)
                    {
                        nextUpSignPanel.SetActive(false);
                        hasPickedNext = false;
                        print("toAttack");
                        defensePlacement = DefensePlacement.Troop;
                        switchPlayer();
                        action = Action.AttackSetup;
                        playerTurn = PlayerTurn.Attack;
                        spawnSpace = GameObject.FindGameObjectWithTag("Respawn").GetComponent<TerritoryHandler>();
                        spawnSpace.playParticle();
                        
                    }
                }
                
            }
            if (action == Action.AttackSetup)
            {
                if (!hasPickedNext)
                {
                    populateUpNext();
                }
                nextUpSignPanel.SetActive(true);
                //print("Attacker, choose your one territory");
                if (attackHasPlacedTroop)
                {
                    nextUpSignPanel.SetActive(false);
                    print("troop placed");
                    spawnSpace.stopParticle();
                    action = Action.SelectOwn;
                    gameMode = GameMode.SelectTerritory;
                    finishedSetup = true;
                    //switchPlayer();
                    
                    
                }
            }

            //END SETUP

            switch (gameMode)
            {
                case GameMode.SelectTerritory:

                    didStartParticles = false; //reset bool
                    if (selectedSpace != null && !didStopParticles) //if deselecting selected space
                    {
                        stopAdjacentParticles();
                    }
                    break;

                case GameMode.SelectAdjacent:

                    didStopParticles = false; //reset bool

                    if (selectedSpace != null && !didStartParticles) //this is immediately after a space is selected in SelectTerritory state
                    {
                        startAdjacentParticles();
                    }
                    break;
            }
        }
        else //if sign is still showing, we want to pause everything else   
        {
            //stopAdjacentParticles();
        }

    }

    //HELPER FUNCTIONS

    public void switchPlayer()
    {
        if (playerTurn == PlayerTurn.Attack)
        {
            playerTurn = PlayerTurn.Defense;
        } else
        {
            playerTurn = PlayerTurn.Attack;
        }
        didShowPlayerTurnSign = false;
    }

    private void presentTurnSign(PlayerTurn player)
    {
        didShowPlayerTurnSign = true;
        isFinishedShowingSign = false;
        PlayerTurnSignController signControl = playerTurnSign.GetComponent<PlayerTurnSignController>();
        signControl.changeSign(player);
        Invoke("signFinished", 8.0f);
    }

    private void signFinished()
    {
        print("SignFinished");
        isFinishedShowingSign = true;
    }

    private void startAdjacentParticles()
    {
        
        didStartParticles = true;
        print("should start particles");
        currentAdjSpaces = selectedSpace.GetComponent<TerritoryHandler>().adjacentTerritories;
        foreach (GameObject space in selectedSpace.GetComponent<TerritoryHandler>().adjacentTerritories) //start adjacent space particles
        {
            space.GetComponent<TerritoryHandler>().playParticle();
        }
        
    }

    private void stopAdjacentParticles()
    {
        
        didStopParticles = true;
        selectedSpace = null;
        foreach (GameObject space in currentAdjSpaces) //stop all adjacent space particles
        {
            space.GetComponent<TerritoryHandler>().stopParticle();
        }
        
    }


    private int defenseTroopPlacement;
    public void incrementDefenseTroopPlacementCount()
    {
        hasPickedNext = false;
        defenseTroopPlacement += 1;
                    
    }

    public int getDefenseTroopValue()
    {
        return defenseTroopPlacement;
    }


    private int defenseTrapPlacement;
    public void incrementDefenseTrapPlacementCount()
    {
        hasPickedNext = false;
        defenseTrapPlacement += 1;
        //print("trap++" + defenseTrapPlacement);
    }

    private int attackSpecialistNum;
    private bool hasGuard;
    private bool hasBerserker;

    public int getTrapValue()
    {
        return defenseTrapPlacement;
    }

    private bool attackHasPlacedTroop;
    public void setAttackPlacedTroop()
    {
        //print("set true");
        hasPickedNext = false;
        attackHasPlacedTroop = true;
    }

    public CharacterClassBase getUpNext()
    {
        return onBoardPiece;
    }

    private void populateUpNext()
    {
        hasPickedNext = true;
        UpNextController nextControl = nextUpSignSpriteObject.GetComponent<UpNextController>();

        if (playerTurn == PlayerTurn.Defense)
        {
            if (defenseTroopPlacement < 5)
            {

                switch (defenseTroopPlacement)
                {
                    case 0:
                        nextPieceSprite.sprite = nextControl.cowboySprt;
                        onBoardPiece = new Cowboy();
                        break;
                    case 1:
                        nextPieceSprite.sprite = nextControl.ghostSprt;
                        onBoardPiece = new Ghost();
                        break;
                    case 2:
                        nextPieceSprite.sprite = nextControl.priestessSprt;
                        onBoardPiece = new Priestess();
                        break;
                    case 3:
                        nextPieceSprite.sprite = nextControl.rebelSprt;
                        onBoardPiece = new Rebel();
                        break;
                    case 4:
                        nextPieceSprite.sprite = nextControl.wandererSprt;
                        onBoardPiece = new Wanderer();
                        break;
                    default:
                        break;

                }
            }
            else
            {
                switch (defenseTrapPlacement)
                {
                    case 0:
                        onBoardPiece = null;
                        nextPieceSprite.sprite = nextControl.barricadeSprt;
                        break;
                    case 1:
                        nextPieceSprite.sprite = nextControl.firebombSprt;
                        break;
                    case 2:
                        nextPieceSprite.sprite = nextControl.groundbombSprt;
                        break;
                    case 3:
                        nextUpSignPanel.SetActive(false);
                        break;
                    default:
                        break;
                }
            }
        } else
        {
            int rnd = (int)UnityEngine.Random.Range(0f, 15f);
            if (attackSpecialistNum != 2 && rnd == 14)
            {
                if (hasGuard && !hasBerserker)
                {
                    nextPieceSprite.sprite = nextControl.berserkerSprt;
                    onBoardPiece = new Berserker();
                } else if (hasBerserker && !hasGuard)
                {
                    nextPieceSprite.sprite = nextControl.guardSprt;
                    onBoardPiece = new Guard();
                } else
                {
                    int specialRnd = (int)UnityEngine.Random.Range(0f, 2f);
                    if (specialRnd == 0)
                    {
                        nextPieceSprite.sprite = nextControl.berserkerSprt;
                        onBoardPiece = new Berserker();
                    } else
                    {
                        nextPieceSprite.sprite = nextControl.guardSprt;
                        onBoardPiece = new Guard();
                    }
                }
            } else
            {
                if (rnd % 2 == 0)
                {
                    nextPieceSprite.sprite = nextControl.builderSprt;
                    print("builder");
                    onBoardPiece = new Builder();
                } else
                {
                    nextPieceSprite.sprite = nextControl.swordsmanSprt;
                    print("sword");
                    onBoardPiece = new Swordsman();
                }
            }
        }
        
        
    }

    private Vector3 newPiecePosition;
    private bool isMovingPiece = false;
    public void movePiece(Vector3 newPosition)
    {
        newPiecePosition = newPosition;
        isMovingPiece = true;
        Invoke("toggleIsMovingPiece", 1.5f);
        //stopAdjacentParticles();

    }

    private void toggleIsMovingPiece()
    {
        isMovingPiece = false;
        stopAdjacentParticles();
        switchPlayer();
        gameMode = GameMode.SelectTerritory;
    }

    
}
