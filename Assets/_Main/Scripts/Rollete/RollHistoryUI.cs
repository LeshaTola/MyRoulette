using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RollHistoryUI : MonoBehaviour
{
	[SerializeField] private Image oneHistoryUITemplate;
	[SerializeField] private Transform container;
	[SerializeField] private int maxHistoryImage = 10;
	[SerializeField] private Roulette roulette;

	[SerializeField] private Sprite paperSprite;
	[SerializeField] private Sprite rockSprite;
	[SerializeField] private Sprite scissorsSprite;

	private List<Image> history;

	private void Awake()
	{
		history = new List<Image>();
	}

	private void Start()
	{
		roulette.OnStateChanged += OnRouletteStateChanged;
	}
	private void OnDestroy()
	{
		roulette.OnStateChanged -= OnRouletteStateChanged;
	}

	private void OnRouletteStateChanged(RouletteState state)
	{
		if (state == RouletteState.Show)
		{
			if (history.Count >= maxHistoryImage)
			{
				var child = container.GetChild(0);
				history.RemoveAt(0);
				Destroy(child.gameObject);
			}

			var newHistoryImage = Instantiate(oneHistoryUITemplate, container);
			switch (roulette.GetViningBet())
			{
				case BetType.Paper:
					newHistoryImage.sprite = paperSprite;
					break;
				case BetType.Rock:
					newHistoryImage.sprite = rockSprite;
					break;
				case BetType.Scissors:
					newHistoryImage.sprite = scissorsSprite;
					break;
			}
			history.Add(newHistoryImage);
		}
	}
}
