using Godot;
using System;
using EventCallback;

public class InputHandler : Node
{
    //Might use them later to store custom key bindings for the game
    int upKey, downKey, leftKey, rightKey, jumpKey, crouchKey, grappleKey, glideKey;

    InputHandleEvent ihei;
    bool upCheck, downCheck, leftCheck, rightCheck;
    ulong lastMousePosTimeEntry = 0;
    bool mouseUpdateCalled = false;

    public override void _UnhandledInput(Godot.InputEvent @event)
    {
        ihei = new InputHandleEvent();
        if (@event is InputEventMouseButton eventMouseButton)
        {
            if (eventMouseButton.Pressed)
            {
                if (eventMouseButton.ButtonIndex == (int)ButtonList.Left) ihei.lmbPressed = true;
                if (eventMouseButton.ButtonIndex == (int)ButtonList.Right) ihei.rmbPressed = true;
            }
            else
            {
                if (eventMouseButton.ButtonIndex == (int)ButtonList.Left) ihei.lmbPressed = false;
                if (eventMouseButton.ButtonIndex == (int)ButtonList.Right) ihei.rmbPressed = false;
            }
        }
        if (@event is InputEventKey eventKey)
        {
            if (eventKey.Pressed)
            {
                if (eventKey.Scancode == (int)KeyList.W) ihei.upPressed = true;
                if (eventKey.Scancode == (int)KeyList.S) ihei.downPressed = true;
                if (eventKey.Scancode == (int)KeyList.A) ihei.leftPressed = true;
                if (eventKey.Scancode == (int)KeyList.D) ihei.rightPressed = true;
                if (eventKey.Scancode == (int)KeyList.Space) ihei.jumpPressed = true;
                if (eventKey.Scancode == (int)KeyList.C) ihei.crouchPressed = true;
                if (eventKey.Scancode == (int)KeyList.Shift) ihei.sprintPressed = true;
                if (eventKey.Scancode == (int)KeyList.E) ihei.abilityPressed = true;
                if (eventKey.Scancode == (int)KeyList.Q)ihei.consolePressed = true;
            }
            else
            {
                if (eventKey.Scancode == (int)KeyList.W) ihei.upPressed = false;
                if (eventKey.Scancode == (int)KeyList.S) ihei.downPressed = false;
                if (eventKey.Scancode == (int)KeyList.A) ihei.leftPressed = false;
                if (eventKey.Scancode == (int)KeyList.D) ihei.rightPressed = false;
                if (eventKey.Scancode == (int)KeyList.Space) ihei.jumpPressed = false;
                if (eventKey.Scancode == (int)KeyList.C) ihei.crouchPressed = false;
                if (eventKey.Scancode == (int)KeyList.Shift) ihei.sprintPressed = false;
                if (eventKey.Scancode == (int)KeyList.E) ihei.abilityPressed = false;
                if (eventKey.Scancode == (int)KeyList.Q)ihei.consolePressed = false;
            }

            ihei.FireEvent();
        }
    }
}
