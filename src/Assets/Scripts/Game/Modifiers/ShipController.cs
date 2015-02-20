using UnityEngine;
using System.Collections;
using Trixter.Core;
using Trixter.GGj15.GameInput;
using Trixter.GGJ15.ValueObjects;
using Trixter.GGJ15.Commands;

namespace Trixter.GGj.Modifiers {
	/// <summary>
	/// Ship controller.
	/// </summary>
	public class ShipController : BaseBehaviour {
		/// <summary>Game input.</summary>
		[Inject]
		public IGameInput input;
		[Inject]
		[HideInInspector]
		public ShipData shipData;
		/// <summary>The vertical force.</summary>
		public float forceVertical = 20.0f;
		/// <summary>The horizontal force.</summary>
		public float rotationAngle = 60.0f;
		/// <summary>The fuel consumption when the engine is fired [0-1].</summary>
		public float fuelConsumptionByEngine = 0.005f;
		/// <summary>The fuel consumption when a rotation is occuring [0-1].</summary>
		public float fuelConsumptionByRotation = 0.001f;
		
		/// <summary>The cached rigid body.</summary>
		protected Rigidbody cachedRigidBody;

		protected override void Start() {
			base.Start();

			this.cachedRigidBody = this.rigidbody;
		}

		protected void FixedUpdate() {
			if (this.input.mainEngine()) {
				this.cachedRigidBody.AddForce(
					this.cachedTransform.rotation * Vector3.forward * this.forceVertical, ForceMode.Acceleration);
				this.shipData.fuel -= this.fuelConsumptionByEngine;
			}

			/*
			if (this.input.thrusterBackwards()) {
				this.cachedRigidBody.AddForce(
					this.cachedTransform.rotation * Vector3.forward * -this.forceVertical, ForceMode.Acceleration);
				this.shipData.fuel -= this.fuelConsumptionByEngine;
			}
			
			if (this.input.thrusterLeft()) {
				this.cachedTransform.Rotate(0, -this.rotationAngle * Time.fixedDeltaTime, 0);
				this.cachedRigidBody.velocity = this.cachedTransform.forward * this.rigidbody.velocity.magnitude;
				this.shipData.fuel -= this.fuelConsumptionByRotation;
			}
		
			if (this.input.thrusterRight()) {
				this.cachedTransform.Rotate(0, this.rotationAngle * Time.fixedDeltaTime, 0);
				this.cachedRigidBody.velocity = this.cachedTransform.forward * this.rigidbody.velocity.magnitude;
				this.shipData.fuel -= this.fuelConsumptionByRotation;
			}
			*/

			if (this.shipData.fuel <= 0) {
				this.dispatcher.Dispatch<GameOverByFuel>();
				this.enabled = false;
			}
			
			this.shipData.speed = this.cachedRigidBody.velocity.magnitude;
		}
	}
}