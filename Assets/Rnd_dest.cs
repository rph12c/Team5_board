using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rnd_dest : MonoBehaviour
{
    public AudioSource tickSource;
    private Vector2 screenBounds;
    // Start is called before the first frame update
    void Start()
    {
        tickSource = GetComponent<AudioSource>();
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
            //print("destroyed");
            Destroy(this.gameObject);

        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "mouse_body")
        {
            //print("bounced");
            Keep_score.scoreValue += 4;
            tickSource.Play();
        }

    }
}
