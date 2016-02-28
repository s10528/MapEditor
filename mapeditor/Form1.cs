using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace mapeditor
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            
            int tate = int.Parse(textBox1.Text);
            int yoko = int.Parse(textBox2.Text);
            button_dipose(yoko,tate,this);
            
        }
        private void addImage(object sender, EventArgs e)
        {
            int buttonId = ((Button)sender).TabIndex; // ボタンの名前
            string imagepath = dict[listBox1.SelectedItem.ToString()];
            System.Drawing.Image img = System.Drawing.Image.FromFile(imagepath);
            list[buttonId].Image = img;
        }

        List<Button> list = new List<Button>();
        public void button_dipose(int width, int height, Form f)
        {
            
            // 初期宣言
            Size s = new Size(32, 32);
            int b_num;
            if (list.Count != 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    // 2-8)フォームに配置
                    f.Controls.RemoveAt(f.Controls.GetChildIndex(list[i]));
                }
                list.Clear();
            }

            // 要素を後ろに追加していく
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Button btn = new Button();
                    b_num = i * width + j;

                    // 2-1)インスタンスを作成
                    // 2-2)配置位置を設定
                    btn.Location = new Point(150 + s.Width * (i % width), 150 + s.Height * (j % height));
                    //Console.WriteLine("i % {1} = {0}",i%width, width); 
                    //Console.WriteLine("i % {1} = {0}", i / height, height);
                    Console.WriteLine("({0},{1})", i % width, i / height);
                    // 2-3)Nameプロパティを設定
                    btn.Name = "Button" + i;
                    // 2-4)サイズを設定
                    btn.Size = new System.Drawing.Size(s.Width, s.Height);
                    // 2-5)TabIndexを設定
                    btn.TabIndex = b_num;
                    // 2-6)ボタンテキストを設定
                    btn.Text = btn.TabIndex.ToString();
                    list.Add(btn);
                    btn.Click += addImage;
                }
            }

            foreach (var btn in list)
            {
                
                // 2-8)フォームに配置
                f.Controls.Add(btn);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        Dictionary<string, string> dict = new Dictionary<string, string>();
        private void button_imgRead_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            string tmppath;
            if(ofd.ShowDialog() == DialogResult.OK)
            {
                tmppath = System.IO.Path.GetFileName(ofd.FileName);
                dict.Add(tmppath, ofd.FileName);
                listBox1.Items.Add(tmppath);
            }
        }
    }



    class imagebutton : Button
    {
        private void editbutton()
        {

        }
    }
}
