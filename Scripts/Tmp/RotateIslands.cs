using Godot;
using System;

public class RotateIslands : Spatial
{
    [Export]
    float rotationSpeed = .01f;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    public override void _Process(float delta)
    {
        RotateY(rotationSpeed * delta);
    }
}
