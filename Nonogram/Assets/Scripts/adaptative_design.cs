using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
public class adaptative_design : MonoBehaviour {


    [Header("Prefabs for display construction")]
    public GameObject black_line;
    public GameObject black_column;
    public GameObject line_numbers;
    public GameObject column_numbers;
    public GameObject square;

    [Header("Parent for prefab (up)")]
    public GameObject line_column_parent;
    public GameObject square_parent;

    private int nb_line;
    private int nb_column;
    string [] dataArray;
    string lins, columnss, myFilePath, fileName;
       // List<string> line_numbers, column_numbers;
  public int numLines;
   public int numColumns;
	// Use this for initialization
	void Awake () {
        

        fileName = "test.txt";
        myFilePath = Application.dataPath + "/" + fileName;



        dataArray = File.ReadAllLines(myFilePath);
        int aux = 0, aux2 = 0, aux3 = 0;
        int numLines = 0, numColumns = 0;
        bool marker = false;
        foreach (string line in dataArray)
        {
            if (aux == 0)
            {//Toma la primera linea del archivo de texto y toma la cantidad de lineas y de columnas que va a tener
                for (int i = 0; i < line.Length; i++)
                {
                    if (line[i] == ',')
                    {
                        marker = true;
                    }
                    if (Char.IsDigit(line[i]) && !marker)
                    {
                        lins += line[i];
                    }
                    else if (Char.IsDigit(line[i]) && marker)
                    {
                        columnss += line[i];
                    }
                }
                numLines = Convert.ToInt32(Convert.ToString(lins));
                //print("****");
                //print(numLines);
                numColumns = Convert.ToInt32(Convert.ToString(columnss));
                //print(numColumns);


            }
            if (aux != 0 && aux2 != 0 && aux3 > 0 && line != "FILAS")
            {
                //line_numbers.Add(line);
                aux2--;
            }
            else if (aux != 0 && aux2 == 0 && aux3 != 0 && line != "COLUMNAS")
            {
                //column_numbers.Add(line);

                aux3--;
            }
            aux++;
            //print(numLines); 



        }


        /******************************************************************************************
        * CONFIGURACIÓN DEL TABLERO DE JUEGO SEGÚN EL NÚMERO DE LÍNEAS / COLUMNAS *
         *                                                                                        *
         *                                                                                        */

        /******************************************************
            * LÍNEAS Y COLUMNAS + ANTECEDENTES *
         *                                                    *
         *                                                    */



        nb_line = numLines;//GameObject.FindWithTag("Picross").GetComponent<picross_game>().line_numbers.Count;
        nb_column =numColumns ;// GameObject.FindWithTag("Picross").GetComponent<picross_game>().column_numbers.Count;

       

        for (int i = 1; i < nb_line; i++)
        {
            GameObject line = Instantiate(black_line, line_column_parent.transform);
            line.name = "Line" + i;
            line.GetComponent<RectTransform>().anchoredPosition = new Vector2(150 - i * (462.5f / nb_line), 0);
        }

        for (int i = 1; i < nb_column; i++)
        {
            GameObject column = Instantiate(black_column, line_column_parent.transform);
            column.name = "Column" + i;
            column.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 150 - i * (462.5f / nb_column));
        }

        GameObject[] lines = GameObject.FindGameObjectsWithTag("Line");
        GameObject[] columns = GameObject.FindGameObjectsWithTag("Column");

