using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pong {
    public partial class Form1 : Form {
        int gap = 60;
        int dx = 10, dy = 10;
        Recquet right_rec;
        Recquet left_rec;

        public Form1() { 
        InitializeComponent();
            this.MouseWheel += new MouseEventHandler(this_MouseWheel);
            this.MouseMove += new MouseEventHandler(this_MouseMove); //или по таймеру      
            
            void this_MouseMove(object sender, MouseEventArgs e) {
                this_MouseWheel(this, e);
            }

            void this_MouseWheel(object sender, MouseEventArgs e) {
                if (e.Delta > 0)
                    left_rec.move_up();
                else if (e.Delta < 0)
                    left_rec.move_down();
   
            }
        }

        private void Form1_Load(object sender, EventArgs e) {
            int width = this.ClientRectangle.Width;
            int height = this.ClientRectangle.Height;
            if (panel1.Top + panel1.Height > height) {
                panel1.Top = height - panel1.Height;
            }
            if (panel2.Top + panel2.Height > height) {
                panel2.Top = height - panel2.Height;
            }
            left_rec = new Recquet(width, height, gap, 10, panel1, "left");
            right_rec = new Recquet(width, height, gap, 10, panel2, "right");
            label1.Top = 0;
            label1.Left = 0;
            label2.Top = 0;
            label2.Left = width - label2.Width;

            Pen p = new Pen(Color.Blue, 5);// цвет линии и ширина
            Point p1 = new Point(5, 10);// первая точка
            Point p2 = new Point(40, 100);// вторая точка

 

    }

    private void Form1_KeyDown(object sender, KeyEventArgs e) {
            //MessageBox.Show(e.KeyCode.ToString());
            if ((e.KeyCode == Keys.W) || (e.KeyCode == Keys.Up)) {
                left_rec.move_up();
            } else if ((e.KeyCode == Keys.S) || (e.KeyCode == Keys.Down)) {
                left_rec.move_down();
            } else if ((e.KeyCode == Keys.P) || (e.KeyCode == Keys.Space)) {
               // if (!tableLayoutPanel2.Visible) {
                    timer1.Enabled = !timer1.Enabled;
                    timer3.Enabled = !timer3.Enabled;
                    label6.Text = label1.Text + " : " + label2.Text;
                    tableLayoutPanel2.Left = this.Width / 2 - tableLayoutPanel2.Width / 2;
                    tableLayoutPanel2.Top = this.Height / 2 - tableLayoutPanel2.Height / 2;
                    tableLayoutPanel2.Visible = !tableLayoutPanel2.Visible;
                    
               // }

            }
        }


        private void timer2_Tick(object sender, EventArgs e) {
            timer1.Enabled = true;
            dy = 10;
            dx = 10;
            timer2.Enabled = false;
        }

        private void timer3_Tick(object sender, EventArgs e) {
            if ((panel3.Left >= this.ClientRectangle.Width / 2) && (dx > 0)) {
                if (panel3.Top + panel3.Height / 2 > right_rec.Body.Top + right_rec.Body.Height / 2) {
                    right_rec.move_down();
                } else {
                    right_rec.move_up();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e) {

            tableLayoutPanel2.Visible = !tableLayoutPanel2.Visible;
            timer1.Enabled = !timer1.Enabled;
            timer3.Enabled = !timer3.Enabled;
            timer1.Interval = 80 - trackBar1.Value;
        }

        private void button2_Click(object sender, EventArgs e) {
            dx = 10;
            dy = 10;
            panel3.Top = this.ClientRectangle.Height / 2;
            panel3.Left = this.ClientRectangle.Width / 2;
            label2.Text = "0";
            label1.Text = "0";

            this.button1_Click(this, null);
        }

        private void timer1_Tick(object sender, EventArgs e) {
            if ((panel3.Top + dy <= 0) && (dy < 0)) {
                panel3.Top = 0;
                dy = -dy;
            } else if ((panel3.Top + panel3.Size.Height + dy >= this.ClientRectangle.Height) && (dy > 0)) {
                panel3.Top = this.ClientRectangle.Height - panel3.Size.Height;
                dy = -dy;
            } else {
                panel3.Top += dy;
            }
            
            if ((panel3.Left + dx <= left_rec.Body.Left + left_rec.Body.Size.Width) && (dx < 0)) {
                panel3.Left = left_rec.Body.Left + left_rec.Body.Size.Width;

                if ((panel3.Top <= left_rec.Body.Top + left_rec.Body.Height) &&
                    (panel3.Top + panel3.Height >= left_rec.Body.Top)) {
                    dx = -dx;
                } else {
                    if (label1.Text == "9") {
                        timer1.Enabled = false;
                        timer3.Enabled = false;
                        tableLayoutPanel2.Visible = true;
                        label6.Text = label1.Text + " : " + label2.Text;
                        button1.Enabled = false;
                        return;
                    }
                    label1.Text = (int.Parse(label1.Text) + 1).ToString();
                    dx = -dx;
                   
                    panel3.Top = this.ClientRectangle.Height / 2;
                    panel3.Left = this.ClientRectangle.Width / 2;
                    timer1.Enabled = false;
                    timer2.Enabled = true;
                }

                dy = Math.Sign(dy) * CounterIncremet.Increment(panel3.Size.Height / 2 + panel3.Top,
                                                               left_rec.Body.Height / 2 + left_rec.Body.Top,
                                                               left_rec.Body.Height, dy);

            } else if ((panel3.Left + panel3.Size.Width + dx >= right_rec.Body.Left) && (dx > 0)) {
                panel3.Left = right_rec.Body.Left - panel3.Size.Width;
                if ((panel3.Top <= right_rec.Body.Top + right_rec.Body.Height) &&
                       (panel3.Top + panel3.Height >= right_rec.Body.Top)) {
                    dx = -dx;
                } else {
                    if (label2.Text == "9") {
                        timer1.Enabled = false;
                        timer3.Enabled = false;
                        tableLayoutPanel2.Visible = true;
                        label6.Text = label1.Text + " : " + label2.Text;
                        button1.Enabled = false;
                        return;
                    }
                    label2.Text = (int.Parse(label2.Text) + 1).ToString();
                    dx = -dx;
                    panel3.Top = this.ClientRectangle.Height / 2;
                    panel3.Left = this.ClientRectangle.Width / 2;

                    timer1.Enabled = false;
                    timer2.Enabled = true;
                }

                dy = Math.Sign(dy) * CounterIncremet.Increment(panel3.Size.Height / 2 + panel3.Top,
                                                               right_rec.Body.Height / 2 + right_rec.Body.Top,
                                                               right_rec.Body.Height, dy);

            } else {
                
                panel3.Left += dx;
            }

           
        }

       
    }

    static class CounterIncremet {
        public static int Increment(int center_ball, int center_recquet, int length_recquet, int dy) {
            int delta;
            
            if (dy < 0) {
                delta = 10 +  5 *(center_ball - center_recquet) / (length_recquet / 2);

            } else {
                delta = 10 +  5 * (center_recquet - center_ball) / (length_recquet / 2);

            }

            return delta;

        }
    }

    //interface Recquet
    //{
    //   void move_up();

    //}

    public class Recquet {
        //public int Offset { get; set; }
        public int Dy { get; set; }
        public int Client_width { get; set; }
        public int Client_height { get; set; }
        public Panel Body { get; set; }

        public Recquet(int width, int height, int offset, int dy, Panel body, string position) {
            //this.Offset = offset;
            this.Dy = dy;
            this.Body = body;
            this.Client_width = width;
            this.Client_height = height;

            if (position == "left") {
                this.Body.Left = offset;
            } else {
                this.Body.Left = this.Client_width - offset - this.Body.Size.Width;
                //Form1.ActiveForm.ClientSize.Width
            }
        }

        public void move_up() {
            if (this.Body.Top >= this.Dy) {
                this.Body.Top -= this.Dy;
            } else {
                this.Body.Top = 0;
            }
        }

        public void move_down() {
            if (this.Body.Top + this.Body.Size.Height + this.Dy <= this.Client_height) {
                this.Body.Top += this.Dy;
            } else {
                this.Body.Top = this.Client_height - this.Body.Size.Height;
            }
        }
    }
}
