using System;

namespace Trixter.GGJ15.ValueObjects {
	/// <summary>
	/// Ship's data.
	/// </summary>
	[Serializable]
	public class ShipData {
		/// <summary>The current engine's fuel [0-1].</summary>
		public float fuel;
		/// <summary>The current state of the hull [0-1].</summary>
		public float hull;
		/// <summary>The current ship's speed.</summary>
		public float speed;
		/// <summary>The time.</summary>
		public float time;
	}
}