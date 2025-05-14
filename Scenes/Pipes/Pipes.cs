using Godot;
using System;

public partial class Pipes : Node2D
{
	[Export] private VisibleOnScreenNotifier2D _screenNotifier;
	[Export] private Area2D _upperPipe;
	[Export] private Area2D _lowerPipe;
	[Export] private Area2D _laser;
	[Export] private float _scrollSpeed = 120.0f;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Attaching methods to signals
		_screenNotifier.ScreenExited += OnScreenExited;
		_lowerPipe.BodyEntered += OnPipeBodyEntered;
		_upperPipe.BodyEntered += OnPipeBodyEntered;
		_laser.BodyEntered += OnLaserBodyEntered;
		SignalManager.Instance.OnPlaneDeath += StopPipes;
	}

	// Called when the node exits the scene tree for the last time.
	public override void _ExitTree()
	{
		SignalManager.Instance.OnPlaneDeath -= StopPipes;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Position -= new Vector2(_scrollSpeed * (float)delta, 0);
	}

	// Handle Plane Scoring
	private void OnLaserBodyEntered(Node2D body)
	{
		ScoreManager.IncrementScore();
	}

	// Handle Plane Collisions
	private void OnPipeBodyEntered(Node2D body)
	{
		if (body is Plane)
		{
			(body as Plane).Die();
		}
		// Can also handle the plane death with grouping like this:
		// if (body.IsInGroup("plane"))
		// {
		// 	(body as Plane).Die();
		// }
	}

	// Remove Pipes when exiting viewport
	private void OnScreenExited()
	{
		QueueFree();
	}

	// Stop pipes scrolling
	private void StopPipes()
	{
		SetProcess(false);
	}
}
