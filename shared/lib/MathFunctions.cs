using System;

public static class MathFunctions
{
    /**
    <summary>
    Returns a random number between min and max
    </summary>
    */
    public static float RandomRange(float min, float max)
    {
        var random = new Random();
        var randomDouble = random.NextDouble();
        var randomValue = min + max * randomDouble;
        return (float)randomValue;
    }
}