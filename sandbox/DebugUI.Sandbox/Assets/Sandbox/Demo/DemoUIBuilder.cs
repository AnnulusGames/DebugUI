using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

namespace DebugUI.Sandbox
{
    public class DemoUIBuilder : DebugUIBuilderBase
    {
        static DemoUIBuilder instance;

        [SerializeField] GameObject prefab;
        [SerializeField] Volume volume;

        ColorAdjustments colorAdjustments;
        Bloom bloom;

        float GravityScale
        {
            get
            {
                return Physics2D.gravity.y / -9.81f;
            }
            set
            {
                var x = Physics2D.gravity.x;
                Physics2D.gravity = new Vector2(x, value * -9.81f);
            }
        }

        protected override void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;
            DontDestroyOnLoad(instance);

            volume.profile.TryGet(out colorAdjustments);
            volume.profile.TryGet(out bloom);

            base.Awake();
        }

        protected override void Configure(IDebugUIBuilder builder)
        {
            builder.ConfigureWindowOptions(options =>
            {
                options.Title = "Demo";
            });

            builder.AddFoldout("Physics", builder =>
            {
                builder.AddSlider("Time Scale", 0f, 3f, () => Time.timeScale, x => Time.timeScale = x);
                builder.AddSlider("Gravity Scale", 0f, 3f, () => GravityScale, x => GravityScale = x);
                builder.AddButton("Add Circle", () => Instantiate(prefab));
                builder.AddButton("Reload Scene", () => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex));
            });

            builder.AddFoldout("Post-processing", builder =>
            {
                builder.AddSlider("Hue Shift", -180f, 180f, () => colorAdjustments.hueShift.value, x => colorAdjustments.hueShift.value = x);
                builder.AddSlider("Bloom Intensity", 0f, 10f, () => bloom.intensity.value, x => bloom.intensity.value = x);
            });
        }
    }
}