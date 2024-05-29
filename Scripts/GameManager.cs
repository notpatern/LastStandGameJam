using Godot;
using Scripts.CustomerScripts;
using Scripts.RecipeScripts;
using Scripts.ZombScripts;

namespace Scripts
{
    public partial class GameManager : Node2D
    {
        [Export] CustomersManager _customerManager;
        [Export] RecipeManager _recipeManager;
        [Export] ZombsManager _zombsManager;
        [Export] Node2D _ZombsTarget;
        public override void _Ready()
        {
            //_customerManager.Start(GetNode<Node2D>("CustomerSpawnPosition"));
            _zombsManager.Start(this,_ZombsTarget);
        }

        public override void _Process(double delta)
        {
            //_customerManager.Update(delta);
            _zombsManager.Update(delta);
        }
    }
}
