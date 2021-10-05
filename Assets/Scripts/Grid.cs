using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Grid : MonoBehaviour
{
   private int rows = 10;
   private int columns = 10;
   private float titleSize = 1;
   private Vector2 position = new Vector2(0, 14);
   
   private GridSquare[,] squares = new GridSquare[10, 10]; 
   private int[] columnIndexes = {0,1,2,3,4,5,6,7,8,9};
      
   public List<GridSquare> gridSquares = new List<GridSquare>();
   public GameObject gridSquare;

   private void Awake()
   {
      Shape.MouseUp += ManipulateGrid;
   }

   public void Start()
   {
      CreateGrid();
   }
   
   private void CreateGrid()
   {
      int squareIndex = 0;
      
      for (int row = 0; row < rows; row++)
      {
         for (int column = 0; column < columns; column++)
         {
            var square = Instantiate(gridSquare, transform).GetComponent<GridSquare>();
            gridSquares.Add(square);
            squares[row, column] = square;

            square.SquareIndex = squareIndex;
            float posX = column * titleSize;
            float posY = row * -titleSize;

            square.transform.position = new Vector2(posX, posY)+position;
            squareIndex++;
         }
      }
   }

   private void ManipulateGrid()
   {
      var occupied = gridSquares.Where(x => x.Occupied).ToList(); 
      if(occupied.Count==0)
      {
         ShapeStorage.Instance.ResetToDefaultPlace();
         return;
      }
      if (occupied.Any(x => x.Activated))
      {
         ShapeStorage.Instance.ResetToDefaultPlace();
      }
      else
      {
         foreach (var square in occupied)
            square.ActivateSquare();
      
         ShapeStorage.Instance.ShapeDestroy();
         CheckIfAnyLineIsCompleted();
      }
   }

   private void CheckIfAnyLineIsCompleted()
   {
      List<int[]> lines= new List<int[]>();

      foreach (var column in columnIndexes)
      {
         lines.Add(GetVerticalLine(column));
      }

      for (int row = 0; row < 10; row++)
      {
         List<int> data = new List<int>(10);
         for (int index = 0; index < 10; index++)
         {
            data.Add(squares[row,index].SquareIndex);
         }
            
         lines.Add(data.ToArray());
      }
      CheckIfSquaresAreCompleted(lines);
   }

   private void CheckIfSquaresAreCompleted(List<int[]> data)
   {
      List<int[]> completedLines = new List<int[]>();

      foreach (var line in data)
      {
         bool lineCopleted = true;

         foreach (var squareIndex in line)
         {
            var comp = gridSquares[squareIndex];
            if (comp.Activated == false)
            {
               lineCopleted = false;
            }
         }

         if (lineCopleted)
         {
            completedLines.Add(line);
         }
      }

      foreach (var line in completedLines)
      {
         foreach (var squareIndex in line)
         {
            GridSquare comp = gridSquares[squareIndex].GetComponent<GridSquare>();
            comp.Deactivate();
         }
      }
   }
   
   private (int, int) GetSquarePosition(int squareIndex)
   {
      int pos_row = -1;
      int pos_col = -1;

      for (int row = 0; row < 10; row++)
      {
         for (int col = 0; col <10; col++)
         {
            if (squares[row, col].SquareIndex == squareIndex)
            {
               pos_row = row;
               pos_col = col;
            }
         }
      }
      return (pos_row, pos_col);
   }
   
   private int[] GetVerticalLine(int squareIndex)
   {
      int[] line = new int [10];
      int squarePositionColumn = GetSquarePosition(squareIndex).Item2;

      for (int index = 0; index < 10; index++)
      {
         line[index] = squares[index, squarePositionColumn].SquareIndex;
      }

      return line;
   }
}
