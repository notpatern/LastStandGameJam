using Godot;
namespace Scripts.CustomerScripts
{
    [GlobalClass]
    public partial class Customer : Resource
    {
        [Export] public CustomerData customerData;
        [Export] PackedScene gfx;

        CustomerGFX customerGFX;
        Area2D area2D;

        public void Start()
        {
            customerGFX = gfx.Instantiate<CustomerGFX>();
            area2D = customerGFX.area;
        }

        public void NextState() {
            customerGFX.NextState();
        }
    }
}
