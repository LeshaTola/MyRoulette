using System;
using System.Collections.Generic;
using UnityEngine;
using YG;

public enum RouletteState
{
	Betting,
	Scrolling,
	Show,
	Restart
}

public enum BetType
{
	Paper,
	Rock,
	Scissors
}

public class Roulette : MonoBehaviour
{
	public const int COUNT_OF_CELLS = 37;
	public const int MAX_COUNT_OF_SPINS = 4;

	public event Action<float> OnBettingTimerChanged;
	public event Action<int, int, float> OnRoletteStartSpin;
	public event Action<RouletteState> OnStateChanged;
	public event Action<float> OnRoletteRestart;
	public event Action<List<Bet>> OnBetting;

	[SerializeField] private float bettingTime;
	[SerializeField] private float scrollTime;
	[SerializeField] private float showTime;
	[SerializeField] private float restartTime;

	[SerializeField] private Wallet wallet;
	[SerializeField] private AudioClip betTimeSound;

	private float bettingTimer;
	private float scrollTimer;
	private float showTimer;
	private float restartTimer;

	private int currentCell;
	private List<Bet> bets;

	private RouletteState state;

	private void Start()
	{
		bets = new List<Bet>();

		state = RouletteState.Betting;
		OnStateChanged?.Invoke(state);
		SoundManager.PlaySound(betTimeSound, Vector3.zero);

		bettingTimer = bettingTime;
		scrollTimer = scrollTime;
		showTimer = showTime;
		restartTimer = restartTime;
	}

	private void Update()
	{
		switch (state)
		{
			case RouletteState.Betting:
				bettingTimer -= Time.deltaTime;
				if (bettingTimer <= 0)
				{
					bettingTimer = bettingTime;

					RandomSpin();

					state = RouletteState.Scrolling;
					OnStateChanged?.Invoke(state);
				}

				OnBettingTimerChanged?.Invoke(bettingTimer);
				break;

			case RouletteState.Scrolling:
				scrollTimer -= Time.deltaTime;
				if (scrollTimer <= 0)
				{
					scrollTimer = scrollTime;

					state = RouletteState.Show;
					OnStateChanged?.Invoke(state);
				}
				break;

			case RouletteState.Show:
				showTimer -= Time.deltaTime;
				if (showTimer <= 0)
				{
					showTimer = showTime;

					ValidateBet();

					state = RouletteState.Restart;
					OnStateChanged?.Invoke(state);

					OnRoletteRestart?.Invoke(restartTime);
				}
				break;

			case RouletteState.Restart:
				restartTimer -= Time.deltaTime;
				if (restartTimer <= 0)
				{
					restartTimer = restartTime;

					bets.Clear();
					OnBetting?.Invoke(bets);

					state = RouletteState.Betting;
					OnStateChanged?.Invoke(state);
					SoundManager.PlaySound(betTimeSound, Vector3.zero);
				}
				break;
		}
	}

	public void RandomSpin()
	{
		int countOfSpins = UnityEngine.Random.Range(1, MAX_COUNT_OF_SPINS);
		currentCell = UnityEngine.Random.Range(0, COUNT_OF_CELLS);
		Debug.Log(countOfSpins + " " + currentCell);

		OnRoletteStartSpin?.Invoke(countOfSpins, currentCell, scrollTime);
	}

	public void MakeBet(Bet bet)
	{
		if (bet == null || bet.Value <= 0f)
		{
			return;
		}

		wallet.DenyMoney(bet.Value);
		bets.Add(bet);
		OnBetting?.Invoke(bets);
	}

	private void ValidateBet()
	{
		BetType viningBet = GetViningBet();

		foreach (var bet in bets)
		{
			if (bet.Type != viningBet)
			{
				continue;
			}

			switch (bet.Type)
			{
				case BetType.Paper:
					wallet.AddMoney(bet.Value * 2);
					break;
				case BetType.Rock:
					wallet.AddMoney(bet.Value * 14);
					break;
				case BetType.Scissors:
					wallet.AddMoney(bet.Value * 2);
					break;
			}
		}
		YandexGame.NewLeaderboardScores("Money", (int)(wallet.Money * 100));
	}

	public BetType GetViningBet()
	{
		if (currentCell == 0)
		{
			return BetType.Rock;
		}
		else if (currentCell % 2 == 0)
		{
			return BetType.Paper;
		}
		else
		{
			return BetType.Scissors;
		}
	}
}

public class Bet
{
	public BetType Type;
	public float Value;
}
