using UnityEngine;
using System.Collections;
using Trixter.Core.Command;
using Trixter.GGJ15.Views;
using Trixter.GGJ15.ValueObjects;
using Trixter.Core.Audio;

namespace Trixter.GGJ15.Commands {
	/// <summary>
	/// Dispatched when the player reaches water.
	/// </summary>
	public class WaterReached : GameCommand {
		[Inject]
		public WaterReachedView waterReached;
		[Inject]
		public GameOverByDamageView gameOverDamage;
		[Inject]
		public GameOverByFuelView gameOverFuel;
		[Inject]
		public HudView hudView;
		
		protected string[] messages = {
			"Thanks to you, humanity found water and perpetuated in a new habitable world. But, hey! You can do better than {0}.",
			"It took you {0} to save the world.",
			"Although you saved the world, you can do it (again) in less than {0}."
		};
		
		public override void Execute (params object[] parameters) {
			this.hudView.enabled = false;
			
			;this.waterReached.message = 
				string.Format(this.messages[Random.Range(0, this.messages.Length - 1)], this.hudView.labelTimer.text);
			this.waterReached.Show();

			this.gameOverDamage.Hide();
			this.gameOverFuel.Hide();
			
			AudioManager.soundEnabled = false;
		}
	}
}