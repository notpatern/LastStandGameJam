using Godot;

namespace Scripts.CustomerScripts {
    public partial class CustomerGFX : Node2D {
        delegate void UpdateDelegate(double delta);
        event UpdateDelegate update;

        delegate void FixedUpdateDelegate(double delta);
        event FixedUpdateDelegate fixedUpdate;

        [Export] public Area2D area;
        public float movementSpeed;

        bool _isColliding = false;
        Godot.RandomNumberGenerator _randomNumberGenerator = new Godot.RandomNumberGenerator();
        int randomDir;

        public override void _Ready() {
            base._Ready();
            int[] direction = {
                -1,
                1
            };

            randomDir = direction[_randomNumberGenerator.Randi() % direction.Length];

            _randomNumberGenerator.Randomize();
            update = DoWaitInLine;
            fixedUpdate = DoWaitInLineMovement;
        }
                    
        public override void _Process(double delta) {
            base._Process(delta);

            update.Invoke(delta);
        }

        public override void _PhysicsProcess(double delta) {
            base._PhysicsProcess(delta);

            fixedUpdate.Invoke(delta);
        }

        public void NextState() {
            update = DoLeave;
            fixedUpdate = DoLeaveMovement;
        }

        private void DoWaitInLine(double delta) {

        }

        private void DoWaitInLineMovement(double delta) {
            if (!area.HasOverlappingAreas()) {
                Position += new Vector2(0, (float)(movementSpeed * delta));
            }
        }

        double time = 5;

        private void DoLeave(double delta) {
            if (time >= 0) {
                time -= delta;
                return;
            }

            GD.Print($"customer destroyed {this}");
            QueueFree();
        }

        private void DoLeaveMovement(double delta) {
            Position += new Vector2(randomDir * (float)(movementSpeed * delta), 0);
        }
    }
}
