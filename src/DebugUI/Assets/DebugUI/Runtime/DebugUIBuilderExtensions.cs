using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace DebugUI
{
    public static class DebugUIBuilderExtensions
    {
        public static IDebugUIBuilder ConfigureWindowOptions(this IDebugUIBuilder builder, Action<DebugWindowOptions> configure)
        {
            var options = builder.Options.OfType<DebugWindowOptions>().FirstOrDefault();
            if (options == null)
            {
                options = new DebugWindowOptions();
                builder.Options.Add(options);
            }
            configure(options);
            return builder;
        }

        public static IDebugUIBuilder AddSpace(this IDebugUIBuilder builder, float height)
        {
            builder.Factories.Add(new DebugSpaceFactory()
            {
                Height = height
            });
            return builder;
        }

        public static IDebugUIBuilder AddLabel(this IDebugUIBuilder builder, string text)
        {
            builder.Factories.Add(new DebugLabelFactory()
            {
                Text = text,
            });
            return builder;
        }

        public static IDebugUIBuilder AddButton(this IDebugUIBuilder builder, string text, Action action)
        {
            builder.Factories.Add(new DebugButtonFactory()
            {
                Text = text,
                Action = action
            });
            return builder;
        }

        public static IDebugUIBuilder AddField<TEnum>(this IDebugUIBuilder builder, string label, Func<TEnum> getter)
            where TEnum : Enum
        {
            builder.Factories.Add(new DebugEnumFieldFactory<TEnum>()
            {
                Label = label,
                Getter = getter,
            });
            return builder;
        }


        public static IDebugUIBuilder AddField<TEnum>(this IDebugUIBuilder builder, string label, Func<TEnum> getter, Action<TEnum> setter)
            where TEnum : Enum
        {
            builder.Factories.Add(new DebugEnumFieldFactory<TEnum>()
            {
                Label = label,
                Getter = getter,
                Setter = setter,
            });
            return builder;
        }

        public static IDebugUIBuilder AddField(this IDebugUIBuilder builder, string label, Func<bool> getter)
        {
            builder.Factories.Add(new DebugFieldFactory<bool, Toggle>()
            {
                Label = label,
                Getter = getter,
            });
            return builder;
        }

        public static IDebugUIBuilder AddField(this IDebugUIBuilder builder, string label, Func<bool> getter, Action<bool> setter)
        {
            builder.Factories.Add(new DebugFieldFactory<bool, Toggle>()
            {
                Label = label,
                Getter = getter,
                Setter = setter,
            });
            return builder;
        }

        public static IDebugUIBuilder AddField(this IDebugUIBuilder builder, string label, Func<int> getter)
        {
            builder.Factories.Add(new DebugFieldFactory<int, IntegerField>()
            {
                Label = label,
                Getter = getter,
            });
            return builder;
        }

        public static IDebugUIBuilder AddField(this IDebugUIBuilder builder, string label, Func<int> getter, Action<int> setter)
        {
            builder.Factories.Add(new DebugFieldFactory<int, IntegerField>()
            {
                Label = label,
                Getter = getter,
                Setter = setter,
            });
            return builder;
        }

        public static IDebugUIBuilder AddField(this IDebugUIBuilder builder, string label, Func<float> getter)
        {
            builder.Factories.Add(new DebugFieldFactory<float, FloatField>()
            {
                Label = label,
                Getter = getter,
            });
            return builder;
        }

        public static IDebugUIBuilder AddField(this IDebugUIBuilder builder, string label, Func<float> getter, Action<float> setter)
        {
            builder.Factories.Add(new DebugFieldFactory<float, FloatField>()
            {
                Label = label,
                Getter = getter,
                Setter = setter,
            });
            return builder;
        }

        public static IDebugUIBuilder AddField(this IDebugUIBuilder builder, string label, Func<string> getter)
        {
            builder.Factories.Add(new DebugFieldFactory<string, TextField>()
            {
                Label = label,
                Getter = getter,
            });
            return builder;
        }

        public static IDebugUIBuilder AddField(this IDebugUIBuilder builder, string label, Func<string> getter, Action<string> setter)
        {
            builder.Factories.Add(new DebugFieldFactory<string, TextField>()
            {
                Label = label,
                Getter = getter,
                Setter = setter,
            });
            return builder;
        }

        public static IDebugUIBuilder AddField(this IDebugUIBuilder builder, string label, Func<Vector2> getter)
        {
            builder.Factories.Add(new DebugCompositeFieldFactory<Vector2, Vector2Field, FloatField, float>()
            {
                Label = label,
                Getter = getter,
            });
            return builder;
        }

        public static IDebugUIBuilder AddField(this IDebugUIBuilder builder, string label, Func<Vector2> getter, Action<Vector2> setter)
        {
            builder.Factories.Add(new DebugCompositeFieldFactory<Vector2, Vector2Field, FloatField, float>()
            {
                Label = label,
                Getter = getter,
                Setter = setter
            });
            return builder;
        }

        public static IDebugUIBuilder AddField(this IDebugUIBuilder builder, string label, Func<Vector3> getter)
        {
            builder.Factories.Add(new DebugCompositeFieldFactory<Vector3, Vector3Field, FloatField, float>()
            {
                Label = label,
                Getter = getter,
            });
            return builder;
        }

        public static IDebugUIBuilder AddField(this IDebugUIBuilder builder, string label, Func<Vector3> getter, Action<Vector3> setter)
        {
            builder.Factories.Add(new DebugCompositeFieldFactory<Vector3, Vector3Field, FloatField, float>()
            {
                Label = label,
                Getter = getter,
                Setter = setter
            });
            return builder;
        }

        public static IDebugUIBuilder AddField(this IDebugUIBuilder builder, string label, Func<Vector4> getter)
        {
            builder.Factories.Add(new DebugCompositeFieldFactory<Vector4, Vector4Field, FloatField, float>()
            {
                Label = label,
                Getter = getter,
            });
            return builder;
        }

        public static IDebugUIBuilder AddField(this IDebugUIBuilder builder, string label, Func<Vector4> getter, Action<Vector4> setter)
        {
            builder.Factories.Add(new DebugCompositeFieldFactory<Vector4, Vector4Field, FloatField, float>()
            {
                Label = label,
                Getter = getter,
                Setter = setter
            });
            return builder;
        }

        public static IDebugUIBuilder AddField(this IDebugUIBuilder builder, string label, Func<Vector2Int> getter)
        {
            builder.Factories.Add(new DebugCompositeFieldFactory<Vector2Int, Vector2IntField, IntegerField, int>()
            {
                Label = label,
                Getter = getter,
            });
            return builder;
        }

        public static IDebugUIBuilder AddField(this IDebugUIBuilder builder, string label, Func<Vector2Int> getter, Action<Vector2Int> setter)
        {
            builder.Factories.Add(new DebugCompositeFieldFactory<Vector2Int, Vector2IntField, IntegerField, int>()
            {
                Label = label,
                Getter = getter,
                Setter = setter,
            });
            return builder;
        }

        public static IDebugUIBuilder AddField(this IDebugUIBuilder builder, string label, Func<Vector3Int> getter)
        {
            builder.Factories.Add(new DebugCompositeFieldFactory<Vector3Int, Vector3IntField, IntegerField, int>()
            {
                Label = label,
                Getter = getter,
            });
            return builder;
        }

        public static IDebugUIBuilder AddField(this IDebugUIBuilder builder, string label, Func<Vector3Int> getter, Action<Vector3Int> setter)
        {
            builder.Factories.Add(new DebugCompositeFieldFactory<Vector3Int, Vector3IntField, IntegerField, int>()
            {
                Label = label,
                Getter = getter,
                Setter = setter,
            });
            return builder;
        }

        public static IDebugUIBuilder AddField(this IDebugUIBuilder builder, string label, Func<Rect> getter)
        {
            builder.Factories.Add(new DebugCompositeFieldFactory<Rect, RectField, FloatField, float>()
            {
                Label = label,
                Getter = getter,
            });
            return builder;
        }

        public static IDebugUIBuilder AddField(this IDebugUIBuilder builder, string label, Func<Rect> getter, Action<Rect> setter)
        {
            builder.Factories.Add(new DebugCompositeFieldFactory<Rect, RectField, FloatField, float>()
            {
                Label = label,
                Getter = getter,
                Setter = setter,
            });
            return builder;
        }

        public static IDebugUIBuilder AddField(this IDebugUIBuilder builder, string label, Func<RectInt> getter, Action<RectInt> setter)
        {
            builder.Factories.Add(new DebugCompositeFieldFactory<RectInt, RectIntField, IntegerField, int>()
            {
                Label = label,
                Getter = getter,
                Setter = setter,
            });
            return builder;
        }

        public static IDebugUIBuilder AddField(this IDebugUIBuilder builder, string label, Func<RectInt> getter)
        {
            builder.Factories.Add(new DebugCompositeFieldFactory<RectInt, RectIntField, IntegerField, int>()
            {
                Label = label,
                Getter = getter,
            });
            return builder;
        }

        public static IDebugUIBuilder AddField(this IDebugUIBuilder builder, string label, Func<Bounds> getter, Action<Bounds> setter)
        {
            builder.Factories.Add(new DebugFieldFactory<Bounds, BoundsField>()
            {
                Label = label,
                Getter = getter,
                Setter = setter,
            });
            return builder;
        }

        public static IDebugUIBuilder AddField(this IDebugUIBuilder builder, string label, Func<Bounds> getter)
        {
            builder.Factories.Add(new DebugFieldFactory<Bounds, BoundsField>()
            {
                Label = label,
                Getter = getter,
            });
            return builder;
        }

        public static IDebugUIBuilder AddField(this IDebugUIBuilder builder, string label, Func<BoundsInt> getter, Action<BoundsInt> setter)
        {
            builder.Factories.Add(new DebugFieldFactory<BoundsInt, BoundsIntField>()
            {
                Label = label,
                Getter = getter,
                Setter = setter,
            });
            return builder;
        }

        public static IDebugUIBuilder AddField(this IDebugUIBuilder builder, string label, Func<BoundsInt> getter)
        {
            builder.Factories.Add(new DebugFieldFactory<BoundsInt, BoundsIntField>()
            {
                Label = label,
                Getter = getter,
            });
            return builder;
        }

        public static IDebugUIBuilder AddSlider(this IDebugUIBuilder builder, string label, float lowValue, float highValue, Func<float> getter, string format = "{0:F2}")
        {
            builder.Factories.Add(new DebugSliderFactory()
            {
                Label = label,
                Format = format,
                LowValue = lowValue,
                HighValue = highValue,
                Getter = getter,
            });
            return builder;
        }

        public static IDebugUIBuilder AddSlider(this IDebugUIBuilder builder, string label, float lowValue, float highValue, Func<float> getter, Action<float> setter, string format = "{0:F2}")
        {
            builder.Factories.Add(new DebugSliderFactory()
            {
                Label = label,
                Format = format,
                LowValue = lowValue,
                HighValue = highValue,
                Getter = getter,
                Setter = setter,
            });
            return builder;
        }

        public static IDebugUIBuilder AddSlider(this IDebugUIBuilder builder, string label, int lowValue, int highValue, Func<int> getter, Action<int> setter, string format = "{0}")
        {
            builder.Factories.Add(new DebugSliderIntFactory()
            {
                Label = label,
                Format = format,
                LowValue = lowValue,
                HighValue = highValue,
                Getter = getter,
                Setter = setter,
            });
            return builder;
        }

        public static IDebugUIBuilder AddSlider(this IDebugUIBuilder builder, string label, int lowValue, int highValue, Func<int> getter, string format = "{0}")
        {
            builder.Factories.Add(new DebugSliderIntFactory()
            {
                Label = label,
                Format = format,
                LowValue = lowValue,
                HighValue = highValue,
                Getter = getter,
            });
            return builder;
        }

        public static IDebugUIBuilder AddProgressBar(this IDebugUIBuilder builder, string label, float lowValue, float highValue, Func<float> getter, string format = "[{0:F2}]")
        {
            builder.Factories.Add(new DebugProgressBarFactory()
            {
                Label = label,
                Format = format,
                LowValue = lowValue,
                HighValue = highValue,
                Getter = getter,
            });
            return builder;
        }

        public static IDebugUIBuilder AddProgressBar(this IDebugUIBuilder builder, string label, int lowValue, int highValue, Func<int> getter, string format = "[{0}]")
        {
            builder.Factories.Add(new DebugProgressBarFactory()
            {
                Label = label,
                Format = format,
                LowValue = lowValue,
                HighValue = highValue,
                Getter = () => getter(),
            });
            return builder;
        }

        public static IDebugUIBuilder AddImage(this IDebugUIBuilder builder, string label, Texture2D texture2D)
        {
            builder.Factories.Add(new DebugStaticImagePreviewFactory()
            {
                Label = label,
                Background = texture2D
            });
            return builder;
        }

        public static IDebugUIBuilder AddImage(this IDebugUIBuilder builder, string label, Sprite sprite)
        {
            builder.Factories.Add(new DebugStaticImagePreviewFactory()
            {
                Label = label,
                Background = Background.FromSprite(sprite)
            });
            return builder;
        }

        public static IDebugUIBuilder AddImage(this IDebugUIBuilder builder, string label, RenderTexture renderTexture)
        {
            builder.Factories.Add(new DebugStaticImagePreviewFactory()
            {
                Label = label,
                Background = Background.FromRenderTexture(renderTexture)
            });
            return builder;
        }

        public static IDebugUIBuilder AddImage(this IDebugUIBuilder builder, string label, VectorImage vectorImage)
        {
            builder.Factories.Add(new DebugStaticImagePreviewFactory()
            {
                Label = label,
                Background = Background.FromVectorImage(vectorImage)
            });
            return builder;
        }

        public static IDebugUIBuilder AddImage(this IDebugUIBuilder builder, string label, Func<Texture2D> getter)
        {
            builder.Factories.Add(new DebugTexture2DImagePreviewFactory()
            {
                Label = label,
                Getter = getter,
            });
            return builder;
        }

        public static IDebugUIBuilder AddImage(this IDebugUIBuilder builder, string label, Func<Sprite> getter)
        {
            builder.Factories.Add(new DebugSpriteImagePreviewFactory()
            {
                Label = label,
                Getter = getter,
            });
            return builder;
        }

        public static IDebugUIBuilder AddImage(this IDebugUIBuilder builder, string label, Func<RenderTexture> getter)
        {
            builder.Factories.Add(new DebugRenderTextureImagePreviewFactory()
            {
                Label = label,
                Getter = getter,
            });
            return builder;
        }

        public static IDebugUIBuilder AddImage(this IDebugUIBuilder builder, string label, Func<VectorImage> getter)
        {
            builder.Factories.Add(new DebugVectorImagePreviewFactory()
            {
                Label = label,
                Getter = getter,
            });
            return builder;
        }

        public static IDebugUIBuilder AddFoldout(this IDebugUIBuilder builder, string label, Action<IDebugUIBuilder> configure)
        {
            builder.Factories.Add(new DebugFoldoutFactory()
            {
                Label = label,
                Configure = configure,
            });
            return builder;
        }

        public static void BuildWith(this IDebugUIBuilder builder, UIDocument uiDocument)
        {
            uiDocument.rootVisualElement.Add(builder.Build());
        }
    }
}