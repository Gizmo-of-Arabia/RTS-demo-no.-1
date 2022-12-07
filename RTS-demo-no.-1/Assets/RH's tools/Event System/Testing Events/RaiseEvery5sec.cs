using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RyanHipplesArchitecture.SO_Events;
using RyanHipplesArchitecture.SO_Variables;

namespace Elevator_Sim.Tests
{
    public class RaiseEvery5sec : MonoBehaviour
    {

        [SerializeField] private FloatReference currentTime;
        [SerializeField] private GameEvent On5secPassedNoParam;
        [SerializeField] private GameEvent_int On5secPassedWithParam;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {


            if (currentTime.Value > 2f)
            {
                //On5secPassedNoParam.Raise();
                //On5secPassedWithParam.Raise(Mathf.RoundToInt(currentTime.Value));
            }
        }
    }
}
