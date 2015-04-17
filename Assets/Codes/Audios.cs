using UnityEngine;
using System.Collections;

public class Audios : MonoBehaviour {
	public AudioClip coinCollect;
	public AudioClip jump;
	public AudioClip dash;
	public AudioClip crash;
	public AudioClip warp;

	private AudioSource audioSource;
	private AudioClip _audioClip;

	public void Awake() {
		audioSource=gameObject.AddComponent<AudioSource>();
	}
	public void playSound(string type)
	{
		if (type == "coin")
			_audioClip = coinCollect;
		else if (type == "jump")
			_audioClip = jump;
		else if (type == "dash")
			_audioClip = dash;
		else if (type == "crash")
			_audioClip = crash;
		else if (type == "warp")
			_audioClip = warp;

		audioSource.clip = _audioClip;
		audioSource.Play ();
	}
}
