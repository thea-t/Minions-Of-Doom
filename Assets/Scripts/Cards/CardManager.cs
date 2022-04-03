using System;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    private List<CardBase> handPile = new List<CardBase>();
    private List<CardBase> deckPile = new List<CardBase>();
    private List<CardBase> discardPile = new List<CardBase>();

    [SerializeField] private GameObject m_DrawPile;
    [SerializeField] private GameObject m_DiscardPile;
    [SerializeField] private GameObject[] m_SnapPoints;

    private void Start()
    {
        ShufflePile(deckPile);
        DrawCards(PlayerStats.CardsToDrawOnStart);
    }

    private void DrawCards(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            CardBase card = deckPile[0];
            handPile.Add(card);
            deckPile.Remove(card);

            if (deckPile.Count == 0)
            {
                deckPile = new List<CardBase>(discardPile);
                discardPile.Clear();
                ShufflePile(deckPile);
            }
            
            card.OnCardDrawn();
        }
    }

    // How to shuffle items in list: https://stackoverflow.com/questions/273313/randomize-a-listt
    private void ShufflePile(List<CardBase> pile)
    {
        System.Random random = new System.Random();
        int n = pile.Count;

        for (int i = pile.Count - 1; i > 1; i--)
        {
            int rnd = random.Next(i + 1);

            CardBase value = pile[rnd];
            pile[rnd] = pile[i];
            pile[i] = value;
        }
    }
}
