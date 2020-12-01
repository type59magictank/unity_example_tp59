using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class control : MonoBehaviour
{
    public GameObject player;
    public Animator action_controller;
    public bool straight;
    public bool left;
    public bool stop;
    // Start is called before the first frame update
    void Start()
    {
        player = gameObject;
        action_controller = player.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        action_controller.SetBool("straight", straight);
        action_controller.SetBool("left", left);
        action_controller.SetBool("stop", stop);
    }

    private void OnGUI()
    {
        if(GUI.Button(new Rect(0, 0, 80, 50), "stand"))
        {
            straight = false;
            left = false;
            stop = false;
        }
        if (GUI.Button(new Rect(0, 50, 80, 50), "straight"))
        {
            straight = true;
            left = false;
            stop = false;
        }
        if (GUI.Button(new Rect(0, 100, 80, 50), "left"))
        {
            straight = false;
            left = true;
            stop = false;
        }
        if (GUI.Button(new Rect(0, 150, 80, 50), "stop"))
        {
            stop = true;
            straight = false;
            left = false;
        }
    }

}
