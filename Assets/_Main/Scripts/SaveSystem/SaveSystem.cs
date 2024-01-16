using UnityEngine;
using YG;

public class SaveSystem : MonoBehaviour
{
	[SerializeField] private Wallet wallet;
	//[SerializeField] private float saveCooldown = 30f;

	private float saveTimer;

	private void OnEnable() => YandexGame.GetDataEvent += Load;

	private void OnDisable() => YandexGame.GetDataEvent -= Load;

	private void Start()
	{
		if (YandexGame.SDKEnabled == true)
		{
			Load();
		}
		//saveTimer = saveCooldown;
	}

	/*	private void Update()
		{
			HandlePeriodicallySaving();
		}

		private void HandlePeriodicallySaving()
		{
			saveTimer -= Time.deltaTime;
			if (saveTimer <= 0)
			{
				saveTimer = saveCooldown;
				Save();
			}
		}*/

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
