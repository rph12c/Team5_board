using UnityEngine;
using System.Collections;

public class DontDestroy : MonoBehavior { 

    void Awake ()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("MainMusic");
        if (objs.Length > 1)
            Destroy(this.gameObject);

        DontDestroyOnLoad(ths.gameObject);
    }
}
