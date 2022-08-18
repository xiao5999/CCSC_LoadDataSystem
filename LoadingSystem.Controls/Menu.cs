using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CCSC智能装车工控系统
{
    public partial class Menu : Form
    {
        //用于记录之前点击的按钮颜色，用于取消高亮
        byte buttonCount = 0;
        public Menu()
        {
            InitializeComponent();


            #region 加载主页面
            Form f = new Main();
            f.TopLevel = false;
            f.Dock = DockStyle.Fill;

            //添加Form的控件集合到主显示页面
            this.mainPanel.Controls.Add(f);
            this.mainPanel.Tag = f;
            f.Show();
            #endregion

            x = this.Width;
            y = this.Height;
            setTag(this);
        }
        public void loadForm(object Form)
        {
            //如果
            if(this.mainPanel.Controls.Count > 0) 
                this.mainPanel.Controls.RemoveAt(0);
            Form f = Form as Form;
            f.TopLevel = false;
            f.Dock = DockStyle.Fill;

            //添加Form的控件集合到主显示页面
            this.mainPanel.Controls.Add(f);
            this.mainPanel.Tag = f;
            f.Show();
        }
        public void cancelHighlight(byte num)
        {
            switch (num)
            {
                case 1:
                    {
                        button1.BackColor = Color.DimGray;
                        break;
                    }
                case 2:
                    {
                        button2.BackColor = Color.DimGray;
                        break;
                    }
                case 3:
                    {
                        button3.BackColor = Color.DimGray;
                        break;
                    }
                case 4:
                    {
                        button4.BackColor = Color.DimGray;
                        break;
                    }
                case 5:
                    {
                        button5.BackColor = Color.DimGray;
                        break;
                    }
                case 6:
                    {
                        button6.BackColor = Color.DimGray;
                        break;
                    }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            loadForm(new Main());
            button1.BackColor = Color.DarkGray;
            cancelHighlight(buttonCount);
            buttonCount = 1;
            //防止点击按钮后清除之前的页面导致，menu()中的记录数据和现在设置数据不一样
            x = this.Width;
            y = this.Height;
            setTag(this);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            loadForm(new State());
            button2.BackColor = Color.DarkGray;
            cancelHighlight(buttonCount);
            buttonCount = 2;
            x = this.Width;
            y = this.Height;
            setTag(this);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            loadForm(new Manual());
            button3.BackColor = Color.DarkGray;
            cancelHighlight(buttonCount);
            buttonCount = 3;
            x = this.Width;
            y = this.Height;
            setTag(this);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            loadForm(new Examine());
            button4.BackColor = Color.DarkGray;
            cancelHighlight(buttonCount);
            buttonCount = 4;
            x = this.Width;
            y = this.Height;
            setTag(this);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            loadForm(new Maintain());
            button5.BackColor = Color.DarkGray;
            cancelHighlight(buttonCount);
            buttonCount = 5;
            x = this.Width;
            y = this.Height;
            setTag(this);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            loadForm(new Examine());
            button6.BackColor = Color.DarkGray;
            cancelHighlight(buttonCount);
            buttonCount = 6;
            x = this.Width;
            y = this.Height;
            setTag(this);
            // 扫描车箱数据，点击后给下位机发送报文  控制拍照扫描
        }
        private void buttonClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonMax_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal) {
                this.WindowState = FormWindowState.Maximized;
                button1.Image = Properties.Resources.大主界面;
                button2.Image = Properties.Resources.大系统状态;
                button3.Image = Properties.Resources.大手动功能;
                button4.Image = Properties.Resources.大装车数据;
                button5.Image = Properties.Resources.大故障与维护;
                button6.Image = Properties.Resources.大扫描数据;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
                button1.Image = Properties.Resources.主界面;
                button2.Image = Properties.Resources.系统状态;
                button3.Image = Properties.Resources.手动功能;
                button4.Image = Properties.Resources.装车数据;
                button5.Image = Properties.Resources.故障与维护;
                button6.Image = Properties.Resources.扫描数据;
            }

        }

        private void buttonMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        #region 控件大小随窗体大小等比例缩放
        private float x;//定义当前窗体的宽度
        private float y;//定义当前窗体的高度
        private void setTag(Control cons)
        {
            foreach (Control con in cons.Controls)
            {
                con.Tag = con.Width + ";" + con.Height + ";" + con.Left + ";" + con.Top + ";" + con.Font.Size;
                if (con.Controls.Count > 0)
                {
                    setTag(con);
                }
            }
        }
        private void setControls(float newx, float newy, Control cons)
        {
            //遍历窗体中的控件，重新设置控件的值
            foreach (Control con in cons.Controls)
            {
                //获取控件的Tag属性值，并分割后存储字符串数组
                if (con.Tag != null)
                {
                    string[] mytag = con.Tag.ToString().Split(new char[] { ';' });
                    //根据窗体缩放的比例确定控件的值
                    try {
                        con.Width = Convert.ToInt32(System.Convert.ToSingle(mytag[0]) * newx);//宽度
                    }
                    finally { }//if (!headPanelList.Contains(con.Name))
                    con.Height = Convert.ToInt32(System.Convert.ToSingle(mytag[1]) * newy);//高度
                    con.Left = Convert.ToInt32(System.Convert.ToSingle(mytag[2]) * newx);//左边距
                    con.Top = Convert.ToInt32(System.Convert.ToSingle(mytag[3]) * newy);//顶边距
                    Single currentSize = System.Convert.ToSingle(mytag[4]) * newy;//字体大小
                    con.Font = new Font(con.Font.Name, currentSize, con.Font.Style, con.Font.Unit);
                    if (con.Controls.Count > 0)
                    {
                        setControls(newx, newy, con);
                    }
                }
            }
        }
        private void Form1_Resize(object sender, EventArgs e)
        {
            float newx = (this.Width) / x;
            float newy = (this.Height) / y;
            setControls(newx, newy, this);
        }
        #endregion
    }
}
