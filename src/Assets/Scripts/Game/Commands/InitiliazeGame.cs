using UnityEngine;
using System.Collections;
using Trixter.Core.Command;
using Trixter.GGJ15.ValueObjects;
using Trixter.GGJ15.Views;
using Trixter.Core.Audio;

namespace Trixter.GGJ15.Commands {
	/// <summary>
	/// Dispatched when the game is initialized.
	/// </summary>
	public class InitializeGame : GameCommand {
		[Inject]
		public ShipData shipData;		
		[Inject]
		public HudView hudView;
		[Inject]
		public GameOverByFuelView gameOverFuel;
		[Inject]
		public GameOverByDamageView gameOverDamage;
		[Inject]
		public WaterReachedView waterReached;
		
		public override void Initialize() {

		}
		
		public override void Execute (params object[] parameters) {
			AudioManager.soundEnabled = true;

			//Sets no gravity.
			Physics.gravity = Vector3.zero;

			//Hide views.
			this.gameOverFuel.Hide();
			this.gameOverDamage.Hide();
			this.waterReached.Hide();

			this.shipData.fuel = 1;
			this.shipData.hull = 1;
			this.shipData.speed = 0;
			this.shipData.time = 0;

			var music = (AudioClip)Resources.Load("Audio/Music/Game");
			AudioManager.PlayMusic(music);
		}
	}
}