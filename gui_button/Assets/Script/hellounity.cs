using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hellounity : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGUI()
    {
        GUI.skin.label.fontSize = 100;
        GUI.Label(new Rect(100, Screen.height / 3, Screen.width, Screen.height), "hello unity");
    }

}
