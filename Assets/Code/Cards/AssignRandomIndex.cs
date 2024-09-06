using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AssignRandomIndex : MonoBehaviour
{
    private List<int> availableIndices;

    private void OnEnable()
    {
        availableIndices = Enumerable.Range(0, transform.parent.childCount).ToList();
        
        System.Random random = new System.Random();
        availableIndices = availableIndices.OrderBy(x => random.Next()).ToList();
        
        foreach (Transform t in transform.parent)
        {
            int randomIndex = availableIndices.First();
            t.SetSiblingIndex(randomIndex);
            availableIndices.Remove(randomIndex);
        }
    }
}
