using System.Collections.Generic;
using UnityEngine.UIElements;

namespace DebugUI
{
    public interface IDebugUIBuilder
    {
        ICollection<IDebugUIOptions> Options { get; }
        ICollection<IDebugUIElementFactory> Factories { get; }
        VisualElement Build();
    }

    public interface IDebugUIOptions
    {

    }
}