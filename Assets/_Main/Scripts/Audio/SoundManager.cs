using UnityEngine;

public class SoundManager : MonoBehaviour
{
	public static void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1f)
	{
		AudioClip audioClip = audioClipArray[Random.Range(0, audioClipArray.Length)];
		PlaySound(audioClip, position, volume);
	}

	public static void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplayer = 1f)
	{
		float volume = 1f;
		AudioSource.PlayClipAtPoint(audioClip, position, volumeMultiplayer * volume);
	}
}
