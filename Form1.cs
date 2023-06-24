using System.Drawing.Drawing2D;

namespace Lab9
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int width = 200;
        int height = 450;

        Pen whitePen = new Pen(Color.White, 1);
        Pen whiteDottedPen = new Pen(Color.White, 3);
        Brush blackBrush = new SolidBrush(Color.Black);
        Brush redBrush = new SolidBrush(Color.Red);
        Brush greyBrush = new SolidBrush(Color.FromArgb(50, 50, 50));
        int initialX = 30, initialY = 30;

        Point[] outer = new Point[2];

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Пульт от приставки";
            whiteDottedPen.DashStyle = DashStyle.Custom;
            whiteDottedPen.DashPattern = new float[] { 5, 7, 3, 4, 5, 4 };
        }

        private void DrawButton(Graphics g, Brush brush, int x, int y)
        {
            int width = 30; int height = 30;
            Rectangle r = new Rectangle(x, y, width, height);
            g.FillEllipse(brush, r);
        }

        private void DrawButtonLine(Graphics g, Brush brush, int y)
        { // Рисует ряд из трёх кнопок
            DrawButton(g, brush, initialX + 20, y);
            DrawButton(g, brush, (initialX + width / 2) - 15, y);
            DrawButton(g, brush, initialX + width - 50, y);
        }

        private void DrawStar(Graphics g, Brush brush, int x, int y)
        {
            // Никогда так не делайте. Я серьёзно. Ни-ко-гда.
            Point[] p = new Point[8];
            p[0] = new Point(x, y+50);
            p[1] = new Point(x+40, y+40);
            p[2] = new Point(x+50, y);
            p[3] = new Point(x+60, y+40);
            p[4] = new Point(x+100, y+50);
            p[5] = new Point(x+60, y+60);
            p[6] = new Point(x+50, y+100);
            p[7] = new Point(x+40, y+60);
            GraphicsPath gp = new GraphicsPath();
            gp.AddPolygon(p);

            Region r = new Region(gp);
            g.FillRegion(Brushes.Red, r);

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            
            // Сам корпус
            Rectangle outer = new Rectangle(this.initialX, this.initialY, width, height);
            g.FillRectangle(greyBrush, outer);

            // Кнопка слева сверху
            DrawButton(g, blackBrush, initialX + 20, initialY + 20);

            // Кнопка включения
            DrawButton(g, redBrush, initialX + width - 50, initialY + 20);

            int curPos = initialY + 90;

            for (int i = 0; i<4; i++)
            {
                DrawButtonLine(g, blackBrush, curPos);
                curPos += 40;
            }

            curPos += 20;

            g.DrawLine(whitePen, new Point(initialX, curPos), new Point(initialX + width, curPos));
            curPos += 10;
            g.DrawLine(whiteDottedPen, new Point(initialX, curPos), new Point(initialX + width, curPos));

            curPos += 30;
            DrawStar(g, redBrush, initialX + 50, curPos);

        }
    }
}