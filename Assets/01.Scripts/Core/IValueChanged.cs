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
	/// ChangableValueEnum 무조건 직렬화해서 인스펙터창에서 값 지정할 것!
	/// </summary>
	public ChangableValueEnum ChangableValueEnum { get; set; }
	event ValueChangedEvent ValueChangedEvent;
}
