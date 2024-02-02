namespace DebugUI
{
    public sealed class DebugWindowOptions : IDebugUIOptions
    {
        public string WindowName { get; set; } = "Debug";
        public bool Draggable { get; set; } = true;
    }
}