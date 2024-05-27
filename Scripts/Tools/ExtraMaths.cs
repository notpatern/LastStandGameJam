using Godot;
using System;

public static class ExtraMaths
{

	public static float Normalize(float value, float max, float min = 0)
	{
		return (value - min)/(max-min); 
	}



}
