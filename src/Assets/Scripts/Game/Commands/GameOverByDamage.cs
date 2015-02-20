using UnityEngine;
using System.Collections;
using Trixter.Core.Command;
using Trixter.GGJ15.Views;
using Trixter.GGJ15.ValueObjects;
using Trixter.Core.Audio;

namespace Trixter.GGJ15.Commands {
	/// <summary>
	/// Dispatched when the game is over by maximum damage.
	/// </summary>
	public class GameOverByDamage : GameCommand {
		[Inject]
		public GameOverByDamageView gameOverDamage;
		[Inject]
		public ShipData shipData;
		[Inject("Ship")]
		public Transform ship;
		[Inject]
		public HudView hudView;

		protected string[] messages = {
			"Now you're dead (on the game, obviously). People may remember you as a hero.",
			"The end. But you can always try again!",
			"Your ship is not undestructable.",
			"When you see HULL BREACH, deorbit the astro now!"
		};
		
		public override void Execute (params object[] parameters) {
			this.hudView.enabled = false;

			this.shipData.hull = 0;
			this.gameOverDamage.message = this.messages[Random.Range(0, this.messages.Length - 1)];
			this.gameOverDamage.Show();
			this.ship.Hide();

			AudioManager.soundEnabled = false;
		}
	}
}