using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movemap : MonoBehaviour
{
    float moveSpeed = 0.25f;
    float jiao = 12.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 x = new Vector3(horizontalInput, 0, 0)*jiao;
        Vector3 y = new Vector3(0, 0, verticalInput)*jiao;

        transform.eulerAngles=Vector3.Lerp(x+y,Vector3.zero, moveSpeed * Time.deltaTime);//插值

        //print(horizontalInput+ "###"+verticalInput);

    }
}
