using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.IO;
enum Cell
{
    Black, White, Empty
}
public class picross_game : MonoBehaviour
{
    private Problem problem;
    private bool is_backtracking_ready = false;
    private Cell[,] solution = null;
   // private Cell[,] fin = null;

//--------------------------------------------------------------------------------------


    public int [,] Matriz_square_result;
//--------------------------------------------------------------------------------------
    public Texture cross;
    public bool ai_mode_activated;
    public bool verify;
    [Space(10)]
    public int picross_level;
    public int numLines;
    public int numColumns;

    void Start()
    {



    }
    public picross_game(Problem P)
    {
        this.problem = P;
    }
    private bool backtracking(Cell[,] this_board, List<int>[] row_constraints, List<int>[] col_constraints, int row, int col)
    {

        if (row == problem.rows)
        {
            this.solution = this_board;
            return true;
        }

        if (col == problem.cols)
        {
            return backtracking(this_board, row_constraints, col_constraints, row + 1, 0);
        }
        // Cell is already Set 
        if (this_board[row, col] != Cell.Empty)
        {
            return backtracking(this_board, row_constraints, col_constraints, row, col + 1);
        }

        // Results from checking boards
        bool is_consistent;
        Cell[,] new_board;

        // Save Board State
        Cell[,] board_black = copyBoard(this_board, problem.rows, problem.cols);
        Cell[,] board_white = copyBoard(this_board, problem.rows, problem.cols);

        // Trying with Black
        board_black[row, col] = Cell.Black;


        (is_consistent, new_board) = checkBoard(board_black, row_constraints, col_constraints, row, col);
        if (is_consistent)
        {
            board_black = new_board;
            if (backtracking(new_board, row_constraints, col_constraints, row, col + 1))
            {
                return true;
                


            }
        }
      
        // Trying with White
        board_white[row, col] = Cell.White;
        (is_consistent, new_board) = checkBoard(board_white, row_constraints, col_constraints, row, col);
        if (is_consistent)
        {
            return backtracking(new_board, row_constraints, col_constraints, row, col + 1);
        }

        // None of the two colors worked
        return false;
    }

    public void solve()
    {

        Cell[,] new_board = newBoard(problem.rows, problem.cols);
        bool result = backtracking(new_board, problem.row_constraints, problem.col_constraints, 0, 0);

    }

    public void showSolution()
    {
        if (!this.is_backtracking_ready)
        {
            this.solve();
            print("si");
          //  showSolution();

        }
        if (this.solution != null)
        {

            print("aqui");
            this.showBoard();
        }
        else
        {
            Console.WriteLine("CANNOT BE SOLVED");
        }
    }



    public void showBoard()
    {
        Console.WriteLine("BOARD:");
        for (int i = 0; i < problem.rows; i++)
        {
            for (int j = 0; j < problem.cols; j++)
            {
                Cell this_cell = solution[i, j];
               
                switch (this_cell)
                {
                    case Cell.Black:
                        
                        print(i + "l" + j + "c");
                        GameObject.Find("Square" + (i+1) + "_" + (j+1)).GetComponent<RawImage>().color = new Color32(0, 0, 0, 255);//pinta los cuadros negros
                        break;
                    case Cell.White:
                        GameObject.Find("Square" + (i+1) + "_" + (j+1)).GetComponent<RawImage>().color = new Color32(255, 255, 255, 255);
                        break;
                    default:
                        GameObject.Find("Square" + (i + 1) + "_" + (j + 1)).GetComponent<RawImage>().color = new Color32(255, 255, 255, 255);
                        break;
                }


            }
 
        }
    }

