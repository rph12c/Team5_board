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

    public Color32 oldColor;
    public Color32 startColor;
    public Color32 hoverColor;
    public GameObject[] adjacentTerritories;

    // Start is called before the first frame update
    void Awake()
    {
        controller = GameController.SharedController;

        sprite = GetComponent<SpriteRenderer>();
        particle = GetComponent<ParticleSystem>();
        particle.Stop();
        sprite.color = startColor;
    }

    void OnMouseEnter()
    {
        print("enter");
        switch (controller.gameMode)
        {
            case GameMode.SelectTerritory:
                oldColor = sprite.color;
                sprite.color = hoverColor;
                particle.Play();
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
        print(controller.gameMode);

        switch (controller.gameMode)
        {
            case GameMode.SelectTerritory:
                controller.selectedSpace = this.gameObject;
                controller.gameMode = GameMode.SelectAdjacent;
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

    public void playParticle()
    {
        particle.Play();
    }

    public void stopParticle()
    {
        particle.Stop();
    }
}
