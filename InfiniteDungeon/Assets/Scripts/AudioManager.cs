using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	[SerializeField]
	private static AudioManager instance;
	[SerializeField]
	private AudioMixerGroup mixerGroup;
	[SerializeField]
	private Sound[] sounds;

	void Awake()
	{

		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;

			s.source.outputAudioMixerGroup = mixerGroup;
		}
	}
	//To play a sound from another script, referencing the Audio Manager
	public void Play(string sound)
	{
		//Looping through all the sounds
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			//In case there is no sound with that name
			Debug.LogWarning("Sound: " + name + " not found!");
			return;

		}
		//Volume and pitch
			s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
			s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));
		
		s.source.Play();
	}

	public void Stop(string sound)
    {
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}
		s.source.Stop();
	}
}
