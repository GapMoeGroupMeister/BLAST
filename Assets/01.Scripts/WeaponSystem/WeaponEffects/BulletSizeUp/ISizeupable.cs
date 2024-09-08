using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISizeupable
{
	public float MultipliedCount { get; set; }
	/// <summary>
	/// 활성화될 때 실행해야 한다 (ex. OnPop())
	/// </summary>
	public void OnResize(float multipliedCount);
}
