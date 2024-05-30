

using Godot;
using Scripts.RecipeScripts;
using System;
using System.Runtime.CompilerServices;

namespace Scripts.CustomerScripts
{
    public class Customer {

        Node2D customerSpawnPosition;

        PackedScene gfx;

        public CustomerData customerData;
        CustomerGFX customerGFX;
        Area2D area2D;

        double time;

        Action customerServed;

        public Customer(CustomerData customerData, PackedScene gfx, Node2D node)
        {
            this.customerSpawnPosition = node;

            this.customerData = customerData;
            this.gfx = gfx;

            Start();
        }

        public void BindAction(Action action) {
            customerServed = action;
        }

        public RecipeStruct GetRecipe() {
            return customerData.recipe.content;
        }

        public void Start()
        {
            customerGFX = gfx.Instantiate<CustomerGFX>();
            customerSpawnPosition.AddChild(customerGFX);
            area2D = customerGFX.area;
            customerGFX.movementSpeed = customerData.moveSpeed;
            time = customerData.patience;

            area2D.Connect("area_entered", new Callable(area2D, nameof(CheckIfRecipeCompleted)));

            GD.Print($"customer instantiated {customerGFX}");

            customerGFX.SetRecipeText("Alcohol\r\nCondiment\r\nSoft");
        }

        private void CheckIfRecipeCompleted(Node2D node) {
            Recipe recipe = (Recipe)node.GetScript();

            if (recipe == null) {
                return;
            }

            if (!recipe.content.Equals(this.GetRecipe())) {
                return;
            }

            NextState();
            customerServed.Invoke();
            CurrencyManager.playerGetMoney(customerData.money);
        }

        public void Update(double delta) {
            if (time >= 0 && customerGFX.stateIndex > 0) {
                time -= delta;
            }
            else if (time < 0 && customerGFX.stateIndex > 0) {
                NextState();
            }
        }

        public void NextState()
        {
            customerGFX.NextState();
        }
    }
}
