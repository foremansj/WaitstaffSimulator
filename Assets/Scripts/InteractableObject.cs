using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    bool isInteractable = true;
    public bool GetInteractable() {
        return isInteractable;
    }

    public void ChangeInteractability(bool value) {
        isInteractable = value;
    }

    public void ReverseInteractability() {
        isInteractable = !isInteractable;
    }
}
