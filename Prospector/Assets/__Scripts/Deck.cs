using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    [Header("Set in inspector")]
    public bool startFaceUp = false;
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
    public Sprite cardFron_tGOld;

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
        cgo.transform.localPosition = new Vector3((cnum % 13) * 3, cnum / 13 * 4, 0);//???

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
        AddPips(card);
        AddFace(card);
        AddBack(card);

        return card;

    }

    //helper
    private Sprite _tSp = null;
    private GameObject _tGO = null;
    private SpriteRenderer _tSR = null;

    private void AddDecorators(Card card)
    {
        //添加角码
        foreach(Decorator deco in decorators)
        {
            if (deco.type == "suit")
            {
                _tGO = Instantiate(prefabSprite) as GameObject;
                _tSR = _tGO.GetComponent<SpriteRenderer>();
                //设置为正确花色
                _tSR.sprite = dictSuits[card.suit];

            }
            else
            {
                _tGO = Instantiate(prefabSprite) as GameObject;
                _tSR = _tGO.GetComponent<SpriteRenderer>();
                _tSR.sprite = rankSprites[card.rank];
                _tSR.color = card.color;
            }

            _tSR.sortingOrder = 1;

            _tGO.transform.parent = card.transform;//????

            _tGO.transform.localPosition = deco.loc;

            if (deco.flip)
            {
                //180度欧拉旋转使得卡牌翻转
                _tGO.transform.rotation = Quaternion.Euler(0, 0, 180);
            }

            if (deco.scale != 1)
            {
                _tGO.transform.localScale = Vector3.one * deco.scale;
            }

            _tGO.name = deco.type;

            card.decoGOs.Add(_tGO);
        }

        cards.Add(card);
    }

    private void AddPips(Card card)
    {
        foreach (Decorator pip in card.def.pips)
        {
            
            _tGO = Instantiate(prefabSprite) as GameObject;
            //设置父对象
            _tGO.transform.SetParent(card.transform);
            //初始位置xml文件数据设置
            _tGO.transform.localPosition = pip.loc;
            //旋转
            if (pip.flip)
            {
                _tGO.transform.rotation = Quaternion.Euler(0, 0, 180);
            }
            //缩放
            if (pip.scale != 1)
            {
                _tGO.transform.localScale = Vector3.one * pip.scale;
            }
            //设置名称
            _tGO.name = "pip";
            //获取渲染组件
            _tSR = _tGO.GetComponent<SpriteRenderer>();
            //设置正确花色
            _tSR.sprite = dictSuits[card.suit];
            //设置sortingOrder花色显示
            _tSR.sortingOrder = 1;
            //加入列表
            card.pipGOs.Add(_tGO);
        }
    }

    private void AddFace(Card card)
    {
        if (card.def.face == "")
        {
            return; //face不为空
        }
        _tGO = Instantiate(prefabSprite) as GameObject;
        _tSR = _tGO.GetComponent<SpriteRenderer>();
        //生成名称
        _tSp = GetFace(card.def.face + card.suit);
        _tSR.sprite = _tSp; //传出sprite
        _tSR.sortingOrder = 1; //设置sortingOrder
        _tGO.transform.SetParent(card.transform);
        _tGO.transform.localPosition = Vector3.zero;
        _tGO.name = "face";
    }

    private void AddBack(Card card)
    {
        // 添加纸牌背面
        _tGO = Instantiate(prefabSprite) as GameObject;
        _tSR = _tGO.GetComponent<SpriteRenderer>();

        _tSR.sprite = cardBack;

        _tGO.transform.SetParent(card.transform);
        _tGO.transform.localPosition = Vector3.zero;
        //背面sortingOrder值高于其他元素
        _tSR.sortingOrder = 2;
        _tGO.name = "back";
        card.back = _tGO;
        //默认的face-up值
        card.faceUp = startFaceUp; //使用card的faceup属性
    }

    //查找正确花牌sprite
    private Sprite GetFace(string faceS)
    {
        foreach (Sprite _tSP in faceSprites)
        {
            // 名称正确
            if (_tSP.name == faceS)
            {
                // 返回正确sprite
                return (_tSP);
            }
        }
        //没有返回null
        return (null);
    }

    //洗牌
    static public void Shuffle(ref List<Card> oCards)//ref引用参数也会修改原始值
    {
        //存储顺序
        List<Card> tCards = new List<Card>();
        int ndx; //移动索引
        tCards = new List<Card>(); //初始化list
                                   // 原始list还有纸牌就循环
        while (oCards.Count > 0)
        {
            //随机抽取纸牌索引
            ndx = Random.Range(0, oCards.Count);
            //添加到临时list
            tCards.Add(oCards[ndx]);
            //从原始list删除
            oCards.RemoveAt(ndx);
        }
        //用新的临时list取代原始list
        oCards = tCards;
        //ref引用参数也会修改原始值
    }

    // Start is called before the first frame update

}
