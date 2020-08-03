using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudCraft : MonoBehaviour
{
    public int numCloud = 40;
    public GameObject[] cloudPrefabs;
    public Vector3 cloudPosMin;
    public Vector3 cloudPosMax;
    public float cloudScaleMin = 1;
    public float cloudScaleMax = 5;
    public float cloudSpeedMult = 0.5f;

    public bool ___________________________;

    public GameObject[] cloudInstances;

    // Start is called before the first frame update

    private void Awake()
    {
        cloudInstances = new GameObject[numCloud];//存储云的实例

        GameObject anchor = GameObject.Find("CloudAnchor");
        //查找父对象
        GameObject cloud;
        for (int i = 0; i < numCloud; i++)//遍历创建实例
        {
            int prefabNum = Random.Range(0, cloudPrefabs.Length);

            cloud = Instantiate(cloudPrefabs[prefabNum]) as GameObject;//创建

            Vector3 cPos = Vector3.zero;
            cPos.x = Random.Range(cloudPosMin.x, cloudPosMax.x);
            cPos.y = Random.Range(cloudPosMin.y, cloudPosMax.y);
            //位置
            float scaleU = Random.value;
            float scaleVal = Mathf.Lerp(cloudScaleMin, cloudScaleMax, scaleU);
            //缩放比例
            cPos.y = Mathf.Lerp(cloudPosMin.y, cPos.y, scaleU);
            //小的离地面靠近
            cPos.z = 100 - 90 * scaleU;
            //小的更远
            cloud.transform.position = cPos;
            cloud.transform.localScale = Vector3.one * scaleVal;
            //应用到对象
            cloud.transform.parent = anchor.transform;
            //成为子对象
            cloudInstances[i] = cloud;//添加到数组
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject cloud in cloudInstances)//遍历已创建云
        {
            float scaleVal = cloud.transform.localScale.x;
            Vector3 cPos = cloud.transform.position;
            //获取比列位置
            cPos.x -= scaleVal * Time.deltaTime * cloudSpeedMult;
            //越大移动越快
            if (cPos.x <= cloudPosMin.x)
            {
                cPos.x = cloudPosMax.x;
                //边缘就回到开始
            }

            cloud.transform.position = cPos;//赋值
        }
    }
}
