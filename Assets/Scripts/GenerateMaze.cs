
using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GenerateMaze : MonoBehaviour {

    // Use this for initialization
    public Transform Wall;
    public Transform Floor;
    public Transform Path;
    public Transform ExitCell;
    public Transform StartCell;
    
    Maze maze;
    Transform obj;
    float WallWidth;
    private GameObject maze_Game;
    public GameObject DrawPathButton;
    int NumberOfRow;
    void Start () {
        maze_Game = GameObject.Find("MazeGameObject");
        WallWidth = Wall.localScale.y;
        NumberOfRow = PlayerPrefs.GetInt("Rows");
        DrawPathButton.SetActive(false);
    }
	


    public Maze GenerateNewMaze()
    {
        this.maze = new Maze(NumberOfRow, NumberOfRow);
        maze.Generate();
        return maze;
    }
    public void DrawMaze()
    {
        Maze maze = GenerateNewMaze();
            for (int  i =0; i< maze_Game.transform.childCount; i++)
            {
                Destroy(maze_Game.transform.GetChild(i).gameObject);
            }

        obj = Instantiate(Floor,  new Vector3(((maze.NumberOfColumns-1) * WallWidth)/2f , -((maze.NumberOfRows - 1) * WallWidth) / 2f, (WallWidth/2f)- Floor.localScale.x), Quaternion.identity);
        obj.transform.parent = maze_Game.transform;
        obj.name = "Floor";
        obj.transform.localScale = new Vector3(maze.NumberOfColumns * WallWidth, maze.NumberOfRows * WallWidth, 1);

        foreach (Cell cell in maze.Cells)
        {        
            if (cell.NorthWall)
            {

                obj = Instantiate(Wall,  new Vector3((cell.Column * WallWidth), -((cell.Row * WallWidth) - (WallWidth / 2f)), 0), Quaternion.identity);
                obj.name = "NorthWall" + "(" + cell.Row + "," + cell.Column + ")";
                obj.transform.Rotate(0, 0, 90);
                obj.transform.parent = maze_Game.transform;
            }
            if (cell.SouthWall)
            {
                obj = Instantiate(Wall,  new Vector3((cell.Column * WallWidth), -((cell.Row * WallWidth) +(WallWidth / 2f)), 0), Quaternion.identity);
                obj.name = "SouthWall" + "(" + cell.Row + "," + cell.Column + ")";
                obj.transform.Rotate(0, 0, 90);
                obj.transform.parent = maze_Game.transform;

            }
            if (cell.EastWall)
            {
                obj = Instantiate(Wall,  new Vector3((cell.Column * WallWidth) + (WallWidth / 2f),- (cell.Row * WallWidth), 0), Quaternion.identity);
                obj.name = "EastWall" + "(" + cell.Row + "," + cell.Column + ")";
                obj.transform.parent = maze_Game.transform;

            }
            if (cell.WestWall)
            {
                obj = Instantiate(Wall,  new Vector3((cell.Column * WallWidth) - (WallWidth / 2f),- (cell.Row * WallWidth), 0), Quaternion.identity);
                obj.name = "WestWall" + "(" + cell.Row + "," + cell.Column + ")";
                obj.transform.parent = maze_Game.transform;

            }
        }

        obj = Instantiate(StartCell,  new Vector3((maze.StartCell.Column * WallWidth), -(maze.StartCell.Row * WallWidth), 0), Quaternion.identity);
        obj.name = "Start (" + maze.StartCell.Row + "," + maze.StartCell.Column + ")";
        obj.transform.parent = maze_Game.transform;

        obj = Instantiate(ExitCell, new Vector3((maze.ExitCell.Column * WallWidth), -(maze.ExitCell.Row * WallWidth), 0), Quaternion.identity);
        obj.name = "Exit(" + maze.ExitCell.Row + "," + maze.ExitCell.Column + ")";
        obj.transform.parent = maze_Game.transform;
        DrawPathButton.SetActive(true);
    }

    public void DrawPath()
    {
        maze.ConstructExitPath();
        foreach (Cell cell in maze.Path)
        {
            obj = Instantiate(Path, new Vector3((cell.Column* WallWidth), -(cell.Row * WallWidth), 0),Quaternion.identity);
            obj.name = "Path (" + cell.Row + "," + cell.Column + ")";
            obj.transform.parent = maze_Game.transform;
          
        }
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Menu");
    }





}

