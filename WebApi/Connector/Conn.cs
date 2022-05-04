
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using WebApi.Model;

namespace WebApi.Connector
{
   
    public class Conn
    {
        private string constring;
        public Conn()
        {
            constring = @"Host=192.168.1.109;Database=Deneme;Username=postgres;Password=123456";
        }
        public List<Task> Task()
        {
            List<Task> allTasks = new List<Task>();
            
              //bAĞLANTI SAĞLANDI
            using (NpgsqlConnection connMySql= new NpgsqlConnection(constring))
            {
               //BAĞLANTIYA İLGİLİ VERİ TABANI SORGUSU VERİLDİ
                using (NpgsqlCommand cmd = connMySql.CreateCommand())
                {
                    cmd.CommandText = "Select * from task";
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Connection = connMySql;
                  


                    connMySql.Open();
                    //İLGİLİ VERİ TABANI SORGUSU ÇALIŞTIRILIP TASKLAR GETİRİLDİ
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                      
                        while (reader.Read())
                        {
                            allTasks.Add(new Task { id = reader.GetInt32(reader.GetOrdinal("id")), task = reader.GetString(reader.GetOrdinal("task_name"))});
                        }
                    }
                }
                connMySql.Close();
            }


         

            return allTasks;

        }
        public List<Task> AddTask(Task task)
        {
            List<Task> allTasks = new List<Task>();


            using (NpgsqlConnection connMySql = new NpgsqlConnection(constring))
            {
                //TASK EKLEMEK İÇİN İLGİLİ VERİ TABANI SORGUSU YAZILDI
                using (NpgsqlCommand cmd = connMySql.CreateCommand())
                {
                    
                    cmd.CommandText = "insert into task(task_name) values(@Task_name)";
                    string tname;
                    tname = task.task;
                    //İLGİLİ Task_name CONTROLLERDAN PARAMETRE OLARAK ALINIP SORGUYA EKLENDİ
                    cmd.Parameters.AddWithValue("@Task_name",tname);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Connection = connMySql;



                    connMySql.Open();
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            allTasks.Add(new Task { id = reader.GetInt32(reader.GetOrdinal("id")), task = reader.GetString(reader.GetOrdinal("task_name")) });
                        }
                    }
                }
                connMySql.Close();
            }




            return allTasks;

        }
        public List<Task> SelectedTask(int id)
        {
            List<Task> allTasks = new List<Task>();


            using (NpgsqlConnection connMySql = new NpgsqlConnection(constring))
            {
                //SEÇİLEN İDYE GÖRE İLGİLİ TASKI BULABİLMESİ İÇİN İLGİLİ VERİ TABANI SORGUSU YAZILDI
                using (NpgsqlCommand cmd = connMySql.CreateCommand())
                {
                    //İLGİLİ id CONTROLLERDAN PARAMETRE OLARAK ALINIP SORGUYA EKLENDİ
                    cmd.CommandText = "Select * from task where id="+id+"";
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Connection = connMySql;



                    connMySql.Open();
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            allTasks.Add(new Task { id = reader.GetInt32(reader.GetOrdinal("id")), task = reader.GetString(reader.GetOrdinal("task_name")) });
                        }
                    }
                }
                connMySql.Close();
            }




            return allTasks;

        }

    }
}
