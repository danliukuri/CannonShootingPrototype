namespace CannonShootingPrototype.Utilities.Patterns.State.Services
{
    public interface IStateRegistrar<in TState> where TState : IState
    {
        void Register(TState state);

        bool UnRegister(TState state);
    }
}