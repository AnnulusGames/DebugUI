using UnityEngine;
using UnityEngine.UIElements;

namespace DebugUI
{
    public abstract class DebugUIBuilderBase : MonoBehaviour
    {
        [SerializeField] UIDocument uiDocument;

        protected abstract void Configure(IDebugUIBuilder builder);

        protected virtual void Awake()
        {
            var debugAppBuilder = new DebugUIBuilder();
            debugAppBuilder.ConfigureWindowOptions(options =>
            {
                options.WindowName = GetType().Name;
            });

            Configure(debugAppBuilder);
            debugAppBuilder.BuildWith(uiDocument);
        }
    }
}