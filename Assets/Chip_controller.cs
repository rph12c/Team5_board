using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chip_controller : MonoBehaviour
{
    public static int chip_state = 0;
    public static int chip_wound = 0;
    // Start is called before the first frame update
    Animator animator;
    float state;

    const int IDLE = 0;
    const int SELECTED = 1;
    const int MOVING = 2;
    int currentAnimState = IDLE;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (chip_state == 0)
        {
            changeState(IDLE);
        }

        if (chip_state == 1)
        {
            changeState(SELECTED);
        }

        if (chip_state == 0)
        {
            changeState(MOVING);
        }

        if (chip_wound == 0)
        {
            animator.SetBool("toWound", false);
        }
        else
        {
            animator.SetBool("toWound", true);
        }
    }

    void changeState(int state)
    {
        if (currentAnimState == state) return;
        print("switch state; state = " + state);

        switch (state)
        {
            case IDLE:
                animator.SetInteger("toState", 0);
                break;
            case SELECTED:
                animator.SetInteger("toState", 1);
                break;
            case MOVING:
                animator.SetInteger("toState", 2);
                break;
        }

    }
}
