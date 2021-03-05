using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gotoend : MonoBehaviour
{
    public Light main;
    // Start is called before the first frame update
    void Start()
    {
        main = this.GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (main.intensity > 6)
        {
            SceneManager.LoadScene("End");
        }
    }

}
