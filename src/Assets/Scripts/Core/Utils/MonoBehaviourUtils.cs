using UnityEngine;

/// <summary>
/// Game object utils.
/// </summary>
public static class GameObjectUtils {
	/// <summary>
	/// Shows the GameObject.
	/// </summary>
	/// <param name="gameObject">Game object.</param>
	public static void Show(this GameObject gameObject) {
		gameObject.SetActive(true);
	}
	
	/// <summary>
	/// Shows the GameObject.
	/// </summary>
	/// <param name="component">Component.</param>
	public static void Show(this Component component) {
		component.gameObject.SetActive(true);
	}
	
	/// <summary>
	/// Hides the GameObject.
	/// </summary>
	/// <param name="gameObject">Game object.</param>
	public static void Hide(this GameObject gameObject) {
		gameObject.SetActive(false);
	}
	
	/// <summary>
	/// Hides the GameObject.
	/// </summary>
	/// <param name="component">Component.</param>
	public static void Hide(this Component component) {
		component.gameObject.SetActive(false);
	}
}