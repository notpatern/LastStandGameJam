using Godot;

namespace Scripts.ZombScripts
{
    public partial class Zomb : Node2D
    {
        [Export] public float speed;
        [Export] public float health;
        [Export] public float damage;
        public Texture2D zombTexture;
        public delegate void MettalicAI();
        MettalicAI state;
        Vector2 debugVector;
        Sprite2D zombSprite = new Sprite2D();
        [Export] CollisionShape2D collisionCircle;

        public override void _Ready()
        {
            GD.Print((collisionCircle.Shape as CircleShape2D).Radius);
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
                state = DoDestroy;
            }
        }

        private void DoSeek()
        {
            GlobalPosition += new Vector2(speed, 0);
            GD.Print("weee");
            GD.Print(GlobalPosition.DistanceSquaredTo(debugVector / 2));
        }

        private void DoDestroy()
        {
            GD.Print("waaa");
        }

        public override void _Process(double delta)
        {
            SwitchState();
            state();
        }
    }
}
