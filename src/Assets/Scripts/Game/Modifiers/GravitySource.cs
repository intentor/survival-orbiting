using UnityEngine;
using System.Collections;
using Trixter.Core;
using Trixter.GGJ15.Constants;
using Trixter.GGj.Modifiers;

namespace Trixter.GGJ15.Modifiers {
	/// <summary>
	/// Applies circular gravity around an object, grabbing any other object
	/// that comes close enough within a the speedy as the gravity force.
	/// </summary>
	[AddComponentMenu("Game/Modifiers/Gravity Force")]
	public class GravitySource : BaseBehaviour {
		/// <summary>Force radius.</summary>
		public float radius;
		/// <summary>The outline segments.</summary>
		public int outlineSegments = 30;
		/// <summary>Indicate if gizmos should be drawn.</summary>
		public bool drawGizmos = true;
		/// <summary>Indicates wheter the system is insensible to new colliders.</summary>
		public bool insensible = false;
		
		/// <summary>Cached rigid body.</summary>
		protected Rigidbody cachedRigidBody;
		/// <summary>The line renderer for the planet's outline force.</summary>
		protected LineRenderer outlineLineRenderer;
		/// <summary>The counter.</summary>
		protected float counter;

		protected override void Start() {
			base.Start();

			this.cachedRigidBody = this.rigidbody;

			this.outlineLineRenderer = this.gameObject.AddComponent<LineRenderer>();
			this.outlineLineRenderer.material = new Material(Shader.Find("Particles/Additive"));
			this.outlineLineRenderer.SetWidth(0.02f, 0.02f);
			this.outlineLineRenderer.SetColors(Color.white, Color.white);
			this.outlineLineRenderer.SetVertexCount(this.outlineSegments + 1);
		}

		protected void FixedUpdate() {
			this.OnDrawOutline();

			if (this.insensible) {
				counter += Time.deltaTime;
				if (counter > 1.0f) {
					this.insensible = false;
					counter = 0;
				} else {
					return;
				}
			}

			var colliders = Physics.OverlapSphere(this.transform.position, this.radius, 1 << GameLayer.DEFAULT);

			for (var colliderIndex = 0; colliderIndex < colliders.Length; colliderIndex++) {
				var receiverCollider = colliders[colliderIndex];

				if (receiverCollider.rigidbody.isKinematic) continue;

				var receiverTransform = receiverCollider.transform;
				
				float angleForward = Mathf.DeltaAngle(
					Mathf.Atan2(receiverTransform.forward.z, receiverTransform.forward.x) * Mathf.Rad2Deg,
					Mathf.Atan2(this.cachedTransform.position.z, this.cachedTransform.position.x) * Mathf.Rad2Deg);

				float angleRight = Mathf.DeltaAngle(
					Mathf.Atan2(receiverTransform.right.z, receiverTransform.right.x) * Mathf.Rad2Deg,
					Mathf.Atan2(this.cachedTransform.position.z, this.cachedTransform.position.x) * Mathf.Rad2Deg);
				
				var direction = Mathf.Sign(
					(angleForward > 0 && angleRight < 0 ? angleForward : (angleForward < 0 && angleRight > 0 ? angleRight : angleForward))
				);
				
				//Adjust the ship.               
	            receiverCollider.rigidbody.isKinematic = true;
	            receiverCollider.GetComponent<ShipController>().enabled = false;

				//Adjust receiver's rotation.
				receiverTransform.LookAt(this.cachedTransform.position);
				receiverTransform.Rotate(0, -90.0f * direction, 0);

				//Adjust orbit for the ship.
				var orbit = receiverCollider.GetComponent<OrbitAstro>();
				var speed = (60.0f / this.radius * direction) * receiverCollider.rigidbody.velocity.magnitude;
				orbit.StartOrbiting(this.cachedTransform, speed, this.radius);
			}
		}

		protected void OnDrawGizmos() {
			var cachedTransform = this.transform;

			var position = cachedTransform.position;
			var rotation = cachedTransform.rotation;	

			var gizmosColor = this.GetGizmosColor();
			Gizmos.color = gizmosColor;

			//Force lines.
			Gizmos.DrawLine(position + ((rotation * Vector3.up) * this.radius), position);
			Gizmos.DrawLine(position + ((rotation * Vector3.down) * this.radius), position);
			Gizmos.DrawLine(position + ((rotation * Vector3.left) * this.radius), position);
			Gizmos.DrawLine(position + ((rotation * Vector3.right) * this.radius), position);
			Gizmos.DrawLine(position + ((rotation * Vector3.forward) * this.radius), position);
			Gizmos.DrawLine(position + ((rotation * Vector3.back) * this.radius), position);

			//Sphere around.
			Gizmos.color = new Color(gizmosColor.r, gizmosColor.g, gizmosColor.b, 0.3f);
			Gizmos.DrawSphere(position, this.radius);
		} 

		protected void OnDrawOutline() {
			float x;
			float z;
			
			float angle = 20f;

			if (this.GetComponent<DamageOrbitingObject>() != null) {
				this.outlineLineRenderer.SetColors(Color.red, Color.red);
			}
			
			for (int i = 0; i < (this.outlineSegments + 1); i++) {
				x = this.cachedTransform.position.x + Mathf.Sin(Mathf.Deg2Rad * angle) * this.radius;
				z = this.cachedTransform.position.z + Mathf.Cos(Mathf.Deg2Rad * angle) * this.radius;
				
				this.outlineLineRenderer.SetPosition (i, new Vector3(x, this.cachedTransform.position.y, z));
				
				angle += (360f / this.outlineSegments);
			}
		}

		/// <summary>
		/// Gets the color of the gizmos.
		/// </summary>
		/// <returns>The gizmos color.</returns>
		protected Color GetGizmosColor() {
			Color gizmosColor = Color.white;

			return gizmosColor;
		}
	}
}