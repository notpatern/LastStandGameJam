using Godot;
using Scripts.RecipeScripts;

namespace Scripts.CustomerScripts
{
    [GlobalClass]
    public partial class CustomerData : Resource
    {
        [Export] public Recipe recipe;
        [Export] public float patience;
        [Export] public int money;
        [Export] public float moveSpeed;
    }
}
