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
        protected MettalicAI state;
        protected Vector2 debugVector;
        protected Sprite2D zombSprite = new Sprite2D();
        [Export] protected CollisionShape2D collisionCircle;
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
            if (GlobalPosition.DistanceTo(target.GlobalPosition) >= 10)
            {
                state = DoSeek;
            } else
            {
                state = DoDestroy;
            }
        }

        protected void DoSeek()
        {
            if(!isAllowedToMove)
            {
                return;
            }
            velocity = new Vector2(speed, speed) * GlobalPosition.DirectionTo(target.GlobalPosition);
            GlobalPosition += velocity;
        }

        protected void DoDestroy()
        {
            
        }

        public override void _Process(double delta)
        {
            SwitchState();
            state();
        }
    }
}
