﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shield : MonoBehaviour
{
    public float rotationsPerSecond = 0.1f;

    public bool _______________________;

    public int levelShown = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int currLevel = Mathf.FloorToInt(Hero.S.shieldLevel);

        if (levelShown != currLevel)
        {
            levelShown = currLevel;

            Material mat = this.GetComponent<Renderer>().material;

            mat.mainTextureOffset = new Vector2(0.2f * levelShown, 0);

        }

        float rZ = (rotationsPerSecond * Time.time * 360) % 360;

        transform.rotation = Quaternion.Euler(0, 0, rZ);

    }
}
