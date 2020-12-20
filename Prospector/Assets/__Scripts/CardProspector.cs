using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// An enum defines a variable type with a few prenamed values // a
public enum eCardState
{
    drawpile,
    tableau,
    target,
    discard
}

public class CardProspector : Card
{ // 从card继承
    [Header("Set Dynamically: CardProspector")]
    // 枚举eCardState使用方式
    public eCardState state = eCardState.drawpile;

    // hiddenBy保存背面展示卡牌
    public List<CardProspector> hiddenBy = new List<CardProspector>();

    // layoutID 和 XML 匹配判断是否为场景纸牌
    public int layoutID;

    // SlotDef 存储从LayoutXML <slot>导入的信息
    public SlotDef slotDef;

    // This allows the card to react to being clicked
    //override public void OnMouseUpAsButton()
    //{
    //    // Call the CardClicked method on the Prospector singleton
    //    Prospector.S.CardClicked(this);
    //    // Also call the base class (Card.cs) version of this method
    //    base.OnMouseUpAsButton(); // }
    //}

}
