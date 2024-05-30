using Godot;

namespace Scripts.CustomerScripts {
    public partial class CustomerGFX : Node2D {
        delegate void UpdateDelegate(double delta);
        event UpdateDelegate update;

        delegate void FixedUpdateDelegate(double delta);
        event FixedUpdateDelegate fixedUpdate;

        public int stateIndex = 0;

        [Export] public Area2D area;
        [Export] public Node2D recipeUi;

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
            update = DoCome;
            fixedUpdate = DoComeMovement;
        }

        public void SetRecipeText(string text)
        {
            foreach (Node child in recipeUi.GetChildren())
            {
                if (child is RichTextLabel)
                {
                    RichTextLabel label = (RichTextLabel)child;
                    label.Text = text;
                }
            }
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
            if (stateIndex == 0) {
                stateIndex++;
                update = DoWaitInLine;
                fixedUpdate = DoWaitInLineMovement;
            }
            else if (stateIndex == 1) {
                stateIndex++;
                update = DoLeave;
                fixedUpdate = DoLeaveMovement;
                recipeUi.QueueFree();
            }            
        }

        private void DoCome(double delta) {
            recipeUi.Visible = false;
        }

        private void DoComeMovement(double delta) {
            if (!area.HasOverlappingAreas()) {
                Position += new Vector2(0, (float)(movementSpeed * delta));
                return;
            }

            NextState();
        }

        private void DoWaitInLine(double delta) {
            if (recipeUi.Visible == false) {
                recipeUi.Visible = true;
            }
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
