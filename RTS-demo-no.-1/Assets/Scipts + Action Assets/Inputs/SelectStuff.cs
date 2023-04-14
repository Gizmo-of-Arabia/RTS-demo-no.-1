using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Selects selectables that the SelectionMesh is colliding with.
/// </summary>
public class SelectStuff : MonoBehaviour
{
    private isSelectable _selectable;
    private void OnTriggerEnter(Collider other)
    {
        _selectable = other.gameObject.GetComponent<isSelectable>();
        if (!_selectable)
        {
            Debug.LogWarning("SelectionMesh collided with something non-selectable!");
            return;
        }
        _selectable.IsSelected = true;
    }
}
