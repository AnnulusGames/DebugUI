using UnityEngine;
using UnityEngine.UIElements;

namespace DebugUI.UIElements
{
    public class FillSlider : Slider
    {
        public sealed new class UxmlFactory : UxmlFactory<FillSlider, UxmlTraits> { }

        readonly VisualElement filler;
        readonly Label valueLabel;

        public string Format { get; set; } = "{0:F2}";

        public FillSlider() : base()
        {
            var dragger = this.Q("unity-dragger");
            var tracker = this.Q("unity-tracker");
            var dragContainer = this.Q("unity-drag-container");

            valueLabel = new Label();
            valueLabel.AddToClassList(UssClasses.debug_ui_slider__label);
            Add(valueLabel);

            filler = new VisualElement();
            filler.AddToClassList(UssClasses.debug_ui_slider__filler);
            tracker.Add(filler);

            dragContainer.AddToClassList("debug-ui-slider__drag-container");

            OnValueChanged(value);
            this.RegisterValueChangedCallback(x => OnValueChanged(x.newValue));
        }

        public void ForceUpdateValue(float x)
        {
            this.value = x;
            OnValueChanged(x);
        }

        void OnValueChanged(float x)
        {
            filler.style.width = Length.Percent(Mathf.InverseLerp(lowValue, highValue, x) * 100f);
            valueLabel.text = string.Format(Format, x);
        }
    }

    public class FillSliderInt : SliderInt
    {
        public sealed new class UxmlFactory : UxmlFactory<FillSliderInt, UxmlTraits> { }

        readonly VisualElement filler;
        readonly Label valueLabel;

        public string Format { get; set; } = "{0}";

        public FillSliderInt() : base()
        {
            var dragger = this.Q("unity-dragger");
            var tracker = this.Q("unity-tracker");
            var dragContainer = this.Q("unity-drag-container");

            valueLabel = new Label();
            valueLabel.AddToClassList(UssClasses.debug_ui_slider__label);
            Add(valueLabel);

            filler = new VisualElement();
            filler.AddToClassList(UssClasses.debug_ui_slider__filler);
            tracker.Add(filler);

            dragContainer.AddToClassList("debug-ui-slider__drag-container");

            OnValueChanged(value);
            this.RegisterValueChangedCallback(x => OnValueChanged(x.newValue));
        }

        public void ForceUpdateValue(int x)
        {
            this.value = x;
            OnValueChanged(x);
        }

        void OnValueChanged(int x)
        {
            filler.style.width = Length.Percent(Mathf.InverseLerp(lowValue, highValue, x) * 100f);
            valueLabel.text = string.Format(Format, x);
        }
    }
}