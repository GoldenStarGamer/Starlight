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

        float[] vertices =
{
        // Position           // Texture coordinates
        -0.5f,  0.5f, 0.0f,   0.0f, 1.0f,  // top left
        -0.5f, -0.5f, 0.0f,   0.0f, 0.0f, // bottom left
         0.5f,  0.5f, 0.0f,   1.0f, 1.0f, // top right
         0.5f, -0.5f, 0.0f,   1.0f, 0.0f, // bottom right
        };

        uint[] indices = {  // note that we start from 0!
            0, 1, 2, // first triangle
            1, 2, 3,
        };

        int VertexBufferObject;
        int VertexArrayObject;
        int ElementBufferObject;

        public Shader shader = new();

        Texture texture = new();

        public Game(int width, int height, string title) : base(GameWindowSettings.Default, new NativeWindowSettings()
        {
            ClientSize = (width, height), Title = title
        }) { }

        protected override void OnLoad()
        {
            base.OnLoad();

            KeyRun esctoquit = new(this, Keys.Escape, () => Close());
            ents.Add(esctoquit);
            
            shader.Create("resources\\shaders\\vertex.vert", "resources\\shaders\\fragment.frag");

            GL.ClearColor(0.2f, 0.3f, 0.3f,1.0f);

            VertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
            
            VertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(VertexArrayObject);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            int texCoordLocation = shader.GetAttribLocation("aTexCoord");
            GL.EnableVertexAttribArray(texCoordLocation);
            GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);

            texture.Make("resources\\dummy.jpg");
            texture.Use();

        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            shader.Use();

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
