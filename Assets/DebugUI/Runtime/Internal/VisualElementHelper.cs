using UnityEngine.UIElements;

namespace DebugUI
{
    internal static class VisualElementHelper
    {
        public static void SetInputFieldsEnabled(VisualElement root, bool enabled)
        {
            foreach (var item in root.Query("unity-text-input").Build())
            {
                item.SetEnabled(enabled);
            }
        }
    }
}