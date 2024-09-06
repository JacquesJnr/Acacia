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

    public void SetInteractable()
    {
        foreach (Card card in cards)
        {
            card.ButtonBehavior.SetInteractable(true);
        }
    }

    public void SetUnineractable()
    {
        foreach (Card card in cards)
        {
            card.ButtonBehavior.SetInteractable(false);
        }
    }

    public void FlipAll()
    {
        foreach (Card card in cards)
        {
            if(card.ButtonBehavior.flipped) {break;}
            card.ButtonBehavior.Flip();
        }
    }

    private void OnEnable()
    {
        GameManager.OnStateChanged += OnStateChanged;
    }

    private void OnStateChanged(GameState state)
    {
        if (state == GameState.Reset)
        {
            FlipAll();
        }

        if (state == GameState.None)
        {
            Clear();
        }
    }
}
