using System.Collections;
using System.Collections.Generic;
using Crogen.PowerfulInput;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [field: SerializeField] public InputReader InputReader;
}
