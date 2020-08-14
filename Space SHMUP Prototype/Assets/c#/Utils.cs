using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BoundsTest//枚举类型
{
    center,//询问对象中心是否位于屏幕中
    onScreen,//对象是否完全位于屏幕中
    offScreen//是否完全位于屏幕之外
}

public class Utils : MonoBehaviour
{
    
    public static Bounds BoundsUnion(Bounds b0,Bounds b1)//接收两个区域类型并返回区域的并集
    {

        if (b0.size == Vector3.zero && b1.size != Vector3.zero)
        {
            return (b1);
        }else if (b0.size != Vector3.zero && b1.size == Vector3.zero)
        {
            return (b0);
        }else if (b0.size == Vector3.zero && b1.size == Vector3.zero)
        {
            return (b0);
        }
        //如果有一个size为0忽略
        b0.Encapsulate(b1.min);
        b0.Encapsulate(b1.max);
        return (b0);
    }

    public static Bounds CombinBoundsOfChildren(GameObject go)
    {
        Bounds b = new Bounds(Vector3.zero, Vector3.zero);
        //创建空白区域变量
        if (go.GetComponent<Renderer>() != null)//如果对象有渲染器组件
        {
            b = BoundsUnion(b, go.GetComponent<Renderer>().bounds);
            //扩张b包括渲染器边界
        }

        if (go.GetComponent<Collider>() != null)//如果对象有碰撞器组件
        {
            b = BoundsUnion(b, go.GetComponent<Collider>().bounds);

        }
        //遍历游戏对象Transform组件的每个子对象
        foreach (Transform t in go.transform)
        {
            b = BoundsUnion(b, CombinBoundsOfChildren(t.gameObject));
            //扩张b包括这些组件的边界
        }

        return (b);

    }

    static public Bounds camBounds//创建一个静态只读全局属性
    {
        get
        {
            if (_camBounds.size == Vector3.zero)//如果没有设置_camBounds
            {
                SetCameraBounds();
                //使用默认摄像机设置调用SetCameraBounds();
            }

            return (_camBounds);
        }//没有set
    }

    static public Bounds _camBounds;//局部静态变量，在属性定义中使用

    public static void SetCameraBounds(Camera cam=null)//可以设置_camBounds变量值也可以直接调用（无参数调用）
    {
        if (cam == null) cam = Camera.main;
        //没有传入参数使用主摄像机
        Vector3 topLeft = new Vector3(0, 0, 0);
        Vector3 bottomRight = new Vector3(Screen.width, Screen.height, 0);
        //假定摄像机为正投影，旋转R【0，0，0】，根据左上和右下创建两个三维坐标
        Vector3 boundTLN = cam.ScreenToWorldPoint(topLeft);
        Vector3 boundBRF = cam.ScreenToWorldPoint(bottomRight);
        //将两个坐标化为世界坐标
        boundTLN.z += cam.nearClipPlane;
        boundBRF.z += cam.farClipPlane;
        //将两个三维向量z值设置为摄像机远景切平面和近景z坐标
        Vector3 center = (boundTLN + boundBRF) / 2f;
        _camBounds = new Bounds(center, Vector3.zero);
        //查找边界框中心
        _camBounds.Encapsulate(boundTLN);
        _camBounds.Encapsulate(boundBRF);
        //扩展_camBounds，使其具有尺寸
    }

    public static Vector3 ScreenBoundsCheck(Bounds bnd,BoundsTest test = BoundsTest.center)
    {
        return (BoundsInBoundsCheck(camBounds,bnd,test));
        //检查边界框bnd是否位于镜头边界框camBounds之内
    }

