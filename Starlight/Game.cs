﻿using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Diagnostics;

namespace Starlight
{
    public class Game : GameWindow
    {
        private static List<Entity> ents = [];

        float[] vertices = {
            0.5f,  0.5f, 0.0f,  // top right
            0.5f, -0.5f, 0.0f,  // bottom right
            -0.5f, -0.5f, 0.0f,  // bottom left
            -0.5f,  0.5f, 0.0f   // top left
        };

        uint[] indices = {  // note that we start from 0!
            0, 1, 3,   // first triangle
            1, 2, 3    // second triangle
        };

        int VertexBufferObject;
        int VertexArrayObject;
        int ElementBufferObject;

        public Shader shader = new();

        Stopwatch timer = new();

        public Game(int width, int height, string title) : base(GameWindowSettings.Default, new NativeWindowSettings()
        {
            ClientSize = (width, height), Title = title
        }) { }

        protected override void OnLoad()
        {
            base.OnLoad();

            KeyRun esctoquit = new(this, Keys.Escape, () => Close());
            ents.Add(esctoquit);

            GL.ClearColor(0.2f, 0.3f, 0.3f,1.0f);

            VertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
            
            VertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(VertexArrayObject);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);
            
            ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);


            shader.Create("shaders\\vertex.vert", "shaders\\fragment.frag");

            timer.Start();
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            shader.Use();

            double timeValue = timer.Elapsed.TotalSeconds;
            float greenValue = (float)Math.Sin(timeValue) / 2.0f + 0.5f;
            int vertexColorLocation = GL.GetUniformLocation(shader.Handle, "vertexColor");

            if (vertexColorLocation == -1)
                throw new Exception("vertexColor NOT FOUND");

            GL.Uniform4(vertexColorLocation, 0.0f, greenValue, 0.0f, 1.0f);

            GL.BindVertexArray(VertexArrayObject);
            GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);

            SwapBuffers();
        }

        protected override void OnFramebufferResize(FramebufferResizeEventArgs e)
        {
            base.OnFramebufferResize(e);

            GL.Viewport(0, 0, e.Width, e.Height);
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        { 
            base.OnUpdateFrame(args);
            Entity.MegaUpdate(ents);
        }

        protected override void OnUnload()
        {
            shader.Dispose();
            base.OnUnload();
        }
    }
}
