using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(ParticleSystem))]
public class TerritoryHandler : MonoBehaviour
{

    private SpriteRenderer sprite;
    private ParticleSystem particle;
    private GameController controller;

    private GameObject populatedPiece;
    private GameObject populatedTrap;
    private TerritoryHolder populatorFaction;

    public Color32 oldColor;
    public Color32 startColor;
    public Color32 hoverColor;
    public Color32 attackColor;
    public GameObject[] adjacentTerritories;
    public GameObject piecePrefab;
    public GameObject trapPrefab;

    // Start is called before the first frame update
    void Awake()
    {
        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

        populatedPiece = null;
        populatedTrap = null;
        populatorFaction = TerritoryHolder.Neutral;
        sprite = GetComponent<SpriteRenderer>();
        particle = GetComponent<ParticleSystem>();
        particle.Stop();
        sprite.color = startColor;
    }

    void OnMouseEnter()
    {
        //print("enter");
       
        switch (controller.gameMode)
        {
            case GameMode.SelectTerritory:
                if (controller.action != Action.AttackSetup)
                {
                    if (this.name != "Blank Space Key")
                    {
                        oldColor = sprite.color;
                        sprite.color = hoverColor;
                        particle.Play();
                    }
                    
                } else
                {
                    if (this.name == "Blank Space Key")
                    {
                        oldColor = sprite.color;
                        sprite.color = hoverColor;
                    }
                }
                break;
            case GameMode.SelectAdjacent:
                oldColor = sprite.color;
                sprite.color = hoverColor;
                break;
        }
        //print(controller.gameMode);
        //switch (controller.gameMode)
        //{
        //    default:
        //        oldColor = sprite.color;
        //        sprite.color = hoverColor;
        //        particle.Play();
        //        break;
        //}
    }

    void OnMouseExit()
    {
        //print("exit");
        switch (controller.gameMode)
        {
            case GameMode.SelectTerritory:
                sprite.color = oldColor;
                particle.Stop();
                break;
            case GameMode.SelectAdjacent:
                sprite.color = oldColor;
                break;
        }
    }

    private void OnMouseDown()
    {
        //print("click");

        
        //print(controller.gameMode);
        
        switch (controller.gameMode)
        {
            case GameMode.SelectTerritory:

                //controller.switchPlayer(); //temp for testing

                if (controller.action == Action.SelectOwn) //If supposed to select own territory
                {
                    controller.selectedSpace = this.gameObject;
                    controller.gameMode = GameMode.SelectAdjacent;
                } else //If supposed to place new troop on territory
                {
                    //if (populatedPiece == null)
                    //{
                        if (controller.playerTurn == PlayerTurn.Defense) //Defense's turn
                        {
                            if (this.name != "Blank Space Key" && this.populatorFaction != TerritoryHolder.Attack)
                            {
                                if (controller.defensePlacement == DefensePlacement.Troop)
                                {
                                    createPiecePrefab();
                                    controller.incrementDefenseTroopPlacementCount();
                                }
                                else
                                {
                                    if (populatedTrap == null)
                                    {
                                    createTrapPrefab();
                                    controller.incrementDefenseTrapPlacementCount();
                                    }
                                }
                            }
                            
                        } else //Attack's turn
                        {
                            if (this.gameObject.name == "Blank Space Key" && this.populatorFaction != TerritoryHolder.Defense) //Can only spawn on bottom space
                            {
                                createPiecePrefab();
                                controller.setAttackPlacedTroop();
                            }
                            
                        }
                        
                        
                    //}
                }
                
                //oldColor already set from hover
                sprite.color = hoverColor;
                break;
            case GameMode.SelectAdjacent:
                if(controller.selectedSpace == this.gameObject) //if click again to deselect
                {
                    controller.gameMode = GameMode.SelectTerritory;
                    sprite.color = oldColor;
                    particle.Stop();
                }
                break;
        }
    }

    private void createPiecePrefab()
    {
        if (populatedPiece == null)
        {
            populatedPiece = Instantiate(piecePrefab, new Vector3(this.transform.position.x, this.transform.position.y, -3), Quaternion.identity);
        }
        
        if (controller.playerTurn == PlayerTurn.Attack)
        {
            populatorFaction = TerritoryHolder.Attack;
        } else
        {
            populatorFaction = TerritoryHolder.Defense;
            print("111");
            GamePieceController pieceControl = populatedPiece.GetComponent<GamePieceController>();
            pieceControl.populateSlot(controller.getDefenseTroopValue());
        }
    }

    private void createTrapPrefab()
    {
        populatedTrap = Instantiate(trapPrefab, new Vector3(this.transform.position.x, this.transform.position.y, -2), Quaternion.identity);
        switch (controller.getTrapValue())
        {
            case 0:
                populatedTrap.GetComponent<SpriteRenderer>().sprite = populatedTrap.GetComponent<TrapController>().barricade;
                break;
            case 1:
                populatedTrap.GetComponent<SpriteRenderer>().sprite = populatedTrap.GetComponent<TrapController>().fireBomb;
                break;
            case 2:
                populatedTrap.GetComponent<SpriteRenderer>().sprite = populatedTrap.GetComponent<TrapController>().groundBomb;
                break;
            default:
                break;
        }
    }

    public void playParticle()
    {
        particle.Play();
    }

    public void stopParticle()
    {
        particle.Stop();
    }
}
