using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RyanHipplesArchitecture.SO_RuntimeSet;
using System.Threading;

public class SpawnFrame : MonoBehaviour
{


    [SerializeField] private SelectionFrame framePrefab;
    [SerializeField] private RuntimeSet_isSelectable allSelectables;

    private SelectionFrame freshFrame;



    // Instantiates Selection Frame, assigns tracked selectable object to frame
    public void DoSpawnFrame(int newSelectableIndex)
    {

        freshFrame = Instantiate(framePrefab, transform);
        freshFrame.SelectedThing = allSelectables.Items[newSelectableIndex];


    }

}
