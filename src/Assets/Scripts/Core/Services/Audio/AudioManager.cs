using UnityEngine;
using System.Collections;

namespace Trixter.Core.Audio {
	/// <summary>
	/// Manages audio.
	/// </summary>
	public static class AudioManager {
		/// <summary>Indicates if sounds are enabled.</summary>
		public static bool soundEnabled {
			get { return isSoundsEnabled; }
			set {
				isSoundsEnabled = value;
				try {
					AudioListener.pause = !isSoundsEnabled;
				} catch {
				}
				
				//HACK: because ignoreListenerPause is not working as expected,
				//if sounds are disabled and there's an enabled music, forces
				//the music to continue playing.
				if (!isSoundsEnabled && channelMusic != null && isMusicEnabled) {
					channelMusic.Play();
				}
			}
		}
		/// <summary>Indicates if the music is enabled.</summary>
		public static bool musicEnabled {
			get { return isMusicEnabled; }
			set {
				isMusicEnabled = value;
				
				if (channelMusic != null) {
					if (isMusicEnabled) {
						channelMusic.Play();
					} else {
						channelMusic.Stop();
					}
				}
			}
		}
		/// <summary>Gets/sets the volume for music and sounds.</summary>
		public static float volume {
			get { return AudioListener.volume; }
			set { AudioListener.volume = value; }
		}
		
		/// <summary>Indicates if sounds are enabled.</summary>
		private static bool isSoundsEnabled;		
		/// <summary>Indicates if the music is enabled.</summary>
		private static bool isMusicEnabled;
		/// <summary>The audio channel for music.</summary>
		private static AudioSource channelMusic;
		/// <summary>The audio channel for SFX.</summary>
		private static AudioSource channelSfx;

		/// <summary>
		/// Initializes the <see cref="Trixter.DevilRain.Core.Audio.AudioManager"/> class.
		/// </summary>
		static AudioManager() {
			var audioManager = new GameObject("AudioManager");
			MonoBehaviour.DontDestroyOnLoad(audioManager);

			channelMusic = audioManager.AddComponent<AudioSource>();
			channelMusic.playOnAwake = false;
			channelMusic.loop = true;
			channelMusic.ignoreListenerPause = true;
			channelMusic.volume = 0.25f;

			channelSfx = audioManager.AddComponent<AudioSource>();
			channelSfx.playOnAwake = false;
			channelSfx.loop = false;
		}

		/// <summary>
		/// Plays a sound.
		/// </summary>
		/// <param name="clip">Clip.</param>
		public static void PlaySound(AudioClip clip) {
			if (channelSfx.clip != null) {
				channelSfx.Stop();
			}

			channelSfx.clip = clip;
			channelSfx.Play();
		}

		/// <summary>
		/// Plays a music.
		/// </summary>
		/// <param name="clip">Clip.</param>
		public static void PlayMusic(AudioClip clip) {
			StopMusic();

			channelMusic.clip = clip;
			channelMusic.Play();
		}

		/// <summary>
		/// Stops the current music, if there's any.
		/// </summary>
		public static void StopMusic() {
			if (channelMusic.clip != null) {
				channelMusic.Stop();
			}
		}
	}
}