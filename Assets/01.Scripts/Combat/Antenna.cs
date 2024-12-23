using UnityEngine;

public class Antenna : MonoBehaviour
{
	[SerializeField] private Vector3 _direction = Vector3.up;
	[SerializeField] private float _rotateSpeed = 300f;
	private void FixedUpdate()
	{
		transform.eulerAngles += _direction * Time.deltaTime * _rotateSpeed;
	}
}
