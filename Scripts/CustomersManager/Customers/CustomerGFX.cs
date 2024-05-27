using Godot;

namespace Scripts.CustomerScripts {
    public partial class CustomerGFX : Node2D {
        delegate void UpdateDelegate(double delta);
        event UpdateDelegate update;

        [Export] public Area2D area;
        public float movementSpeed;

        bool _isColliding = false;

        public override void _Ready() {
            base._Ready();

            update = DoWaitInLine;
        }

        double time = 0;
                    
        public override void _Process(double delta) {
            base._Process(delta);

            update.Invoke(delta);

            time += delta;

            if (time >= 5f)
            {
                GD.Print("caca");
                QueueFree();
            }            
        }

        public void NextState() {
            update = DoLeave;
        }

        private void DoWaitInLine(double delta) {

            if (!area.HasOverlappingAreas())
            {
                Position += new Vector2(0, (float)(movementSpeed * delta));
            }
        }

        private void DoLeave(double delta) {

        }
    }
}
