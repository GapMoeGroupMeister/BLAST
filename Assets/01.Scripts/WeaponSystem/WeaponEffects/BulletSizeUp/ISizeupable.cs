using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISizeupable
{
	public Vector3 DefaultSize { get; set; }
	/// <summary>
	/// ó���� ������ 1�� �ʱ�ȭ�ϱ�
	/// </summary>
	public BulletSizeUpWeapon BulletSizeUpWeapon { get; set; }
	public float MultipliedCount { get; set; }
	/// <summary>
	/// Ȱ��ȭ�� �� �����ؾ� �Ѵ� (ex. OnPop())
	/// </summary>
	public void OnResize();
}
