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
        Size tip_size;
        Dictionary<String, Bitmap> img_dict;
        List<String> img_list;
        int img_count = 0;

        Dictionary<Point, String> mapdata;

        List<Button> button_list;
        const int num_controls = 10;
        const int btn_margin = -1;

        public Form1()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            tip_size = new Size(32, 32);
            button_list = new List<Button>();
            img_list = new List<String>();
            img_dict = new Dictionary<string, Bitmap>();

            mapdata = new Dictionary<Point, string>();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            mapdata.Clear();
            int tate = int.Parse(textBox1.Text);
            int yoko = int.Parse(textBox2.Text);

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

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode ==  Keys.Delete)
            {
                Object selected = listBox1.SelectedItem;
                listBox1.Items.Remove(selected);
                img_list.Remove((String)selected);
            }
        }

        private void addImage(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null)
                return;
            int buttonId = ((Button)sender).TabIndex - num_controls - 1; // ボタンの名前
            String imagename = listBox1.SelectedItem.ToString();
            Bitmap img = img_dict[imagename];
            int x = int.Parse(button_list[buttonId].Text.Split(',')[0]);
            int y = int.Parse(button_list[buttonId].Text.Split(',')[1]);
            if (radio_front.Checked)
            {
                if(!mapdata.ContainsKey(new Point(x, y)))
                    mapdata.Add(new Point(x, y), imagename + "0");
                else
                    mapdata.Add(new Point(x, y), imagename + "0");
            }
            else if (radio_back.Checked)
            {
                mapdata.Add(new Point(x, y), imagename + "1");
            }
            button_list[buttonId].Image = img;
        }

        public void button_dipose(int width, int height, Form f)
        {

            // 初期宣言
            Size s = new Size(38, 38);
            int b_num;
            if (button_list.Count != 0)
            {
                for (int i = 0; i < button_list.Count; i++)
                {
                    // 2-8)フォームに配置
                    f.Controls.RemoveAt(f.Controls.GetChildIndex(button_list[i]));
                }
                button_list.Clear();
            }

            // 要素を後ろに追加していく
            b_num = num_controls;
            for (int j = height - 1; j >= 0 ; j--)
            {
                for (int i = 0; i <= width; i++)
                {
                    Button btn = new Button();
                    b_num++;
                    // 2-1)インスタンスを作成
                    // 2-2)配置位置を設定
                    btn.Location = new Point(150 + (s.Width + btn_margin) * (i % width), 150 + (s.Height + btn_margin) * ((height - 1 - j) % height));
                    // 2-3)Nameプロパティを設定
                    btn.Name = "Tip" + (b_num - num_controls);
                    // 2-4)サイズを設定
                    btn.Size = s;
                    // 2-5)TabIndexを設定
                    btn.TabIndex = b_num;
                    // 2-6)ボタンテキストを設定
                    btn.Text = i + "," + j;                     
                    button_list.Add(btn);
                    btn.Click += addImage;
                }
            }

            foreach (var btn in button_list)
            {
                // 2-8)フォームに配置
                f.Controls.Add(btn);
            }
        }

        private void button_imgRead_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if(img_list.Contains(ofd.FileName))
                {
                    MessageBox.Show("既に登録されています");
                    return;
                }

                split_img(ofd.FileName);

                foreach (var i in img_dict)
                {
                    if(!listBox1.Items.Contains(i.Key))
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
                    foreach(var v in mapdata)
                    {
                        sw.Write(v.Value + "," + v.Key.X + "," + v.Key.Y);                        
                        sw.Write("\r\n");
                    }
                }
            }
        }

        private void split_img(String path)
        {
            Bitmap img_base;
            Bitmap img_tip;

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
                //FolderBrowserDialogクラスのインスタンスを作成
                FolderBrowserDialog fbd = new FolderBrowserDialog();

                //上部に表示する説明テキストを指定する
                fbd.Description = "フォルダを指定してください。";
                //ルートフォルダを指定する
                //デフォルトでDesktop
                fbd.RootFolder = Environment.SpecialFolder.Desktop;
               
                //ユーザーが新しいフォルダを作成できるようにする
                //デフォルトでTrue
                fbd.ShowNewFolderButton = true;

                //ダイアログを表示する
                if (fbd.ShowDialog(this) == DialogResult.OK)
                {
                    for (int i = 0; i <= img_base.Size.Height - tip_size.Height; i += tip_size.Height)
                    {
                        for (int j = 0; j <= img_base.Size.Width - tip_size.Width; j += tip_size.Width)
                        {
                            Rectangle rect = new Rectangle(j, i, tip_size.Width, tip_size.Height);
                            img_tip = img_base.Clone(rect, img_base.PixelFormat);
                            img_tip.Save(@fbd.SelectedPath+ "\\" +tip_count.ToString()+ System.IO.Path.GetExtension(path));
                            img_dict.Add(img_count.ToString() + tip_count.ToString(), img_tip);
                            tip_count++;
                        }
                    }
                    img_base.Dispose();
                }                
            }
            img_count++;
            img_list.Add(path);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (img_dict != null)
                foreach (var i in img_dict)
                    i.Value.Dispose();
        }
    }
}
