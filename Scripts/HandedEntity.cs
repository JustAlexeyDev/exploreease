using System;
using System.Collections.Generic;
using UnityEngine;

public class HandedEntity : MonoBehaviour
{
    public Transform[] handPoints;
    public List<Weapon> itemsInHands;
    
    public bool AttachItem(Weapon reference)
    {
        if (handPoints.Length < itemsInHands.Count + 1)
        {
            return false;
        }

        itemsInHands.Add(reference);

        RaspolozitPredmeti();

        return true;
    }

    public Weapon TakeItem(int index)
    {
        var itemsInHand = itemsInHands[index];

        itemsInHands[index] = null;
        
        return itemsInHand;
    }

    public void NextHand()
    {
        if (itemsInHands.Count > 1)
        {
            var firstItem = itemsInHands[0];
            itemsInHands.Remove(firstItem);
            itemsInHands.Add(firstItem);

            RaspolozitPredmeti();
        }
    }

    public Weapon GetItemInMainHand()
    {
        return itemsInHands.Count > 0 ? itemsInHands[0] : null;
    }

    public void PreviousHand()
    {
        if (itemsInHands.Count > 0)
        {
            var lastItem = itemsInHands[^1];
            itemsInHands.Remove(lastItem);
            itemsInHands.Insert(Math.Max(0, itemsInHands.Count - 1), lastItem);

            RaspolozitPredmeti();
        }
    }

    public void RaspolozitPredmeti()
    {
        var min = Math.Min(itemsInHands.Count, handPoints.Length);
        for (int i = 0; i < min; i++)
        {
            itemsInHands[i].transform.SetParent(handPoints[i]);
            itemsInHands[i].transform.position = handPoints[i].position;
            itemsInHands[i].transform.rotation = handPoints[i].rotation;
            itemsInHands[i].transform.localScale = handPoints[i].localScale;

            itemsInHands[i].enabled = false;
        }
        
        itemsInHands[0].enabled = true;
    }
}