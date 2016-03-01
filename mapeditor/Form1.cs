using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace mapeditor
{

    public partial class Form1 : Form
    {
        string[,] mapdata;
        List<Button> list = new List<Button>();
        Dictionary<string, string> dict = new Dictionary<string, string>();

        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            
            int tate = int.Parse(textBox1.Text);
            int yoko = int.Parse(textBox2.Text);

            mapdata = new string[tate,yoko] ;
            //for (int i = 0; i < tate; i++)
            //{
            //    for (int j = 0; j < yoko; j++)
            //    {
            //        mapdata[i, j] = 0;
            //        console.writeline(mapdata.l);
            //    }
            //}

            button_dipose(yoko,tate,this);

            
        }
        private void addImage(object sender, EventArgs e)
        {
            int buttonId = ((Button)sender).TabIndex-1; // ボタンの名前
            string imagepath = dict[listBox1.SelectedItem.ToString()];
            string imagename = System.IO.Path.GetFileNameWithoutExtension(imagepath);
            System.Drawing.Image img = System.Drawing.Image.FromFile(imagepath);
            Console.WriteLine(list[buttonId].Text.Split(',')[0]);
            int x = int.Parse(list[buttonId].Text.Split(',')[0]);
            int y = int.Parse(list[buttonId].Text.Split(',')[1]);


            if (radio_front.Checked)
            {
                mapdata[x, y] = imagename + "0";
            }
            else if (radio_back.Checked)
            {
                mapdata[x, y] = imagename + "1";
            }
            
            list[buttonId].Image = img;
             
        }
    
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
            b_num = 0;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Button btn = new Button();
                    b_num++;

                    // 2-1)インスタンスを作成
                    // 2-2)配置位置を設定
                    btn.Location = new Point(150 + s.Width * (i % width), 150 + s.Height * (j % height));
                    
                    //Console.WriteLine("i % {1} = {0}",i%width, width); 
                    //Console.WriteLine("i % {1} = {0}", i / height, height);
                    //Console.WriteLine("({0},{1})", i % width, i / height);
                    // 2-3)Nameプロパティを設定
                    btn.Name = "Button" + i;
                    // 2-4)サイズを設定
                    btn.Size = new System.Drawing.Size(s.Width, s.Height);
                    // 2-5)TabIndexを設定
                    btn.TabIndex = b_num;
                    // 2-6)ボタンテキストを設定
                    btn.Text = i + "," + j;
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

        private void button_mapExport_Click(object sender, EventArgs e)
        {
            int tate = int.Parse(textBox1.Text);
            int yoko = int.Parse(textBox2.Text);
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "CSVファイル(カンマ区切り)(*.csv)|*.csv;";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(sfd.FileName))
                {
                    for (int i = 0; i < tate; i++)
                    {
                        for (int j = 0; j < yoko; j++)
                        {
                            sw.Write(mapdata[j, i]);
                            if (j < yoko - 1)
                            {
                                sw.Write(",");
                            }

                        }
                        sw.Write("\r\n");
                    }
                }
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
