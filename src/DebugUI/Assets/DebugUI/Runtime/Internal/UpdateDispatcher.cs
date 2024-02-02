using UnityEngine;

namespace DebugUI
{
    [AddComponentMenu("")]
    [DisallowMultipleComponent]
    internal sealed class UpdateDispatcher : MonoBehaviour
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Init()
        {
            instance = new GameObject(nameof(UpdateDispatcher)).AddComponent<UpdateDispatcher>();
            DontDestroyOnLoad(instance);
        }

        static UpdateDispatcher instance;

        readonly UpdateRunner updateRunner = new(ex => Debug.LogException(ex));

        public static void Register(IUpdateRunnerItem item)
        {
            instance.updateRunner.Add(item);
        }

        void Update()
        {
            updateRunner.Run();
        }
    }
}