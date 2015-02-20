using UnityEngine;
using System.Collections;
using Trixter.Core.Command;
using Trixter.GGJ15.Views;
using Trixter.GGJ15.ValueObjects;

namespace Trixter.GGJ15.Commands {
	/// <summary>
	/// Deals damage to the ship.
	/// </summary>
	public class DealDamage : GameCommand {
		[Inject]
		public ShipData shipData;
		[Inject("Ship")]
		public Transform ship;
		
		public override void Execute (params object[] parameters) {
			this.shipData.hull -= (float)parameters[0];

			if (this.shipData.hull <= 0) {
				this.dispatcher.Dispatch<GameOverByDamage>();
			}
		}
	}
}