    public static Vector3 BoundsInBoundsCheck(Bounds bigB,Bounds lilB,BoundsTest test = BoundsTest.onScreen)
    {//检查边界框libB是否位于bigB之内
        //根据boundstest不同函数行为不同
        Vector3 pos = lilB.center;
        //获取中心
        Vector3 off = Vector3.zero;

        switch (test)
        {

            case BoundsTest.center://函数确定将libB的中心平移到bigB之内
                //需要平移的方向和距离用三维向量off表示
                if (bigB.Contains(pos))
                {
                    return (Vector3.zero);
                }

                if (pos.x > bigB.max.x)
                {
                    off.x = pos.x - bigB.max.x;
                }else if (pos.x < bigB.min.x)
                {
                    off.x = pos.x - bigB.min.x;
                }

                if (pos.y > bigB.max.y)
                {
                    off.y = pos.y - bigB.max.y;
                }
                else if (pos.y < bigB.min.y)
                {
                    off.y = pos.y - bigB.min.y;
                }

                if (pos.z > bigB.max.z)
                {
                    off.z = pos.z - bigB.max.z;
                }
                else if (pos.z < bigB.min.z)
                {
                    off.z = pos.z - bigB.min.z;
                }

                return (off);


            case BoundsTest.onScreen://将libB整体平移到bigB之内
                {
                    if (bigB.Contains(lilB.min) && bigB.Contains(lilB.max))
                    {
                        return (Vector3.zero);
                    }

                    if (lilB.max.x > bigB.max.x)
                    {
                        off.x = lilB.max.x - bigB.max.x;
                    }else if (lilB.min.x < bigB.min.x)
                    {
                        off.x = lilB.min.x - bigB.min.x;
                    }

                    if (lilB.max.y > bigB.max.y)
                    {
                        off.y = lilB.max.y - bigB.max.y;
                    }
                    else if (lilB.min.y < bigB.min.y)
                    {
                        off.y = lilB.min.y - bigB.min.y;
                    }

                    if (lilB.max.z > bigB.max.z)
                    {
                        off.z = lilB.max.z - bigB.max.z;
                    }
                    else if (lilB.min.z < bigB.min.z)
                    {
                        off.z = lilB.min.z - bigB.min.z;
                    }

                    return (off);

                }

            case BoundsTest.offScreen://任意一部分libB移动到bigB之内需要的方向和距离
                {
                    bool cMin = bigB.Contains(lilB.min);
                    bool cMax = bigB.Contains(lilB.max);

                    if (cMin || cMax)
                    {
                        return(Vector3.zero);
                    }

                    if (lilB.min.x > bigB.max.x)
                    {
                        off.x = lilB.min.x - bigB.max.x;
                    }else if (lilB.max.x < bigB.min.x)
                    {
                        off.x = lilB.max.x - bigB.min.x;
                    }

                    if (lilB.min.y > bigB.max.y)
                    {
                        off.y = lilB.min.y - bigB.max.y;
                    }
                    else if (lilB.max.y < bigB.min.y)
                    {
                        off.y = lilB.max.y - bigB.min.y;
                    }

                    if (lilB.min.z > bigB.max.z)
                    {
                        off.z = lilB.min.z - bigB.max.z;
                    }
                    else if (lilB.max.z < bigB.min.z)
                    {
                        off.z = lilB.max.z - bigB.min.z;
                    }

                }
                return (off);
        }

        return (Vector3.zero);

    }

    //递归方法寻找parent树找到自定义标签
    public static GameObject FindTaggedParent(GameObject go)
    {//如果当前对象有自定义标签
        if (go.tag != "Untagged")
        {
            return (go);//返回游戏对象
        }

        if (go.transform.parent == null)//如果没有父对象
        {
            return (null);

        }

        return (FindTaggedParent(go.transform.parent.gameObject));//递归向上寻找

    }

    public static GameObject FindTaggedParent(Transform t)//以Transform为参数
    {
        return (FindTaggedParent(t.gameObject));
    }

    static public Material[] GetAllMaterials(GameObject go)//用一个List返回游戏对象及其子对象所有材质
    {
        List<Material> mats = new List<Material>();
        if (go.GetComponent<Renderer>() != null)
        {
            mats.Add(go.GetComponent<Renderer>().material);
        }
        foreach(Transform t in go.transform)
        {
            mats.AddRange(GetAllMaterials(t.gameObject));
        }
        return (mats.ToArray());
    }

}
