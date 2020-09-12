using Godot;
using System;
using EventCallback;

public class InputHandler : Node
{
int upKey, downKey, leftKey, rightKey, jumpKey, crouchKey, grappleKey, glideKey;

    InputHandleEvent ihei;
    bool upCheck, downCheck, leftCheck, rightCheck;
    ulong lastMousePosTimeEntry = 0;
    bool mouseUpdateCalled = false;

    public override void _Ready()
    {
        //Set the input key to be used for adding the key events fom the keyboard
        InputEventKey inputKeyboard = new InputEventKey();
        //Set the input to be used for adding the key events fom the mouse
        InputEventMouseButton inputMouse = new InputEventMouseButton();

        InputMap.AddAction("LeftClick");
        inputKeyboard.Scancode = int(KeyList.A);
        //Used to add keyboard keys
        InputMap.ActionAddEvent("LeftClick", inputKeyboard);
    }

    public override void _UnhandledInput(Godot.InputEvent @event)
    {
        if (@event is InputEventMouseButton || @event is InputEventKey eventKey)
        {
            ihei = new InputHandleEvent();
if (eventKey.Pressed && eventKey.Scancode == (int)KeyList.Escape)) ihei.upPressed = true;
            
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
