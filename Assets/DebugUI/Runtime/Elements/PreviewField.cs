using UnityEngine.UIElements;

namespace DebugUI.UIElements
{
    public class PreviewField : VisualElement
    {
        public sealed new class UxmlFactory : UxmlFactory<PreviewField, UxmlTraits> { }

        public sealed new class UxmlTraits : BindableElement.UxmlTraits
        {
            readonly UxmlStringAttributeDescription text = new() { name = "text", defaultValue = "Image" };

            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);
                if (ve is PreviewField preview)
                {
                    preview.Text = text.GetValueFromBag(bag, cc);
                }
            }
        }

        public PreviewField()
        {
            label = new Label();
            Add(label);

            image = new VisualElement();
            image.AddToClassList(UssClasses.debug_ui_image_preview__image);
            Add(image);

            AddToClassList(UssClasses.debug_ui_image_preview);
        }

        public StyleBackground BackgroundImage
        {
            get => image.style.backgroundImage;
            set => image.style.backgroundImage = value;
        }

        public string Text
        {
            get => label.text;
            set => label.text = value;
        }

        readonly Label label;
        readonly VisualElement image;
    }
}