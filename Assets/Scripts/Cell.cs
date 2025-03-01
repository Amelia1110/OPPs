using UnityEngine;

public struct Cell
{
    public enum Type
    {
        Invalid, // Default when creating a new Cell();
        Empty,
        Mine,
        Number,
    }

    public Vector3Int position;
    public Type type;
    public int number;
    public bool revealed;
    public bool flagged;
    public bool exploded;
}
