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

        Bitmap img_back = new Bitmap(32, 32);
        Size tip_size = new Size(32, 32);
        Dictionary<String, Bitmap> img_list;
        int img_count = 0;
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

            mapdata = new string[tate, yoko];
            //for (int i = 0; i < tate; i++)
            //{
            //    for (int j = 0; j < yoko; j++)
            //    {
            //        mapdata[i, j] = 0;
            //        console.writeline(mapdata.l);
            //    }
            //}

            button_dipose(yoko, tate, this);


        }
        private void addImage(object sender, EventArgs e)
        {
            int buttonId = ((Button)sender).TabIndex - 1; // ボタンの名前
            string imagename = listBox1.SelectedItem.ToString();
            Bitmap img = img_list[imagepath];
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
            Size s = new Size(38, 38);
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
                    // 2-3)Nameプロパティを設定
                    btn.Name = "Button" + i;
                    // 2-4)サイズを設定
                    btn.Size = s;
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
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                tmppath = System.IO.Path.GetFileName(ofd.FileName);
                dict.Add(tmppath, ofd.FileName);


                split_img(ofd.FileName);

                foreach (var i in img_list)
                {
                    listBox1.Items.Add(i.Key);
                }
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

        private void split_img(String path)
        {
            Bitmap img_base;
            Bitmap img_tip;

            img_list = new Dictionary<string, Bitmap>();
            int tip_count = 1;
            // 画像を切り抜く
            if (path != null)
            {

                img_base = new Bitmap(path);
                if (img_base.Size.Width % 32 != 0 || img_base.Size.Width % 32 != 0)
                {
                    img_base.Dispose();
                    MessageBox.Show("ファイルが対応していません");
                    return;
                }   
                int i = 0;
                while (i <= img_base.Size.Width - tip_size.Width)
                {
                    int j = 0;
                    while (j <= img_base.Size.Height - tip_size.Height)
                    {
                        Rectangle rect = new Rectangle(i, j, tip_size.Width, tip_size.Height);
                        img_tip = img_base.Clone(rect, img_base.PixelFormat);

                        img_list.Add(img_count.ToString()+tip_count.ToString(), img_tip);
                        tip_count++;
                        
                        j += tip_size.Height;
                    }
                    i += tip_size.Width;
                }
                img_base.Dispose();
            }
            img_count++;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (img_list != null)
                foreach (var i in img_list)
                    i.Value.Dispose();
        }
    }
}
