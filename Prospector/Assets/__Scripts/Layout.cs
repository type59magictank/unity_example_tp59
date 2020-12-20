using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SlotDef
{
    public float x;
    public float y;
    public bool faceUp = false;
    public string layerName = "Default";
    public int layerID = 0;
    public int id;
    public List<int> hiddenBy = new List<int>();
    public string type = "slot";
    public Vector2 stagger;
}

public class Layout : MonoBehaviour
{
    public PT_XMLReader xmlr; 
    public PT_XMLHashtable xml; //便于访问xml
    public Vector2 multiplier; //牌面中心距离
                               // SlotDef引用
    public List<SlotDef> slotDefs; //0-3排中的list
    public SlotDef drawPile;
    public SlotDef discardPile;
    //存储根据layerid确定的所有图层名称
    public string[] sortingLayerNames = new string[] { "Row0", "Row1", "Row2", "Row3", "Discard", "Draw" };
    // 读取layoutxml文件函数

    public void ReadLayout(string xmlText)
    {
        xmlr = new PT_XMLReader();
        xmlr.Parse(xmlText); //解析xml字符串
        xml = xmlr.xml["xml"][0]; //设置xml快速访问内容
        // 读取设置间距数值
        multiplier.x = float.Parse(xml["multiplier"][0].att("x"));
        multiplier.y = float.Parse(xml["multiplier"][0].att("y"));
        // 读入位置
        SlotDef tSD;
        // slotsX为读取slot快捷方式
        PT_XMLHashList slotsX = xml["slot"];
        for (int i = 0; i < slotsX.Count; i++)
        {
            tSD = new SlotDef(); // 新建SlotDef实例
            if (slotsX[i].HasAtt("type"))
            {
                // 如果 <slot> 有type 内容，读取
                tSD.type = slotsX[i].att("type");
            }
            else
            {
                // 如果没有设置为 "slot"表示场景中的纸牌
                tSD.type = "slot";
            }
            // 解析各属性为数值
            tSD.x = float.Parse(slotsX[i].att("x"));
            tSD.y = float.Parse(slotsX[i].att("y"));
            tSD.layerID = int.Parse(slotsX[i].att("layer"));
            // 转换layerID为layerName文本
            tSD.layerName = sortingLayerNames[tSD.layerID]; 
            switch (tSD.type)
            {
                // 根据<slot>属性拉取附属属性
                case "slot":
                    tSD.faceUp = (slotsX[i].att("faceup") == "1");
                    tSD.id = int.Parse(slotsX[i].att("id"));
                    if (slotsX[i].HasAtt("hiddenby"))
                    {
                        string[] hiding = slotsX[i].att("hiddenby").Split(',');
                        foreach (string s in hiding)
                        {
                            tSD.hiddenBy.Add(int.Parse(s));
                        }
                    }
                    slotDefs.Add(tSD);
                    break;
                case "drawpile":
                    tSD.stagger.x = float.Parse(slotsX[i].att("xstagger"));
                    drawPile = tSD;
                    break;
                case "discardpile":
                    discardPile = tSD;
                    break;
            }
        }
    }
}
