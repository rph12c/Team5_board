using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    // Start is called before the first frame update

    private Camera cam;
    private float targetZoom;
    private float zoomFactor = 3f;
    [SerializeField] private float zoomLerpSpeed = 10;
    [SerializeField] private float panLerpSpeed = 10;

    void Start()
    {
        cam = Camera.main;
        targetZoom = cam.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        float scrollData;
        scrollData = Input.GetAxis("Mouse ScrollWheel");

        targetZoom -= scrollData * zoomFactor;
        targetZoom = Mathf.Clamp(targetZoom, 4.5f, 27.84217f);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, Time.deltaTime * zoomLerpSpeed);


        //print(transform.position);
        if (Input.GetKey(KeyCode.RightArrow) && transform.position.x < 20)
        {
            transform.Translate(new Vector3(panLerpSpeed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x > -20)
        {
            transform.Translate(new Vector3(-panLerpSpeed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.DownArrow) && transform.position.y > -23)
        {
            transform.Translate(new Vector3(0, -panLerpSpeed * Time.deltaTime, 0));
        }
        if (Input.GetKey(KeyCode.UpArrow) && transform.position.y < 23)
        {
            transform.Translate(new Vector3(0, panLerpSpeed * Time.deltaTime, 0));
        }
    }
}
