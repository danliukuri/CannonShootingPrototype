namespace CannonShootingPrototype.Utilities.Patterns.State.Machines
{
    public interface IStateMachine
    {
        void ChangeStateTo<TState>() where TState : IState;

        public void ChangeStateTo<TState, TEnterArgument>(TEnterArgument argument) 
            where TState : IEnterableState<TEnterArgument>;
    }
}