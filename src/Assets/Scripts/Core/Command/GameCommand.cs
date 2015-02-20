using UnityEngine;
using System;
using System.Collections;
using Intentor.Yadic;

namespace Trixter.Core.Command {
	/// <summary>
	/// Abstraction for a game command.
	/// </summary>
	public abstract class GameCommand : IDisposable {
		/// <summary>Reference to the DI container.</summary>
		[Inject]
		public IInjectionContainer container;		
		/// <summary>The dispatcher that handle this command.</summary>
		[Inject]
		public CommandDispatcher dispatcher;
		/// <summary>Indicates whether the command is running.</summary>
		public bool running { get; set; }
		/// <summary>Indicates whether the command must be kept alive even after its execution.</summary>
		public bool keepAlive { get; set; }

		/// <summary>
		/// Initializes the command.
		/// </summary>
		public virtual void Initialize() {
			this.running = false;
			this.keepAlive = false;
		}

		/// <summary>
		/// Executes the command.
		/// <param name="parameters">Command parameters.</param>
		public abstract void Execute(params object[] parameters);

		/// <summary>
		/// Retains the command in use.
		/// 
		/// Always release the command .
		/// </summary>
		public void Retain() {
			this.keepAlive = true;
		}

		/// <summary>
		/// Releases the command.
		/// </summary>
		public void Release() {
			this.keepAlive = false;

			this.dispatcher.ReleaseCommand(this);
		}

		/// <summary>
		/// Releases all resource used by the <see cref="Trixter.DevilRain.Core.Command.GameCommand"/> object.
		/// </summary>
		/// <remarks>Call <see cref="Dispose"/> when you are finished using the
		/// <see cref="Trixter.DevilRain.Core.Command.GameCommand"/>. The <see cref="Dispose"/> method leaves the
		/// <see cref="Trixter.DevilRain.Core.Command.GameCommand"/> in an unusable state. After calling
		/// <see cref="Dispose"/>, you must release all references to the
		/// <see cref="Trixter.DevilRain.Core.Command.GameCommand"/> so the garbage collector can reclaim the memory that the
		/// <see cref="Trixter.DevilRain.Core.Command.GameCommand"/> was occupying.</remarks>
		public virtual void Dispose() {
			this.running = false;
			this.keepAlive = false;
		}
	}
}