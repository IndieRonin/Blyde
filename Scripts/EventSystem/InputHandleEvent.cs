using Godot;
using System;

namespace EventCallback
{
    public class InputHandleEvent : Event<InputHandleEvent>
    {
        public bool upPressed, downPressed, leftPressed, rightPressed, lmbPressed, rmbPressed, abilityPressed, jumpPressed, sprintPressed, crouchPressed, consolePressed;
        public bool upRelease, downRelease, leftRelease, rightRelease, lmbRelease, rmbRelease,  abilityRelease, jumpRelease, sprintRelease, crouchRelease;
    }
}
