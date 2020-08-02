using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI

public class test1 : MonoBehaviour
{
    public Text mytext;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mytext.GetComponent<Text>().text = "helloworld";
        
    }
}
