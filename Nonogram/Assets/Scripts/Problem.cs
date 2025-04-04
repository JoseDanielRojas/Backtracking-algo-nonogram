using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class Problem : MonoBehaviour
{

    string rowsAux, colsAux;
    public short rows;
    public short cols;
    public List<int>[] row_constraints;
    public List<int>[] col_constraints;
    //Default file. MAKE SURE TO CHANGE THIS LOCATION AND FILE PATH TO YOUR FILE   
    List<string> line_numbers, column_numbers;

    public Problem(string filename)
    {
        line_numbers = new List<string>();
        column_numbers = new List<string>();
        int aux = 0, aux2 = 0, aux3 = 0, rowPos = 0, colPos = 0;
        bool flag = false;
        // Read a text file line by line.  
        string[] lines = File.ReadAllLines(filename);

        foreach (string line in lines)
        {
            if (aux == 0)
            {//Toma la primera linea del archivo de texto y toma la cantidad de lineas y de columnas que va a tener
                for (int i = 0; i < line.Length; i++)
                {
                    if (line[i] == ',')
                    {
                        flag = true;
                    }
                    if (Char.IsDigit(line[i]) && !flag)
                    {
                        rowsAux += line[i];
                    }
                    else if (Char.IsDigit(line[i]) && flag)
                    {
                        colsAux += line[i];
                    }
                }

                rows = Convert.ToInt16(Convert.ToString(rowsAux));
                cols = Convert.ToInt16(Convert.ToString(colsAux));
                aux2 = rows;
                aux3 = cols;
                print(cols);
                // Initialize the Arrays
                row_constraints = new List<int>[rows];
                col_constraints = new List<int>[cols];

                // Initialize the Lists
                for (int i = 0; i < rows; i++)
                {
                    row_constraints[i] = new List<int>();
                }
                for (int i = 0; i < cols; i++)
                {
                    col_constraints[i] = new List<int>();
                }

            }
            if (aux != 0 && aux2 != 0 && aux3 > 0 && line != "FILAS")
            {
                column_numbers.Add(line);
                string[] line_num = line.Split(',');
                row_constraints[rowPos] = line_num.OfType<string>().ToList().Select<string, int>(Int32.Parse).ToList();
                rowPos++; ;
                aux2--;
            }
            else if (aux != 0 && aux2 == 0 && aux3 != 0 && line != "COLUMNAS")
            {
                line_numbers.Add(line);
                string[] col_num = line.Split(',');
                col_constraints[colPos] = col_num.OfType<string>().ToList().Select<string, int>(Int32.Parse).ToList();
                colPos++;
                aux3--;
            }
            aux++;
        }
        Reset_Clue();
        Add_Clue();
       

    }

    public Problem(short rows, short cols)
    {
        this.rows = rows;
        this.cols = cols;

        // Initialize the Arrays
        row_constraints = new List<int>[rows];
        col_constraints = new List<int>[cols];

        // Initialize the Lists
        for (int i = 0; i < rows; i++)
        {
            row_constraints[i] = new List<int>();
        }
        for (int i = 0; i < cols; i++)
        {
            col_constraints[i] = new List<int>();
        }
    }

    public void read()
    {
        for (int i = 0; i < row_constraints.Length; i++)
        {
            print("Row = " + i);
            foreach (int j in row_constraints[i])
            {
                print(j);
            }
        }
        for (int m = 0; m < col_constraints.Length; m++)
        {
            print("Col = " + m);
            foreach (int n in col_constraints[m])
            {
                print(n);
            }
        }


    }

    void Reset_Clue()
    { //Restablecimiento de filas y columnas que indican números en filas / columnas antes de la asignación
        for (int i = 0; i < line_numbers.Count; i++)
        {
            GameObject.Find("Line" + (i + 1) + "Numbers").GetComponent<Text>().text = "";
        }
        for (int j = 0; j < column_numbers.Count; j++)
        {
            GameObject.Find("Column" + (j + 1) + "Numbers").GetComponent<Text>().text = "";
        }
    }
    void Add_Clue()
    { //asignación de los números en las filas / columnas de acuerdo con lo que se ha inicializado en el editor en el formulario {<nb1>,...,<nbn>}
        for (int i = 0; i < rows; i++)
        {
            String[] Updated_line_numbers = column_numbers[i].Split(',');

            for (int j = 0; j < Updated_line_numbers.Length; j++)
            {
                GameObject.Find("Line" + (i + 1) + "Numbers").GetComponent<Text>().text += Updated_line_numbers[j].ToString();
                if (j != (Updated_line_numbers.Length - 1))
                    GameObject.Find("Line" + (i + 1) + "Numbers").GetComponent<Text>().text += " ";
            }

        }
        for (int i = 0; i < cols; i++)
        {
            String[] Updated_column_numbers = line_numbers[i].Split(',');
            for (int j = 0; j < Updated_column_numbers.Length; j++)
            {
                GameObject.Find("Column" + (i + 1) + "Numbers").GetComponent<Text>().text += Updated_column_numbers[j].ToString();
                if (j != (Updated_column_numbers.Length - 1))
                    GameObject.Find("Column" + (i + 1) + "Numbers").GetComponent<Text>().text += "\n";
            }
        }
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
