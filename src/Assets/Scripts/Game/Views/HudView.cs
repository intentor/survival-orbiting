using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Trixter.Core;
using Trixter.GGJ15.ValueObjects;

namespace Trixter.GGJ15.Views {
	/// <summary>
	/// HUD view.
	/// </summary>
	[AddComponentMenu("Game/Views/Hud")]
	public class HudView : BaseBehaviour {
		/// <summary>The ship data.</summary>
		[Inject]
		public ShipData shipData;
		/// <summary>The label timer.</summary>
		public Text labelTimer;
		/// <summary>The label hull.</summary>
		public Text labelHull;
		/// <summary>The hull breach alert.</summary>
		public GameObject alertHullBreach;
		/// <summary>The label fuel.</summary>
		public Text labelFuel;
		/// <summary>The fuel low alert.</summary>
		public GameObject alertFuelLow;

		protected void Update() {
			this.shipData.time += Time.deltaTime;

			this.labelTimer.text = string.Format("{0:0}:{1:00}", 
				Mathf.Floor(this.shipData.time / 60), Mathf.RoundToInt(this.shipData.time % 60));
			this.ConfigureLabel(this.labelHull, this.shipData.hull, this.alertHullBreach);
			this.ConfigureLabel(this.labelFuel, this.shipData.fuel, this.alertFuelLow);
		}

		/// <summary>
		/// Configures the label.
		/// </summary>
		/// <param name="label">Label.</param>
		/// <param name="value">Value.</param>
		protected void ConfigureLabel(Text label, float value, GameObject alertObject) {
			label.text = string.Format("{0:0}%", value * 100);

			if (value < 0.2f) {
				label.color = Color.red;
				alertObject.Show();

				var audioSource = alertObject.GetComponent<AudioSource>();
				if (!audioSource.isPlaying) {
					audioSource.Play();
				}
			} else if (value < 0.5f) {
				label.color = Color.yellow;
				alertObject.Hide();
			} else {
				label.color = Color.white;
				alertObject.Hide();
			}
		}
	}
}