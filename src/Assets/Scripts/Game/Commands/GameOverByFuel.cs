using UnityEngine;
using System.Collections;
using Trixter.Core.Command;
using Trixter.GGJ15.Views;
using Trixter.GGJ15.ValueObjects;
using Trixter.Core.Audio;

namespace Trixter.GGJ15.Commands {
	/// <summary>
	/// Dispatched when the game is over by full fuel comsuption.
	/// </summary>
	public class GameOverByFuel : GameCommand {	
		[Inject]
		public GameOverByFuelView gameOverFuel;
		[Inject]
		public ShipData shipData;
		[Inject]
		public HudView hudView;

		protected string[] messages = {
			"Now you'll wander through the immensity of the space forever...",
			"Humanity's last hope is lost (until you try again).",
			"This is a game... so, try again!",
			"When you see FUEL LOW, start saving fuel!"
		};
		
		public override void Execute (params object[] parameters) {
			this.hudView.enabled = false;

			this.shipData.fuel = 0;
			this.gameOverFuel.message = this.messages[Random.Range(0, this.messages.Length - 1)];
			this.gameOverFuel.Show();
			
			AudioManager.soundEnabled = false;
		}
	}
}