using UnityEngine;
using System.Collections;

namespace Trixter.Core.Command {
	/// <summary>
	/// Command ticker, used to trigger Update events on any commands that
	/// implement IUpdateable.
	/// </summary>
	public class CommandUpdater : MonoBehaviour {
		/// <summary>The command dispatcher related to this command updater.</summary>
		public CommandDispatcher dispatcher;

		/// <summary>
		/// Called once per frame.
		/// </summary>
		protected void Update() {
			//It the game is paused, Update is not called.
			if (Time.deltaTime == 0) return;

			for (var cmdIndex = 0; cmdIndex < this.dispatcher.updateableCommands.Count; cmdIndex++) {
				this.dispatcher.updateableCommands[cmdIndex].Update();
			}
		}
	}
}