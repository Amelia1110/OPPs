using Unity.VisualScripting;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public int width = 16;
    public int height = 16;
    public int mineCount = 32;


    private Board board;
    private Cell[,] state;
    private bool gameActive;
    private bool firstClick;

    private void Awake()
    {
        board = GetComponentInChildren<Board>();
    }

    private void Start()
    {
        NewGame();
    }

    /* Runs once at the start of each game */
    public void NewGame()
    {
        gameActive = true;
        firstClick = true;
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
    // [SynchronizableMethod]
    public void Update()
    {
        // TODO: For restart
        //if (...) {
        //    NewGame();
        //}

        if (gameActive)
        {
            // Note 0 is left click 1 is right click
            if (Input.GetMouseButtonDown(1)) // Flagging
            {
                ToggleFlag();
            }
            else if (Input.GetMouseButtonDown(0))
            {
                Reveal();
                firstClick = false;
            }
        }

    }

    private void ToggleFlag()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cellPosition = board.tilemap.WorldToCell(worldPosition);
        Cell cell = GetCell(cellPosition.x, cellPosition.y);

        if (cell.type == Cell.Type.Invalid || cell.revealed) return; // Cases where click should do nothing

        cell.flagged = !cell.flagged;
        state[cellPosition.x, cellPosition.y] = cell;
        board.Render(state);
    }

    private void Reveal()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cellPosition = board.tilemap.WorldToCell(worldPosition);
        Cell cell = GetCell(cellPosition.x, cellPosition.y);

        if (cell.type == Cell.Type.Invalid || cell.revealed || cell.flagged) return; // Cases where click should do nothing

        // If a mine is the first thing they click, turn it into an empty cell
        if (firstClick && cell.type == Cell.Type.Mine)
        {
            cell.type = Cell.Type.Empty;

            for (int adjacentx = -1; adjacentx <= 1; adjacentx++)
            {
                for (int adjacenty = -1; adjacenty <= 1; adjacenty++)
                {
                    // Skip center cell (already known)
                    if (adjacentx == 0 && adjacenty == 0) continue;

                    int x = cell.position.x + adjacentx;
                    int y = cell.position.y + adjacenty;

                    // Update count
                    ReduceNumberByOne(x, y);
                }
            }
        }
        
        switch (cell.type)
        {
            case Cell.Type.Mine:
                Explode(cell);
                break;
            case Cell.Type.Empty:
                Flood(cell);
                CheckWinCondition();
                break;
            default:
                cell.revealed = true;
                state[cellPosition.x, cellPosition.y] = cell;
                CheckWinCondition();
                break;
        }

        board.Render(state);
    }

    private void Flood(Cell cell)
    {
        if (cell.revealed) return;
        if (cell.type == Cell.Type.Mine || cell.type == Cell.Type.Invalid) return;

        cell.revealed = true;
        state[cell.position.x, cell.position.y] = cell;

        if (cell.type == Cell.Type.Empty) {
            Flood(GetCell(cell.position.x - 1, cell.position.y));
            Flood(GetCell(cell.position.x + 1, cell.position.y));
            Flood(GetCell(cell.position.x, cell.position.y - 1));
            Flood(GetCell(cell.position.x, cell.position.y + 1));
        }
    }

    private void Explode(Cell cell)
    {
        // TODO: Reroute to a menu
        gameActive = false;

        cell.revealed = true;
        cell.exploded = true;

        state[cell.position.x, cell.position.y] = cell;

        // Reveal all other mines
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                cell = state[x, y];

                if (cell.type == Cell.Type.Mine)
                {
                    cell.revealed = true;
                    state[x, y] = cell;                
                }
            }
        }
    }

    private void CheckWinCondition()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0;y < height; y++)
            {
                Cell cell = state[x, y];

                if (cell.type != Cell.Type.Mine && !cell.revealed) {
                    return;
                }
            }
        }

        Debug.Log("You won");
        gameActive = false;
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

    private void ReduceNumberByOne(int x, int y)
    {
        state[x, y].number--;
        if (state[x, y].number == 0)
        {
            state[x, y].type = Cell.Type.Empty;
        }
    }
}
