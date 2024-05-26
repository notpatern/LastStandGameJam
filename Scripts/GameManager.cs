using Godot;


public partial class GameManager : Node2D
{
	[Export] CustomerManager _customerManager;

	public override void _Ready()
	{
		_customerManager.Start();
	}

	
	public override void _Process(double delta)
	{
		_customerManager.Update();
	}
}
