﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    static public Hero S;

    public float speed = 30;
    public float rollMult = -45;
    public float pitchMult = 30;

    public float shieldLevel = 1;

    public bool _____________________________;

    public Bounds bounds;

    private void Awake()
    {
        S = this;

        bounds = Utils.CombinBoundsOfChildren(this.gameObject);

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float xAxis = Input.GetAxis("Horizontal");

        float yAxis = Input.GetAxis("Vertical");

        Vector3 pos = transform.position;
        pos.x += xAxis * speed * Time.deltaTime;
        pos.y += xAxis * speed * Time.deltaTime;
        transform.position = pos;

        transform.rotation = Quaternion.Euler(yAxis * pitchMult, xAxis * pitchMult, 0);
    }
}
