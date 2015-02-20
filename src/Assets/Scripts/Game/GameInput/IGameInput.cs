namespace Trixter.GGj15.GameInput {
	/// <summary>
	/// Represents the game input.
	/// </summary>
	public interface IGameInput {
		/// <summary>
		/// Indicates whether the main engine is being activated.
		/// </summary>
		bool mainEngine();

		/// <summary>
		/// Indicates whether the left thruster is being activated.
		/// </summary>
		bool thrusterLeft();

		/// <summary>
		/// Indicates whether the right thruster is being activated.
		/// </summary>
		bool thrusterRight();

		/// <summary>
		/// Indicates whether the backward thruster is being activated.
		/// </summary>
		bool thrusterBackwards();
	}
}