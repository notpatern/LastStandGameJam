using Godot;

namespace Scripts.CustomerScripts {
	[GlobalClass]
	public partial class CustomerManagerData : Resource {
		[Export] public float timeBetweenCustomers;
		[Export] public float timeVariance;
		[Export] public float scalingMultiplier;
	}
}