using Godot;
using Scripts.RecipeScripts;
using System.Collections.Generic;
using System.Linq;

namespace Scripts.CustomerScripts
{
    public partial class CustomersManager : Resource
    {
        Node node;

        [Export] CustomerScriptableObject[] customerScriptableObjects;
        List<Customer> liveCustomers = new List<Customer>();
        Area2D queueHitBox;

        Godot.RandomNumberGenerator randomNumberGenerator = new Godot.RandomNumberGenerator();

        public void Start(Node node)
        {
            this.node = node;
            randomNumberGenerator.Randomize();
            // queueHitBox.Connect("area_entered", new Callable(this, nameof(CheckIfRecipeConmpleted)));
            InstatiateCustomer();
        }

        public void Update()
        {

        }

        private void InstatiateCustomer()
        {
            int index = randomNumberGenerator.RandiRange(0, customerScriptableObjects.Length - 1);

            CustomerScriptableObject customer = customerScriptableObjects[index];
            liveCustomers.Add(new Customer(customer.customerData, customer.gfx, node));
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
