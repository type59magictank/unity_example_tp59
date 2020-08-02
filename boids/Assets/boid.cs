using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;

public class boid : MonoBehaviour
{
    static public List<boid> boids;

    public Vector3 velocity;
    public Vector3 newVelocity;
    public Vector3 newPosition;

    public List<boid> neighbors;
    public List<boid> collisionRisks;
    public boid closest;

    private void Awake()
    {
        if (boids == null)
        {
            boids = new List<boid>();
        }

        boids.Add(this);

        Vector3 randpos = Random.insideUnitSphere * boidspawner.S.spawnRadius;
        randpos.y = 0;
        this.transform.position = randpos;
        velocity = Random.onUnitSphere;
        velocity *= boidspawner.S.spawnVelocity;

        neighbors = new List<boid>();
        collisionRisks = new List<boid>();

        this.transform.parent = GameObject.Find("boids").transform;

        Color randcolor = Color.black;
        while (randcolor.r + randcolor.g + randcolor.b < 1.0f)
        {
            randcolor = new Color(Random.value, Random.value, Random.value);
        }
        Renderer[] rends = gameObject.GetComponentsInChildren<Renderer>();
        foreach (Renderer r in rends)
        {
            r.material.color = randcolor;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        List<boid> neighbors = GetNeighbors(this);

        newVelocity = velocity;
        newPosition = this.transform.position;

        Vector3 neighborVel = GetAverageVelocity(neighbors);
        newVelocity += neighborVel * boidspawner.S.velocityMatchingAmt;

        Vector3 neighborCenterOffset = GetAveragePosition(neighbors) - this.transform.position;
        newVelocity += neighborCenterOffset * boidspawner.S.flockCenterAmt;

        Vector3 dist;
        if (collisionRisks.Count > 0)
        {
            Vector3 collisionAveragePos = GetAveragePosition(collisionRisks);
            dist = collisionAveragePos - this.transform.position;
            newVelocity += dist * boidspawner.S.collisionAvoidanceAmt;
        }

        dist = boidspawner.S.mousepos - this.transform.position;
        if (dist.magnitude > boidspawner.S.mouseAvoidamceAmt)
        {
            newVelocity += dist * boidspawner.S.mouseAttractionAmt;
        }
        else
        {
            newVelocity -= dist.normalized * boidspawner.S.mouseAvoidamceDist * boidspawner.S.mouseAvoidamceDist;
        }


    }

    private void LateUpdate()
    {
        velocity = (1 - boidspawner.S.velocityLerpamtAmt) * velocity + boidspawner.S.velocityLerpamtAmt * newVelocity;

        if (velocity.magnitude > boidspawner.S.maxVelocity)
        {
            velocity = velocity.normalized * boidspawner.S.maxVelocity;
        }
        if (velocity.magnitude < boidspawner.S.minVelocity)
        {
            velocity = velocity.normalized * boidspawner.S.minVelocity;
        }

        newPosition = this.transform.position + velocity * Time.deltaTime;

        this.transform.LookAt(newPosition);
        this.transform.position = newPosition;

    }

    public List<boid> GetNeighbors(boid boi)
    {
        float closeestDist = float.MaxValue;
        Vector3 delta;
        float dist;
        neighbors.Clear();
        collisionRisks.Clear();

        foreach (boid b in boids)
        {
            if (b == boi) continue;
            delta = b.transform.position - boi.transform.position;
            dist = delta.magnitude;
            if (dist < closeestDist)
            {
                closeestDist = dist;
                closest = b;
            }
            if (dist < boidspawner.S.nearDist)
            {
                neighbors.Add(b);
            }
            if (dist < boidspawner.S.collisionDist)
            {
                collisionRisks.Add(b);
            }
        }
        if (neighbors.Count == 0)
        {
            neighbors.Add(closest);
        }
        return (neighbors);
    }

    public Vector3 GetAveragePosition(List<boid> someBoids)
    {
        Vector3 sum = Vector3.zero;
        foreach (boid b in someBoids)
        {
            sum += b.transform.position;
        }
        Vector3 center = sum / someBoids.Count;
        return (center);
    }

    public Vector3 GetAverageVelocity(List<boid> someboids)
    {
        Vector3 sum = Vector3.zero;
        foreach (boid b in someboids)
        {
            sum += b.velocity;
        }
        Vector3 avg = sum / someboids.Count;
        return (avg);
    }

}
