using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralleax : MonoBehaviour
{
    public GameObject poi;
    public GameObject[] panels;
    public float scrollSpeed = -30f;
    public float motionMult = 0.25f;

    private float paneHt;
    private float depth;

    // Start is called before the first frame update
    void Start()
    {
        paneHt = panels[0].transform.localScale.y;
        depth = panels[0].transform.position.z;

        panels[0].transform.position = new Vector3(0, 0, depth);
        panels[1].transform.position = new Vector3(0, paneHt, depth);
    }

    // Update is called once per frame
    void Update()
    {
        float tY, tX = 0;
        tY = Time.time * scrollSpeed % paneHt + (paneHt * 0.5f);
        if (poi != null)
        {
            tX = -poi.transform.position.x * motionMult;
        }

        panels[0].transform.position = new Vector3(tX, tY, depth);

        if (tY >= 0)
        {
            panels[1].transform.position = new Vector3(tX, tY - paneHt, depth);
        }
        else
        {
            panels[1].transform.position = new Vector3(tX, tY + paneHt, depth);
        }
    }
}
