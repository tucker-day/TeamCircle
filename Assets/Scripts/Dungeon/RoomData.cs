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
    public byte distance = 0;
    private byte _edgeData = 0;

    public EdgeType GetEdgeType(Edges edge)
    {
        byte temp = (byte)(_edgeData >> (int)edge);
        temp = (byte)(temp & 0b_0000_0011);

        return GetEnumConversion(temp);
    }

    public void SetEdgeType(Edges edge, EdgeType type)
    {
        byte newType = GetByteConversion(type);
        newType = (byte)(newType << (int)edge);

        byte filter = 0b00000011;
        filter = (byte)(filter << (int)edge);
        filter = (byte)~filter;

        _edgeData = (byte)(_edgeData & filter);
        _edgeData = (byte)(_edgeData | newType);
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
            Debug.Log("Invalid Input into GetEnumConversion()!");
            return 0;
        }
    }

    public byte GetByteConversion(EdgeType type)
    {
        switch (type)
        {
            case EdgeType.Wall:
                return 0b_0000_0000;
            case EdgeType.Door:
                return 0b_0000_0001;
            case EdgeType.Breakable:
                return 0b_0000_0010;
            case EdgeType.Open:
                return 0b_0000_0011;
        }

        Debug.Log("Invalid Input into GetByteConversion()!");
        return 0;
    }
}
