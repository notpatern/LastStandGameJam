using Godot;

namespace Scripts.ZombScripts
{
    public partial class Zomb : Node2D
    {
        [Export] public float speed;
        [Export] public float health;
        [Export] public float damage;
        public Node2D target;
        public Texture2D zombTexture;
        public delegate void MettalicAI();
        MettalicAI state;
        Vector2 debugVector;
        Sprite2D zombSprite = new Sprite2D();
        [Export] CollisionShape2D collisionCircle;
        public float radius;
        [Export] public float repulsionForce = 2;
        public Vector2 velocity;

        public bool isAllowedToMove = true;

        public override void _Ready()
        {
            radius = (collisionCircle.Shape as CircleShape2D).Radius;
            AddChild(zombSprite);
            zombSprite.Texture = zombTexture;
            debugVector = GetViewport().GetVisibleRect().Size;
            state = DoSeek;
        }

        public void SwitchState()
        {
            if (GlobalPosition.DistanceSquaredTo(debugVector/2) >= 1000)
            {
                state = DoSeek;
            } else
            {
                //state = DoDestroy;
            }
        }

        private void DoSeek()
        {
            if(!isAllowedToMove)
            {
                return;
            }
            velocity = new Vector2(speed, speed) * GlobalPosition.DirectionTo(target.GlobalPosition);
            GlobalPosition += velocity;
        }

        private void DoDestroy()
        {
            
        }

        public override void _Process(double delta)
        {
            SwitchState();
            state();
        }
    }
}
