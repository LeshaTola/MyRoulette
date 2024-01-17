using System;
using TMPro;
using UnityEngine;

public class WalletUI : MonoBehaviour
{
	[SerializeField] private Wallet wallet;
	[SerializeField] private TextMeshProUGUI moneyText;

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
