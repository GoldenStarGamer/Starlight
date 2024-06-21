using GLFW;
using OpenGL;

namespace Starlight
{
	public static class WinManager
	{
		public static Window GetWindow()
		{
			if (!Glfw.Init())
			{
				String msg;
				throw new System.Exception($"GLFW STARTUP ERROR {Glfw.GetError(out msg)}: {msg}");
			}
			Glfw.WindowHint(Hint.ContextVersionMajor, 3);
			Glfw.WindowHint(Hint.ContextVersionMinor, 3);
			Glfw.WindowHint(Hint.OpenglProfile, Profile.Core);
			var window = Glfw.CreateWindow(800, 600, "Starlight", GLFW.Monitor.None, Window.None);
			if (window == Window.None)
			{
				String msg;
				throw new System.Exception($"GLFW WINDOW CREATION ERROR {Glfw.GetError(out msg)}: {msg}");
			}
			return window;
		}
		public static void frameSizeChange(IntPtr window, int width, int height)
		{
			Gl.Viewport(0,0, width, height);
		}
	}
}
