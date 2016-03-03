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
        Size tip_size = new Size(32, 32);
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
            int buttonId = ((Button)sender).TabIndex-1; // ボタンの名前
            string imagepath = listBox1.SelectedItem.ToString();
            Bitmap img = img_list[imagepath];
            list[buttonId].Image = img;
            Console.WriteLine(list[buttonId].Size);
            
       }

        List<Button> list = new List<Button>();
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
                    btn.AutoSize = true;
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

        Dictionary<string, string> dict = new Dictionary<string, string>();
        private void button_imgRead_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            string tmppath;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                tmppath = System.IO.Path.GetFileName(ofd.FileName);
                dict.Add(tmppath, ofd.FileName);
                
             
                split_img_2(ofd.FileName);

                foreach (var i in img_list)
                {
                    listBox1.Items.Add(i.Key);
                }
            }
            
        }
        Dictionary<String, Bitmap> img_list;

        private void split_img_1(String path)
        {
            Bitmap img_base;
            Bitmap img_tip;
            img_list = new Dictionary<string, Bitmap>();
            int tip_count = 0;
            // 画像を切り抜く
            if (path != null)
            {

                img_base = new Bitmap(path);
                string img_name = path.Split('\\')[path.Split('\\').GetUpperBound(0)];
                Console.WriteLine(img_name);
                int i = 0, j = 0;
                while (i <= img_base.Size.Width - tip_size.Width)
                {
                    while (j <= img_base.Size.Height - tip_size.Height)
                    {
                        Rectangle rect = new Rectangle(i, j, tip_size.Width, tip_size.Height);
                        img_tip = img_base.Clone(rect, img_base.PixelFormat);
                        img_list.Add(img_name + "_" + tip_count, img_tip);

                        tip_count++;
                        Console.WriteLine(img_name + "_" + tip_count);
                        if (j + tip_size.Height * 2 <= img_base.Size.Height)
                            j += tip_size.Height;
                        else
                            j += j + tip_size.Height - img_base.Size.Height;
                    }
                    i += tip_size.Width;
                }

                img_base.Dispose();
            }
        }

        private void split_img_2(String path)
        {
            Bitmap img_base;
            Bitmap img_back = new Bitmap(@"C:\Users\s1534\Pictures\background.bmp");
            Bitmap img_tip;

            img_list = new Dictionary<string, Bitmap>();
            int tip_count = 0;
            // 画像を切り抜く
            if (path != null)
            {

                img_base = new Bitmap(path);
                string img_name = path.Split('\\')[path.Split('\\').GetUpperBound(0)];
                Console.WriteLine(img_name);
                Console.WriteLine(img_base.Size);
                int i = 0;
                do
                {
                    int j = 0;
                    while (j <= img_base.Size.Height - tip_size.Height)
                    {
                        Rectangle rect = new Rectangle(i, j, tip_size.Width, tip_size.Height);
                        img_tip = img_base.Clone(rect, img_base.PixelFormat);
                        img_list.Add(img_name + "_" + tip_count, img_tip);

                        tip_count++;
                        Console.WriteLine(img_name + "_" + tip_count);
                        j += tip_size.Height;
                        if (j + tip_size.Height * 2 > img_base.Size.Height)
                            j -= j + tip_size.Height - img_base.Size.Height;
                    }
                    i += tip_size.Width;
                    if (i + tip_size.Width * 2 > img_base.Size.Width)
                        i -= i + tip_size.Width - img_base.Size.Width;

                } while (i <= img_base.Size.Width - tip_size.Width);

                img_base.Dispose();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (img_list != null)
                foreach (var i in img_list)
                    i.Value.Dispose();
        }
    }
}
