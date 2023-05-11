using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

[DefaultExecutionOrder(-1)]
public class DatabaseManager : MonoBehaviour
{
    public static DatabaseManager Instance;

    public List<Emp> empList { get; private set; } = new List<Emp>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

    }


    void Start()
    {
        TextAsset data = Resources.Load("EMPData") as TextAsset;
        string[] lines = data.text.Split('\n');
        // string[] lines = File.ReadAllLines(filePath);

        // Loop through each line in the CSV file
        for (int i = 1; i < lines.Length; i++)
        {
            string[] values = lines[i].Split(',');

            empList.Add(new Emp(values[0], values[1]));
        }

        foreach (Emp emp in empList)
        {
            Debug.Log(emp.empId + " " + emp.empName);
        }

    }


    void Update()
    {

    }

    private void OnDestroy()
    {

    }
}

[Serializable]
public struct Emp
{
    public Emp(string empId, string empName)
    {
        this.empId = empId;
        this.empName = empName;
    }
    public string empId;
    public string empName;
}
