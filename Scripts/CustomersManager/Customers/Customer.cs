

using Godot;

namespace Scripts.CustomerScripts
{
    public class Customer {

        Node node;

        PackedScene gfx;

        public CustomerData customerData;
        CustomerGFX customerGFX;
        Area2D area2D;

        public Customer(CustomerData customerData, PackedScene gfx, Node node)
        {
            this.node = node;

            this.customerData = customerData;
            this.gfx = gfx;

            Start();
        }

        public void Start()
        {
            customerGFX = gfx.Instantiate<CustomerGFX>();
            node.AddChild(customerGFX);
            area2D = customerGFX.area;
            customerGFX.movementSpeed = customerData.moveSpeed;

            GD.Print($"customer instantiated {customerGFX}");
        }

        public void NextState()
        {
            customerGFX.NextState();
        }
        
    }
}
