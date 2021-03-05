using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_tigger : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player_;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    void OnTriggerEnter(Collider other)
    {
        //print(other.name);
        if (other.tag == "enemy")
        {
            //Debug.LogError("-1");
            //playercontroller.S.trigger = true;
            player_.GetComponent<playercontroller>().trigger = true;
        }
    }

}
