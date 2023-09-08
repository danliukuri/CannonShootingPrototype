namespace CannonShootingPrototype.Utilities.Patterns.State
{
    public interface IEnterableState : IState
    {
        void Enter();
    }

    public interface IEnterableState<in TArgument> : IState
    {
        void Enter(TArgument argument);
    }
}