using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISizeupable
{
	public Vector3 DefaultSize { get; set; }
	/// <summary>
	/// 처음에 무조건 1로 초기화하기
	/// </summary>
	public BulletSizeUpWeapon BulletSizeUpWeapon { get; set; }
	public float MultipliedCount { get; set; }
	/// <summary>
	/// 활성화될 때 실행해야 한다 (ex. OnPop())
	/// </summary>
	public void OnResize();
}
