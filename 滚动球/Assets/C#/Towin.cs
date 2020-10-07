using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Towin : MonoBehaviour
{
    static public bool goal = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "sphere")
        {
            Towin.goal = true;
            Color c = GetComponent<Renderer>().material.color;

            c.a = 1;

            this.GetComponent<Renderer>().material.color = c;
        }
    }

}
