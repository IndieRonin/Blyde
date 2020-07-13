using Godot;
using System;
namespace EventCallback
{

    public class SendUIEvent : Event<SendUIEvent>
    {
        public UIState uiState;
    }

}
