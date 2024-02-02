using UnityEngine;

namespace DebugUI.Sandbox
{
    public sealed class DebugUISandbox : DebugUIBuilderBase
    {
        [Header("Fields")]
        [SerializeField] bool boolValue;
        [SerializeField] float floatValue;
        [SerializeField] int intValue;
        [SerializeField] Vector2 vector2Value;
        [SerializeField] Vector3 vector3Value;
        [SerializeField] Vector4 vector4Value;
        [SerializeField] Vector2Int vector2IntValue;
        [SerializeField] Vector3Int vector3IntValue;
        [SerializeField] Bounds boundsValue;

        [Header("Slider")]
        [SerializeField, Range(0f, 10f)] float sliderValue;
        [SerializeField, Range(0, 10)] int sliderIntValue;

        [Header("Images")]
        [SerializeField] Sprite staticSpriteValue;
        [SerializeField] Sprite dynamicSpriteValue;

        void Button1()
        {
            Debug.Log("Hello!");
        }

        void Button2()
        {
            Debug.Log("How are you?");
        }

        void Button3()
        {
            Debug.Log("Goodbye!");
        }

        protected override void Configure(IDebugUIBuilder builder)
        {
            builder.ConfigureWindowOptions(options =>
            {
                options.WindowName = "Sandbox";
            });

            builder.AddFoldout("Labels", builder =>
            {
                builder.AddLabel("Hello!");
                builder.AddLabel("How are you?");
                builder.AddLabel("Goodbye!");
            });

            builder.AddFoldout("Buttons", builder =>
            {
                builder.AddButton("Button1", Button1);
                builder.AddButton("Button2", Button2);
                builder.AddButton("Button3", Button3);
            });

            builder.AddFoldout("Fields", builder =>
            {
                builder.AddField("Bool", () => boolValue, x => boolValue = x);
                builder.AddField("Float", () => floatValue, x => floatValue = x);
                builder.AddField("Vector2", () => vector2Value, x => vector2Value = x);
                builder.AddField("Vector3", () => vector3Value, x => vector3Value = x);
                builder.AddField("Vector4", () => vector4Value, x => vector4Value = x);
                builder.AddField("Vector2Int", () => vector2IntValue, x => vector2IntValue = x);
                builder.AddField("Vector3Int", () => vector3IntValue, x => vector3IntValue = x);
                builder.AddField("Bounds", () => boundsValue, x => boundsValue = x);
            });
            
            builder.AddFoldout("Sliders", builder =>
            {
                builder.AddSlider("Slider", 0f, 10f, () => sliderValue, x => sliderValue = x);
                builder.AddProgressBar("Progress", 0f, 10f, () => sliderValue);

                builder.AddSlider("Slider (Int)", 0, 10, () => sliderIntValue, x => sliderIntValue = x);
                builder.AddProgressBar("Progress", 0, 10, () => sliderIntValue);
            });

            builder.AddFoldout("Images", builder =>
            {
                builder.AddImage("Static", staticSpriteValue);
                builder.AddImage("Dynamic", () => dynamicSpriteValue);
            });

        }
    }
}