using Godot;
using Scripts.CustomerScripts;
using Scripts.RecipeScripts;
using Scripts.UI.StandScripts;
using Scripts.ZombScripts;

namespace Scripts
{
    public partial class GameManager : Node2D
    {
        [Export] CustomersManager _customerManager;
        [Export] RecipeManager _recipeManager;
        [Export] ZombsManager _zombsManager;
        [Export] Stand _stand;
        [Export] Node2D _ZombsTarget;
        public override void _Ready()
        {
            _customerManager.Start(GetNode<Node2D>("CustomerSpawnPosition"));
            // _stand.Start();
            //_zombsManager.Start(this,_ZombsTarget);

            BindAction();
        }

        public override void _Process(double delta)
        {
            _customerManager.Update(delta);
            //_zombsManager.Update(delta);
        }

        private void BindAction()
        {
            _customerManager.BindCustomerRatio(_stand.GetCustomerInfo);
            _stand.BindGetStandInfo(_zombsManager.GetStandInfo);
        }
    }
}
