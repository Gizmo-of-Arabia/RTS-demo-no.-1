using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RyanHipplesArchitecture.SO_RuntimeSet;
using System.Threading;

public class SpawnFrame : MonoBehaviour
{


    [SerializeField] private SelectionFrame FramePrefab;

    private SelectionFrame freshFrame;


    // Instantiates Selection Frame, assigns tracked selectabled object to frame
    public void DoSpawnFrame()
    {

        freshFrame = Instantiate(FramePrefab, transform);

    }

}
