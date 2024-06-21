using GLFW;
using OpenGL;

namespace Starlight
{ 
	class Program
	{
		private static List<Entity> ents = [];
		static void Main(string[] args)
		{
			var window = WinManager.GetWindow();
			Glfw.MakeContextCurrent(window);
			Gl.Viewport(0, 0, 800, 600);
			Glfw.SetFramebufferSizeCallback(window, WinManager.frameSizeChange);
			while (!Glfw.WindowShouldClose(window))
			{
				Entity.MegaUpdate(ents);
				Glfw.SwapBuffers(window);
				Glfw.PollEvents();
			}
		}
	}
}