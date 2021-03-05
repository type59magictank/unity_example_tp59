using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ai_move : MonoBehaviour
{
    public NavMeshAgent agent;

    public float goalDistance = 40f;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 target = transform.position + Vector3.forward * goalDistance;
        agent.SetDestination(target);
        agent.speed = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
