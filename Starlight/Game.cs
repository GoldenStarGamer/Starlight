using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Starlight
{
    public class Game : GameWindow
    {
        private static List<Entity> ents = [];

        public Game(int width, int height, string title) : base(GameWindowSettings.Default, new NativeWindowSettings()
        {
            ClientSize = (width, height), Title = title
        }) { }

        public override void Run()
        {
            KeyRun esctoquit = new(this, Keys.Escape, () => Close());
            ents.Add(esctoquit);
            base.Run();
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        { 
            base.OnUpdateFrame(args);
            Entity.MegaUpdate(ents);
        }
    }
}
