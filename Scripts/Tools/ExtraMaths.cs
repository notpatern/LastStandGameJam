using Godot;
using System;

public static class ExtraMaths
{

	public static float Normalize(float value, float max, float min = 0)
	{
		return (value - min)/(max-min); 
	}
    
    public static Vector2 GetRandomDirection()
    {
        RandomNumberGenerator randy = new RandomNumberGenerator();
        randy.Randomize();
        Random random = new Random((int)randy.Randi());
        Vector2 randDist = new Vector2((float)random.NextDouble(), (float)random.NextDouble());
        return randDist;
    }

}
