using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CardListUI : MonoBehaviour
{
    private void Start()
    {
        DisableCardList();
        //ShowCardListUI();
    }
    public List<Card> cardList;
    public void DisableCardList()
    {
        foreach(Card card in cardList)
        {
            card.DisableCard();
        }
    }
    void EnableCardList()
    {
        foreach(Card card in cardList)
        {
            card.EnableCard();
        }
    }
    public void ShowCardListUI()
    {
        GetComponent<RectTransform>().DOAnchorPosY(480, 1.5f);
        EnableCardList();
    }
}
