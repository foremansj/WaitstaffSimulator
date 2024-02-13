using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] List<MenuItemSO> appetizers;

    public MenuItemSO GetRandomAppetizer() {
        return appetizers[Random.Range(0, appetizers.Count)];
    }
}
