using CannonShootingPrototype.Architecture.GameStates;
using CannonShootingPrototype.Utilities.Patterns.State.Machines;
using UnityEngine;

namespace CannonShootingPrototype.Architecture.Bootstrap
{
    [RequireComponent(typeof(SceneDependenciesProvider)), RequireComponent(typeof(AssetsDependenciesProvider))]
    public class SceneBootstrapper : MonoBehaviour
    {
        private void Awake()
        {
            var assetsDependenciesProvider = GetComponent<AssetsDependenciesProvider>();
            var sceneDependenciesProvider = GetComponent<SceneDependenciesProvider>();

            var compositionRoot = new CompositionRoot(assetsDependenciesProvider, sceneDependenciesProvider);
            IStateMachine gameStateMachine = compositionRoot.Initialize();

            gameStateMachine.ChangeStateTo<SetupGameState>();
        }
    }
}