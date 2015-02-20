using UnityEngine;
using System.Collections;
using Trixter.Core;
using Trixter.GGj15.GameInput;
using Trixter.GGJ15.ValueObjects;
using Trixter.GGJ15.Commands;
using Trixter.GGJ15.Constants;

namespace Trixter.GGj.Modifiers {
	/// <summary>
	/// Detect out of world.
	/// </summary>
	public class DetectOutOfWorld : BaseBehaviour {
		protected void OnTriggerExit(Collider other) {
			if (other.CompareTag(GameTag.WORLD_LIMITS)) {
				this.dispatcher.Dispatch<GameOverByDamage>();
			}
		}
	}
}