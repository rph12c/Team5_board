using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnSignController : MonoBehaviour
{
    public Sprite defenseSprite;
    public Sprite offenseSprite;

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
            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * 1);
            
        }
        
    }

    public void changeSign(PlayerTurn turn)
    {
        if (canStartAnimation)
        {
            canStartAnimation = false;
            Invoke("continueSignTransform", 5.0f);
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

    private void continueSignTransform()
    {
        print("RESUME");
        Invoke("resetSign", 5.0f);
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
