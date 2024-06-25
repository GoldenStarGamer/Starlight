namespace Starlight
{ 
	class Program
	{
		static void Main(string[] args)
		{
			Game game = new(800, 600, "Starlight");
			game.Run();
		}
	}
}