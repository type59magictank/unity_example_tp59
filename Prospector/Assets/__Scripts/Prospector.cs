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

    [Header("set dynamically")]
    public Deck deck;

    private void Awake()
    {
        S = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        deck = GetComponent<Deck>();//获取deck脚本组件
        deck.Inindeck(deckXML.text);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
