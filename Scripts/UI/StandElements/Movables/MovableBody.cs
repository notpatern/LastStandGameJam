using Godot;
using System;

namespace Scripts.UI.StandElements {
	public partial class MovableBody : Area2D {
		
        public Movable movableParent;

        public override void _InputEvent(Viewport viewport, InputEvent @event, int shapeIdx) {
            //left click pressed
            if (@event is InputEventMouseButton && ((InputEventMouseButton)@event).ButtonIndex == MouseButton.Left && ((InputEventMouseButton)@event).Pressed) {
                movableParent.SetPickedUp();
            }
            //left click released
            else if (@event is InputEventMouseButton && ((InputEventMouseButton)@event).ButtonIndex == MouseButton.Left && !((InputEventMouseButton)@event).Pressed) {
                if (movableParent.state == movableParent.PickedUpState) {
                    movableParent.SetDropped();
                }
            }
        }
    }
}
