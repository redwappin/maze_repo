using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell {


    public int Row;
    public int Column;
    static System.Random Rnd = new System.Random();
    public bool EastWall;
    public bool WestWall;
    public bool NorthWall;
    public bool SouthWall;
    private string NorthCellId;
    private string SouthCellId;
    private string EastCellId;
    private string WestCellId;
    public bool Visited;
    private string Id;
    private Cell[,] Cells;
    public bool PathCellVisited;

    //private bool isInExitPath;

    public Cell(int row, int column, ref Cell[,] cells)
    {
        this.Visited = false;
        this.PathCellVisited = false;
        this.Row = row;
        this.Column = column;
        this.NorthWall = true;
        this.SouthWall = true;
        this.EastWall = true;
        this.WestWall = true;
        this.Id = String.Concat("l", row, "c", column);
        NorthCellId = String.Concat("l", row - 1, "c", column);
        SouthCellId = String.Concat("l", row + 1, "c", column);
        EastCellId = String.Concat("l", row, "c", column + 1);
        WestCellId = String.Concat("l", row, "c", column - 1);
        if (row - 1 < 0) NorthCellId = "bruh";
        if (row + 1 > PlayerPrefs.GetInt("Rows")-1) SouthCellId = "bruh";
        if (column + 1 > PlayerPrefs.GetInt("Rows")-1) EastCellId = "bruh";
        if (column - 1 < 0) WestCellId = "bruh";
        this.Cells = cells;
        Cells[row,column]=this;
    }

        public Cell getNeighbor()
        {

            List<Cell> visitedcells = new List<Cell>();
            if (!(NorthCellId == "bruh") && Cells[Row-1,Column].Visited == false) visitedcells.Add(Cells[Row-1,Column]);
            if (!(SouthCellId == "bruh") && Cells[Row + 1, Column].Visited == false) visitedcells.Add(Cells[Row + 1, Column]);
            if (!(EastCellId == "bruh") && Cells[Row, Column + 1].Visited == false) visitedcells.Add(Cells[Row, Column + 1]);
            if (!(WestCellId == "bruh") && Cells[Row, Column - 1].Visited == false) visitedcells.Add(Cells[Row, Column - 1]);

            Cell currentCell = null;
            if (visitedcells.Count > 0)
            {
                int index = Rnd.Next(visitedcells.Count);
                currentCell = visitedcells[index];
            }
            return currentCell;

        }

        public List<Cell> getOpenNeighbors()
        {
            List<Cell> c = new List<Cell>();
            if (!(this.NorthWall) && !(Cells[Row-1,Column].PathCellVisited))
                c.Add(Cells[Row-1,Column]);

            if (!(this.SouthWall) && !(Cells[Row + 1, Column].PathCellVisited))
                c.Add(Cells[Row+1,Column]);

            if (!(this.EastWall) && !(Cells[Row, Column + 1].PathCellVisited))
                c.Add(Cells[Row,Column+1]);

            if (!(WestWall) && !(Cells[Row, Column - 1].PathCellVisited))
                c.Add(Cells[Row,Column-1]);

            return c;

        }

        public Cell DeleteCellWalls(Stack<Cell> stack)
        {
            Cell nextCell = this.getNeighbor();
            if ((nextCell != null))
            {
                stack.Push(nextCell);
                if (nextCell.Id == this.NorthCellId)
                {
                    this.NorthWall = false;
                    nextCell.SouthWall = false;
                }
                else if (nextCell.Id == this.SouthCellId)
                {
                    this.SouthWall = false;
                    nextCell.NorthWall = false;
                }
                else if (nextCell.Id == this.EastCellId)
                {
                    this.EastWall = false;
                    nextCell.WestWall = false;
                }
                else if (nextCell.Id == this.WestCellId)
                {
                    this.WestWall = false;
                    nextCell.EastWall = false;
                }
            }
            else if (!(stack.Count == 0))
            {
                nextCell = stack.Pop();
            }
            else
            {
                return null;
            }
            return nextCell;
        }


}
