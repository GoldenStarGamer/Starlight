using GLFW;
using OpenGL;

namespace Starlight
{ 
	class Program
	{
		private static List<Entity> ents = [];
		static void Main(string[] args)
		{
			Window window = new();

			if (!Glfw.Init())
			{
				String msg;
				throw new System.Exception($"GLFW STARTUP ERROR {Glfw.GetError(out msg)}: {msg}");
			}

			Glfw.WindowHint(Hint.ContextVersionMajor, 3);
			Glfw.WindowHint(Hint.ContextVersionMinor, 3);
			Glfw.WindowHint(Hint.OpenglProfile, Profile.Core);

			window = Glfw.CreateWindow(800, 600, "Starlight", GLFW.Monitor.None, Window.None);
			if (window == Window.None)
			{
				String msg;
				throw new System.Exception($"GLFW WINDOW CREATION ERROR {Glfw.GetError(out msg)}: {msg}");
			}

			Glfw.MakeContextCurrent(window);
			Gl.Viewport(0, 0, 800, 600);
			Glfw.SetFramebufferSizeCallback(window, (IntPtr window, int width, int height) => Gl.Viewport(0, 0, width, height));

			KeyRun esctoquit = new(window, Keys.Escape, () => Glfw.SetWindowShouldClose(window, true));
			ents.Add(esctoquit);

			while (!Glfw.WindowShouldClose(window))
			{
				Entity.MegaUpdate(ents);
				Gl.ClearColor(1, 0, 0, 0.5f);
				Gl.Clear(ClearBufferMask.ColorBufferBit);
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