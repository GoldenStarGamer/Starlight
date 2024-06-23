﻿using Microsoft.VisualBasic.FileIO;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public class Shader : IDisposable
{
    int Handle;

    public Shader(string vertexPath, string fragmentPath)
    {
        int VertexShader, FragmentShader;

        string VertexShaderSource = File.ReadAllText(vertexPath);
        string FragmentShaderSource = File.ReadAllText(fragmentPath);   

        VertexShader = GL.CreateShader(ShaderType.VertexShader);
        GL.ShaderSource(VertexShader, VertexShaderSource);

        FragmentShader = GL.CreateShader(ShaderType.FragmentShader);
        GL.ShaderSource(FragmentShader, FragmentShaderSource);

        GL.CompileShader(VertexShader);

        GL.GetShader(VertexShader, ShaderParameter.CompileStatus, out int vertSuccess);
        if (vertSuccess == 0)
        {
            string infoLog = GL.GetShaderInfoLog(VertexShader);
            Console.WriteLine(infoLog);
            throw new Exception($"VERTEX SHADER COMPILE ERROR: {infoLog}");
        }

        GL.CompileShader(FragmentShader);

        GL.GetShader(FragmentShader, ShaderParameter.CompileStatus, out int fragSuccess);
        if (fragSuccess == 0)
        {
            string infoLog = GL.GetShaderInfoLog(FragmentShader);
            throw new Exception($"FRAGMENT SHADER COMPILE ERROR: {infoLog}");
        }

        Handle = GL.CreateProgram();

        GL.AttachShader(Handle, VertexShader);
        GL.AttachShader(Handle, FragmentShader);

        GL.LinkProgram(Handle);

        GL.GetProgram(Handle, GetProgramParameterName.LinkStatus, out int success);
        if (success == 0)
        {
            string infoLog = GL.GetProgramInfoLog(Handle);
            throw new Exception($"SHADER LINKING ERROR: {infoLog}");
        }

        GL.DetachShader(Handle, VertexShader);
        GL.DetachShader(Handle, FragmentShader);
        GL.DeleteShader(FragmentShader);
        GL.DeleteShader(VertexShader);
    }

    public void Use()
    {
        GL.UseProgram(Handle);
    }

    private bool disposedValue = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            GL.DeleteProgram(Handle);

            disposedValue = true;
        }
    }

    ~Shader()
    {
        if (disposedValue == false)
        {
            Console.WriteLine("GPU Resource leak! Did you forget to call Dispose()?");
        }
    }


    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}