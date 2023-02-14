using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RyanHipplesArchitecture.SO_RuntimeSet;
using System.Threading;

/// <summary>
/// Creates Selection Frames out of a prefab.
/// They are instantiated, their partner object (SelectedThing) is set using the given index (newSelectableIndex).
/// Then they are initializied (a la custom Start(), named SelectionFrameInit()).
/// </summary>
public class SpawnFrame : MonoBehaviour
{


    [SerializeField] private SelectionFrame framePrefab;
    [SerializeField] private RuntimeSet_isSelectable allSelectables;

    private SelectionFrame _freshFrame;



    /// <summary>
    /// Instantiates Selection Frame, assigns tracked selectable object to frame
    /// </summary>
    /// <param name="newSelectableIndex">Index of newest Selectable item in the Selectables Runtime Set.
    /// It's given by the OnNewSelectableCreated event.
    /// </param>
    public void DoSpawnFrame(int newSelectableIndex)
    {

        _freshFrame = Instantiate(framePrefab, transform);
        _freshFrame.SelectedThing = allSelectables.Items[newSelectableIndex];
        _freshFrame.SelectionFrameInit();


    }

}
