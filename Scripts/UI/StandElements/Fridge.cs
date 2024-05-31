using Godot;
using System;
using System.Collections.Generic;


namespace Scripts.UI.StandElements {

	public partial class Fridge : Node2D {

        [Export] private CollisionShape2D[] fridgeZones;
        [Export] private PackedScene[] elementsInFridge;

        string spawnInputName;

        public override void _Process(double delta) {
            if (Input.IsActionJustPressed(spawnInputName)) {

            }
        }
    }
}