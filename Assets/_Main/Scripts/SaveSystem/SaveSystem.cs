using UnityEngine;
using YG;

public class SaveSystem : MonoBehaviour
{
	[SerializeField] private Wallet wallet;
	[SerializeField] private Roulette roulette;

	private void OnEnable() => YandexGame.GetDataEvent += Load;

	private void OnDisable() => YandexGame.GetDataEvent -= Load;

	private void Start()
	{
		if (YandexGame.SDKEnabled == true)
		{
			Load();
		}
		roulette.OnStateChanged += OnRouletteStateChanged;
	}

	private void OnDestroy()
	{
		roulette.OnStateChanged -= OnRouletteStateChanged;
	}

	public void Load()
	{
		wallet.LoadData();
	}

	public void Save()
	{
		wallet.SaveData();

		YandexGame.SaveProgress();
	}

	private void OnRouletteStateChanged(RouletteState state)
	{
		if (state == RouletteState.Show)
		{
			Save();
		}
	}
}
