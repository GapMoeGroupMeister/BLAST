using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class LoadingSceneImageLoader : MonoBehaviour
{
    [SerializeField] private Sprite[] _sprites;
	[SerializeField] private SpriteRenderer[] _spriteRenderer;
	[SerializeField] private float _fadeDelay = 1f;
	private float _currentTime = 0;
	private int _currentImageIndex = 0;
	 
	[SerializeField] private TextMeshProUGUI _tipText;
	[SerializeField] private string[] tips;
	private int _currentTipIndex = 0;


	private void Update()
	{
		_currentTime += Time.deltaTime;

		if (_currentTime > _fadeDelay)
		{
			_currentTime = 0;
			Sequence seq = DOTween.Sequence();
			_spriteRenderer[_currentImageIndex % 2].sprite = _sprites[_currentImageIndex % _sprites.Length];
			_spriteRenderer[(_currentImageIndex + 1) % 2].sprite = _sprites[(_currentImageIndex+1)%_sprites.Length];
			seq.Append(_spriteRenderer[(_currentImageIndex+1) % 2].DOFade(1, 0.5f));
			seq.Join(_spriteRenderer[_currentImageIndex%2].DOFade(0, 0.5f));
			++_currentImageIndex;
		}

		if(Input.GetMouseButtonDown(0))
		{
			_tipText.text = tips[_currentTipIndex % tips.Length];
			++_currentTipIndex;
		}
	}
}
