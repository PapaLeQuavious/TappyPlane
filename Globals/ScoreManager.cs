using Godot;
using System;

public partial class ScoreManager : Node
{
	public static ScoreManager Instance { get; private set; }
	private uint _score;
	private uint _highScore;

	private const string SCORE_FILE = "user://tappyplane.savedata";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Instance = this;
	}

	// Called when the node is removed from the scene tree.
	public override void _ExitTree()
	{
		SaveScoreToFile();
	}

	// Returns current Score
	public static uint GetScore()
	{
		return Instance._score;
	}
	// Returns High Score
	public static uint GetHighScore()
	{
		return Instance._highScore;
	}

	// Sets current Score and checks if it is a new High Score
	public static void SetScore(uint value)
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

	private void LoadScoreFromFile()
	{
		using FileAccess file = FileAccess.Open(SCORE_FILE, FileAccess.ModeFlags.Write);
		if (file != null)
		{
			_highScore = file.Get32();
		}
	}
	private void SaveScoreToFile()
	{
		using FileAccess file = FileAccess.Open(SCORE_FILE, FileAccess.ModeFlags.Write);
		if (file != null)
		{
			file.Store32(_highScore);

		}
	}
}
