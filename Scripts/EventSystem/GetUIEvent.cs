using Godot;
using System;
namespace EventCallback
{

    public class GetUIEvent : Event<GetUIEvent>
    {
        public UIState uiState;
    }

}
