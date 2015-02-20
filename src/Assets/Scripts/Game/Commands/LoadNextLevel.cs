using UnityEngine;
using System.Collections;
using Trixter.Core.Command;
using Trixter.GGJ15.Views;
using Trixter.GGJ15.ValueObjects;

namespace Trixter.GGJ15.Commands {
	/// <summary>
	/// Dispatched when the player reaches water.
	/// </summary>
	public class LoadNextLevel : GameCommand {		
		public override void Execute (params object[] parameters) {
			LevelData.currentLevel++;

			if (LevelData.currentLevel > LevelData.totalLevels) {
				LevelData.currentLevel = 1;
			}

			Application.LoadLevel(string.Format("{0}{1}", LevelData.baseLevelName, LevelData.currentLevel));
		}
	}
}