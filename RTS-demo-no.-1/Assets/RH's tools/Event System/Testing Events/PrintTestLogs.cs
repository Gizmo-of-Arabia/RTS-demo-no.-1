using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elevator_Sim.Tests
{


    [CreateAssetMenu]
    public class PrintTestLogs  : ScriptableObject
    {


        public void Print()
        {
            Debug.Log("Print() was called without a param!");

        }

        public void Print(int time)
        {
            Debug.Log("Print(int) was called with a param: " + time.ToString());

        }
    }
}
