using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Program : MonoBehaviour
{
    void Start()
    {

        string textFile = "C:\\Users\\maria\\Desktop\\TEC\\test.txt";
        Problem problem = new Problem(textFile);
        picross_game solver = new picross_game(problem);
        solver.showSolution();


        
    }
 
    void Update()
    {
        
    }
}
