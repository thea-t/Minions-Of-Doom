using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class DeckManager : MonoBehaviour
{
    private List<MinionBase> handPile = new List<MinionBase>();
    [SerializeField] private List<MinionBase> deckPile = new List<MinionBase>();
    private List<MinionBase> discardPile = new List<MinionBase>();

    [SerializeField] private GameObject m_DrawPile;
    [SerializeField] private GameObject m_DiscardPile;
    [SerializeField] private BNG.SnapZone[] m_SnapPoints;


    //Shuffling the deck pile when the game starts, and drawing random cards in the player's hand 
    //Subscribing to Turn Manager's events.
    //Listening for the end and the beginning of each turn in order to discard and draw cards 
    private void Start()
    {
        ShufflePile(deckPile);

        GameManager.Instance.TurnManager.EnemyTurn += (delegate
        {
            StartCoroutine(DiscardMinions(handPile.Count));
        });

        GameManager.Instance.TurnManager.PlayerTurn += delegate
        {
            StartCoroutine(DrawingMinions(GameManager.Instance.Player.CardsToDrawOnStart));
        };
    }

    //Draws certain amount of cards by iterating through player's deck
    private IEnumerator DrawingMinions(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            MinionBase minion = deckPile[0];
            handPile.Add(minion);
            deckPile.Remove(minion);
            minion.transform.position = m_SnapPoints[i].transform.position;
            minion.transform.rotation = Quaternion.Euler(0, m_SnapPoints[i].transform.eulerAngles.y, 0);
            


            if (deckPile.Count == 0)
            {
                ResetDeckPile();
            }

            yield return new WaitForSeconds(0.1f);
            minion.OnMinionDrawn();
        }
    }

    //Reseting the deck pile when the cards are over
    void ResetDeckPile()
    {
        deckPile = new List<MinionBase>(discardPile);

        for (int i = 0; i < deckPile.Count; i++)
        {
            deckPile[i].transform.DOMove(m_DrawPile.transform.position, 0.1f);
        }

        discardPile.Clear();
        ShufflePile(deckPile);
    }

    //Discarding certain amount of cards
    private IEnumerator DiscardMinions(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            MinionBase minion = handPile[i];
            discardPile.Add(minion);
            minion.transform.DOMove(m_DiscardPile.transform.position, 0.5f);
            minion.transform.DOScale(Vector3.zero, 0.5f);

            yield return new WaitForSeconds(0.1f);
        }

        handPile.Clear();
    }


    //Shuffles the pile
    // How to shuffle items in list: https://stackoverflow.com/questions/273313/randomize-a-listt
    private void ShufflePile(List<MinionBase> pile)
    {
        System.Random random = new System.Random();
        int n = pile.Count;

        for (int i = pile.Count - 1; i > 1; i--)
        {
            int rnd = random.Next(i + 1);

            MinionBase value = pile[rnd];
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