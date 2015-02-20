using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Intentor.Yadic;

namespace Trixter.Core.Command {
	/// <summary>
	/// Dispatches commands.
	/// </summary>
	public class CommandDispatcher {
		/// <summary>Reference to the DI container.</summary>
		[Inject]
		public IInjectionContainer container;
		/// <summary>The commands list.</summary>
		public Dictionary<System.Type, GameCommand> commands = new Dictionary<Type, GameCommand>();
		/// <summary>Commands that can receive Update.</summary>
		public List<IUpdateable> updateableCommands = new List<IUpdateable>();

		/// <summary>
		/// Initializes a new instance of the <see cref="Trixter.Core.Command.CommandDispatcher"/> class.
		/// </summary>
		public CommandDispatcher() {
			var gameObject = new GameObject("CommandUpdater");
			var updater = gameObject.AddComponent<CommandUpdater>();
			updater.dispatcher = this;
		}
		
		/// <summary>
		/// Registers a command of type <typeparamref name="T"/>.
		/// </summary>
		/// <typeparam name="T">The type of the command to be registered.</typeparam>
		public void Register<T>() where T : GameCommand, new() {
			var type = typeof(T);
			
			Register(type);
		}

		/// <summary>
		/// Registers a command of type <paramref name="type"/>.
		/// </summary>
		/// <param name="T">The type of the command to be registered.</param>
		public void Register(Type type) {
			GameCommand command = (GameCommand)Activator.CreateInstance(type);
			
			if (commands.ContainsKey(type)) {
				commands[type] = command;
			} else {
				commands.Add(type, command);
			}

			this.container.Inject(command);
			
			command.Initialize();
		}

		/// <summary>
		/// Registers all commands from a given namespace.
		/// </summary>
		/// <param name="namespaceName">Namespace name.</param>
		public void RegisterFromNamespace(string namespaceName) {
			var commands = 
				from t in Assembly.GetExecutingAssembly().GetTypes()
				where t.IsClass && t.Namespace == namespaceName
				select t;

			foreach (var command in commands) {
				Register(command);
			}
		}

		/// <summary>
		/// Dispatch the specified parameters.
		/// </summary>
		/// <typeparam name="T">The type of the command to be dispatched.</typeparam>
		/// <param name="parameters">Command parameters.</param>
		public void Dispatch<T>(params object[] parameters) where T : GameCommand {
			var type = typeof(T);

			if (commands.ContainsKey(type)) {
				var command = commands[type];

				command.running = true;
				command.Execute(parameters);

				if (command.keepAlive) {
					if (command is IUpdateable) {
						updateableCommands.Add((IUpdateable)command);
					}
				} else {
					command.running = false;
				}
			} else {
				throw new KeyNotFoundException(string.Format(
					"There is no command registered for the type ", type)
				);
			}
		}

		/// <summary>
		/// Releases a command.
		/// </summary>
		/// <param name="command">Command to be released.</param>
		public void ReleaseCommand(GameCommand command) {
			command.running = false;
			command.keepAlive = false;

			if (command is IUpdateable) {
				updateableCommands.Remove((IUpdateable)command);
			}
		}
		
		/// <summary>
		/// Releases all command that are running.
		/// </summary>
		public void ReleaseAllCommands() {
			foreach (var entry in commands) {
				ReleaseCommand(entry.Value);
			}
		}

		/// <summary>
		/// Disposes all commands.
		/// </summary>
		public void DisposeAll() {
			updateableCommands.Clear();

			foreach (var entry in commands) {
				entry.Value.Dispose();
			}

			commands.Clear();
		}
	}
}
