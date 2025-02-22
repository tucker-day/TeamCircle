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
    Hall,
    Breakable,
    Open
}

public class RoomData
{
    public byte distance = 0;
    private byte _edgeData = 0;

    public EdgeType GetEdgeType(Edges edge)
    {
        // shift to get only the bits that specify the desired edge's type
        byte temp = (byte)(_edgeData >> (int)edge);
        temp = (byte)(temp & 0b_0000_0011);

        return GetEnumConversion(temp);
    }

    public void SetEdgeType(Edges edge, EdgeType type)
    {
        // get the bits for the new type and shift it into the correct position
        byte newType = GetByteConversion(type);
        newType = (byte)(newType << (int)edge);

        // create a filter for the edge data to remove old data
        byte filter = 0b00000011;
        filter = (byte)(filter << (int)edge);
        filter = (byte)~filter;

        // remove the old data, and instert the new data
        _edgeData = (byte)(_edgeData & filter);
        _edgeData = (byte)(_edgeData | newType);
    }

    // takes in a byte, and returrns the matching edge type enum
    public EdgeType GetEnumConversion(byte type)
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

    // converts an enum edge type into its byte representation
    public byte GetByteConversion(EdgeType type)
    {
        switch (type)
        {
            case EdgeType.Wall:
                return 0b_0000_0000;
            case EdgeType.Hall:
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
