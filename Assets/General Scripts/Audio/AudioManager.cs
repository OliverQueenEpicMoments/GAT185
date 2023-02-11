using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : Singleton<AudioManager> {
	[SerializeField] AudioSourceController AudioSourceSFX;
	[SerializeField] AudioSourceController AudioSourceMusic;

	// List of available audio source controllers
	private List<AudioSourceController> audioSourceControllers = new List<AudioSourceController>();

	public AudioSourceController GetController(AudioData.Type type) {
		AudioSourceController Output = null;

		// check for available audio source controllers
		if (audioSourceControllers.Count > 0) {
			// check matching type
			Output = audioSourceControllers.Find(audioSourceController => audioSourceController.type == type);
			if (Output != null) {
				// found available, remove from audio source controllers
				audioSourceControllers.Remove(Output);

				return Output;
			}
		}

		// could not get available audio source controller
		// create new audio source controller
		switch (type) {
			case AudioData.Type.SFX:
				return Instantiate(AudioSourceSFX);
			case AudioData.Type.MUSIC:
				return Instantiate(AudioSourceMusic);
			default:
				return null;
		}
	}

	public void ReturnController(AudioSourceController controller) {
		// return audio source controller to available list
		if (audioSourceControllers.Contains(controller) == false) {
			audioSourceControllers.Add(controller);
		}
	}
}