        if (nb_line<nb_column)
        {
            float size = GameObject.Find("Border_Horizontal_Line").GetComponent<RectTransform>().anchoredPosition.x
                - GameObject.Find("Border_Bot_Line").GetComponent<RectTransform>().anchoredPosition.x;

            float test = ((size * ((float)nb_line / (float)nb_column)));

            GameObject.Find("ContentBackground").GetComponent<RectTransform>().offsetMin = new Vector2(20.5f, 590f- (test / size)*(590f-278.5f));
            GameObject.Find("ContentBackground").GetComponent<RectTransform>().offsetMax = new Vector2(-20.5f, -590f + (test / size) * (590f - 278.5f));
            float diff = GameObject.Find("ContentBackground").GetComponent<RectTransform>().offsetMin.y - 278.5f;
            GameObject.Find("ContentBackground").GetComponent<RectTransform>().offsetMin = new Vector2(20.5f, GameObject.Find("ContentBackground").GetComponent<RectTransform>().offsetMin.y+diff);
            GameObject.Find("ContentBackground").GetComponent<RectTransform>().offsetMax = new Vector2(-20.5f, -278.5f);

            GameObject.Find("Border_Top_Line").GetComponent<RectTransform>().anchoredPosition =
                new Vector2(
                    GameObject.Find("Border_Top_Line").GetComponent<RectTransform>().anchoredPosition.x*(test/size),
                    GameObject.Find("Border_Top_Line").GetComponent<RectTransform>().anchoredPosition.y
                    );
            GameObject.Find("Border_Bot_Line").GetComponent<RectTransform>().anchoredPosition =
                new Vector2(
                    GameObject.Find("Border_Bot_Line").GetComponent<RectTransform>().anchoredPosition.x * (test / size),
                    GameObject.Find("Border_Bot_Line").GetComponent<RectTransform>().anchoredPosition.y
                    );
            GameObject.Find("Border_Horizontal_Line").GetComponent<RectTransform>().anchoredPosition =
                new Vector2(
                    GameObject.Find("Border_Horizontal_Line").GetComponent<RectTransform>().anchoredPosition.x * (test /size),
                    GameObject.Find("Border_Horizontal_Line").GetComponent<RectTransform>().anchoredPosition.y
                    );


            GameObject.Find("Border_Left_Line").GetComponent<RectTransform>().sizeDelta =
                new Vector2(
                    GameObject.Find("Border_Left_Line").GetComponent<RectTransform>().sizeDelta.x * (test / size),
                    GameObject.Find("Border_Left_Line").GetComponent<RectTransform>().sizeDelta.y
                    );
            GameObject.Find("Border_Right_Line").GetComponent<RectTransform>().sizeDelta =
                new Vector2(
                    GameObject.Find("Border_Right_Line").GetComponent<RectTransform>().sizeDelta.x * (test / size),
                    GameObject.Find("Border_Right_Line").GetComponent<RectTransform>().sizeDelta.y
                    );
            GameObject.Find("Border_Vertical_Line").GetComponent<RectTransform>().sizeDelta =
                new Vector2(
                    GameObject.Find("Border_Vertical_Line").GetComponent<RectTransform>().sizeDelta.x * (test / size),
                    GameObject.Find("Border_Vertical_Line").GetComponent<RectTransform>().sizeDelta.y
                    );

            int i = 1;
            foreach (GameObject line in lines)
            {
                line.GetComponent<RectTransform>().anchoredPosition =
                new Vector2(
                    GameObject.Find("Border_Horizontal_Line").GetComponent<RectTransform>().anchoredPosition.x +
                    ((GameObject.Find("Border_Bot_Line").GetComponent<RectTransform>().anchoredPosition.x
                    - GameObject.Find("Border_Horizontal_Line").GetComponent<RectTransform>().anchoredPosition.x)*i/nb_line),
                    line.GetComponent<RectTransform>().anchoredPosition.y
                    );
                i++;
            }

            foreach (GameObject column in columns)
            {
                column.GetComponent<RectTransform>().sizeDelta =
                    new Vector2(
                        column.GetComponent<RectTransform>().sizeDelta.x * (test / size),
                        column.GetComponent<RectTransform>().sizeDelta.y
                        );
            }

            GameObject.Find("Current_Completion_Display").GetComponent<RectTransform>().offsetMin = new Vector2(10f,-153f* (test / size)+5f* (test / size));
            GameObject.Find("Current_Completion_Display").GetComponent<RectTransform>().offsetMax = new Vector2(-472f, -10f);


        }
        else if (nb_line > nb_column)
        {
            float size = GameObject.Find("Border_Vertical_Line").GetComponent<RectTransform>().anchoredPosition.y
                - GameObject.Find("Border_Right_Line").GetComponent<RectTransform>().anchoredPosition.y;

            float test = ((size * ((float)nb_line / (float)nb_column)));

            GameObject.Find("ContentBackground").GetComponent<RectTransform>().offsetMin = new Vector2(20.5f, 590f - (test / size) * (590f - 278.5f));
            GameObject.Find("ContentBackground").GetComponent<RectTransform>().offsetMax = new Vector2(-20.5f, -590f + (test / size) * (590f - 278.5f));
            float diff = GameObject.Find("ContentBackground").GetComponent<RectTransform>().offsetMin.y - 278.5f;
            GameObject.Find("ContentBackground").GetComponent<RectTransform>().offsetMin = new Vector2(20.5f, GameObject.Find("ContentBackground").GetComponent<RectTransform>().offsetMin.y + diff);
            GameObject.Find("ContentBackground").GetComponent<RectTransform>().offsetMax = new Vector2(-20.5f, -278.5f);

            GameObject.Find("Border_Top_Line").GetComponent<RectTransform>().anchoredPosition =
                new Vector2(
                    GameObject.Find("Border_Top_Line").GetComponent<RectTransform>().anchoredPosition.x * (test / size),
                    GameObject.Find("Border_Top_Line").GetComponent<RectTransform>().anchoredPosition.y
                    );
            GameObject.Find("Border_Bot_Line").GetComponent<RectTransform>().anchoredPosition =
                new Vector2(
                    GameObject.Find("Border_Bot_Line").GetComponent<RectTransform>().anchoredPosition.x * (test / size)+5,
                    GameObject.Find("Border_Bot_Line").GetComponent<RectTransform>().anchoredPosition.y
                    );
            GameObject.Find("Border_Horizontal_Line").GetComponent<RectTransform>().anchoredPosition =
                new Vector2(
                    GameObject.Find("Border_Horizontal_Line").GetComponent<RectTransform>().anchoredPosition.x * (test / size),
                    GameObject.Find("Border_Horizontal_Line").GetComponent<RectTransform>().anchoredPosition.y
                    );


            GameObject.Find("Border_Left_Line").GetComponent<RectTransform>().anchoredPosition =
                new Vector2(
                    (GameObject.Find("Border_Top_Line").GetComponent<RectTransform>().anchoredPosition.x + GameObject.Find("Border_Bot_Line").GetComponent<RectTransform>().anchoredPosition.x)/2,
                    GameObject.Find("Border_Left_Line").GetComponent<RectTransform>().anchoredPosition.y
                    );
            GameObject.Find("Border_Vertical_Line").GetComponent<RectTransform>().anchoredPosition =
                new Vector2(
                    (GameObject.Find("Border_Top_Line").GetComponent<RectTransform>().anchoredPosition.x + GameObject.Find("Border_Bot_Line").GetComponent<RectTransform>().anchoredPosition.x) / 2,
                    GameObject.Find("Border_Vertical_Line").GetComponent<RectTransform>().anchoredPosition.y
                    );
            GameObject.Find("Border_Right_Line").GetComponent<RectTransform>().anchoredPosition =
                new Vector2(
                    (GameObject.Find("Border_Top_Line").GetComponent<RectTransform>().anchoredPosition.x + GameObject.Find("Border_Bot_Line").GetComponent<RectTransform>().anchoredPosition.x) / 2,
                    GameObject.Find("Border_Right_Line").GetComponent<RectTransform>().anchoredPosition.y
                    );

            GameObject.Find("Border_Left_Line").GetComponent<RectTransform>().sizeDelta =
                new Vector2(
                    GameObject.Find("Border_Left_Line").GetComponent<RectTransform>().sizeDelta.x * (test / size) - 15,
                    GameObject.Find("Border_Left_Line").GetComponent<RectTransform>().sizeDelta.y
                    );
            GameObject.Find("Border_Right_Line").GetComponent<RectTransform>().sizeDelta =
                new Vector2(
                    GameObject.Find("Border_Right_Line").GetComponent<RectTransform>().sizeDelta.x * (test / size) - 15,
                    GameObject.Find("Border_Right_Line").GetComponent<RectTransform>().sizeDelta.y
                    );
            GameObject.Find("Border_Vertical_Line").GetComponent<RectTransform>().sizeDelta =
                new Vector2(
                    GameObject.Find("Border_Vertical_Line").GetComponent<RectTransform>().sizeDelta.x * (test / size) - 15,
                    GameObject.Find("Border_Vertical_Line").GetComponent<RectTransform>().sizeDelta.y
                    );

            if (GameObject.Find("Border_Left_Line").GetComponent<RectTransform>().sizeDelta.x>= ((float)Screen.height / (float)Screen.width)*560f)
            {
                Debug.Log(Screen.height);
                Debug.Log(GameObject.Find("Border_Left_Line").GetComponent<RectTransform>().sizeDelta.x);
                float ratio = (GameObject.Find("Border_Left_Line").GetComponent<RectTransform>().sizeDelta.x/1000) * ((float)Screen.width / (float)Screen.height);
                Debug.Log(ratio);

                GameObject.Find("Line_N_Column").GetComponent<RectTransform>().localScale = new Vector3(1f/ (ratio* ((float)Screen.height / (float)Screen.width)), 1f / (ratio * ((float)Screen.height / (float)Screen.width)), 1);
                if((float)Screen.width > (float)Screen.height)
                    GameObject.Find("Line_N_Column").GetComponent<RectTransform>().localScale = new Vector3(1f / (1.75f*ratio * ((float)Screen.height / (float)Screen.width)), 1f / (1.75f * ratio * ((float)Screen.height / (float)Screen.width)), 1);
            }

            int i = 1;
            foreach (GameObject line in lines)
            {
                line.GetComponent<RectTransform>().anchoredPosition =
                new Vector2(
                    GameObject.Find("Border_Horizontal_Line").GetComponent<RectTransform>().anchoredPosition.x +
                    ((GameObject.Find("Border_Bot_Line").GetComponent<RectTransform>().anchoredPosition.x
                    - GameObject.Find("Border_Horizontal_Line").GetComponent<RectTransform>().anchoredPosition.x) * i / nb_line),
                    line.GetComponent<RectTransform>().anchoredPosition.y
                    );
                i++;
            }

            foreach (GameObject column in columns)
            {
                column.GetComponent<RectTransform>().sizeDelta =
                    new Vector2(
                        column.GetComponent<RectTransform>().sizeDelta.x * (test / size),
                        column.GetComponent<RectTransform>().sizeDelta.y
                        );
            }

            GameObject.Find("Current_Completion_Display").GetComponent<RectTransform>().offsetMin = new Vector2(10f, -153f * (test / size) - 5f * (test / size));
            GameObject.Find("Current_Completion_Display").GetComponent<RectTransform>().offsetMax = new Vector2(-472f, -10f);


        }
        /*                                                   *
         *                                                   *
         *          LINES AND COLUMNS + BACKGROUND         *
         * ***************************************************/



