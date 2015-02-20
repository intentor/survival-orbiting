using UnityEngine;
using System.Collections;
using Trixter.Core;
using Trixter.GGj15.GameInput;
using Trixter.GGJ15.ValueObjects;
using Trixter.GGJ15.Commands;
using Trixter.GGJ15.Constants;

namespace Trixter.GGj.Modifiers {
	/// <summary>
	/// Marks the planet as having water.
	/// </summary>
	public class WaterPlanet : BaseBehaviour {
		protected override void Start() {
			base.Start();

			var prefab = Resources.Load("Prefabs/WaterSign");
			var gameObject = (GameObject)Instantiate(prefab);
			gameObject.transform.SetParent(this.cachedTransform);

			var planetCollider = this.collider;
			if (this.collider == null) {
				planetCollider = this.cachedTransform.FindChild("PlanetObject").GetComponent<Collider>();
			}
			
			gameObject.transform.localPosition = new Vector3(0, planetCollider.bounds.extents.y * 30.0f, 0);
			gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
		}
	}
}