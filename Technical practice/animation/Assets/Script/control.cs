using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class control : MonoBehaviour
{
    public GameObject player;
    public Animator action_controller;
    public bool straight;
    public bool left;
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
    }

    private void OnGUI()
    {
        if()
    }

}
