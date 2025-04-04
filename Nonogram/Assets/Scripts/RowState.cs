using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.UI;

class RowState : MonoBehaviour
{
    public List<int> black_blocks;
    public Cell last_read;
    public int missing_cells;

    public RowState(List<int> black_blocks, Cell last_read, int missing_cells)
    {
        this.black_blocks = black_blocks;
        this.last_read = last_read;
        this.missing_cells = missing_cells;
    }

    public Tuple<bool, int> validate(List<int> original_constraint)
    {
        List<int> constraint = new List<int>(original_constraint);
        if (black_blocks.Count > constraint.Count)
        {
            return new Tuple<bool, int>(false, 0);
        }

        // Erase completed constraints
        while (black_blocks.Count > 0)
        {
            if (black_blocks[0] == constraint[0])
            {
                black_blocks.RemoveAt(0);
                constraint.RemoveAt(0);
            }
            else if (black_blocks[0] > constraint[0] || last_read == Cell.White)
            {
                return new Tuple<bool, int>(false, 0);
            }
            else
            {
                break;
            }
        }

        // Default additional black cells
        int additional_black_cells = 0;

        // If a black cell needs to be completed
        if (last_read == Cell.Black && black_blocks.Count > 0)
        {
            int incomplete_black_block = constraint[0];
            int current_black_block = black_blocks[black_blocks.Count - 1];

            additional_black_cells = incomplete_black_block - current_black_block;
            if (additional_black_cells == missing_cells)
            {
                constraint.RemoveAt(0);
                missing_cells = 0;
                if (constraint.Count > 0)
                {
                    return new Tuple<bool, int>(false, 0);
                }
            }
            else if (additional_black_cells < missing_cells)
            {
                constraint.RemoveAt(0);
                missing_cells = missing_cells - additional_black_cells - 1;
            }
            else
            {
                return new Tuple<bool, int>(false, 0);
            }
        }

        if (constraint.Count == 1)
        {
            if (constraint[0] == missing_cells)
            {
                return new Tuple<bool, int>(true, missing_cells);
            }
        }
        return new Tuple<bool, int>(canFitBlackBlocks(constraint, missing_cells), additional_black_cells);

    }

    private bool canFitBlackBlocks(List<int> constraint, int missing_cells)
    {
        return constraint.Sum() + constraint.Count - 1 <= missing_cells;

    }

// Start is called before the first frame update
void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
