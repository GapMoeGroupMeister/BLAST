using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LoadingSceneImageLoader : MonoBehaviour
{
    [SerializeField] private Sprite[] _sprites;
	[SerializeField] private SpriteRenderer[] _spriteRenderer;
	private int _count = 0;

	[SerializeField] private float _fadeDelay = 1f;
	private float _currentTime = 0;

	private void Update()
	{
		_currentTime += Time.deltaTime;

		if (_currentTime > _fadeDelay)
		{
			_currentTime = 0;
			Sequence seq = DOTween.Sequence();
			_spriteRenderer[_count % 2].sprite = _sprites[_count % _sprites.Length];
			_spriteRenderer[(_count + 1) % 2].sprite = _sprites[(_count+1)%_sprites.Length];
			seq.Append(_spriteRenderer[(_count+1) % 2].DOFade(1, 0.5f));
			seq.Join(_spriteRenderer[_count%2].DOFade(0, 0.5f));
			++_count;
		}
	}
}
