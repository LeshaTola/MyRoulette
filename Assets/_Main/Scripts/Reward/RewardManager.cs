using UnityEngine;
using YG;

public enum RewardId
{
	AddMoney,
}

public class RewardManager : MonoBehaviour
{
	[SerializeField] private Wallet wallet;

	private void OnEnable() => YandexGame.RewardVideoEvent += Rewarded;

	private void OnDisable() => YandexGame.RewardVideoEvent -= Rewarded;

	void Rewarded(int id)
	{
		RewardId rewardId = (RewardId)id;

		switch (rewardId)
		{
			case RewardId.AddMoney:
				int addMoneyAmount = 100;
				wallet.AddMoney(addMoneyAmount);
				break;
		}
	}
}
