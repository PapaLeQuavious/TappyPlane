using Godot;
using System;

public partial class Plane : CharacterBody2D
{

	const float GRAVITY = 800.0f;
	const float FLIGHT_POWER = 400.0f;
	[Export] private AnimationPlayer _anims;
	[Export] private AnimatedSprite2D _planeSprite;

	[Signal] public delegate void OnPlaneDeathEventHandler();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		// Velocity returns a copy of the value so must be stored before editing
		Vector2 velocity = Velocity;
		if (Input.IsActionJustPressed("fly"))
		{
			GD.Print("flying!");
			velocity.Y -= FLIGHT_POWER;
			Velocity = velocity; // Set target Velocity
			MoveAndSlide(); // Invoke MoveAndSlide to move based on Velocity

			_anims.Play(name: "Power");
		}
		else
		{
			velocity.Y += GRAVITY * (float)delta; // Apply GRAVITY to target Velocity
			Velocity = velocity; // Set target Velocity
			MoveAndSlide(); // Invoke MoveAndSlide to move based on Velocity
		}

		if (IsOnFloor())
		{
			Die();
		}
	}

	public void Die()
	{
		SetPhysicsProcess(false);
		_planeSprite.Stop();
		EmitSignal(SignalName.OnPlaneDeath);
		GD.Print("Plane Died");
	}

}

