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
            var builder = new DebugUIBuilder();
            builder.ConfigureWindowOptions(options =>
            {
                options.WindowName = GetType().Name;
            });

            Configure(builder);
            builder.BuildWith(uiDocument);
        }
    }
}