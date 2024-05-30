using Godot;
using Scripts.ZombScripts;
using System.Collections.Generic;
using System.Linq;

namespace Scripts.GlassAttacks {
    public partial class GlassAttackOvertime : GlassAttack {

        [Export] private float tickDelay = 0.5f;

        private Timer timer = new Timer();
        private Timer damageTickTimer = new Timer();
        public override void _Ready() {
            base._Ready();
            timer.Connect("timeout", new Callable(this, nameof(OnAttackEndDuration)));
            timer.WaitTime = duration;
            timer.OneShot = true;
            timer.Start();

            damageTickTimer.Connect("timeout", new Callable(this, nameof(OnTick)));
            damageTickTimer.WaitTime = tickDelay;
            damageTickTimer.OneShot = false;
            damageTickTimer.Start();
        }

        private void OnTick() {
            List<Area2D> overlappingAreas = hitBox.GetOverlappingAreas().ToList();
            int length = overlappingAreas.Count;

            for (int i = length; i > 0; i--) {
                DealDamage((Zomb)overlappingAreas[i].GetParent());
            }
        }

        protected override void OnHit(Area2D area) {
            base.OnHit(area); //checks if area;GetParent() is Zomb.
            Zomb zomb = (Zomb)area.GetParent();
            DealDamage(zomb);
        }

        private void OnAttackEndDuration() {
            QueueFree(); //add animations later.
        }
    }
}

