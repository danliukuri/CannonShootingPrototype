using CannonShootingPrototype.Architecture.GameStates;
using CannonShootingPrototype.Utilities.Patterns.State.Machines;
using UnityEngine;

namespace CannonShootingPrototype.Architecture.Bootstrap
{
    [RequireComponent(typeof(SceneDependenciesProvider))]
    public class SceneBootstrapper : MonoBehaviour
    {
        private void Awake()
        {
            var sceneDependenciesProvider = GetComponent<SceneDependenciesProvider>();
            var compositionRoot = new CompositionRoot(sceneDependenciesProvider);
            IStateMachine gameStateMachine = compositionRoot.Initialize();

            gameStateMachine.ChangeStateTo<SetupGameState>();
        }
    }
}