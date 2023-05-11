using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;
using System;

[DefaultExecutionOrder(-1)]
public class DatabaseManager : MonoBehaviour
{
    public static DatabaseManager Instance;
    private MySqlConnection connection;
    private string connStr;
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
        ConnectToDB();
    }

    private void ConnectToDB()
    {
        try
        {
            connStr = "server=192.168.1.116;user=root;database=Game;port=3306;password=sql1234";
            connection = new MySqlConnection(connStr);
            connection.Open();
            Debug.Log("connected to database");
        }
        catch (Exception e)
        {
            Debug.Log(e + "cannot connect to database");
        }
    }

    public void InsertToDB(string name)
    {
        try
        {
            string sql = "INSERT INTO `Game`(`Name`, `Score`) VALUES ('" + name + "')";
            MySqlCommand cmd = new MySqlCommand(sql, connection);
            cmd.ExecuteNonQuery();
            Debug.Log("inserted to database");
        }
        catch (Exception e)
        {
            Debug.Log(e + "cannot insert to database");
        }
    }

    void Update()
    {

    }

    private void OnDestroy()
    {
        connection.Close();
    }
}
