using UnityEngine;

public class MathUtilities
{
    public static int ConvertGridToSquare(int x, int y)
    {
        return Mathf.FloorToInt(y / 3) * 3 + Mathf.FloorToInt(x / 3);
    }
    
    public static int ConvertGridToIndexInSquare(int x, int y)
    {
        return ((y % 3) * 3 + x % 3);
    }
}
