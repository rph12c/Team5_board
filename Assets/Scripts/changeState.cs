using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeState : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonClickedWound()
    {
        Debug.Log("Button was clciked");
        if (Chip_controller.chip_wound == 0)
        {
            Chip_controller.chip_wound = 1;
            print("chip is now wounded");
        }
        else
        {
            Chip_controller.chip_wound = 0;
            print("Chip is no longer wounded");
        }

    }

    public void ButtonClickedSelect()
    {
        Chip_controller.chip_state = 1;
    }

    public void ButtonClickedMove()
    {
        Chip_controller.chip_state = 2;
    }

    public void ButtonClickedDeselect()
    {
        print("Idle Button Pressed");
        Chip_controller.chip_state = 0;
    }
}
