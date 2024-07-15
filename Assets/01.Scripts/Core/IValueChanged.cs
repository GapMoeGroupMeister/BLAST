public delegate void ValueChangedEvent(float value);

public enum ChangableValueEnum
{
	Speed,
	Hp,
	Overload
}

public interface IValueChanged
{
	/// <summary>
	/// ChangableValueEnum ������ ����ȭ�ؼ� �ν�����â���� �� ������ ��!
	/// </summary>
	public ChangableValueEnum ChangableValueEnum { get; set; }
	event ValueChangedEvent ValueChangedEvent;
}
