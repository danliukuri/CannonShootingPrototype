using System;
using CannonShootingPrototype.Utilities.Patterns.State.Services;

namespace CannonShootingPrototype.Utilities.Patterns.State.Containers
{
    public class StatesContainerInitializer : IDisposable
    {
        private readonly IStateRegistrar<IState> _stateContainer;
        private readonly IState[] _states;

        public StatesContainerInitializer(IStateRegistrar<IState> stateContainer, params IState[] states)
        {
            _stateContainer = stateContainer;
            _states = states;
        }

        public void Initialize()
        {
            foreach (IState state in _states)
                _stateContainer.Register(state);
        }

        public void Dispose()
        {
            foreach (IState state in _states)
                _stateContainer.UnRegister(state);
        }
    }
}