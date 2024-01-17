using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class WalletUI : MonoBehaviour
{
	[SerializeField] private Wallet wallet;
	[SerializeField] private TextMeshProUGUI moneyText;
	[SerializeField] private Button AddMoneyButton;

	private void Awake()
	{
		AddMoneyButton.onClick.AddListener(() =>
		{
			YandexGame.RewVideoShow((int)RewardId.AddMoney);
		});
	}

	private void Start()
	{
		wallet.OnMoneyChanged += OnMoneyChanged;
		UpdateUI(wallet.Money);
	}

	private void OnDestroy()
	{
		wallet.OnMoneyChanged -= OnMoneyChanged;
	}

	private void OnMoneyChanged()
	{
		UpdateUI(wallet.Money);
	}

	private void UpdateUI(float coins)
	{
		moneyText.text = Math.Round(coins, 2).ToString();

	}
}
