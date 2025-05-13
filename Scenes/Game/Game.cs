using Godot;
using System;

public partial class Game : Node2D
{
	[Export] private Marker2D _spawnUpper;
	[Export] private Marker2D _spawnLower;
	[Export] private Timer _pipeSpawner;
	[Export] private PackedScene _pipeScene;
	[Export] private Node2D _pipesHolder;
	[Export] private Plane _plane;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Plane plane = GetNode<Plane>("Plane");
		_plane.OnPlaneDeath += GameOver;
		_pipeSpawner.Timeout += SpawnPipes;
		SpawnPipes();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public float GetSpawnY()
	{
		return (float)GD.RandRange(_spawnUpper.Position.Y, _spawnLower.Position.Y);
	}

	private void SpawnPipes()
	{
		Pipes newPipes = _pipeScene.Instantiate<Pipes>();
		float xPos = _spawnUpper.Position.X;
		// Add pipes as child of pipesHolder then set spawnpoint.
		_pipesHolder.AddChild(newPipes);
		newPipes.Position = new Vector2(xPos, GetSpawnY());
	}

	private void StopPipes()
	{
		_pipeSpawner.Stop();
		foreach (Node pipe in _pipesHolder.GetChildren())
		{
			pipe.SetProcess(false);
		}
	}

	private void GameOver()
	{
		GD.Print("OnPlaneDeath Signal Recieved!");
		StopPipes();
	}
}
