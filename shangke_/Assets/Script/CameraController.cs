using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public PlayerInput pi;
    public float horizontalSpeed=20.0f;
    public float verticalSpeed = 20.0f;
    public float cameraDampVAlue = 0.5f;

    private GameObject playerHandle;
    private GameObject cameraHandle;
    private GameObject modle;
    private float tempEulerX;
    private GameObject camera;

    private Vector3 cameraDampVelocity;

    // Start is called before the first frame update
    void Awake()
    {
        cameraHandle = transform.parent.gameObject;
        playerHandle = cameraHandle.transform.parent.gameObject;
        tempEulerX = 20;
        modle = playerHandle.GetComponent<ActorController>().model;
        camera = Camera.main.gameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 tempModleEuler = modle.transform.eulerAngles;


        playerHandle.transform.Rotate(Vector3.up, pi.Jright * horizontalSpeed * Time.fixedDeltaTime);
        //cameraHandle.transform.Rotate(Vector3.right, pi.Jup * -verticalSpeed * Time.deltaTime);
        //tempEulerX = cameraHandle.transform.eulerAngles.x;
        tempEulerX -= pi.Jup * -verticalSpeed * Time.fixedDeltaTime;
        tempEulerX = Mathf.Clamp(tempEulerX, -40, 30);
        //欧拉角
        cameraHandle.transform.localEulerAngles = new Vector3(tempEulerX, 0, 0);

        modle.transform.eulerAngles = tempModleEuler;

        //camera.transform.position = Vector3.Lerp(camera.transform.position,transform.position,0.1f);
        //camera跟随
        camera.transform.position = Vector3.SmoothDamp(camera.transform.position, transform.position, ref cameraDampVelocity,cameraDampVAlue);
        camera.transform.eulerAngles = transform.eulerAngles;
    }
}
