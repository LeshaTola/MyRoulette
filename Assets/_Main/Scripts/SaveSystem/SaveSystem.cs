using UnityEngine;
using YG;

public class SaveSystem : MonoBehaviour
{
	[SerializeField] private Wallet wallet;

	private void OnEnable() => YandexGame.GetDataEvent += Load;

	private void OnDisable() => YandexGame.GetDataEvent -= Load;

	private void Start()
	{
		if (YandexGame.SDKEnabled == true)
		{
			Load();
		}
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
}
