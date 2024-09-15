using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using TMPro;

public class SceneLoadingManager : MonoBehaviour
{
	private static string _nextScene;
	[SerializeField] private TextMeshProUGUI _descriptionText;
	[SerializeField] [TextArea(20, 100)] private string _description;
	[SerializeField] private string _readyForBattle;

	public static void LoadScene(string sceneName)
	{
		_nextScene = sceneName;
		SceneManager.LoadScene("LoadingScene");
	}

	private void Start()
	{
		LoadSceneProcess();
	}

	private void TMPRemoveLastChar(TMP_Text text)
	{
		if (text.text.Length == 0) return;
		text.text = text.text.Remove(text.text.Length - 1);
	}
	private bool TMPLastCharEqual(TMP_Text text, char word)
	{
		if (text.text.Length == 0) return false;
		return text.text[text.text.Length - 1].Equals(word);
	}

	private async void LoadSceneProcess()
	{
		var op = SceneManager.LoadSceneAsync(_nextScene);
		op.allowSceneActivation = false;

		int cnt = 0;
		int maxCount = _description.Length;
		bool isFormat = false;
		int formatStart = -1;
		_descriptionText.text = string.Empty;

		while(cnt < maxCount)
		{
			if (TMPLastCharEqual(_descriptionText, '_'))
			{
				TMPRemoveLastChar(_descriptionText);
			}

			if (_description[cnt]=='<')
			{
				formatStart = cnt;
				isFormat = true;
				cnt++;
			}
			else if (_description[cnt] == '>')
			{
				_descriptionText.text += _description.Substring(formatStart, cnt - formatStart + 2);
				isFormat = false;
				cnt++;
			}
			else
			{
				if (isFormat == false)
				{
					_descriptionText.text += _description[cnt];
				}
			}

			_descriptionText.text += '_';

			if(_description[cnt].Equals('\n'))
				await Task.Delay(100);
			else
			{
				float randomNum = 0.1f;
				if(randomNum < Random.Range(0f, 1f))
					await Task.Delay(5);
				else
					await Task.Delay(80);
			}

			cnt++;
		}
		TMPRemoveLastChar(_descriptionText);

		await WaitUntilComplete(op);

		LoadComplete(op);
	}

	private async Task WaitUntilComplete(AsyncOperation op)
	{
		while(op.progress < 0.9f)
		{
			await Task.Delay(100);
		}
	}

	private void LoadComplete(AsyncOperation op)
	{
		OnLoadSceneComplete(op);
	}

	private async void OnLoadSceneComplete(AsyncOperation op)
	{
		await Task.Delay(1000);
		int cnt = 0;
		_descriptionText.text += '\n';
		while (cnt < _readyForBattle.Length)
		{
			if (TMPLastCharEqual(_descriptionText, '_'))
			{
				TMPRemoveLastChar(_descriptionText);
			}

			_descriptionText.text += _readyForBattle[cnt];

			_descriptionText.text += '_';

			await Task.Delay(10);
			cnt++;
		}

		TMPRemoveLastChar(_descriptionText);

		//±ôºý±ôºý
		for (int i = 0; i < 3; ++i)
		{
			_descriptionText.text += '_';
			await Task.Delay(500);
			TMPRemoveLastChar(_descriptionText);
			await Task.Delay(500);
		}

		op.allowSceneActivation = true;
	}
}
