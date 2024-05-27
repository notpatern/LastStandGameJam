using Godot;

namespace Scripts.UI.StandElements {
    public /*abstract*/ partial class Movable : Node2D {

        [Export] private NodePath bodyPath;
        private MovableBody body;

        private Vector2 mouseOffset;

        public delegate void State(double delta);
        public State state;
        
        public override void _Ready() {
            body = GetNode<MovableBody>(bodyPath);
            body.movableParent = this;

            state = DroppedState;
        }

        public override void _Process(double delta) {
            state(delta);
        }

        public virtual void PickedUpState(double delta) {
            Position = mouseOffset + GetGlobalMousePosition();

        }

        public virtual void DroppedState(double delta) {

        }

        public virtual void SetPickedUp() {
            state = PickedUpState;
            mouseOffset = Position - GetGlobalMousePosition();
        }

        public virtual void SetDropped() {
            state = DroppedState;
        }
    }
}

