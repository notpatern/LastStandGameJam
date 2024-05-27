using Godot;
using System;
using Scripts.UI.StandElements.Usable;

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
            //if is iUsable
            if (movableParent is iUsable) {
                //right click pressed
                if (@event is InputEventMouseButton && ((InputEventMouseButton)@event).ButtonIndex == MouseButton.Right && ((InputEventMouseButton)@event).Pressed) {
                    ((iUsable)movableParent).StartUse();
                }
                //right click released
                else if (@event is InputEventMouseButton && ((InputEventMouseButton)@event).ButtonIndex == MouseButton.Right && !((InputEventMouseButton)@event).Pressed) {
                    ((iUsable)movableParent).StopUse();
                }
            }
            
        }
    }
}
