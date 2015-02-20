using UnityEngine;
using Trixter.Core.Command;

namespace Trixter.Core {
	/// <summary>
	/// Base MonoBehaviour.
	/// </summary>
	public abstract class BaseBehaviour : MonoBehaviour {
		/// <summary>The command dispatcher.</summary>
		[Inject]
		public CommandDispatcher dispatcher;

		/// <summary>Reference to the Transform component.</summary>
		protected Transform cachedTransform;

		/// <summary>
		/// Called when the component is awaken.
		/// 
		/// If overriden on derived classes, always call base.Awake().
		/// </summary>
		protected virtual void Start() {
			this.cachedTransform = this.transform;

			this.Inject();
		}
	}
}