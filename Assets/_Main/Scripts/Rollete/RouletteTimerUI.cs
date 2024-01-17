using System;
using TMPro;
using UnityEngine;

public class RouletteTimerUI : MonoBehaviour
{
	[SerializeField] private Roulette roulette;
	[SerializeField] private TextMeshProUGUI timerText;

	private void Start()
	{
		roulette.OnBettingTimerChanged += OnRouletteBettingTimerChanged;
	}

	private void OnDestroy()
	{
		roulette.OnBettingTimerChanged -= OnRouletteBettingTimerChanged;
	}

	private void OnRouletteBettingTimerChanged(float time)
	{
		timerText.text = Math.Round(time, 2).ToString();
	}

	public void Hide()
	{
		gameObject.SetActive(false);
	}

	public void Show()
	{
		gameObject.SetActive(true);
	}
}
