using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze  {


    static System.Random rnd = new System.Random();
    public int NumberOfRows;
    public int NumberOfColumns;
    public Cell StartCell;
    public Cell ExitCell;
    public List<Cell> Path = new List<Cell>();
    public Cell[,] Cells;
    Stack<Cell> MazeStack = new Stack<Cell>();
    Stack<Cell> PathStack = new Stack<Cell>();

    public Maze(int numberOfRows, int numberOfColumns)
    {
        this.NumberOfColumns = numberOfColumns;
        this.NumberOfRows = numberOfRows;
        Cells = new Cell[numberOfRows, numberOfColumns];
        this.Generate();
    }
    public void Generate()
    {

        for (int actualrow = 0; actualrow < NumberOfRows; actualrow++)
        {

            for (int actualcolumn = 0;  actualcolumn < NumberOfColumns; actualcolumn ++ )
            {
                new Cell(actualrow, actualcolumn,ref Cells);
            }
        }
        VisitCells();
        this.StartCell = ChooseStartCell();
        this.ExitCell = ChooseExitCell();
    }

    private void VisitCells()
    {       
        Cell startCell = Cells[0,0];
        startCell.Visited = true;

        while ((startCell != null))
        {
            startCell = startCell.DeleteCellWalls(MazeStack);

            if (startCell != null)
            {
                startCell.Visited = true;
            }

        }
        MazeStack.Clear();
    }

    public Cell ChooseStartCell()
    {
        return this.StartCell = Cells[rnd.Next(this.NumberOfRows), rnd.Next(this.NumberOfColumns)];

    }
    public Cell ChooseExitCell()
    {
        return this.ExitCell = Cells[ rnd.Next(this.NumberOfRows),rnd.Next(this.NumberOfColumns)];
    }

    private Cell PutOpenCellInStack(Cell actualCell)
    {
        List<Cell> openCells = actualCell.getOpenNeighbors();
        Cell nextCell = null;
        if (openCells.Count > 0)
        {
            int index = rnd.Next(openCells.Count);
            nextCell = openCells[index];
        }

        if ((nextCell != null))
        {
            PathStack.Push(nextCell);
        }
        else if (!(PathStack.Count == 0))
        {
            nextCell = PathStack.Pop();
            if (nextCell.getOpenNeighbors().Count> 0)
            {
                PathStack.Push(nextCell);
            }
        }
        else
        {
            return null;
        }
        return nextCell;
    }

    public void ConstructExitPath()
    {
        PathStack.Clear();
        Cell actualcell = Cells[StartCell.Row, StartCell.Column];
        actualcell.PathCellVisited = true;
        do
        {
            actualcell = PutOpenCellInStack(actualcell);
            if (actualcell != null)
            {
                actualcell.PathCellVisited = true;
            }
            if (actualcell == ExitCell)
            {
                PathStack.Pop();
            }

        } while (actualcell != null && actualcell != ExitCell);
        foreach (Cell cell in PathStack)
        {
            Path.Add(cell);
        }
        PathStack.Clear();
    }








}
