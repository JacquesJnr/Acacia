using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMatch : MonoBehaviour
{
    [SerializeField] private List<int> matchIds;
    public static event Action<int> OnMatch;
    public static event Action OnNoMatch;

    // Add the clicked cards ID to an array
    private void OnEnable()
    {
        Card.OnCardShown += OnCardShown;
    }

    private void OnCardShown(int id)
    {
        matchIds.Add(id);
        bool hasDuplicates = matchIds.Distinct().Count() != matchIds.Count();
        
        if (matchIds.Count <=1) { return; }
        
        if (hasDuplicates)
        {
            OnMatch?.Invoke(id);
        }
        else
        {
            OnNoMatch?.Invoke();
        }
        
        matchIds.Clear();
    }
    
    public void RemoveDuplicates(int id)
    {
        for (int i = 0; i < matchIds.Count; i++)
        {
            if (matchIds[i] == id)
            {
                matchIds.RemoveAt(i);
            }
        }
    }

    public void ClearMatches()
    {
        matchIds.Clear();
    }

    private void OnDisable()
    {
        Card.OnCardShown -= OnCardShown;
    }
}
