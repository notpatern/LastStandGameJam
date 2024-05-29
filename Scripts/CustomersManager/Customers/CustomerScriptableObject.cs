using Godot;
namespace Scripts.CustomerScripts
{
    [GlobalClass]
    public partial class CustomerScriptableObject : Resource
    {
        [Export] public CustomerData customerData;
        [Export] public PackedScene gfx;
    }
}
