using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boidspawner : MonoBehaviour
{

    static public boidspawner S;

    public int numBoids = 500;
    public GameObject boidPrefab;
    public float spawnRadius = 100f;
    public float spawnVelocity = 10f;
    public float minVelocity = 0f;
    public float maxVelocity = 30f;
    public float nearDist = 30f;
    public float collisionDist = 5f;
    public float velocityMatchingAmt= 0.01f;
    public float flockCenterAmt = 0.15f;
    public float collisionAvoidanceAmt = -0.5f;
    public float mouseAttractionAmt = 0.01f;
    public float mouseAvoidamceAmt = 0.75f;
    public float mouseAvoidamceDist = 15f;
    public float velocityLerpamtAmt = 0.25f;

    public bool _____________;

    public Vector3 mousepos;

    // Start is called before the first frame update
    void Start()
    {
        S = this;
        for (int i = 0; i <= numBoids; i++)
        {
            Instantiate(boidPrefab);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        Vector3 mousePod2d = new Vector3(Input.mousePosition.x, Input.mousePosition.y, this.transform.position.y);
        mousepos = this.GetComponent<Camera>().ScreenToWorldPoint(mousePod2d);
    }

}
