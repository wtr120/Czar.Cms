using Dapper;
using Sample05.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Sample05
{
    class Program
    {
        public const string connectstring = "Data Source=.;User ID=sa;Password=123456;Initial Catalog=CoreCms;Pooling=true;Max Pool Size=100;";
        static void Main(string[] args)
        {
            test_select_content_with_comment();
            Console.ReadLine();
        }

        static void test_select_content_with_comment()
        {
            using (var conn = new SqlConnection(connectstring))
            {
                string sql_insert = @"select * from content where id=@id;
select * from comment where content_id=@id;";
                using (var result = conn.QueryMultiple(sql_insert, new { id = 5 }))
                {
                    var content = result.ReadFirstOrDefault<ContentWithCommnet>();
                    content.comments = result.Read<Comment>();
                    Console.WriteLine($"test_select_content_with_comment:内容5的评论数量{content.comments.Count()}");
                }

            }
        }
    }
}
