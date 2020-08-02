﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{

    public GameObject launchPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        Transform launchPointTrans = transform.Find("LaunchPoint");
        launchPoint = launchPointTrans.gameObject;
        launchPoint.SetActive(false);

    }

    private void OnMouseEnter()
    {
        //print("Slingshot:OnMouseEnter");
        launchPoint.SetActive(true);
    }

    private void OnMouseExit()
    {
        //print("Slingshot:OnMouseExit");
        launchPoint.SetActive(false);
    }

}