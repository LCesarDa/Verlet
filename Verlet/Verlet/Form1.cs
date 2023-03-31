using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Verlet
{
    public partial class Form1 : Form
    {
        Canvas canvas;
        Graphics g;
        PictureBox pictureBox;
        Button nl;
        List<VLevel> level;
        int a = 0;
        float r = 0, b = 255;
        Boolean win, los, balls;
        static Random rand = new Random();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            canvas = new Canvas(new Size(Form1.ActiveForm.Width - 100, Form1.ActiveForm.Height - 100));
            pictureBox = new PictureBox
            {
                Image = canvas.bitmap,
                Size = new Size(Form1.ActiveForm.Width - 100, Form1.ActiveForm.Height - 100),
                Location = new Point(5, 5),
                BackColor = Color.Gray,
                
            };
            this.Controls.Add(pictureBox);
            level = new List<VLevel>();
            //level.Add(new VLevel(canvas, a, rand));

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            canvas.FastClear();
            if (los)
            {
                canvas.g.DrawImage(Resource1.loss, new Rectangle(0, 0, (int)canvas.Width, (int)canvas.Height));
                Form1.ActiveForm.BackColor = Color.Red;
                pictureBox.Invalidate();
            } else
            {
                if (a < level.Count)
                {
                    if (r < 255)
                    {
                        r += 10 / ((float)a + 10);
                        b -= 10 / ((float)a + 10);
                    } else los = true;
                    if (los) return;
                    level[a].solve.Step(canvas);
                    for (int i = 0; i < level[a].obs.Count; i++)
                    {
                        level[a].obs[i].Render(canvas, (int)r, (int)b);
                    }
                    for (int i = 0; i < level[a].ball.Count; i++)
                    {
                        balls = level[a].ball[i].WRender();
                        if(!balls) canvas.g.FillEllipse(new SolidBrush(Color.FromArgb(0,(int)b,(int)r)), level[a].ball[i].pos.x - level[a].ball[i].radius, level[a].ball[i].pos.y - level[a].ball[i].radius, level[a].ball[i].diameter, level[a].ball[i].diameter);
                    }
                    if (a < 10)
                    {
                        for (int i = 0; i < level[a].anchor.Count; i++)
                        {
                            level[a].anchor[i].pos = level[a].anchor[i].old;
                            canvas.g.FillEllipse(Brushes.White, level[a].anchor[i].pos.x - level[a].anchor[i].radius, level[a].anchor[i].pos.y - level[a].anchor[i].radius, level[a].anchor[i].diameter, level[a].anchor[i].diameter);
                        }
                    } else
                    {
                        for (int i = 0; i < level[a].anchor.Count-3; i++)
                        {
                            level[a].anchor[i].pos = level[a].anchor[i].old;
                            canvas.g.FillEllipse(Brushes.White, level[a].anchor[i].pos.x - level[a].anchor[i].radius, level[a].anchor[i].pos.y - level[a].anchor[i].radius, level[a].anchor[i].diameter, level[a].anchor[i].diameter);
                        }
                        for (int i = level[a].anchor.Count - 3; i < level[a].anchor.Count; i++)
                        {
                            level[a].anchor[i].WRender();
                            canvas.g.FillEllipse(Brushes.White, level[a].anchor[i].pos.x - level[a].anchor[i].radius, level[a].anchor[i].pos.y - level[a].anchor[i].radius, level[a].anchor[i].diameter, level[a].anchor[i].diameter);
                        }
                    }
                    win = level[a].player.WRender();
                    canvas.g.FillEllipse(Brushes.Black, level[a].player.pos.x - level[a].player.radius, level[a].player.pos.y - level[a].player.radius, level[a].player.diameter*4, level[a].player.diameter*4);
                    if (level[a].grab)
                    {
                        level[a].rope[level[a].rope.Count - 1].Render(canvas);
                        level[a].rope[level[a].rope.Count - 1].sticks[level[a].rope[level[a].rope.Count - 1].sticks.Count - 1].Update();
                        canvas.DrawLine(new System.Drawing.Point((int)level[a].rope[level[a].rope.Count - 1].sticks[level[a].rope[level[a].rope.Count - 1].sticks.Count - 1].startPoint.pos.x, (int)level[a].rope[level[a].rope.Count - 1].sticks[level[a].rope[level[a].rope.Count - 1].sticks.Count - 1].startPoint.pos.y), new System.Drawing.Point((int)level[a].rope[level[a].rope.Count - 1].sticks[level[a].rope[level[a].rope.Count - 1].sticks.Count - 1].endPoint.pos.x, (int)level[a].rope[level[a].rope.Count - 1].sticks[level[a].rope[level[a].rope.Count - 1].sticks.Count - 1].endPoint.pos.y), System.Drawing.Color.White);

                    }

                    pictureBox.BackColor = Color.FromArgb((int)r, 0, (int)b);
                    Form1.ActiveForm.BackColor = Color.FromArgb((int)r, 0, (int)b);
                    pictureBox.Invalidate();
                    if (win) a++;
                }
                else
                {
                    if (win)
                    {
                        canvas.g.DrawImage(Resource1.Load, new Rectangle(0, 0, (int)canvas.Width, (int)canvas.Height));
                        Form1.ActiveForm.BackColor = Color.White;
                        pictureBox.Invalidate();
                    }
                    else
                    {
                        canvas.g.DrawImage(Resource1.Start, new Rectangle(0, 0, (int)canvas.Width, (int)canvas.Height));
                        pictureBox.Invalidate();
                    }
                }
            }
            
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Space) 
            {
                if (los)
                {
                    level.Clear();
                    a = 0;
                    los = false;

                }
                else
                {
                    if (a < level.Count)
                    {
                        if (!level[a].grab)
                        {
                            level[a].player.old = new Vec2(level[a].player.old.x - 2, level[a].player.old.y - 2);
                            level[a].rope.Add(new VRope(level[a].anchor[level[a].near()], level[a].player, canvas));
                        }
                        level[a].grab = true;
                    }
                    else
                    {
                        level.Add(new VLevel(canvas, a, rand));
                        b = 255;
                        r = 0;
                    }
                }
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Space)
            {
                if (a < level.Count)
                {
                    if (level[a].grab)
                    {
                        level[a].player.old = new Vec2(level[a].player.old.x - 10, level[a].player.old.y + 10);
                    }
                    level[a].grab = false;
                }
            }
        }
    }
}
