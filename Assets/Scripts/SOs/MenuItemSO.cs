using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Menu Item", menuName = "Menu Item")]
public class MenuItemSO : ScriptableObject
{
    public string itemName;
    public string description;

    public float baseCost;
    public float taxRate;
    
    public float ticketTimeSeconds;
    public string itemCategory;

    public float GetTotalCost()
    {
        return baseCost + (baseCost * taxRate);
    }
}
