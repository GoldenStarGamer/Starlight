using GLFW;
using OpenGL;

namespace Starlight
{ 
	class Program
	{
		private static List<Entity> ents = [];
		static void Main(string[] args)
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
			Glfw.MakeContextCurrent(window);
			// Here lies Gl.Viewport(0, 0, 800, 600); He was found to create an anoying exeption, that stupid bitch.
			Glfw.SetFramebufferSizeCallback(window, WinManager.frameSizeChange);
			while (!Glfw.WindowShouldClose(window))
			{
				Entity.MegaUpdate(ents);
				Glfw.SwapBuffers(window);
				Glfw.PollEvents();
			}
			try
			{
				Glfw.Terminate();
			}
			catch (GLFW.Exception e)
			{
				Console.Error.WriteLine($"ERROR: {e.Message}");
			}
		}
	}
}