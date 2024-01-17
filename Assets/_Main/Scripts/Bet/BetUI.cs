using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BetUI : MonoBehaviour
{
	[SerializeField] private TMP_InputField betInputField;

	[Header("Input Field Buttons")]
	[SerializeField] private Button clearButton;
	[SerializeField] private Button add001Button;
	[SerializeField] private Button add01Button;
	[SerializeField] private Button add1Button;
	[SerializeField] private Button add10Button;
	[SerializeField] private Button add100Button;
	[SerializeField] private Button subtractButton;
	[SerializeField] private Button multiplyButton;
	[SerializeField] private Button maxButton;

	[Header("Bet Buttons")]
	[SerializeField] private Button betPaperButton;
	[SerializeField] private Button betRockButton;
	[SerializeField] private Button betScissorsButton;

	[Header("Other")]
	[SerializeField] private Wallet wallet;
	[SerializeField] private Roulette roulette;

	private void Awake()
	{
		clearButton.onClick.AddListener(() =>
		{
			betInputField.text = "";
		});

		add001Button.onClick.AddListener(() => AddToInputField(0.01f));
		add01Button.onClick.AddListener(() => AddToInputField(0.1f));
		add1Button.onClick.AddListener(() => AddToInputField(1f));
		add10Button.onClick.AddListener(() => AddToInputField(10f));
		add100Button.onClick.AddListener(() => AddToInputField(100f));

		subtractButton.onClick.AddListener(() =>
		{
			float currentValue = ParseInput();
			currentValue /= 2;
			betInputField.text = currentValue.ToString();
		});

		multiplyButton.onClick.AddListener(() =>
		{
			float currentValue = ParseInput();
			currentValue *= 2;
			betInputField.text = currentValue.ToString();
		});

		maxButton.onClick.AddListener(() =>
		{
			betInputField.text = wallet.Money.ToString();
		});

		betPaperButton.onClick.AddListener(() => roulette.MakeBet(new Bet() { Type = BetType.Paper, Value = ParseInput() }));
		betRockButton.onClick.AddListener(() => roulette.MakeBet(new Bet() { Type = BetType.Rock, Value = ParseInput() }));
		betScissorsButton.onClick.AddListener(() => roulette.MakeBet(new Bet() { Type = BetType.Scissors, Value = ParseInput() }));
	}

	private void Start()
	{
		betInputField.onValueChanged.AddListener((string s) => { Validate(); });
		roulette.OnBetting += OnBetting;
		roulette.OnStateChanged += OnRouletteStateChanged;
	}

	private void OnDestroy()
	{
		roulette.OnStateChanged -= OnRouletteStateChanged;
		roulette.OnBetting -= OnBetting;
	}

	private void AddToInputField(float amount)
	{
		float currentValue = ParseInput();
		currentValue += amount;
		betInputField.text = currentValue.ToString();
	}

	private float ParseInput()
	{
		if (string.IsNullOrEmpty(betInputField.text))
		{
			return 0f;
		}

		float.TryParse(betInputField.text, out float res);

		return res;
	}

	private void Validate()
	{
		float currentValue = ParseInput();

		if (currentValue < 0f)
		{
			betInputField.text = "0,00";
		}

		if (currentValue > wallet.Money)
		{
			betInputField.text = wallet.Money.ToString();
		}
	}

	private void OnRouletteStateChanged(RouletteState obj)
	{
		switch (obj)
		{
			case RouletteState.Betting:
				betPaperButton.interactable = true;
				betRockButton.interactable = true;
				betScissorsButton.interactable = true;
				break;
			case RouletteState.Scrolling:
				betPaperButton.interactable = false;
				betRockButton.interactable = false;
				betScissorsButton.interactable = false;
				break;
		}
	}

	private void OnBetting(List<Bet> obj)
	{
		Validate();
	}
}
