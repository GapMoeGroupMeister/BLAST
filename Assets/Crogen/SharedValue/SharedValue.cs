using UnityEngine;

namespace Assets.Crogen.SharedValue
{
	public class SharedValue<T>
	{
		private T _value;
		public T Value
		{
			get => _value;
			set
			{
				_value = value;
			}
		}
	}
}
