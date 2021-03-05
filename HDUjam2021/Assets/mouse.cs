using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouse : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        /*
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePos;
        Vector3 zhongjie = transform.position;
        zhongjie.y = 1;
        transform.position = zhongjie;
        */

        // (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray,out hit))
            {
                Vector3 palce = hit.point;
                palce.y = 0;
                transform.position = palce;
                //Debug.Log(palce);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePos;
        Vector3 zhongjie = transform.position;
        zhongjie.y = 1;
        transform.position = zhongjie;
        Debug.Log(zhongjie);
        //Debug.Log(Input.mousePosition);*/
    }
}
