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
        // Create a list of available sibling indices
        availableIndices = Enumerable.Range(0, transform.parent.childCount).ToList();
        
        // Randomize the order of available indices
        System.Random random = new System.Random();
        availableIndices = availableIndices.OrderBy(x => random.Next()).ToList();
        
        // Assign this object a random sibling index
        foreach (Transform t in transform.parent)
        {
            int randomIndex = availableIndices.First();
            t.SetSiblingIndex(randomIndex);
            availableIndices.Remove(randomIndex);
        }
    }
}
