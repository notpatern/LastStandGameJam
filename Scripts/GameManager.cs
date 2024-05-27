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

        public override void _Ready()
        {
            _customerManager.Start();
            _recipeManager.Start();
            _zombsManager.Start();
        }

        public override void _Process(double delta)
        {
            _customerManager.Update();
            _zombsManager.Update();
        }
    }
}
