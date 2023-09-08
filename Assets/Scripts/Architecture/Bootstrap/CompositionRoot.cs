using CannonShootingPrototype.Architecture.GameStates;
using CannonShootingPrototype.Utilities.Patterns.State;
using CannonShootingPrototype.Utilities.Patterns.State.Containers;
using CannonShootingPrototype.Utilities.Patterns.State.Machines;

namespace CannonShootingPrototype.Architecture.Bootstrap
{
    public class CompositionRoot
    {
        private readonly SceneDependenciesProvider _sceneDependenciesProvider;

        public CompositionRoot(SceneDependenciesProvider sceneDependenciesProvider) =>
            _sceneDependenciesProvider = sceneDependenciesProvider;

        public IStateMachine Initialize()
        {
            var states = new IState[] { new SetupGameState() };
            var stateContainer = new StateContainer();
            var statesContainerInitializer = new StatesContainerInitializer(stateContainer, states);
            var stateMachine = new StateMachine(stateContainer);

            statesContainerInitializer.Initialize();

            return stateMachine;
        }
    }
}