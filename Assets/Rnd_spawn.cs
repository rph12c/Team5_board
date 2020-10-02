using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rnd_spawn : MonoBehaviour
{

    public GameObject roundPrefab;
    public float generationTime = 0.4f;
    private Vector2 screenBounds;
    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(timedCreation());
    }

    private void spawn()
    {
        //print("spawn");
        GameObject s = Instantiate(roundPrefab) as GameObject;
        s.transform.position = new Vector2(Random.Range(-screenBounds.x, screenBounds.x), screenBounds.y * 2);
    }

    IEnumerator timedCreation()
    {
        while (true)
        {
            yield return new WaitForSeconds(generationTime);
            spawn();
        }
    }

    // Update is called once per frame
    //void Update()
    //{

    //}
}
