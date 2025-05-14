using Godot;
using System;

public partial class Hud : Control
{
	[Export] private Label _scoreLabel;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SignalManager.Instance.OnScored += OnScored;
	}
	// Called when the node is removed from the scene tree.
	public override void _ExitTree()
	{
		SignalManager.Instance.OnScored -= OnScored;
	}

	// Updates the Score Label Text when the score changes
	private void OnScored()
	{
		_scoreLabel.Text = $"{ScoreManager.GetScore():0000}";
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
