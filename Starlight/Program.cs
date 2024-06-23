namespace Starlight
{ 
	class Program
	{
		static void Main(string[] args)
		{
			Game game = new(600, 800, "Starlight");
			game.Run();
		}
	}
}