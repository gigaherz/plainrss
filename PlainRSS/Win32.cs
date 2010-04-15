﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace PlainRSS
{
    public enum KeyModifiers : uint
    {
        None = 0,
        Alt = 1,
        Control = 2,
        Shift = 4,
        Windows = 8
    }

    // class that exposes needed win32 gdi functions.
    class Win32
    {
	    public enum Bool
	    {
		    False= 0,
		    True
	    };


	    [StructLayout(LayoutKind.Sequential)]
	    public struct Point
	    {
		    public Int32 x;
		    public Int32 y;

		    public Point(Int32 x, Int32 y) { this.x= x; this.y= y; }
	    }


	    [StructLayout(LayoutKind.Sequential)]
	    public struct Size {
		    public Int32 cx;
		    public Int32 cy;

		    public Size(Int32 cx, Int32 cy) { this.cx= cx; this.cy= cy; }
	    }


	    [StructLayout(LayoutKind.Sequential, Pack=1)]
	    struct ARGB
	    {
		    public byte Blue;
		    public byte Green;
		    public byte Red;
		    public byte Alpha;
	    }


	    [StructLayout(LayoutKind.Sequential, Pack=1)]
	    public struct BLENDFUNCTION
	    {
		    public byte BlendOp;
		    public byte BlendFlags;
		    public byte SourceConstantAlpha;
		    public byte AlphaFormat;
	    }


	    public const Int32 ULW_COLORKEY = 0x00000001;
	    public const Int32 ULW_ALPHA    = 0x00000002;
	    public const Int32 ULW_OPAQUE   = 0x00000004;

	    public const byte AC_SRC_OVER  = 0x00;
	    public const byte AC_SRC_ALPHA = 0x01;


	    [DllImport("user32.dll", ExactSpelling=true, SetLastError=true)]
	    public static extern Bool UpdateLayeredWindow(IntPtr hwnd, IntPtr hdcDst, ref Point pptDst, ref Size psize, IntPtr hdcSrc, ref Point pprSrc, Int32 crKey, ref BLENDFUNCTION pblend, Int32 dwFlags);

	    [DllImport("user32.dll", ExactSpelling=true, SetLastError=true)]
	    public static extern IntPtr GetDC(IntPtr hWnd);

	    [DllImport("user32.dll", ExactSpelling=true)]
	    public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

	    [DllImport("gdi32.dll", ExactSpelling=true, SetLastError=true)]
	    public static extern IntPtr CreateCompatibleDC(IntPtr hDC);

	    [DllImport("gdi32.dll", ExactSpelling=true, SetLastError=true)]
	    public static extern Bool DeleteDC(IntPtr hdc);

	    [DllImport("gdi32.dll", ExactSpelling=true)]
	    public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);

	    [DllImport("gdi32.dll", ExactSpelling=true, SetLastError=true)]
	    public static extern Bool DeleteObject(IntPtr hObject);

        public const int WM_HOTKEY = 0x312;

        [DllImport("user32.dll", ExactSpelling= true, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool RegisterHotKey(
            IntPtr hWnd,       // window to receive hot-key notification
            int id,            // identifier of hot key
            KeyModifiers mods, // key-modifier flags
            Keys vk            // virtual-key code
        );

        [DllImport("user32.dll", ExactSpelling=true, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);
    }
}
