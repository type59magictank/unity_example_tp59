using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    [Header("Set in inspector")]
    //花色
    public Sprite suitClub;
    public Sprite suitDiamond;
    public Sprite suitHeart;
    public Sprite suitSpade;
    public Sprite[] faceSprites;
    public Sprite[] rankSprites;
    public Sprite cardBack;
    public Sprite cardBackGold;
    public Sprite cardFront;
    public Sprite cardFrontGold;

    public GameObject prefabSprite;
    public GameObject prefabCaard;


    [Header("Set Dynamically")]

    public PT_XMLReader xmlr;
    public List<string> cardNames;
    public List<Card> cards;
    public List<Decorator> decorators;
    public List<CardDefinition> cardDefinitions;
    public Transform deckAnchor;
    public Dictionary<string, Sprite> dictSuits;

    public void Inindeck(string deckXMLText)
    {
        //创建锚点
        if (GameObject.Find("_Deck") == null)
        {
            GameObject anchorGO = new GameObject("_Deck");
            deckAnchor = anchorGO.transform;
        }
        //使用必要的sprite初始化字典
        dictSuits = new Dictionary<string, Sprite>()
        {
            {"C",suitClub },
            {"D",suitDiamond },
            {"H",suitHeart },
            {"S",suitSpade }
        };


        //读取
        ReadDeck(deckXMLText);

        MakeCards();
    }

    public void ReadDeck(string deckXMLText)
    {
        xmlr = new PT_XMLReader();//新建XML读取器
        xmlr.Parse(deckXMLText);//使用这个读取器解析文件

        ////test
        string s = "xml[0] decorator[0]";
        s += "type=" + xmlr.xml["xml"][0]["decorator"][0].att("type");
        s += "x=" + xmlr.xml["xml"][0]["decorator"][0].att("x");
        s += "y=" + xmlr.xml["xml"][0]["decorator"][0].att("y");
        s += "scale=" + xmlr.xml["xml"][0]["decorator"][0].att("scale");
        //print(s);

        //读取纸牌角码
        decorators = new List<Decorator>();
        //获取标签项构成list
        PT_XMLHashList xDecos = xmlr.xml["xml"][0]["decorator"];
        Decorator deco;
        for (int i = 0; i < xDecos.Count; i++)
        {
            //复制出xml内容
            deco = new Decorator();

            deco.type = xDecos[i].att("type");
            //bool值
            deco.flip = (xDecos[i].att("flip") == "1");

            deco.scale = float.Parse(xDecos[i].att("scale"));

            deco.loc.x = float.Parse(xDecos[i].att("x"));
            deco.loc.y = float.Parse(xDecos[i].att("y"));
            deco.loc.z = float.Parse(xDecos[i].att("z"));
            //添加到list
            decorators.Add(deco);

        }
        //点数对应花色符号位置
        cardDefinitions = new List<CardDefinition>();

        PT_XMLHashList xCardDefs = xmlr.xml["xml"][0]["card"];
        for (int i = 0; i < xCardDefs.Count; i++)
        {

            CardDefinition cDef = new CardDefinition();

            cDef.rank = int.Parse(xCardDefs[i].att("rank"));
            //单独处理pip标签
            PT_XMLHashList xPips = xCardDefs[i]["pip"];
            if (xPips != null)
            {
                for (int j = 0; j < xPips.Count; j++)
                {

                    deco = new Decorator();

                    deco.type = "pip";
                    deco.flip = (xPips[j].att("flip") == "1");
                    deco.loc.x = float.Parse(xPips[j].att("x"));
                    deco.loc.y = float.Parse(xPips[j].att("y"));
                    deco.loc.z = float.Parse(xPips[j].att("z"));
                    if (xPips[j].HasAtt("scale"))
                    {
                        deco.scale = float.Parse(xPips[j].att("scale"));
                    }
                    cDef.pips.Add(deco);
                }
            }

            //花牌包括一个face属性（JQK）
            if (xCardDefs[i].HasAtt("face"))
            {
                cDef.face = xCardDefs[i].att("face");
            }
            cardDefinitions.Add(cDef);
        }

    }

    //搜索返回点数定义
    public CardDefinition GetCardDefinitionByRank(int rnk)
    {
        foreach(CardDefinition cd in cardDefinitions)
        {
            if (cd.rank == rnk)
            {
                return (cd);
            }
        }
        return (null);
    }
    
    //创建card对象
    public void MakeCards()
    {
        //创建纸牌名称
        cardNames = new List<string>();
        string[] letters = new string[] { "C", "D", "H", "S" };
        foreach(string s in letters)
        {
            for (int i = 0; i < 13; i++)
            {
                cardNames.Add(s + (i + 1));
            }
        }
        //存储所有纸牌名称
        cards = new List<Card>();

        for (int i = 0; i < cardNames.Count; i++)
        {

            cards.Add(MakeCard(i));
        }

    }

    //
    private Card MakeCard(int cnum)
    {

        GameObject cgo = Instantiate(prefabCaard) as GameObject;

        cgo.transform.parent = deckAnchor;
        //获取card组件
        Card card = cgo.GetComponent<Card>();

        //排列纸牌
        cgo.transform.localPosition = new Vector3((cnum % 13) * 3, cnum % 13 * 4, 0);//???

        //设置基本属性
        card.name = cardNames[cnum];
        card.suit = card.name[0].ToString();
        card.rank = int.Parse(card.name.Substring(1));
        if (card.suit == "D" || card.suit == "H")
        {
            card.colS = "Red";
            card.color = Color.red;
        }

        //提取纸牌定义
        card.def = GetCardDefinitionByRank(card.rank);

        AddDecorators(card);
        

        return card;

    }

    //helper
    private Sprite tSp = null;
    private GameObject tGO = null;
    private SpriteRenderer tSR = null;

    private void AddDecorators(Card card)
    {
        //添加角码
        foreach(Decorator deco in decorators)
        {
            if (deco.type == "suit")
            {
                tGO = Instantiate(prefabSprite) as GameObject;
                tSR = tGO.GetComponent<SpriteRenderer>();
                //设置为正确花色
                tSR.sprite = dictSuits[card.suit];

            }
            else
            {
                tGO = Instantiate(prefabSprite) as GameObject;
                tSR = tGO.GetComponent<SpriteRenderer>();
                tSR.sprite = rankSprites[card.rank];
                tSR.color = card.color;
            }

            tSR.sortingOrder = 1;

            tGO.transform.parent = card.transform;//????

            tGO.transform.localPosition = deco.loc;

            if (deco.flip)
            {
                //180度欧拉旋转使得卡牌翻转
                tGO.transform.rotation = Quaternion.Euler(0, 0, 180);
            }

            if (deco.scale != 1)
            {
                tGO.transform.localScale = Vector3.one * deco.scale;
            }

            tGO.name = deco.type;

            card.decoGOs.Add(tGO);
        }

        cards.Add(card);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
