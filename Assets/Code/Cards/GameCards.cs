using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCards : MonoBehaviour
{
    public List<Card> cards;

    public void Add(Card card)
    {
        cards.Add(card);
    }

    public void Clear()
    {
        foreach (Card card in cards)
        {
            Destroy(card.gameObject);
        }
        cards.Clear();
    }

    public void ClearMatchedCards(int id)
    {
        foreach (Card card in cards)
        {
            if (card.ID == id)
            {
                card.ButtonBehavior.OnMatched(0.5f);
            }
        }
    }
    
    public void FlipUpturned()
    {
        foreach (Card card in cards)
        {
            if (card.ButtonBehavior.isFaceUp)
            { 
              card.ButtonBehavior.DelayedFlip(0.75f);
            }
        }
    }

    private void OnEnable()
    {
        GameManager.OnStateChanged += OnStateChanged;
        CardMatch.OnMatch += ClearMatchedCards;
        CardMatch.OnNoMatch += FlipUpturned;
    }

    private void OnStateChanged(GameState state)
    {
        if (state == GameState.Reset)
        {
            FlipUpturned();
        }

        if (state == GameState.None)
        {
            Clear();
        }
    }
}
