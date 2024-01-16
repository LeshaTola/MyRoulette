using System;
using UnityEngine;

public enum RoletteState
{
	Betting,
	Scrolling,
	Show,
	Restart
}

public class Roulette : MonoBehaviour
{
	public const int COUNT_OF_CELLS = 37;
	public const int MAX_COUNT_OF_SPINS = 4;

	public event Action<float> OnBettingTimerChanged;
	public event Action<int, int, float> OnRoletteStartSpin;
	public event Action<float> OnRoletteRestart;

	[SerializeField] private float bettingTime;
	[SerializeField] private float scrollTime;
	[SerializeField] private float showTime;
	[SerializeField] private float restartTime;

	private float bettingTimer;
	private float scrollTimer;
	private float showTimer;
	private float restartTimer;

	private RoletteState state;

	private void Start()
	{
		state = RoletteState.Betting;
		bettingTimer = bettingTime;
		scrollTimer = scrollTime;
		showTimer = showTime;
		restartTimer = restartTime;
	}

	private void Update()
	{
		switch (state)
		{
			case RoletteState.Betting:
				bettingTimer -= Time.deltaTime;
				if (bettingTimer <= 0)
				{
					bettingTimer = bettingTime;
					RandomSpin();
					state = RoletteState.Scrolling;
				}
				OnBettingTimerChanged?.Invoke(bettingTime);
				break;

			case RoletteState.Scrolling:
				scrollTimer -= Time.deltaTime;
				if (scrollTimer <= 0)
				{
					scrollTimer = scrollTime;
					state = RoletteState.Show;
				}
				break;
			case RoletteState.Show:
				showTimer -= Time.deltaTime;
				if (showTimer <= 0)
				{
					showTimer = showTime;

					OnRoletteRestart?.Invoke(restartTime);
					state = RoletteState.Restart;
				}
				break;
			case RoletteState.Restart:
				restartTimer -= Time.deltaTime;
				if (restartTimer <= 0)
				{
					restartTimer = restartTime;
					state = RoletteState.Betting;
				}
				break;
		}
	}

	public void RandomSpin()
	{
		int countOfSpins = UnityEngine.Random.Range(1, MAX_COUNT_OF_SPINS);
		int cellId = UnityEngine.Random.Range(0, COUNT_OF_CELLS);
		Debug.Log(countOfSpins + " " + cellId);

		OnRoletteStartSpin?.Invoke(countOfSpins, cellId, scrollTime);
	}
}
