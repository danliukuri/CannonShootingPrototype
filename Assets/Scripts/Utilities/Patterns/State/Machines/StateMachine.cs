using CannonShootingPrototype.Utilities.Patterns.State.Services;

namespace CannonShootingPrototype.Utilities.Patterns.State.Machines
{
    public class StateMachine : IStateMachine
    {
        private readonly IStateProvider _stateProvider;
        private IState _currentState;

        public StateMachine(IStateProvider stateProvider) => _stateProvider = stateProvider;

        public void ChangeStateTo<TState>() where TState : IState
        {
            (_currentState as IExitableState)?.Exit();
            var newState = _stateProvider.Get<TState>();
            _currentState = newState;
            (newState as IEnterableState)?.Enter();
        }

        public void ChangeStateTo<TState, TEnterArgument>(TEnterArgument argument)
            where TState : IEnterableState<TEnterArgument>
        {
            (_currentState as IExitableState)?.Exit();
            var newState = _stateProvider.Get<TState>();
            _currentState = newState;
            newState.Enter(argument);
        }
    }
}