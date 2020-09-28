using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp : MonoBehaviour
{	
	public float speed;
	float x,y;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		x = Input.GetAxis("Horizontal");
		y = Input.GetAxis ("Vertical");
        //transform.position = transform.position + new Vector3(x*speed, y*speed, 0f);
		transform.position += new Vector3(x*speed, y*speed, 0f);
    }
}
