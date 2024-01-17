using System.Collections.Generic;
using UnityEngine;

public class OneBetContainerUI : MonoBehaviour
{
	[SerializeField] private Transform container;
	[SerializeField] private Roulette roulette;
	[SerializeField] private BetType betType;
	[SerializeField] private OneBetUI oneBetUITemplate;

	private void Start()
	{
		roulette.OnBetting += OnBetting;
	}

	private void OnDestroy()
	{
		roulette.OnBetting -= OnBetting;
	}

	private void UpdateUI(List<Bet> listOfBets)
	{
		foreach (Transform child in container)
		{
			Destroy(child.gameObject);
		}

		foreach (Bet bet in listOfBets)
		{
			if (bet.Type == betType)
			{
				var oneBetUI = Instantiate(oneBetUITemplate, container);
				oneBetUI.SetupUI(bet.Value);
			}
		}
	}

	private void OnBetting(List<Bet> listOfBets)
	{
		UpdateUI(listOfBets);
	}
}
