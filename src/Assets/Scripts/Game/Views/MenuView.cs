using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Trixter.Core;
using Trixter.GGJ15.ValueObjects;
using Trixter.Core.Audio;

namespace Trixter.GGJ15.Views {
	/// <summary>
	/// Menu view.
	/// </summary>
	[AddComponentMenu("Game/Views/Menu")]
	public class MenuView : MonoBehaviour {
		protected GameObject help;

		protected void Start() {
			this.help = GameObject.Find("Views/Help");
			this.help.Hide();
			
			AudioManager.soundEnabled = true;
			
			var music = (AudioClip)Resources.Load("Audio/Music/Menu");
			AudioManager.PlayMusic(music);
		}

		public void ShowHelp() {
			this.help.Show();
			this.Hide();
		}

		public void StartGame() {
			LevelData.currentLevel = 1;
			Application.LoadLevel("Level1");
		}
	}
}