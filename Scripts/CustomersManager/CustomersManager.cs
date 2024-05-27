using Godot;
using Scripts.RecipeScripts;
using System.Linq;

namespace Scripts.CustomerScripts
{
    public partial class CustomersManager : Resource
    {
        [Export] Customer[] customerScriptableObjects;
        Customer[] liveCustomers;
        Area2D queueHitBox;

        public void Start()
        {
            queueHitBox.Connect("area_entered", new Callable(this, nameof(CheckIfRecipeConmpleted)));
        }

        public void Update()
        {

        }

        public void CheckIfRecipeConmpleted(Node2D node) {

            Recipe recipe = (Recipe)node.GetScript();

            if (recipe == null) {
                return;
            }

            foreach (Customer customer in liveCustomers) {
                // checks whether there are any elements in [customer.customerData.recipe.content] that are not in [recipe.content] and inverts the restult
                if (!customer.customerData.recipe.content.condiments.Except(recipe.content.condiments).Any()) {
                    customer.NextState();
                    break;
                }
            }
        }
    }
}
