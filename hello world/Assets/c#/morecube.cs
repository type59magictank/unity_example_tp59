using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class morecube : MonoBehaviour
{
    public GameObject cubemore;
    // Start is called before the first frame update
    void Start()
    {
        //Instantiate(cubemore); 
    }

    // Update is called once per frame
    void Update()
    {
        Instantiate(cubemore);
    }
}
