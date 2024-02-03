using UnityEngine;
using UnityEngine.SceneManagement;

namespace DebugUI.Sandbox
{
    public class DemoUIBuilder : DebugUIBuilderBase
    {
        static DemoUIBuilder instance;

        [SerializeField] GameObject prefab;

        protected override void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;
            DontDestroyOnLoad(instance);
            base.Awake();
        }

        protected override void Configure(IDebugUIBuilder builder)
        {
            builder.ConfigureWindowOptions(options =>
            {
                options.Title = "Demo";
            });

            builder.AddLabel("Physics");
            builder.AddButton("Add Circle", () => Instantiate(prefab));
            builder.AddSlider("Time Scale", 0f, 2f, () => Time.timeScale, x => Time.timeScale = x);

            builder.AddLabel("Scene");
            builder.AddButton("Reload", () => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex));
        }
    }
}