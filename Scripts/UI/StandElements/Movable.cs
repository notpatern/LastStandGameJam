using Godot;
using System.Security.Cryptography.X509Certificates;

namespace Scripts.UI.StandElements {

    [GlobalClass]
    public abstract partial class Movable : Node2D {

        [Export] public MovableBody body;
        [Export] private Node2D gfxContainer;

        [Export] private float maxGFXOffset = -15;
        [Export] private float GFXOffsetSpeed = -500;
        private Vector2 mouseOffset;

        protected Vector2 screenSize;
        protected Vector2 spriteSize = Vector2.Zero;

        public delegate void State(double delta);
        public State stateMovable;
        
        public override void _Ready() {
            body.movableParent = this;
            stateMovable = DroppedState;
            screenSize = GetViewport().GetVisibleRect().Size;
        }

        public override void _Process(double delta) {
            stateMovable(delta);
            StayWithinScreen(spriteSize);
        }

        private void StayWithinScreen(Vector2 spriteSize) {
            Position = new Vector2(Mathf.Clamp(Position.X, spriteSize.X, screenSize.X - spriteSize.X), Mathf.Clamp(Position.Y, spriteSize.Y, screenSize.Y - spriteSize.Y));
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

        #region Visuals
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
        #endregion
    }
}

