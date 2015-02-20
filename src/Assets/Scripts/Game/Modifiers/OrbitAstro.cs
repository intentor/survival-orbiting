using UnityEngine;
using System.Collections;
using Trixter.Core;
using Trixter.GGJ15.Constants;
using Trixter.GGj.Modifiers;
using Trixter.GGj15.GameInput;
using Trixter.GGJ15.ValueObjects;
using Trixter.GGJ15.Commands;

namespace Trixter.GGJ15.Modifiers {
	/// <summary>
	/// Rotates around an astro.
	/// </summary>
	[AddComponentMenu("Game/Modifiers/Orbit astro")]
	public class OrbitAstro : BaseBehaviour {
		/// <summary>Game input.</summary>
		[Inject]
		public IGameInput input;
		[Inject]
		[HideInInspector]
		public ShipData shipData;
		/// <summary>The astro.</summary>
		public Transform astro;
		/// <summary>The astro radius.</summary>
		public float astroRadius;
		/// <summary>The rotation speed.</summary>
		public float speed = 30.0f;
		/// <summary>The rotation acceleration.</summary>
		public float acceleration = 1.0f;
		/// <summary>The fuel consumption when the engine is fired [0-1].</summary>
		public float fuelConsumptionByEngine = 0.001f;
		/// <summary>Indicates whether the engine was activated.</summary>
		public bool engineActivated;
		/// <summary>The smooth follow.</summary>
		public SmoothFollow follow;
		/// <summary>The force to tangent.</summary>
		public float forceToTangent = 30.0f;
		/// <summary>The original speed.</summary>
		protected float originalSpeed;

		protected void Awake() {			
			this.follow = Camera.main.GetComponent<SmoothFollow>();
		}
		
		protected override void Start() {
			base.Start();
		}
		
		protected void FixedUpdate() {
			if (this.input.mainEngine()) {
				this.speed += this.acceleration;
				this.shipData.fuel -= this.fuelConsumptionByEngine;

				if (this.shipData.fuel <= 0) {
					this.dispatcher.Dispatch<GameOverByFuel>();
					this.enabled = false;
				}

				this.engineActivated = true;
			} else if (this.engineActivated) {
				this.engineActivated = false;

				var controller = this.GetComponent<ShipController>();
				controller.enabled = true;

				var force = forceToTangent * (this.speed / this.originalSpeed);
				this.rigidbody.isKinematic = false;
				this.rigidbody.AddForce(this.cachedTransform.rotation * Vector3.forward * force, ForceMode.Impulse);
				this.astro.GetComponent<GravitySource>().insensible = true;
				//this.follow.target = this.cachedTransform;
				this.enabled = false;
			}

			this.cachedTransform.RotateAround(this.astro.position, Vector3.up, this.speed * Time.deltaTime);
		}

		public void StartOrbiting(Transform astro, float speed, float radius) {
			this.originalSpeed = speed;
			this.speed = speed;
			this.astro = astro;
			this.astroRadius = radius;
			//this.follow.target = null;
			this.enabled = true;

			if (this.astro.GetComponent<WaterPlanet>() != null) {
				this.dispatcher.Dispatch<WaterReached>();
			}
		}
	}
}