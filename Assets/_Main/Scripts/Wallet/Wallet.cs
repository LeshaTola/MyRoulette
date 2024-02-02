using System;
using UnityEngine;
using YG;

public class Wallet : MonoBehaviour, IUseSaves
{
	public event Action OnMoneyChanged;

	public int Money { get; private set; }

	public void AddMoney(int count)
	{
		if (count <= 0)
		{
			return;
		}

		Money += count;
		OnMoneyChanged?.Invoke();
	}

	public void DenyMoney(int count)
	{
		if (count <= 0)
		{
			return;
		}

		Money -= count;
		OnMoneyChanged?.Invoke();
	}

	public void SaveData()
	{
		YandexGame.savesData.Money = Money;
	}

	public void LoadData()
	{
		Money = YandexGame.savesData.Money;
		OnMoneyChanged?.Invoke();
	}
}
