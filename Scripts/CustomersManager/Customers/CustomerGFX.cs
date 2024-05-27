using Godot;

namespace Scripts.CustomerScripts {
    public partial class CustomerGFX : Node2D {
        int index = 0;

        delegate void UpdateDelegate();
        event UpdateDelegate update;

        [Export] public Area2D area;

        public override void _Ready() {
            base._Ready();

            update = DoCome;
        }

        public override void _Process(double delta) {
            base._Process(delta);

            update.Invoke();
        }

        // this is so scuffed but hey it looks good in Customer.cs
        public void NextState() {
            if (index == 0) {
                index++;
                update = DoIdle;
            } 
            else if (index == 1) {
                index++;
                update = DoLeave;
            }
        }

        private void DoCome() {

        }

        private void DoIdle() {

        }

        private void DoLeave() {

        }

        private new void Dispose() {

        }
    }
}
