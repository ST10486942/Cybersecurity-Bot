using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace ProgPart2
{
    public class TaskManager
    {
        private readonly string connectionString =
            "Server=localhost;Port=3306;Database=cybersecurity_chatbot;Uid=root;Pwd=Masemola@Ani942;";
             //Test database connection
        public bool TestConnection()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    Console.WriteLine("Database connected successfully!");

                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Connection Error: " + ex.Message);

                return false;
            }
        }
        public bool AddTask(string userName, string title, string description, string reminderInput = null)
        {
            DateTime? reminderDate = null;

            // Parse simple reminders like "in 7 days", "tomorrow", "in 3 days"
            if (!string.IsNullOrEmpty(reminderInput))
            {
                reminderDate = ParseReminder(reminderInput);
            }

            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = @"INSERT INTO tasks 
                           (User_Name, Task_Name, Task_Description, ReminderDate) 
                           VALUES (@user, @title, @desc, @reminder)";

                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@user", userName);
                        cmd.Parameters.AddWithValue("@title", title);
                        cmd.Parameters.AddWithValue("@desc", description ?? "");
                        cmd.Parameters.AddWithValue("@reminder", reminderDate.HasValue ? (object)reminderDate.Value : DBNull.Value);
                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("AddTask Error: " + ex.Message);
                return false;
            }
        }

        private DateTime? ParseReminder(string input)
        {
            string lower = input.ToLower();
            try
            {
                if (lower.Contains("tomorrow"))
                    return DateTime.Now.AddDays(1);

                if (lower.Contains("day") || lower.Contains("days"))
                {
                    // Extract number (e.g. "in 7 days")
                    var numbers = System.Text.RegularExpressions.Regex.Matches(lower, @"\d+");
                    if (numbers.Count > 0 && int.TryParse(numbers[0].Value, out int days))
                        return DateTime.Now.AddDays(days);
                }

                if (lower.Contains("week") || lower.Contains("weeks"))
                {
                    var numbers = System.Text.RegularExpressions.Regex.Matches(lower, @"\d+");
                    if (numbers.Count > 0 && int.TryParse(numbers[0].Value, out int weeks))
                        return DateTime.Now.AddDays(weeks * 7);
                }
            }
            catch { }

            return DateTime.Now.AddDays(7); // Default: 7 days
        }

        public List<string> GetAllTasks(string userName)
        {

            List<string> tasks = new List<string>();


            try
            {

                using (MySqlConnection conn =
                    new MySqlConnection(connectionString))
                {

                    conn.Open();


                    string query =
                    @"SELECT 
                        Task_Name,
                        Task_Description,
                        ReminderDate,
                        IsCompleted

                    FROM tasks

                    WHERE User_Name = @username

                    ORDER BY CreatedDate DESC";



                    using (MySqlCommand cmd =
                        new MySqlCommand(query, conn))
                    {


                        cmd.Parameters.AddWithValue(
                            "@username",
                            userName);



                        using (MySqlDataReader reader =
                            cmd.ExecuteReader())
                        {


                            while (reader.Read())
                            {


                                bool completed =
                                Convert.ToBoolean(
                                    reader["IsCompleted"]);



                                string status =
                                    completed
                                    ? "[Completed]"
                                    : "[Pending]";



                                string name =
                                    reader["Task_Name"].ToString();



                                string reminder = "";



                                if (reader["ReminderDate"] != DBNull.Value)
                                {

                                    DateTime date =
                                    Convert.ToDateTime(
                                        reader["ReminderDate"]);



                                    reminder =
                                    " | Reminder: "
                                    + date.ToString("dd MMM yyyy");

                                }



                                tasks.Add(
                                $"{status} {name}{reminder}");

                            }


                        }

                    }

                }

            }

            catch (Exception ex)
            {

                tasks.Add(
                "Error loading tasks: "
                + ex.Message);

            }



            return tasks;

        }

        public bool CompleteTask(int taskID)
        {

            try
            {

                using (MySqlConnection conn =
                    new MySqlConnection(connectionString))
                {

                    conn.Open();


                    string query =
                    @"UPDATE tasks

                      SET IsCompleted = true

                      WHERE Task_ID = @id";



                    MySqlCommand cmd =
                    new MySqlCommand(query, conn);



                    cmd.Parameters.AddWithValue(
                        "@id",
                        taskID);



                    cmd.ExecuteNonQuery();


                }


                return true;

            }

            catch (Exception ex)
            {

                Console.WriteLine(
                    "Complete Task Error: "
                    + ex.Message);


                return false;

            }

        }

    }
}