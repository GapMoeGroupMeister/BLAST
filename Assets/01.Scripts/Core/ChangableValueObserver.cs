using System.Collections.Generic;

public class ChangableValueObserver : MonoSingleton<ChangableValueObserver>
{
    public Dictionary<ChangableValueEnum, IValueChanged> valueChangedDictionary;

	private void Awake()
	{
		valueChangedDictionary = new Dictionary<ChangableValueEnum, IValueChanged>();

		IValueChanged[] ivalueChangeds = FindExtension.FindInterfaces<IValueChanged>();

		for (int i = 0; i < ivalueChangeds.Length; ++i)
		{
			valueChangedDictionary.Add(ivalueChangeds[i].ChangableValueEnum, ivalueChangeds[i]);
		}
	}
}
