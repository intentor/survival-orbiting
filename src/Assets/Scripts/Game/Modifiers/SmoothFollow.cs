using UnityEngine;
using System.Collections;
using Trixter.Core;

namespace Trixter.GGJ15.Modifiers {
	/// <summary>
	/// Smooth follows a camera.
	/// </summary>
	[AddComponentMenu("Camera-Control/Smooth Follow")]
	public class SmoothFollow : BaseBehaviour {
		// The target we are following
		public Transform target;
		// The distance in the x-z plane to the target
		public float distance = 10.0f;
		// the height we want the camera to be above the target
		public float height = 5.0f;
		// How much we
		public float heightDamping = 2.0f;
		public float rotationDamping = 3.0f;
		/// <summary>The object to orbit to.</summary>
		public Transform orbit {
			get {
				if (this.originalTarget == null) {
					return null;
				} else {
					return this.target;
				}
			}
			set {
				if (value == null) {
					this.target = this.originalTarget;
				} else {
					//Only saves the original target if it's not already saved.
					if (this.originalTarget == null) this.originalTarget = this.target;

					this.target = value;
				}
			}	
		}

		protected Transform originalTarget;
		
		protected void LateUpdate () {
			// Early out if we don't have a target
			if (!target) return;
		
			// Calculate the current rotation angles
			float wantedRotationAngle = target.eulerAngles.y;
			float wantedHeight = target.position.y + height;
			
			float currentRotationAngle = this.cachedTransform.eulerAngles.y;
			float currentHeight = this.cachedTransform.position.y;
			
			// Damp the rotation around the y-axis
			currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
			// Damp the height
			currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);
			
			// Convert the angle into a rotation
			var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);
			// Set the position of the camera on the x-z plane to:
			// distance meters behind the target
			this.cachedTransform.position = target.position;
			this.cachedTransform.position -= currentRotation * Vector3.forward * distance;
			
			// Set the height of the camera
			this.cachedTransform.position = new Vector3(this.cachedTransform.position.x, currentHeight, this.cachedTransform.position.z);
			// Always look at the target
			this.cachedTransform.LookAt(this.target);
			
			//var targetRotation = Quaternion.LookRotation(this.target.position - this.cachedTransform.position);
			//this.cachedTransform.rotation = Quaternion.Slerp(this.cachedTransform.rotation, targetRotation, Time.deltaTime);
		}
	}
}