using UnityEngine;
using System.Collections;

namespace Trixter.Core.Command {
	/// <summary>
	/// Used to allows a command to receive updates.
	/// </summary>
	public interface IUpdateable {
		/// <summary>
		/// Called every frame.
		/// </summary>
		void Update();
	}
}