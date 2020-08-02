using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public float speed = 10f;
    public float fire = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }

    public virtual void move()
    {
        Vector3 temppos = pos;
        temppos.y -= speed * Time.deltaTime;
        pos = temppos;
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.gameObject;
        switch (other.tag)
        {
            case "Hero":
                break;
            case "HeroLaser":
                Destroy(this.gameObject);
                break;
        }

    }

    public Vector3 pos
    {
        get
        {
            return (this.transform.position);
        }
        set
        {
            this.transform.position = value;
        }
    }

}
