using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName ="SO/StageWaveSO")]
public class StageWaveSO : ScriptableObject
{

    public WaveSO[] wavelist;
    public float waveTerm = 4f;
}