        /******************************************************
         *      CLICKABLE SQUARE + COMPLETION TABLE     *
         *                                                    *
         *                                                    */


        for (int i=1;i<= nb_line; i++)
        {
            for(int j=1;j<= nb_column; j++)
            {
                float dist = GameObject.Find("Line1").GetComponent<RectTransform>().anchoredPosition.x - GameObject.Find("Line2").GetComponent<RectTransform>().anchoredPosition.x;
                GameObject line = Instantiate(square, GameObject.Find("Line_N_Column").transform);
                line.name = "Square" + i+"_"+j;
                line.GetComponent<RectTransform>().anchoredPosition =
                    new Vector2(
                        GameObject.Find("Border_Horizontal_Line").GetComponent<RectTransform>().anchoredPosition.x - ((dist * i) - (dist / 2)),
                        GameObject.Find("Border_Vertical_Line").GetComponent<RectTransform>().anchoredPosition.y - ((dist * j) - (dist / 2))
                    );

                line.GetComponent<RectTransform>().sizeDelta =
                    new Vector2(
                        dist,
                        dist
                    );
                line.transform.SetParent(square_parent.transform);
            }
        }



        /*                                                   *
         *                                                   *
         *      CLICKABLE SQUARE + COMPLETION TABLE    *
         * ***************************************************/




