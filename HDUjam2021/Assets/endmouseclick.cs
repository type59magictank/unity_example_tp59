using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endmouseclick : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject main;
    public Light mlight;
    private bool exit_ = false;
    void Start()
    {
        mlight = main.GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mlight.intensity > 100) exit_ = true;
    }

    private void OnMouseDown()
    {
        if (exit_)
        {
            SceneManager.LoadScene("MainScene");
        }
    }

}
