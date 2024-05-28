

using Godot;

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

        public void Start()
        {
            customerGFX = gfx.Instantiate<CustomerGFX>();
            customerSpawnPosition.AddChild(customerGFX);
            area2D = customerGFX.area;
            customerGFX.movementSpeed = customerData.moveSpeed;
            time = customerData.patience;

            GD.Print($"customer instantiated {customerGFX}");
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