        /******************************************************
         *NUMBERS OF LINES AND COLUMNS*
         *                                                    *
         *                                                    */



        for (int i = 1; i <= nb_line; i++)
        {
            float dist = GameObject.Find("Line1").GetComponent<RectTransform>().anchoredPosition.x - GameObject.Find("Line2").GetComponent<RectTransform>().anchoredPosition.x;
            GameObject number = Instantiate(line_numbers, GameObject.Find("Line_N_Column").transform);
            number.name = "Line" + i + "Numbers";
            number.GetComponent<RectTransform>().anchoredPosition =
                new Vector2(
                    GameObject.Find("Border_Horizontal_Line").GetComponent<RectTransform>().anchoredPosition.x - ((dist * i)-(dist/2)),
                    GameObject.Find("Border_Vertical_Line").GetComponent<RectTransform>().anchoredPosition.y+260
                );
            number.GetComponent<Text>().fontSize = (int)(Mathf.Clamp((int)(dist/1.25f),10.0f,45.0f));
        }

        for (int i = 1; i <= nb_column; i++)
        {
            float dist = GameObject.Find("Column1").GetComponent<RectTransform>().anchoredPosition.y - GameObject.Find("Column2").GetComponent<RectTransform>().anchoredPosition.y;
            GameObject number = Instantiate(column_numbers, GameObject.Find("Line_N_Column").transform);
            number.name = "Column" + i + "Numbers";
            number.GetComponent<RectTransform>().anchoredPosition =
                new Vector2(
                    GameObject.Find("Border_Horizontal_Line").GetComponent<RectTransform>().anchoredPosition.x + 510,
                    GameObject.Find("Border_Vertical_Line").GetComponent<RectTransform>().anchoredPosition.y - ((dist * i) - (dist / 2))
                );
            number.GetComponent<Text>().fontSize = (int)(Mathf.Clamp((int)((GameObject.Find("Line1").GetComponent<RectTransform>().anchoredPosition.x - GameObject.Find("Line2").GetComponent<RectTransform>().anchoredPosition.x) / 1.25f),10.0f,45.0f));
    }



        /*                                                   *
         *                                                   *
         *         NUMBERS OF LINES AND COLUMNS       *
         * ***************************************************/




        /*                                                                                        *
         *                                                                                        *
         *      SETTING UP THE GAME BOARD ACCORDING TO THE NUMBER OF LINES / COLUMNS    *
         ******************************************************************************************/

    }

    // Update is called once per frame
    void Update () {

       // nb_columns GameObject.FindWithTag("Picross").GetComponent<>().column_numbers.Count;
    }
}
