using UnityEngine;
using System.Collections;
using Trixter.Core;

namespace Trixter.GGJ15.Modifiers {
	/// <summary>
	/// Draws the force line.
	/// </summary>
	[RequireComponent(typeof(LineRenderer))]
	public class DrawForceLine : BaseBehaviour {
		/// <summary>The line renderer for the force.</summary>
		protected LineRenderer line;

		protected override void Start() {
			base.Start();

			this.line = this.GetComponent<LineRenderer>();
		}

		protected void FixedUpdate() {
			/*
			var lineCount = 2;
			this.line.SetVertexCount(lineCount);
			
			var startPos = new Vector3(
				this.cachedTransform.position.x,
				this.cachedTransform.position.y,
				this.cachedTransform.position.z + this.collider.bounds.extents.z);

			startPos = this.cachedTransform.position;
			this.line.SetPosition(0, startPos);
			this.line.SetPosition(1, startPos + this.cachedTransform.forward);
			*/
			/*
			var lineCount = 20;
			this.line.SetVertexCount(lineCount);

			var startPos = new Vector3(
				this.cachedTransform.position.x,
				this.cachedTransform.position.y,
				this.cachedTransform.position.z + this.collider.bounds.extents.z);
			var position = startPos;
			var velocity = this.rigidbody.velocity;

			for (var lineIndex = 0; lineIndex < lineCount; lineIndex++) {
				this.line.SetPosition(lineIndex, position);
				velocity += Physics.gravity * Time.fixedDeltaTime;
				position += velocity * Time.fixedDeltaTime;
			}*/
		}
	}
}