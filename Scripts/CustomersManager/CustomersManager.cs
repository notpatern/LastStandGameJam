using Godot;
using Scripts.RecipeScripts;
using System;
using System.Collections.Generic;

namespace Scripts.CustomerScripts
{
    [GlobalClass]
    public partial class CustomersManager : Resource
    {
        Node2D customerSpawnPosition;

        [Export] CustomerManagerData managerData;
        [Export] CustomerScriptableObject[] customerScriptableObjects;
        List<Customer> liveCustomers = new List<Customer>();
        Area2D queueHitBox;

        int totalCustomers = 0;
        int customersServed = 0;

        float timer;

        Godot.RandomNumberGenerator randomNumberGenerator = new Godot.RandomNumberGenerator();

        Action<float> getCustomerRatio;

        public void Start(Node2D node)
        {
            timer = managerData.timeBetweenCustomers;
            GD.Print(managerData.timeBetweenCustomers);
            this.customerSpawnPosition = node;
            randomNumberGenerator.Randomize();
            // queueHitBox.Connect("area_entered", new Callable(this, nameof(CheckIfRecipeCompleted)));

            InstatiateCustomer();
        }

        public void BindCustomerRatio(Action<float> action) {
            getCustomerRatio = action;
        }

        double time = 0;

        public void Update(double delta)
        {
            UpdateCustomers(delta);
            WaitForNextCustomer(delta);
        }

        private void WaitForNextCustomer(double delta) {

            if (time <= timer) {
                time += delta;
                return;
            }

            time = 0;
            InstatiateCustomer();
            RamdomizeSpawnerTime();
        }

        private void RamdomizeSpawnerTime() {
            float timeVatiance = randomNumberGenerator.RandfRange(-managerData.timeVariance, managerData.timeVariance);
            timer = managerData.timeBetweenCustomers + timeVatiance;
        }

        private void InstatiateCustomer()
        {
            int index = randomNumberGenerator.RandiRange(0, customerScriptableObjects.Length - 1);

            CustomerScriptableObject customer = customerScriptableObjects[index];
            Customer customerInstance = new Customer(customer.customerData, customer.gfx, customerSpawnPosition);
            customerInstance.BindAction(CustomerServed);
            liveCustomers.Add(customerInstance);
            totalCustomers++;
        }
        
        private void UpdateCustomers(double delta) {
            if (liveCustomers.Count <= 0) {
                return;
            }

            foreach (Customer customer in liveCustomers)
            {
                customer.Update(delta);
            }
        }

        public void CheckIfRecipeCompleted(Node2D node) {

            Recipe recipe = (Recipe)node.GetScript();

            if (recipe == null) {
                return;
            }

            foreach (Customer customer in liveCustomers) {
                if (!customer.GetRecipe().Equals(recipe.content)) {
                    customer.NextState(); 
                    break;
                }
            }
        }

        private void CustomerServed() {
            customersServed++;
            getCustomerRatio(customersServed / totalCustomers);
        }
    }
}
