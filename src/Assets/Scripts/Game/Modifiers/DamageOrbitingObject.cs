using UnityEngine;
using System.Collections;
using Trixter.Core;
using Trixter.GGJ15.Constants;
using Trixter.GGJ15.Commands;

namespace Trixter.GGJ15.Modifiers {
	/// <summary>
	/// Damage objects orbiting the astro.
	/// </summary>
	[RequireComponent(typeof(GravitySource))]
	public class DamageOrbitingObject : BaseBehaviour {
		/// <summary>The damage per second.</summary>
		public float damagePerSecond = 0.1f;

		/// <summary>The gravity source.</summary>
		protected GravitySource gravitySource;

		protected override void Start() {
			base.Start();

			this.gravitySource = this.GetComponent<GravitySource>();
		}

		protected void FixedUpdate() {
			var colliders = Physics.OverlapSphere(this.transform.position, this.gravitySource.radius * 1.1f, 1 << GameLayer.DEFAULT);
			
			for (var colliderIndex = 0; colliderIndex < colliders.Length; colliderIndex++) {
				var receiverCollider = colliders[colliderIndex];

				if (receiverCollider.CompareTag(GameTag.SHIP)) {
					this.dispatcher.Dispatch<DealDamage>(this.damagePerSecond * Time.fixedDeltaTime);
				}
			}
		}
	}
}