using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    private List<CardBase> handPile = new List<CardBase>();
    [SerializeField] private List<CardBase> deckPile = new List<CardBase>();
    private List<CardBase> discardPile = new List<CardBase>();

    [SerializeField] private GameObject m_DrawPile;
    [SerializeField] private GameObject m_DiscardPile;
    [SerializeField] private BNG.SnapZone[] m_SnapPoints;

    private CardBase draggedCard;

    //Shuffling the deck pile when the game starts, and drawing random cards in the player's hand 
    //Subscribing to Turn Manager's events.
    //Listening for the end and the beginning of each turn in order to discard and draw cards 
    private void Start()
    {
        ShufflePile(deckPile);
        StartCoroutine(DrawCards(GameManager.Instance.Player.CardsToDrawOnStart));

        GameManager.Instance.TurnManager.EnemyTurn += (delegate
        {
            StartCoroutine(DiscardCards(handPile.Count));
        });

        GameManager.Instance.TurnManager.PlayerTurn += delegate
        {
            StartCoroutine(DrawCards(GameManager.Instance.Player.CardsToDrawOnStart));
        };
    }

    //Draws certain amount of cards by iterating through player's deck
    private IEnumerator DrawCards(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            CardBase card = deckPile[0];
            handPile.Add(card);
            deckPile.Remove(card);
            m_SnapPoints[i].GrabGrabbable(card.grabbable);


            if (deckPile.Count == 0)
            {
                ResetDeckPile();
            }

            yield return new WaitForSeconds(0.1f);
            card.OnCardDrawn();
        }
    }

    //Reseting the deck pile when the cards are over
    void ResetDeckPile()
    {
        deckPile = new List<CardBase>(discardPile);

        for (int i = 0; i < deckPile.Count; i++)
        {
            deckPile[i].transform.DOMove(m_DrawPile.transform.position, 0.1f);
        }

        discardPile.Clear();
        ShufflePile(deckPile);
    }

    //Discarding certain amount of cards
    private IEnumerator DiscardCards(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            CardBase card = handPile[i];
            discardPile.Add(card);
            card.transform.DOMove(m_DiscardPile.transform.position, 0.5f);
            card.transform.DOScale(Vector3.zero, 0.5f);

            yield return new WaitForSeconds(0.1f);
        }

        handPile.Clear();
    }


    //Shuffles the pile
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

    #region Deprecated
/*
    //OLD CODE THAT WAS CREATED WHILE THE TARGET PLATFORM WAS STILL MOBILE
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (draggedCard == null)
            {
             //   draggedCard = GameManager.Instance.RaycastManager.GetByRay<CardBase>();
            }

            if (draggedCard) draggedCard.OnDragBegin();

        }
        else if (Input.GetMouseButton(0))
        {
            if (draggedCard) draggedCard.OnDrag();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (draggedCard) draggedCard.OnDragEnd();

            draggedCard = null;
        }

    }*/
#endregion

}