using UnityEngine;
using Intentor.Yadic;
using Trixter.Core.Command;
using Trixter.GGJ15.Commands;
using Trixter.GGJ15.Constants;
using Trixter.GGj15.GameInput;
using Trixter.GGJ15.ValueObjects;
using Trixter.GGJ15.Views;

namespace Trixter.GGJ15 {
	/// <summary>
	/// Game context root.
	/// </summary>
	public class GameRoot : Intentor.Yadic.ContextRoot {
		/// <summary>The command dispatcher.</summary>
		protected CommandDispatcher dispatcher;

		public override void SetupContainers() {
			this.dispatcher = new CommandDispatcher();

			var container = new InjectionContainer();
			container.RegisterExtension<UnityBindingContainerExtension>();

			container.Bind<IInjectionContainer>().To<InjectionContainer>(container);
			container.Bind<CommandDispatcher>().To<CommandDispatcher>(this.dispatcher);
			container.Bind<Transform>().To<Transform>(GameObject.FindGameObjectWithTag(GameTag.SHIP).transform).As("Ship");
			container.Bind<ShipData>().ToSingleton();

			//Views
			container.Bind<HudView>().ToGameObject("Views/Hud");
			container.Bind<GameOverByFuelView>().ToGameObject("Views/GameOverByFuel");
			container.Bind<GameOverByDamageView>().ToGameObject("Views/GameOverByDamage");
			container.Bind<WaterReachedView>().ToGameObject("Views/WaterReached");

			#if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IOS)
			
			container.Bind<IGameInput>().ToSingleton<DesktopGameInput>();

			#else
			
			container.Bind<IGameInput>().ToSingleton<DesktopGameInput>();

			#endif
			
			this.AddContainer(container);
			
			this.dispatcher.RegisterFromNamespace("Trixter.GGJ15.Commands");
		}
		
		public override void Init() {
			this.dispatcher.Dispatch<InitializeGame>();
		}
	}
}