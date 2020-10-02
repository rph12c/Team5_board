using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rnd_dest : MonoBehaviour
{

    private Vector2 screenBounds;
    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        //print(screenBounds.y);
        //print("start");
    }

    // Update is called once per frame
    void Update()
    {
        //print(transform.position.y);
        if (transform.position.y < -10)
        {
            print("destroyed");
            Destroy(this.gameObject);

        }
    }
}
