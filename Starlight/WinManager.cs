using GLFW;
using OpenGL;

namespace Starlight
{
	public static class WinManager
	{
		public static void frameSizeChange(IntPtr window, int width, int height)
		{
			Gl.Viewport(0,0, width, height);
		}
	}
}
