using Godot;
using System;

public partial class ScoreManager : Node
{
	public static ScoreManager Instance { get; private set; }
	private int _score;
	private int _highScore;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Instance = this;
	}

	// Returns current Score
	public static int GetScore()
	{
		return Instance._score;
	}
	// Returns High Score
	public static int GetHighScore()
	{
		return Instance._highScore;
	}

	// Sets current Score and checks if it is a new High Score
	public static void SetScore(int value)
	{
		Instance._score = value;

		// Check for new High Score and set if true
		if (Instance._score > Instance._highScore)
		{
			Instance._highScore = Instance._score;
		}

		GD.Print($"Score: {Instance._score}, High Score: {Instance._highScore}");
		SignalManager.EmitOnScored();
	}

	// Resets current Score to 0
	public static void ResetScore()
	{
		SetScore(0);
	}
	// Increments current Score by 1
	public static void IncrementScore()
	{
		SetScore(GetScore() + 1);
	}
}
