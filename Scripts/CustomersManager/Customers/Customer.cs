using Godot;
namespace Scripts.CustomerScripts
{
    [GlobalClass]
    public partial class Customer : Resource
    {
        [Export] CustomerData customerData;
        [Export] PackedScene gfx;

        public void Start()
        {

        }
    }
}