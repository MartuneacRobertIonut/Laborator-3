using System;
using System.Drawing;
using System.Threading;
using DocumentFormat.OpenXml.Office2010.Excel;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using Color = System.Drawing.Color;

namespace OpenTK_console_sample02
{
    class SimpleWindow3D : GameWindow
    {
        bool shape = true;
        KeyboardState lastKeyPress;
        Random r = new Random();
        Color randomColor1;
        Color randomColor2;

        //GL.Color3(toColor.R, toColor.G, toColor.B)

        public SimpleWindow3D() : base(800, 600)
        {
            VSync = VSyncMode.On;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            randomColor1 = Color.FromArgb(r.Next(256), r.Next(256), r.Next(256));
            randomColor2 = Color.FromArgb(r.Next(256), r.Next(256), r.Next(256));
            GL.ClearColor(Color.Blue);
            GL.Enable(EnableCap.DepthTest);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, Width, Height);

            double aspect_ratio = Width / (double)Height;

            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)aspect_ratio, 1, 64);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspective);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            KeyboardState keyboard = OpenTK.Input.Keyboard.GetState();
            MouseState mouse = OpenTK.Input.Mouse.GetState();

            if (keyboard[OpenTK.Input.Key.Escape])
            {
                Exit();
                return;
            }
            else if (keyboard[OpenTK.Input.Key.C] && !keyboard.Equals(lastKeyPress))
            {

                if (shape == true)
                {
                    shape = false;
                    randomColor1 = Color.FromArgb(r.Next(256), r.Next(256), r.Next(256));
                    randomColor2 = Color.FromArgb(r.Next(256), r.Next(256), r.Next(256));
                }
                else
                {
                    shape = true;
                    randomColor1 = Color.FromArgb(r.Next(256), r.Next(256), r.Next(256));
                    randomColor2 = Color.FromArgb(r.Next(256), r.Next(256), r.Next(256));
                }
            }
            lastKeyPress = keyboard;


            if (mouse[OpenTK.Input.MouseButton.Left])
            {
                if (shape == true)
                {
                    shape = false;
                    randomColor1 = Color.FromArgb(r.Next(256), r.Next(256), r.Next(256));
                    randomColor2 = Color.FromArgb(r.Next(256), r.Next(256), r.Next(256));
                }
                else
                {
                    shape = true;
                    randomColor1 = Color.FromArgb(r.Next(256), r.Next(256), r.Next(256));
                    randomColor2 = Color.FromArgb(r.Next(256), r.Next(256), r.Next(256));
                }
            }
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            Matrix4 lookat = Matrix4.LookAt(15, 50, 15, 0, 0, 0, 0, 1, 0);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref lookat);

            if (shape == true)
            {
                DrawTry(randomColor1, randomColor2);
                DrawAxes_OLD();

            }
            else
            {

                DrawTry(randomColor1, randomColor2);
                DrawAxes_OLD();

            }

            SwapBuffers();
            Thread.Sleep(1);
        }

        private void DrawAxes_OLD()
        {
            GL.Begin(PrimitiveType.Lines);

            // X
            GL.Color3(Color.Red);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(20, 0, 0);

            // Y
            GL.Color3(Color.Green);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 20, 0);

            // Z
            GL.Color3(Color.Yellow);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 0, 20);


            GL.End();
        }

        private void DrawTry(Color x, Color y)
        {
            GL.Begin(PrimitiveType.Triangles);

            GL.Color3(x);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 15, 0);
            GL.Vertex3(10, 0, 0);

            GL.End();
            GL.Begin(PrimitiveType.Triangles);

            GL.Color3(y);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 15, 0);
            GL.Vertex3(0, 0, 10);

            GL.End();

            GL.Begin(PrimitiveType.Triangles);

            GL.Color3(Color.Gray);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 0, 10);
            GL.Vertex3(10, 0, 0);

            GL.End();
        }

        [STAThread]
        static void Main(string[] args)
        {

            using (SimpleWindow3D example = new SimpleWindow3D())
            {

                example.Run(30.0, 0.0);
            }
        }
    }

}
