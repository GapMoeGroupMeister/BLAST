using Crogen.PowerfulInput;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [field: SerializeField] public InputReader InputReader { get; private set; }
    [field: SerializeField] public Player Player { get; private set; }

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.I))
		{
			Vector3 ranPos = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
			XPManager.Instance.CreateXP(ranPos);
		}
	}
}
