using Godot;

namespace Scripts.UI.StandElements {
    public abstract partial class Movable : Node2D {

        [Export] private NodePath bodyPath;
        private MovableBody body;
        
        public override void _Ready() {
            body = GetNode<MovableBody>(bodyPath);
        }

        public override void _Process(double delta) {
        }

        public virtual void PickUp() {
        }
        public virtual void Drop() {
        }
    }
}

