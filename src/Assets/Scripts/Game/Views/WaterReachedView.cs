using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Trixter.Core;
using Trixter.GGJ15.ValueObjects;
using Trixter.GGJ15.Commands;

namespace Trixter.GGJ15.Views {
	/// <summary>
	/// Water reached view.
	/// </summary>
	[AddComponentMenu("Game/Views/Water reached")]
	public class WaterReachedView : BaseBehaviour {
		/// <summary>The label message.</summary>
		public Text labelMessage;
		
		/// <summary>Game over message.</summary>
		public string message {
			get { return this.labelMessage.text; }
			set { this.labelMessage.text = value; }
		}
		
		public void Restart() {
			this.dispatcher.Dispatch<LoadNextLevel>();
		}
		
		public void Menu() {
			Application.LoadLevel("Menu");
		}
	}
}