﻿

using Godot;
using Scripts.RecipeScripts;
using System.Linq;

namespace Scripts.CustomerScripts
{
    public class Customer {

        Node2D customerSpawnPosition;

        PackedScene gfx;

        public CustomerData customerData;
        CustomerGFX customerGFX;
        Area2D area2D;

        double time;

        public Customer(CustomerData customerData, PackedScene gfx, Node2D node)
        {
            this.customerSpawnPosition = node;

            this.customerData = customerData;
            this.gfx = gfx;

            Start();
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
        }

        public void Update(double delta) {
            if (time >= 0) {
                time -= delta;
                return;
            }

            NextState();
        }

        public void NextState()
        {
            customerGFX.NextState();
        }
    }
}
