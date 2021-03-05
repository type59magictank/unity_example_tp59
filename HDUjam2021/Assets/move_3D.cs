using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_3D : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        Vector3 x = this.transform.position;
        x.y += 0.05f;
        this.transform.position = x;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
