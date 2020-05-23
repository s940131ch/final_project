using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace Assets
{
    public class SqlAccess
    {
        public static MySqlConnection sqlconnection;
        private static string database = "final_project";
        private static string datatable = "db";
        private static string host = "203.222.24.233";
        private static string id = "root";
        private static string pwd = "";

        public SqlAccess()
        {
            OpenSql();
        }

        public static void OpenSql()
        {
            try
            {
                //string.Format是將指定的 String型別的資料中的每個格式項替換為相應物件的值的文字等效項。
                string sqlString = string.Format("Database={0};Data Source={1};User Id={2};Password={3};", database, host, id, pwd, "3306");
                sqlconnection = new MySqlConnection(sqlString);
                sqlconnection.Open();
            }
            catch (Exception)
            {
                throw new Exception("伺服器連線失敗.....");
            }
        }

        public void Select(string ID, string password)
        {
            if (sqlconnection.State == ConnectionState.Open)
            {
                if (Search(ID, password) == false)
                    Debug.Log("登入失敗");
                else
                    Debug.Log("登入成功");
            }
            else
                Debug.Log("無法連上伺服器");
        }

        public void Insert(string ID, string password)
        {
            if (sqlconnection.State == ConnectionState.Open)
            {
                if (Search(ID) == true)
                    Debug.Log("此帳號已有人使用");
                else
                {
                    string sqlString = "INSERT INTO db(ID, Password) VALUES ('" + ID + "', '" + password + "')";
                    MySqlCommand cmd = new MySqlCommand(sqlString, sqlconnection);
                    cmd.ExecuteScalar();
                    Debug.Log("註冊成功");
                }
            }
        }

        public bool Search(string ID, string password)
        {
            string sqlString = "SELECT ID FROM db WHERE ID LIKE '" + ID + "' AND Password LIKE '" + password + "'";
            
            try
            {
                MySqlCommand cmd = new MySqlCommand(sqlString, sqlconnection);
                if (cmd.ExecuteScalar() == null)
                    return false;
                else
                    return true;
            }
            catch (Exception e)
            {
                throw new Exception("SQL:" + sqlString + "/n" + e.Message.ToString());
            }
        }

        public bool Search(string ID)
        {
            string sqlString = "SELECT ID FROM db WHERE ID LIKE '" + ID + "'";
            try
            {
                MySqlCommand cmd = new MySqlCommand(sqlString, sqlconnection);
                if (cmd.ExecuteScalar() == null)
                    return false;
                else
                    return true;
            }
            catch (Exception e)
            {
                throw new Exception("SQL:" + sqlString + "/n" + e.Message.ToString());
            }
        }

        public void Close()
        {

            if (sqlconnection != null)
            {
                sqlconnection.Close();
                sqlconnection.Dispose();
                sqlconnection = null;
            }

        }

    }
}

