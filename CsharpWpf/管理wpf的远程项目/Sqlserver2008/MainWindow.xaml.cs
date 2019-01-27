using System;

using System.Collections.Generic;

using System.ComponentModel;

using System.Data;

using System.Drawing;

using System.Linq;

using System.Text;

using System.Threading.Tasks;

using System.Data.OleDb;//新建引用

using System.Data.SqlClient;//新建引用
using System.Windows;

namespace Sqlserver2008
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (tbx1.Text == "" || tbx2.Text == "")
            {
                MessageBox.Show("input userName or userPassword");
            }
            else
            {
                SqlConnection conn = new SqlConnection("Data Source=(local);Initial Catalog=abc;Integrated Security=True");

                conn.Open();

                SqlCommand cmd = new SqlCommand("select * from [login] where account='" + tbx1.Text.Trim() + "' and password='" + tbx2.Text.Trim() + "'", conn);//这个表名一定要加上[]

                SqlDataReader sdr = cmd.ExecuteReader();

                sdr.Read();

                MessageBox.Show("获取到了数据");

                if (sdr.HasRows)
                {
                    MessageBox.Show("登录成功！");
                }
                else
                {
                    MessageBox.Show("用户名或者密码错误");
                }

                conn.Close();
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
                if (tbx1.Text == "" || tbx2.Text == "")
                    MessageBox.Show("请输入用户名和密码");
                else
                {
                    //链接服务器
                    SqlConnection conn = new SqlConnection("Data Source=(local);Initial Catalog=abc;Integrated Security=True");

                    //打开服务器
                    conn.Open();

                    //通过服务器打开数据库
                    SqlCommand cmd = new SqlCommand($"select * from [login] where account = {tbx1.Text.Trim()}", conn);

                    //获取数据库读取对象
                    SqlDataReader sdr = cmd.ExecuteReader();

                    //打开数据库读取对象
                    sdr.Read();

                    //数据库是否有该行数据
                    if (sdr.HasRows)
                    {
                        MessageBox.Show("该用户已注册，请使用其他用户名");
                    }
                    else
                    {
                        //关闭数据库读取对象
                        sdr.Close();

                        //往数据库的指定多行中插入指定的值
                        String insert = $"insert into [login] (account,password) values ({tbx1.Text},{tbx2.Text})";

                        //将数据库插入服务器
                        SqlCommand icmd = new SqlCommand(insert, conn);

                        //执行sql语句
                        icmd.ExecuteNonQuery();

                        conn.Close();//关闭连接

                        conn.Dispose();//释放资源

                        MessageBox.Show("注册成功");
                    }
            }
        }
    }
}
