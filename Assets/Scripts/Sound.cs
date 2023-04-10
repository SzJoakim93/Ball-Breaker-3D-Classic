using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    [SerializeField]
    AudioSource [] sounds;

    [SerializeField]
    AudioSource bg_music;

    static LinkedList<int> soundTracks = new LinkedList<int>();

    void Start()
    {
        if (Settings.MusicEnabled)
        {
			if (soundTracks.Count == 0) {
				for (int i = 0; i < 8; i++)
					soundTracks.AddFirst(i);
			}
			
			float x = Random.Range(0.0f, soundTracks.Count);
			int soundTrack = getTrackByIndex((int)x);
			soundTracks.Remove(soundTrack);
            bg_music.clip = Resources.Load<AudioClip>($"Music/{soundTrack+1}");

			bg_music.volume = Settings.MusicVolume;
			bg_music.Play();
            Debug.Log($"Music/{soundTrack+1}.wav");
        }

        foreach (var sound in sounds)
            sound.volume = Settings.SoundVolume;
    }
    public void Play(int index)
    {
        if (Settings.SoundEnabled)
            sounds[index].Play();
    }

    int getTrackByIndex(int x) {
		int i = 0;
		foreach (var s in soundTracks) {
			if (i == x)
				return s;
			i++;
		}

		return -1;
	}
}