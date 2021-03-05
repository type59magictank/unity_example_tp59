using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightup : MonoBehaviour
{
    public Light main;
    public GameObject godown;
    // Start is called before the first frame update
    void Start()
    {
        main = this.GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (main.intensity < 100)
        {
            main.intensity *= (1 + Time.deltaTime);
            main.range *= (1 + Time.deltaTime);
        }
        else
        {
            //godown.GetComponent<onmouseclick>().enabled = true;
        }
        
    }
    
}
