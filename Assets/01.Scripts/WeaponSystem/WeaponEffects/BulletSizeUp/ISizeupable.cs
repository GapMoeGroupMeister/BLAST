using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISizeupable
{
	public float MultipliedCount { get; set; }
	/// <summary>
	/// Ȱ��ȭ�� �� �����ؾ� �Ѵ� (ex. OnPop())
	/// </summary>
	public void OnResize(float multipliedCount);
}
