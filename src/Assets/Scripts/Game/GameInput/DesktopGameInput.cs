using UnityEngine;

namespace Trixter.GGj15.GameInput {
	/// <summary>
	/// Desktop game input.
	/// </summary>
	public class DesktopGameInput : IGameInput {
		/// <summary>
		/// Indicates whether the main engine is being activated.
		/// </summary>
		public bool mainEngine() {
			return Input.GetKey(KeyCode.UpArrow);
		}

		/// <summary>
		/// Indicates whether the left thruster is being activated.
		/// </summary>
		public bool thrusterLeft() {
			return Input.GetKey(KeyCode.LeftArrow);
		}

		/// <summary>
		/// Indicates whether the right thruster is being activated.
		/// </summary>
		public bool thrusterRight() {
			return Input.GetKey(KeyCode.RightArrow);
		}

		/// <summary>
		/// Indicates whether the backward thruster is being activated.
		/// </summary>
		public bool thrusterBackwards() {
			return Input.GetKeyDown(KeyCode.DownArrow);
		}
	}
}