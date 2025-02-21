using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Edges
{
    Upper = 0,
    Right = 2,
    Lower = 4,
    Left = 6
}

public enum EdgeType
{
    Wall,
    Door,
    Breakable,
    Open
}

public class RoomData
{
    private byte _edgeData;

    public EdgeType GetEdgeType(Edges edge)
    {
        return GetEnumConversion(_edgeData);
    }

    public void SetEdgeType(Edges edge, EdgeType type)
    {

    }

    public EdgeType GetEnumConversion(byte type)
    {
        // this cannot be a switch statment. trust me, i tried.
        if (type == GetByteConversion(EdgeType.Wall))
        {
            return EdgeType.Wall;
        }
        else if (type == GetByteConversion(EdgeType.Door))
        {
            return EdgeType.Door;
        }
        else if (type == GetByteConversion(EdgeType.Open))
        {
            return EdgeType.Open;
        }
        else if (type == GetByteConversion(EdgeType.Breakable))
        {
            return EdgeType.Breakable;
        }
        else
        {
            return 0;
        }
    }

    public byte GetByteConversion(EdgeType type)
    {
        switch (type)
        {
            case EdgeType.Wall:
                return 0b0000000;
            case EdgeType.Door:
                return 0b0000001;
            case EdgeType.Breakable:
                return 0b0000010;
            case EdgeType.Open:
                return 0b0000011;
        }

        return 0;
    }
}
