using GLFW;

namespace Starlight
{ 
    class Program
    {
        static void GLFWStart()
        {
            Glfw.Init();
            Glfw.WindowHint(Hint.ContextVersionMajor, 3);
            Glfw.WindowHint(Hint.ContextVersionMinor, 3);
            Glfw.WindowHint(Hint.OpenglProfile, Profile.Core);

        }
        
        static void Main(string[] args)
        {
            GLFWStart();
        }
    }
    
}