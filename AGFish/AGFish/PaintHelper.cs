using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using FrontedUI;

namespace AGFish
{
    /// <summary>
    /// 绘图类
    /// </summary>
    public static class PaintHelper
    {
        /// <summary>
        /// 获取Graphics用于绘制
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static Graphics CreateGraphic(this Bitmap bitmap)
        {
            var g = Graphics.FromImage(bitmap);
            g.SmoothingMode = SmoothingMode.AntiAlias;
          //  g.Clear(Color.FromKnownColor(KnownColor.ActiveCaptionText));
            return g;
        }

        /// <summary>
        /// 绘制直线
        /// </summary>
        /// <param name="g">绘图图面</param>
        /// <param name="color">颜色</param>
        /// <param name="thickness">粗细</param>
        /// <param name="start">直线起始点</param>
        /// <param name="end">直线终止点</param>
        public static void DrawLine(this Graphics g, Color color, float thickness, PointF start, PointF end)
        {
            if(g==null)
                return;
            using (var pen = new Pen(color, thickness))
            {
                 g.DrawLine(pen,start,end);
            }
        }

      
        /// <summary>
        /// 绘制矩形
        /// </summary>
        /// <param name="g">绘图图面</param>
        /// <param name="color">颜色</param>
        /// <param name="thickness">粗细</param>
        /// <param name="center">矩形中心点坐标</param>
        /// <param name="width">矩形宽度</param>
        /// <param name="height">矩形高度</param>
        /// <param name="angle">矩形角度</param>
        public static void DrawRect(this Graphics g, Color color, float thickness, PointF center, float width, float height,float angle)
        {
            if (g == null)
                return;
            using (var pen = new Pen(color, thickness))
            {
                using(var gp=new GraphicsPath())
                {
                    gp.AddRectangle(new RectangleF(center.X - width / 2f, center.Y - height / 2f, width, height));
                    using (var matrix = new Matrix())
                    {
                            matrix.RotateAt(angle,
                               center);
                            gp.Transform(matrix);
                    }
                    g.DrawPath(pen,gp);
                }
          
            }
        }
        /// <summary>
        /// 绘制圆形
        /// </summary>
        /// <param name="g">绘图图面</param>
        /// <param name="color">颜色</param>
        /// <param name="thickness">粗细</param>
        /// <param name="center">圆心坐标</param>
        /// <param name="radius">圆半径</param>
        public static void DrawCircle(this Graphics g, Color color, float thickness, PointF center, float radius)
        {
            if (g == null)
                return;
            using (var pen = new Pen(color, thickness))
            {
                g.DrawEllipse(pen, new RectangleF(center.X - radius, center.Y - radius, radius * 2, radius * 2));
            }
        }
        /// <summary>
        /// 绘制点
        /// </summary>
        /// <param name="g">绘图图面</param>
        /// <param name="color">颜色</param>
        /// <param name="p">点坐标</param>
        public static void DrawPoint(this Graphics g, Color color, PointF p)
        {
            if (g == null)
                return;
            var width = 10F;
            var p1 = p.GetEndPoint(-135F, width / 2f);
            var p2 = p.GetEndPoint(45F, width / 2f);
            g.DrawLine(color, 1f, p1, p2);
            var p3 = p.GetEndPoint(-45F, width / 2f);
            var p4 = p.GetEndPoint(135F, width / 2f);
            g.DrawLine(color, 1f, p3, p4);
        }
    }

    public static class MathHelper
    {

        /// <summary>
        /// 依据给定起点角度与距离返回线段终点
        /// </summary>
        /// <param name="start"></param>
        /// <param name="angle"></param>
        /// <param name="distance"></param>
        /// <returns></returns>
        public static PointF GetEndPoint(this PointF start, float angle, float distance)
        {
            var radian = angle * Math.PI / 180;
            var radian2 = (90 - angle) * Math.PI / 180;
            var deltaX = distance * (float)Math.Cos(radian);
            var deltaY = distance * (float)Math.Cos(radian2);
            return new PointF(start.X + deltaX, start.Y + deltaY);
        }
    }
}
