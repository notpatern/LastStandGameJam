using Godot;

namespace Scripts.UI.StandElements {
    [GlobalClass]
    public abstract partial class Movable : Node2D {
        [Export] private MovableBody body;
        [Export] private Node2D gfxContainer;

        [Export] private float maxGFXOffset = -15;
        [Export] private float GFXOffsetSpeed = -500;
        private Vector2 mouseOffset;

        public delegate void State(double delta);
        public State stateMovable;
        
        public override void _Ready() {
            body.movableParent = this;
            stateMovable = DroppedState;
        }

        public override void _Process(double delta) {
            stateMovable(delta);
        }

        public virtual void PickedUpState(double delta) {
            Position = mouseOffset + GetGlobalMousePosition();
            MoveUp(delta);
        }

        public virtual void DroppedState(double delta) {
            MoveDown(delta);
        }

        public virtual void SetPickedUp() {
            stateMovable = PickedUpState;
            mouseOffset = Position - GetGlobalMousePosition();
        }

        public virtual void SetDropped() {
            stateMovable = DroppedState;
        }

        public void MoveUp(double delta) {
            //this is juiceable
            if (gfxContainer.Position.Y > maxGFXOffset) {
                gfxContainer.Position += new Vector2(0, GFXOffsetSpeed) * (float)delta;
            }
        }
        public void MoveDown(double delta) {
            //this is juiceable
            if (gfxContainer.Position.Y < 0) {
                gfxContainer.Position -= new Vector2(0, GFXOffsetSpeed) * (float)delta;
            }
        }
    }
}

