using Godot;
using Scripts.RecipeScripts;

namespace Scripts.CustomerScripts
{
    [GlobalClass]
    public partial class CustomerData : Resource
    {
        public Recipe recipe;
        public float patience;
        public float money;
    }
}
