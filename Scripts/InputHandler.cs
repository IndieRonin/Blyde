using Godot;
using System;
using EventCallback;

public class InputHandler : Node
{
    InputHandleEvent ihei;
    bool upCheck, downCheck, leftCheck, rightCheck;
    ulong lastMousePosTimeEntry = 0;
    bool mouseUpdateCalled = false;

    public override void _UnhandledInput(Godot.InputEvent @event)
    {
        if (@event is InputEventMouseButton || @event is InputEventKey)
        {
            ihei = new InputHandleEvent();
            if (@event.IsActionPressed("LeftClick")) ihei.lmbPressed = true;
            if (@event.IsActionReleased("LeftClick")) ihei.lmbRelease = true;
            if (@event.IsActionPressed("RightClick")) ihei.rmbPressed = true;
            if (@event.IsActionReleased("RightClick")) ihei.rmbRelease = true;

            if (@event.IsActionPressed("MoveUp")) ihei.upPressed = true;
            if (@event.IsActionReleased("MoveUp")) ihei.upRelease = true;
            if (@event.IsActionPressed("MoveDown")) ihei.downPressed = true;
            if (@event.IsActionReleased("MoveDown")) ihei.downRelease = true;
            if (@event.IsActionPressed("MoveLeft")) ihei.leftPressed = true;
            if (@event.IsActionReleased("MoveLeft")) ihei.leftRelease = true;
            if (@event.IsActionPressed("MoveRight")) ihei.rightPressed = true;
            if (@event.IsActionReleased("MoveRight")) ihei.rightRelease = true;

            if (@event.IsActionPressed("Jump")) ihei.jumpPressed = true;
            if (@event.IsActionPressed("Jump")) ihei.jumpRelease = true;
            if (@event.IsActionPressed("Crouch")) ihei.crouchPressed = true;
            if (@event.IsActionPressed("Crouch")) ihei.crouchRelease = true;
            if (@event.IsActionPressed("Sprint")) ihei.sprintPressed = true;
            if (@event.IsActionPressed("Sprint")) ihei.sprintRelease = true;
            if (@event.IsActionPressed("Ability")) ihei.abilityPressed = true;
            if (@event.IsActionPressed("Ability")) ihei.abilityRelease = true;
            if (@event.IsActionPressed("ToggleConsole")) ihei.consolePressed = true;
        
            ihei.FireEvent();
        }
    }
}
