using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace LinguaSeries.Controls
{
    public class RoundedButton : Button
    {
        public int Radius { get; set; } = 20;
        public RoundedButton()
        {
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            BackColor = Theme.Accent;
            ForeColor = Theme.Text;
            Cursor = Cursors.Hand;
        }
        protected override void OnPaint(PaintEventArgs pevent)
        {
            var rect = ClientRectangle;
            using (var path = new GraphicsPath())
            {
                int d = Radius;
                path.AddArc(rect.X, rect.Y, d, d, 180, 90);
                path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
                path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
                path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
                path.CloseFigure();
                Region = new Region(path);
            }
            base.OnPaint(pevent);
        }
    }
}
