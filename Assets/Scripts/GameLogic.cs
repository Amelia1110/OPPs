using Unity.VisualScripting;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public int width = 16;
    public int height = 16;
    private int mineCount = 32;


    private Board board;
    private Cell[,] state;

    private void Awake()
    {
        board = GetComponentInChildren<Board>();
    }

    private void Start()
    {
        NewGame();
    }



    /* Runs once at the start of each game */
    private void NewGame()
    {
        state = new Cell[width, height];

        GenerateCells();
        GenerateMines();
        GenerateNumbers();

        board.Render(state);
    }

    // Initialize board with num of cells
    private void GenerateCells()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Cell cell = new Cell();
                cell.position = new Vector3Int(x, y, 0);
                cell.type = Cell.Type.Empty;
                state[x, y] = cell;
            }   
        }
    }

    // Populate board with randomly placed mines
    private void GenerateMines()
    {
        for (int i = 0; i < mineCount; i++)
        {
            int x = Random.Range(0, width);
            int y = Random.Range(0, height);

            // Sets cell to be a mine if there is not already a mine there
            if (state[x, y].type != Cell.Type.Mine)
            {
                state[x, y].type = Cell.Type.Mine;
            }
            else
            {
                i--; // Retrys otherwise
            }
        } 
    }

    // Populate board with numbers based off the mines
    private void GenerateNumbers()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Cell cell = state[x, y];

                if (cell.type == Cell.Type.Mine)
                {
                    continue;
                }

                cell.number = CountMines(x, y);

                if (cell.number > 0)
                {
                    cell.type = Cell.Type.Number;
                }

                state[x, y] = cell; // Reassign cell since we've made changes
            }
        }
    }

    // Counts the number of surrounding mines for a singular mine
    private int CountMines(int cellx, int celly)
    {
        int count = 0;

        for (int adjacentx = -1; adjacentx <= 1; adjacentx++)
        {
            for (int adjacenty = -1; adjacenty <= 1; adjacenty++)
            {
                // Skip center cell (already known)
                if (adjacentx == 0 && adjacenty == 0) continue; 

                int x = cellx + adjacentx;
                int y = celly + adjacenty;

                // Update count
                if (GetCell(x, y).type == Cell.Type.Mine) count++;
            }
        }

        return count;
    }



    /* Runs every frame */
    private void Update()
    {
        // Note 0 is left click 1 is right click
        if (Input.GetMouseButtonDown(1)) // Flagging
        {
            ToggleFlag();
        }
    }

    private void ToggleFlag()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cellPosition = board.tilemap.WorldToCell(worldPosition);
        Cell cell = GetCell(cellPosition.x, cellPosition.y);

        if (cell.type == Cell.Type.Invalid || cell.revealed) return;

        cell.flagged = !cell.flagged;
        state[cellPosition.x, cellPosition.y] = cell;
        board.Render(state);
    }

    // Checks if user clicked a cell and returns the position of the cell within the 2D game state array
    private Cell GetCell(int x, int y)
    {
        if (IsValid(x, y))
        {
            return state[x, y];
        }
        else
        {
            return new Cell();
        }
    }

    // Checks for all things within game board
    private bool IsValid(int x, int y)
    {
        return x >= 0 && x < width && y >= 0 && y < height;
    }
}
