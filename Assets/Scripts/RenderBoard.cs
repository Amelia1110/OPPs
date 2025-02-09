using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{
    public Tilemap tilemap { get; private set; }

    private float shiftDown = 2;

    // All types of tiles
    public Tile tileUnknown;
    public Tile tileEmpty;
    public Tile tileMine;
    public Tile tileExploded;
    public Tile tileFlag;
    public Tile tileNum1;
    public Tile tileNum2;
    public Tile tileNum3;
    public Tile tileNum4;
    public Tile tileNum5;
    public Tile tileNum6;
    public Tile tileNum7;
    public Tile tileNum8;

    private void Awake()
    {
        tilemap = GetComponent<Tilemap>();
    }

    public void Render(Cell[,] state)
    {
        int width = state.GetLength(0);
        int height = state.GetLength(1);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Cell cell = state[x, y];
                tilemap.SetTile(cell.position, GetTile(cell));
            }
        }

        CenterTilemap();
    }

    // Calculate boundaries of the board (used to center tilemap)
    private BoundsInt CalculateTilemapBounds()
    {
        tilemap.CompressBounds();
        return tilemap.cellBounds;
    }

    public void CenterTilemap()
    {
        BoundsInt bounds = CalculateTilemapBounds();
        Vector3Int size = bounds.size;
        Vector3Int center = Vector3Int.RoundToInt(bounds.center);

        Vector3 offset = new Vector3(
            -center.x * tilemap.cellSize.x,
            -(center.y + shiftDown) * tilemap.cellSize.y,
            0
        );

        tilemap.transform.position = offset;
    }

    public Vector2 GetOffset()
    {
        BoundsInt bounds = CalculateTilemapBounds();
        Vector3Int center = Vector3Int.RoundToInt(bounds.center);

        return new Vector2(
            -center.x * tilemap.cellSize.x,
            -(center.y + shiftDown) * tilemap.cellSize.y
        );
    }

    // Determine the type of tile
    private Tile GetTile(Cell cell)
    {
        if (cell.revealed)
        {
            return GetRevealedTile(cell);
        }
        else if (cell.flagged)
        {
            return tileFlag;
        }
        else
        {
            return tileUnknown;
        }

    }

    // If tile is revealed, determine which cell type to render
    private Tile GetRevealedTile(Cell cell)
    {
        switch (cell.type)
        {
            case Cell.Type.Empty: return tileEmpty;
            case Cell.Type.Mine: return cell.exploded ? tileExploded : tileMine;
            case Cell.Type.Number: return GetNumberTile(cell);
            default: return null;
        }
    }

    // If tile is next to one or more bombs, determine which number to render
    private Tile GetNumberTile(Cell cell)
    {
        switch (cell.number)
        {
            case 1: return tileNum1;
            case 2: return tileNum2;
            case 3: return tileNum3;
            case 4: return tileNum4;
            case 5: return tileNum5;
            case 6: return tileNum6;
            case 7: return tileNum7;
            case 8: return tileNum8;
            default: return null;
        }
    }
}
