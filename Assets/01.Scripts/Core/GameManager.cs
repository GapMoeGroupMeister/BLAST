using System.Collections;
using System.Collections.Generic;
using Crogen.PowerfulInput;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [field: SerializeField] public InputReader InputReader { get; private set; }
    [field: SerializeField] public Player Player { get; private set; }
}
