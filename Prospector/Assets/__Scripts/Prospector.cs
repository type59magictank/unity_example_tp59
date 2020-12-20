using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Prospector : MonoBehaviour
{
    static public Prospector S;

    [Header("Set in inspector")]
    public TextAsset deckXML;
    public TextAsset layoutXML;
    public float xOffset = 3;
    public float yOffset = -2.5f;
    public Vector3 layoutCenter;


    [Header("set dynamically")]
    public Deck deck;
    public Layout layout;
    public List<CardProspector> drawPile;
    public Transform layoutAnchor;
    public CardProspector target;
    public List<CardProspector> tableau;
    public List<CardProspector> discardPile;


    private void Awake()
    {
        S = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        deck = GetComponent<Deck>();//获取deck脚本组件
        deck.Inindeck(deckXML.text);

        Deck.Shuffle(ref deck.cards);
        //洗牌
        //Card c;
        //for (int cNum = 0; cNum < deck.cards.Count; cNum++)
        //{
        //    c = deck.cards[cNum];
        //    c.transform.localPosition = new Vector3((cNum % 13) * 3, cNum / 13 * 4, 0);
        //}
        layout = GetComponent<Layout>();//获取layout脚本组件
        layout.ReadLayout(layoutXML.text);//将layoutXML文件传递给脚本
        drawPile = ConvertListCardsToListCardProspectors(deck.cards);


    }

    List<CardProspector> ConvertListCardsToListCardProspectors(List<Card> lCD)
    {
        List<CardProspector> lCP = new List<CardProspector>();
        CardProspector tCP;
        foreach (Card tCD in lCD)
        {
            tCP = tCD as CardProspector;//关键字as将Card转换为CardProspector
            lCP.Add(tCP);
        }
        return (lCP);
    }

    CardProspector Draw()
    {
        CardProspector cd = drawPile[0];//取出0号卡
        drawPile.RemoveAt(0);//在原始list删除
        return (cd);//返回
    }

    //定位纸牌初始场景
    void LayoutGame()
    {
        // 创建空对象作为锚点
        if (layoutAnchor == null)
        {
            GameObject tGO = new GameObject("_LayoutAnchor");
            // 在层级面板中创建
            layoutAnchor = tGO.transform; // 获取transform
            layoutAnchor.transform.position = layoutCenter; // 定位
        }
        CardProspector cp;
        // 按照布局
        foreach (SlotDef tSD in layout.slotDefs)
        {
            // 遍历SlotDefs在layout.slotDefs
            cp = Draw(); // 从顶部取一张牌
            //cp = MakeGold(cp);
            cp.faceUp = tSD.faceUp; // 设置faceUp为SlotDef的值
            cp.transform.parent = layoutAnchor; // 设置父元素layoutAnchor
                                                // 代替之前的父元素_deck
            cp.transform.localPosition = new Vector3(
            layout.multiplier.x * tSD.x,
            layout.multiplier.y * tSD.y,
            -tSD.layerID);
            // 根据slotDef设置位置
            cp.layoutID = tSD.id;
            cp.slotDef = tSD;
            // 使得CardProspectors具有CardState.tableau的状态
            cp.state = eCardState.tableau;
            //cp.SetSortingLayerName(tSD.layerName); // Set the sorting layers
            tableau.Add(cp); // 加入list
        }
    }

        // Update is called once per frame
        void Update()
    {
        
    }
}
