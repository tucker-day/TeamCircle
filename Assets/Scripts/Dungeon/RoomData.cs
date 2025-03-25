using System;
using UnityEngine;

public enum Edges
{
    Upper = 0,
    Right = 1,
    Lower = 2,
    Left = 3,
}

public enum EdgeType
{
    Wall,
    Hall,
    Open,
}

public class RoomData
{
    public byte distance = 0;
    public byte distanceSinceBranch = 0;
    private byte _edgeData = 0;

    public EdgeType GetEdgeType(Edges edge)
    {
        // shift to get only the bits that specify the desired edge's type
        byte temp = (byte)(_edgeData >> ((int)edge * 2));
        temp = (byte)(temp & 0b_0000_0011);

        return GetEnumConversion(temp);
    }

    public void SetEdgeType(Edges edge, EdgeType type)
    {
        // get the bits for the new type and shift it into the correct position
        byte newType = GetByteConversion(type);
        newType = (byte)(newType << ((int)edge * 2));

        // create a filter for the edge data to remove old data
        byte filter = 0b00000011;
        filter = (byte)(filter << ((int)edge * 2));
        filter = (byte)~filter;

        // remove the old data, and instert the new data
        _edgeData = (byte)(_edgeData & filter);
        _edgeData = (byte)(_edgeData | newType);
    }

    public int GetNonWallCount()
    {
        int nonWalls = 0;
        foreach (Edges edge in Enum.GetValues(typeof(Edges))) {
            if (GetEdgeType(edge) != EdgeType.Wall) nonWalls++;
        }
        return nonWalls;
    }

    // takes in a byte, and returrns the matching edge type enum
    private static EdgeType GetEnumConversion(byte type)
    {
        // this cannot be a switch statment. trust me, i tried. c# dislikes
        // it when you use functions in your switch cases so I had to use
        // if else statments
        if (type == GetByteConversion(EdgeType.Wall))
        {
            return EdgeType.Wall;
        }
        else if (type == GetByteConversion(EdgeType.Hall))
        {
            return EdgeType.Hall;
        }
        else if (type == GetByteConversion(EdgeType.Open))
        {
            return EdgeType.Open;
        }
        else
        {
            Debug.Log("Invalid Input into GetEnumConversion()!");
            return 0;
        }
    }

    // converts an enum edge type into its byte representation
    private static byte GetByteConversion(EdgeType type)
    {
        switch (type)
        {
            case EdgeType.Wall:
                return 0b_0000_0000;
            case EdgeType.Hall:
                return 0b_0000_0001;
            case EdgeType.Open:
                return 0b_0000_0010;
        }

        Debug.Log("Invalid Input into GetByteConversion()!");
        return 0;
    }

    public static Vector2Int GetEdgeVectorConversion(Edges edge)
    {
        switch (edge)
        {
            case Edges.Upper:
                return Vector2Int.up;
            case Edges.Lower:
                return Vector2Int.down;
            case Edges.Right:
                return Vector2Int.right;
            case Edges.Left:
                return Vector2Int.left;
            default:
                return Vector2Int.zero;
        }
    }
}
