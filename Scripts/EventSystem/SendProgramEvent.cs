using Godot;
using System;
using System.Collections.Generic;

namespace EventCallback
{
    public class SendProgramEvent : Event<SendProgramEvent>
    {
        public Dictionary<ulong, InputActions> leftInputTimer = new Dictionary<ulong, InputActions>();
        public Dictionary<ulong, InputActions> rightInputTimer = new Dictionary<ulong, InputActions>();
        public Dictionary<ulong, InputActions> upInputTimer = new Dictionary<ulong, InputActions>();
        public Dictionary<ulong, InputActions> downInputTimer = new Dictionary<ulong, InputActions>();
        public Dictionary<ulong, InputActions> lmbInputTimer = new Dictionary<ulong, InputActions>();
        public Dictionary<ulong, InputActions> rmbInputTimer = new Dictionary<ulong, InputActions>();
        public Dictionary<ulong, Vector2> mousePosTimer = new Dictionary<ulong, Vector2>();
    }


}
