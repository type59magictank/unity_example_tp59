using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController : MonoBehaviour
{

    public GameObject model;
    public PlayerInput pi;
    public float walkSpeed=2.4f;
    public float runMultiplier = 2.0f;
    public float jumpVelocity = 5.0f;
    public float rollVelocity = 1.0f;
    public float jabMultiplier = 3.0f;

    [SerializeField]
    private Animator anim;
    private Rigidbody rigid;
    private Vector3 planarVec;
    private Vector3 thrustVec;


    [SerializeField]
    private bool lockPlanar = false;

    // Start is called before the first frame update
    void Awake()
    {
        pi = GetComponent<PlayerInput>();
        anim = model.GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("forward", pi.Dmag * Mathf.Lerp(anim.GetFloat("forward"), ((pi.run) ? 2.0f : 1.0f), 0.5f));//跑步插值

        if (rigid.velocity.magnitude > 1.0f)
        {
            anim.SetTrigger("roll");
        }

        if (pi.jump)
        {
            anim.SetTrigger("jump");
        }
        

        if (pi.Dmag > 0.1f)
        {
            model.transform.forward = Vector3.Slerp(model.transform.forward, pi.Dvec, 0.3f);//旋转
        }

        if (lockPlanar == false)
        {
            planarVec = pi.Dmag * model.transform.forward * walkSpeed * ((pi.run) ? runMultiplier : 1.0f);//动量
        }

        
    }

    private void FixedUpdate()//物理引擎模拟（刚体）
    {
        //rigid.position += planarVec * Time.fixedDeltaTime;//间隔不同
        rigid.velocity = new Vector3(planarVec.x, rigid.velocity.y, planarVec.z) + thrustVec;
        thrustVec = Vector3.zero;
    }

    //Message processing block
    public void OnJumpEnter()
    {
        //print("jump");
        pi.inputEnable = false;
        lockPlanar = true;
        thrustVec = new Vector3(0, jumpVelocity, 0);
    }

    //public void OnJumpExit()
    //{
    //    //print("exit");
    //    pi.inputEnable = true;
    //    lockPlanar = false;
    //}

    public void IsGround()
    {
        anim.SetBool("isGround", true);
    }

    public void IsNotGround()
    {
        anim.SetBool("isGround", false);
    }

    public void OnGroundEnter()
    {
        pi.inputEnable = true;
        lockPlanar = false;
    }

    public void OnFallEnter()
    {
        pi.inputEnable = false;
        lockPlanar = true;
    }

    public void OnRollEnter()
    {
        pi.inputEnable = false;
        lockPlanar = true;
        thrustVec = new Vector3(0, rollVelocity, 0);
    }

    public void OnJabEnter()
    {
        pi.inputEnable = false;
        lockPlanar = true;
        
    }

    public void OnJabUpdate()
    {
        thrustVec = model.transform.forward * anim.GetFloat("jabVelocity")*jabMultiplier;
    }

}