    // Board Functions
    private Cell[,] copyBoard(Cell[,] board, short rows, short columns)
    {
        Cell[,] new_board = new Cell[rows, columns];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                new_board[i, j] = board[i, j];
            }
        }
        return new_board;
    }

    private Cell[,] newBoard(short rows, short columns)
    {
        Cell[,] new_board = new Cell[rows, columns];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                new_board[i, j] = Cell.Empty;
            }
        }
        return new_board;
    }

    private Cell[,] transposeBoard(Cell[,] board, int rows, int columns)
    {
        Cell[,] transposed_board = new Cell[columns, rows];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                transposed_board[j, i] = board[i, j];
            }
        }
        return transposed_board;
    }

    // Check Board
    private Tuple<bool, Cell[,]> checkBoard(Cell[,] this_board, List<int>[] row_constraints, List<int>[] col_constraints,
        int pending_row, int pending_col)
    {

        // Set of Pending Rows
        HashSet<int> pending_rows = new HashSet<int>();
        pending_rows.Add(pending_row);

        // Set of Pending Columns
        HashSet<int> pending_cols = new HashSet<int>();
        pending_cols.Add(pending_col);

        // State variables
        bool checking_columns = false;
        HashSet<int> pending = pending_rows;
        HashSet<int> next_pending = pending_cols;
        List<int>[] constraints = row_constraints;

        // Start checking
        while (pending_rows.Count > 0 || pending_cols.Count > 0)
        {
            // Set row length (for rows)
            int row_length = problem.cols;

            // Tranpose the first time if necessary
            if (checking_columns)
            {
                this_board = transposeBoard(this_board, problem.rows, problem.cols);
                // Change row length (for columns)
                row_length = problem.rows;

            }

            pending = checking_columns ? pending_cols : pending_rows;
            next_pending = checking_columns ? pending_rows : pending_cols;
            constraints = checking_columns ? col_constraints : row_constraints;

            foreach (int aux_row in pending)
            {

                (bool consistent, int additional_black_cells) = checkRow(this_board, aux_row, row_length, constraints[aux_row]);


                if (!consistent)
                {
                    return new Tuple<bool, Cell[,]>(false, null);
                }


                // Add Additional Black Cells
                for (int i = 0; i < row_length; i++)
                {
                    // No more black cells to be added
                    if (additional_black_cells == 0)
                    {
                        break;
                    }

                    // If cell is empty, add the black cell
                    if (this_board[aux_row, i] == Cell.Empty)
                    {
                        // Set to Black
                        this_board[aux_row, i] = Cell.Black;
                        additional_black_cells--;

                        next_pending.Add(i);
                    }


                }

            }

            pending.Clear();

            // Transpose the second time if necessary
            if (checking_columns)
            {
                this_board = transposeBoard(this_board, problem.cols, problem.rows);
            }

            // Change from rows -> cols or cols -> rows
            checking_columns = !checking_columns;
        }

        // No contradiction was found
        /*
        Console.WriteLine("AFTER CHECKING BOARD: vvvvvvv");
        showBoard(this_board);
        Console.WriteLine("AFTER CHECKING BOARD: ^^^^^^^");
        */
        return new Tuple<bool, Cell[,]>(true, this_board);
    }

    // Check Row
    private RowState getCurrentState(Cell[,] board, int row, int row_length)
    {
        List<int> black_blocks = new List<int>();
        bool reading_black = false;
        int counter = 0;
        Cell last_read = Cell.White;
        int inspected_cells = 0;
        for (int i = 0; i < row_length; i++)
        {
            Cell cell = board[row, i];
            if (cell == Cell.Empty)
            {
                break;
            }
            else if (cell == Cell.White && reading_black)
            {
                black_blocks.Add(counter);
                counter = 0;
            }
            else if (cell == Cell.Black)
            {
                counter++;
            }
            last_read = cell;
            inspected_cells = inspected_cells + 1;
            reading_black = cell == Cell.Black;
        }
        if (reading_black)
        {
            black_blocks.Add(counter);
        }

        int missing_cells = row_length - inspected_cells;
        return new RowState(black_blocks, last_read, missing_cells);
    }

    private Tuple<bool, int> checkRow(Cell[,] board, int row, int row_length, List<int> constraint)
    {
        // Validate that empty cells are only at the end
        bool has_found_first_empty = false;
        for (int i = 0; i < row_length; i++)
        {
            Cell this_cell = board[row, i];
            if (has_found_first_empty && this_cell != Cell.Empty)
            {
                return new Tuple<bool, int>(true, 0);
            }
            has_found_first_empty = has_found_first_empty || this_cell == Cell.Empty;
        }

        // Otherwise, return True
        RowState current_state = getCurrentState(board, row, row_length);
        return current_state.validate(constraint);
    }
   


 
 


 

  
         
    void Update()
    {


    }
}
