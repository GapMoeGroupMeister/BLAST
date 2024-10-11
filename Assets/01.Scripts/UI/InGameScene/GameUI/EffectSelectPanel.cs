using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EffectSelectPanel : UIPanel
{
    [SerializeField] private WeaponUIDataSO uiDataSO;
    [SerializeField] private EffectSelectSlot[] slots;
    List<WeaponType> weaponTypes = new List<WeaponType>();

    //Managements
    private WeaponManager _weaponManager;

    private int _openCount = 0;
    public bool isOpenedSelectPanel = false;

    private void Start()
    {
        _weaponManager = WeaponManager.Instance;

        for (int i = 0; i < slots.Length; ++i)
        {
            slots[i].OnSelectedEndEvent += HandleSelectedEnd;
        }

        XPManager.Instance.OnLevelUpEvent += HandleLevelUp;
        foreach (WeaponType type in Enum.GetValues(typeof(WeaponType)))
        {
            if (type == 0) continue;
            weaponTypes.Add(type);
        }
    }

    private void OnDestroy()
    {
        for (int i = 0; i < slots.Length; ++i)
        {
            slots[i].OnSelectedEndEvent -= HandleSelectedEnd;
        }
    }

    private void Update()
	{
        if (_openCount > 0 && isOpenedSelectPanel == false)
        {
            PlayerPartController.GetCurrentPlayerPart().SetCanAttackValue(false);
            isOpenedSelectPanel = true;
            Open();
        }
    }


    private void HandleLevelUp(int level)
	{
        ++_openCount;
    }

    public override void Open()
    {
        //셔플
        WeaponTypeShuffle(weaponTypes);

        //카드 결정
        List<WeaponType> curWeaponTypes = new List<WeaponType>();
		foreach (var weaponType in weaponTypes)
		{
            Weapon weapon = _weaponManager.GetWeapon(weaponType);

            //등록할 수 있는 무기인가
            if (weapon.canUse)
			{
                //등록 가능한 자리가 없고
                if(WeaponManager.Instance.GetCurWeaponCount() == 10)
				{
                    Debug.Log("sfsf");
                    //새로운 무기를 만난다면 추가하지 않겠다.
                    if (weapon.weaponEnabled == false)
                        continue;
				}

                //전용 무기인가
                if (weapon.isUniqueWeapon)
				{
                    if(weapon.partType != PlayerPartController.GetCurrentPlayerPart().playerPartType)
                        continue;
				}
                //전용 무기가 아니라면
                curWeaponTypes.Add(weaponType);
            }
            //등록을 모두 마쳤다면
            if(curWeaponTypes.Count >= 3)
			{
                break;
			}
		}

        //카드 셋팅
        SetUpWeaponCards(curWeaponTypes);

        RectTransform rectTrm = transform as RectTransform;
        rectTrm.anchoredPosition = new Vector2(0f, -400f);
        Sequence seq = DOTween.Sequence();
        seq.SetUpdate(true);
        seq.Append(rectTrm.DOAnchorPosY(0f, 0.3f));
        seq.Join(_canvasGroup.DOFade(1, _activeDuration).SetUpdate(_useUnscaledTime));
        seq.AppendInterval(0.3f);
        seq.AppendCallback(() =>
        {
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        });
    }

	public override void Close()
	{
        RectTransform rectTrm = transform as RectTransform;
        rectTrm.anchoredPosition =  new Vector2(0f, 0f);
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
        Sequence seq = DOTween.Sequence();
        seq.SetUpdate(true);
        seq.Append(rectTrm.DOAnchorPosY(-400f, 0.3f));
        seq.Join(_canvasGroup.DOFade(0, _activeDuration).SetUpdate(_useUnscaledTime));
        seq.AppendCallback(() => 
        {
            --_openCount;
            isOpenedSelectPanel = false;
            PlayerPartController.GetCurrentPlayerPart().SetCanAttackValue(true);
        });
    }

	private void SetUpWeaponCards(List<WeaponType> curWeaponTypes)
    {
		for (int i = 0; i < curWeaponTypes.Count; ++i)
		{
            slots[i].SetWeaponInfo(
                curWeaponTypes[i], 
                uiDataSO[curWeaponTypes[i]]);
        }

        //멈추기
        TimeManager.PauseTime();
    }

	public List<WeaponType> WeaponTypeShuffle(List<WeaponType> list)
    {
        // Random 인스턴스 생성 (Unity에서 Random.Range를 사용할 수도 있지만, 시스템의 랜덤 클래스를 사용하는 것이 일반적)
        System.Random rng = new System.Random();

        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);  // 0부터 n 사이의 무작위 인덱스를 선택
            WeaponType value = list[k];
            list[k] = list[n];
            list[n] = value;
        }

        return list;
    }

    private void HandleSelectedEnd()
	{
        TimeManager.PlayTime();
        Close();
	}
}
