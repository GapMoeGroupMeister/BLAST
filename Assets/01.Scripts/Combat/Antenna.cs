using UnityEngine;

public class Antenna : MonoBehaviour
{
	[SerializeField] private float _rotateSpeed = 300f;
	private void FixedUpdate()
	{
		transform.eulerAngles += new Vector3(0, Time.deltaTime * _rotateSpeed, 0);
	}
}
