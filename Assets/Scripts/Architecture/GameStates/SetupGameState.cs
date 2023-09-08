using CannonShootingPrototype.Utilities.Patterns.State;
using UnityEngine;

namespace CannonShootingPrototype.Architecture.GameStates
{
    public class SetupGameState : IEnterableState
    {
        public void Enter() => Debug.Log(nameof(SetupGameState) + "." + nameof(Enter));
    }
}