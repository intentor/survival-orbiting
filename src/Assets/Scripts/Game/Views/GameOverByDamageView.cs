using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Trixter.Core;

namespace Trixter.GGJ15.Views {
	/// <summary>
	/// Game over by damage view.
	/// </summary>
	[AddComponentMenu("Game/Views/Game Over by Damage")]
	public class GameOverByDamageView : BaseBehaviour {
		/// <summary>The label message.</summary>
		public Text labelMessage;

		/// <summary>Game over message.</summary>
		public string message {
			get { return this.labelMessage.text; }
			set { this.labelMessage.text = value; }
		}

		public void Restart() {
			Application.LoadLevel(Application.loadedLevel);
		}
		
		public void Menu() {
			Application.LoadLevel("Menu");
		}
	}
}