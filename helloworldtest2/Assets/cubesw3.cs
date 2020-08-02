using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubesw3 : MonoBehaviour
{
    public GameObject cup;
    public List<GameObject> gol;
    public float sf = 0.95f;
    public int nc = 0;

    // Start is called before the first frame update
    void Start()
    {
        gol = new List<GameObject>(); //chu shi hua
    }

    // Update is called once per frame
    void Update()
    {
        nc++;
        GameObject gobj = Instantiate(cup) as GameObject;
        gobj.name = "cube" + nc;
        Color c = new Color(Random.value, Random.value, Random.value);
        gobj.GetComponent<Renderer>().material.color = c;//5.0 gen xing xie fa
        gobj.transform.position = Random.insideUnitSphere;//sui ji wei zhi fen bu
        gol.Add(gobj);
        List<GameObject> removeList = new List<GameObject>();//cun chu xu yao shan chu li fang ti

        foreach (GameObject gt in gol)
        {
            float scale = gt.transform.localScale.x;
            scale *= sf;
            gt.transform.localScale = Vector3.one * scale;
            if (scale <= 0.1f)
            {
                removeList.Add(gt);
            }
        }

        foreach(GameObject gt in removeList)
        {
            gol.Remove(gt);
            Destroy(gt);
        }

    }
}
