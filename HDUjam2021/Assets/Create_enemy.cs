using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Create_enemy : MonoBehaviour
{
    public GameObject[] father;
    public int[] time;
    public int i=0;

    public int start = 1;
    public int end = 2;
    // Start is called before the first frame update
    void Start()
    {
        //father = GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        i++;
        int time_ = i / 50;
        if (time_ > time[start])
        {
            father[start].SetActive(true);
            father[start-1].SetActive(false);
            start++;
        }
    }
}
