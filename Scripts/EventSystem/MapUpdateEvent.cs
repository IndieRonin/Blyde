using Godot;
using System;

namespace EventCallback
{
    public class MapUpdateEvent : Event<MapUpdateEvent>
    {
        //The location of the collision
        public Vector2 CollisionPos;
    }
}
