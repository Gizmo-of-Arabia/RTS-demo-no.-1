using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RyanHipplesArchitecture.SO_RuntimeSet;
using System.Threading;

public class SpawnFrame : MonoBehaviour
{
    [SerializeField]
    private RuntimeSet_isSelectable AllSelectables;

    [SerializeField]
    private GameObject FramePrefab;

    private GameObject freshFrame;
    private SelectionFrame freshFrameComp;


    // Instantiates Selection Frame, assigns tracked selectabled object to frame
    public void DoSpawnFrame()
    {

        freshFrame = Instantiate(FramePrefab, transform);
        freshFrameComp = freshFrame.GetComponent<SelectionFrame>();
        freshFrameComp.selectedThing = AllSelectables.Last();

    }

}
