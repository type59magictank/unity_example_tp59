using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
//using UnityEngine.WSA;

public class FollowCam : MonoBehaviour
{
    static public FollowCam S;

    public float easing = 0.05f;

    public bool ______________________________;

    public GameObject poi;//兴趣点
    public float camZ;
    public Vector2 minXY=Vector2.zero;

    private void Awake()
    {
        S = this;
        camZ = this.transform.position.z;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector3 destination;

        if (poi == null)
        {
            destination = Vector3.zero;
        }
        else
        {
            destination = poi.transform.position;

            if (poi.tag == "Projectile")
            {
                if (poi.GetComponent<Rigidbody>().IsSleeping())
                {
                    poi = null;

                    return;
                }
            }
        }
        //兴趣点位置
        destination.x = Mathf.Max(0, destination.x);
        destination.y = Mathf.Max(0, destination.y);
        //限定xy最小值
        
        destination = Vector3.Lerp(transform.position, destination, easing);
        //位置插值
        destination.z = camZ;
        transform.position = destination;
        //设置摄像机位置

        this.GetComponent<Camera>().orthographicSize = destination.y + 10;
        //设置使得地面始终在
    }

}
