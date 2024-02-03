using System;
using System.Collections.Generic;
using UnityEngine.UIElements;

using System.Linq;
using DebugUI.UIElements;

namespace DebugUI
{
    public sealed class DebugUIBuilder : IDebugUIBuilder
    {
        internal readonly List<IDebugUIElementFactory> factories = new();
        internal readonly List<IDebugUIOptions> options = new();

        public ICollection<IDebugUIElementFactory> Factories => factories;
        public ICollection<IDebugUIOptions> Options => options;

        public VisualElement Build()
        {
            var window = new DebugWindow();

            List<IDisposable> disposables = new();
            foreach (var factory in factories)
            {
                window.Add(factory.CreateVisualElement(disposables));
            }

            window.RegisterCallback<DetachFromPanelEvent>(eventData =>
            {
                foreach (var item in disposables) item.Dispose();
                disposables.Clear();
            });

            var windowOptions = Options.OfType<DebugWindowOptions>().FirstOrDefault();
            if (windowOptions != null)
            {
                window.Text = windowOptions.Title;
                window.SetDraggable(windowOptions.Draggable);
            }

            return window;
        }
    }
}