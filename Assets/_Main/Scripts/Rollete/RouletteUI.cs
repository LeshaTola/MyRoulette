using DG.Tweening;
using UnityEngine;

public class RouletteUI : MonoBehaviour
{
	[Header("Main")]
	[SerializeField] private AnimationCurve rouletteSpeed;
	[SerializeField] private Roulette roulette;
	[SerializeField] private Cell paper;
	[SerializeField] private Cell rock;
	[SerializeField] private Cell scissors;

	[Header("Rolette Container")]
	[SerializeField] private RectTransform container;

	[Header("Other")]
	[SerializeField] private GameObject pointer;
	[SerializeField] private RouletteTimerUI TimerUI;

	private float cellWidth;
	private float startPositionX;

	private void Awake()
	{
		CreateRolette();
		cellWidth = container.GetChild(0).GetComponent<RectTransform>().sizeDelta.x;
		startPositionX = container.position.x;
	}

	private void Start()
	{
		roulette.OnRoletteStartSpin += OnRoletteStartSpin;
		roulette.OnRoletteRestart += OnRoletteRestart;
		roulette.OnStateChanged += OnRouletteStateChanged;
	}

	private void OnDestroy()
	{
		roulette.OnRoletteStartSpin -= OnRoletteStartSpin;
		roulette.OnRoletteRestart -= OnRoletteRestart;
		roulette.OnStateChanged -= OnRouletteStateChanged;
	}

	private void CreateRolette()
	{
		CreateCells(1, 5);

		for (int i = 0; i < Roulette.MAX_COUNT_OF_SPINS; i++)
		{
			CreateOneSpin();
		}

		Cell zero = Instantiate(rock, container);
		CreateCells(1, 4);
	}

	private void CreateCells(int from, int to)
	{
		for (int i = from; i < to; i++)
		{
			Cell cell = i % 2 == 0 ? Instantiate(paper, container) : Instantiate(scissors, container);
		}
	}

	private void CreateOneSpin()
	{
		Cell zero = Instantiate(rock, container);

		CreateCells(1, Roulette.COUNT_OF_CELLS);
	}

	private void RestartRolette(float restartTime)
	{
		container.DOAnchorPosX(startPositionX, restartTime).SetEase(rouletteSpeed);
	}

	private void SpinRolette(int spins, int id, float spinTime)
	{
		float fullSpinRange = spins * cellWidth * Roulette.COUNT_OF_CELLS;
		float spinRangeForCell = id * cellWidth;
		float halfOfCellWidth = cellWidth / 2;
		float littleOffset = Random.Range(-(halfOfCellWidth - 10), (halfOfCellWidth - 10));

		container.DOAnchorPosX(startPositionX - fullSpinRange - spinRangeForCell + littleOffset, spinTime).SetEase(rouletteSpeed);
	}

	//private Ienumerator

	private void OnRoletteRestart(float time)
	{
		RestartRolette(time);
	}

	private void OnRoletteStartSpin(int spins, int id, float time)
	{
		SpinRolette(spins, id, time);
	}

	private void OnRouletteStateChanged(RouletteState obj)
	{
		if (obj == RouletteState.Betting)
		{
			pointer.SetActive(false);
			TimerUI.Show();
		}
		else
		{
			pointer.SetActive(true);
			TimerUI.Hide();
		}
	}
}
