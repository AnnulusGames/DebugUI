using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace DebugUI.UIElements
{
    [Serializable]
    public class DebugWindow : VisualElement
    {
        public sealed new class UxmlFactory : UxmlFactory<DebugWindow, UxmlTraits> { }

        public sealed new class UxmlTraits : BindableElement.UxmlTraits
        {
            readonly UxmlStringAttributeDescription text = new() { name = "text", defaultValue = "Debug" };
            readonly UxmlBoolAttributeDescription value = new() { name = "value", defaultValue = true };

            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);
                if (ve is DebugWindow window)
                {
                    window.Text = text.GetValueFromBag(bag, cc);
                    window.SetValueWithoutNotify(value.GetValueFromBag(bag, cc));
                }
            }
        }

        public override VisualElement contentContainer => scrollView.contentContainer;

        [SerializeField] string text = "Debug";
        [SerializeField] bool value;

        public bool Value
        {
            get => value;
            set
            {
                if (this.value == value) return;

                using var evt = ChangeEvent<bool>.GetPooled(this.value, value);
                evt.target = this;
                SetValueWithoutNotify(value);
                SendEvent(evt);
            }
        }

        public string Text
        {
            get => text;
            set
            {
                this.text = value;
                foldout.text = value;
            }
        }

        public void SetValueWithoutNotify(bool newValue)
        {
            value = newValue;
            foldout.value = value;
        }

        public void SetDraggable(bool draggable)
        {
            if (dragManipulator != null) this.RemoveManipulator(dragManipulator);
            if (draggable) 
            {
                var toggle = foldout.Q<Toggle>();
                dragManipulator = new DebugWindowDragManipulator(this, toggle, toggle);
                toggle.AddManipulator(dragManipulator);
            }
        }

        public VisualElement BackgroundElement => background;

        readonly Foldout foldout;
        readonly ScrollView scrollView;
        readonly VisualElement background;
        
        DebugWindowDragManipulator dragManipulator;

        public DebugWindow()
        {
            AddToClassList(UssClasses.debug_ui_window);

            background = new VisualElement();
            background.AddToClassList(UssClasses.debug_ui_window_background);
            hierarchy.Add(background);

            foldout = new Foldout()
            {
                value = value,
                text = text
            };
            foldout.RegisterValueChangedCallback((evt) =>
            {
                if (evt.currentTarget == evt.target)
                {
                    Value = foldout.value;
                    evt.StopPropagation();
                }
            });

            background.Add(foldout);

            scrollView = new(ScrollViewMode.VerticalAndHorizontal);

            static void InitScroller(Scroller scroller)
            {
                scroller.AddToClassList(UssClasses.debug_ui_scroller);
                scroller.slider.AddToClassList(UssClasses.debug_ui_scroller__slider);
                scroller.Remove(scroller.highButton);
                scroller.Remove(scroller.lowButton);
                scroller.Q("unity-tracker").AddToClassList(UssClasses.debug_ui_scroller__tracker);
                scroller.Q("unity-dragger").AddToClassList(UssClasses.debug_ui_scroller__dragger);
            }

            InitScroller(scrollView.verticalScroller);
            InitScroller(scrollView.horizontalScroller);
            scrollView.verticalScroller.AddToClassList(UssClasses.debug_ui_scroller_vertical);
            scrollView.horizontalScroller.AddToClassList(UssClasses.debug_ui_scroller_horizontal);

            scrollView.contentViewport.style.flexGrow = 0f;

            foldout.Add(scrollView);

            var toggle = foldout.Q<Toggle>();
            schedule.Execute(() =>
            {
                transform.position = new Vector2(
                    Mathf.Clamp(transform.position.x, parent.contentRect.xMin, parent.contentRect.xMax - toggle.contentRect.width),
                    Mathf.Clamp(transform.position.y, parent.contentRect.yMin, parent.contentRect.yMax - toggle.contentRect.height)
                );
            })
            .Every(1);
        }
    }
}