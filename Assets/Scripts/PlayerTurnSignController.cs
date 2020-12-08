using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnSignController : MonoBehaviour
{
    public Sprite defenseSprite;
    public Sprite offenseSprite;
    public Sprite defenseWin;
    public Sprite offenseWin;

    private Vector3 newPosition;
    private Vector3 startPosition;

    private bool canStartAnimation = true;

    private void Awake()
    {
        newPosition = new Vector3(-1, 3, -80);
        startPosition = new Vector3(-1, -35, -80);
        transform.position = startPosition;
    }

    private bool canMove = false;

    private void Update()
    {
        if (canMove)
        {
            //print(newPosition);
            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * 2); //was 1
            
        }
        
    }

    public void changeSign(PlayerTurn turn)
    {
        if (canStartAnimation)
        {
            canStartAnimation = false;
            Invoke("continueSignTransform", 3.0f); //was 5
            SpriteRenderer spriteRend = this.gameObject.GetComponent<SpriteRenderer>();
            if (turn == PlayerTurn.Defense)
            {
                spriteRend.sprite = defenseSprite;
            }
            else
            {
                spriteRend.sprite = offenseSprite;
            }
            //print("can move");
            canMove = true;
        }
    }

    public void displayWinner(PlayerTurn player)
    {
        if (canStartAnimation)
        {
            canStartAnimation = false;
            Invoke("continueSignTransform", 3.0f); //was 5
            SpriteRenderer spriteRend = this.gameObject.GetComponent<SpriteRenderer>();
            if (player == PlayerTurn.Defense)
            {
                spriteRend.sprite = defenseWin;
            }
            else
            {
                spriteRend.sprite = offenseWin;
            }
            //print("can move");
            canMove = true;
        }
    }

    private void continueSignTransform()
    {
        //print("RESUME");
        Invoke("resetSign", 3.0f); //was 5
        newPosition = new Vector3(-1, 35, -80);
    }

    private void resetSign()
    {
        canMove = false;
        transform.position = startPosition;
        newPosition = new Vector3(-1, 3, -80);
        canStartAnimation = true;
    }

}
