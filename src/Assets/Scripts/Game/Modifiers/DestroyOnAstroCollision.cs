using UnityEngine;
using System.Collections;
using Trixter.Core;
using Trixter.GGJ15.Constants;
using Trixter.GGJ15.Commands;

namespace Trixter.GGJ15.Modifiers {
	/// <summary>
	/// Destroy on astro collision.
	/// </summary>
	public class DestroyOnAstroCollision : BaseBehaviour {
		protected void OnCollisionEnter(Collision other) {
			if (other.collider.CompareTag(GameTag.PLANET) || other.collider.CompareTag(GameTag.STAR)) {
				if (this.CompareTag(GameTag.SHIP)) {
					this.dispatcher.Dispatch<GameOverByDamage>();
				}
			}
		}
	}
}