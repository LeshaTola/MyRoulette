using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class OneBetUI : MonoBehaviour
{
	[SerializeField] private Image profileImage;
	[SerializeField] private TextMeshProUGUI nameText;
	[SerializeField] private TextMeshProUGUI betText;
	[SerializeField] private ImageLoadYG loadYG;

	public void SetupUI(float bet)
	{
		if (loadYG != null && YandexGame.auth)
		{
			loadYG.Load(YandexGame.playerPhoto);

		}

		nameText.text = YandexGame.playerName;
		betText.text = bet.ToString();
	}
}
