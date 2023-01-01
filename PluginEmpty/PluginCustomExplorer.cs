using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Rainmeter;
using CustomExplorerLibrary;
using System.Runtime.CompilerServices;
using static CustomExplorerLibrary.WinAPI;

// Overview: This is a blank canvas on which to build your plugin.

// Note: GetString, ExecuteBang and an unnamed function for use as a section variable
// have been commented out. If you need GetString, ExecuteBang, and/or section variables 
// and you have read what they are used for from the SDK docs, uncomment the function(s)
// and/or add a function name to use for the section variable function(s). 
// Otherwise leave them commented out (or get rid of them)!

namespace PluginCustomExplorer
{
    class Measure
    {
        static public implicit operator Measure(IntPtr data)
        {
            return (Measure)GCHandle.FromIntPtr(data).Target;
        }
        public string inputStr;
        public IntPtr buffer = IntPtr.Zero;
    }

    public class Plugin
    {
        [DllExport]
        public static void Initialize(ref IntPtr data, IntPtr rm)
        {
            data = GCHandle.ToIntPtr(GCHandle.Alloc(new Measure()));
            API api = (API)rm;
            API.Log(rm, API.LogType.Notice, "Started DLL");
            Main.InitCustomTaskBar();
        }

        [DllExport]
        public static void Finalize(IntPtr data)
        {
            Measure measure = (Measure)data;
            if (measure.buffer != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(measure.buffer);
            }
            GCHandle.FromIntPtr(data).Free();

            Main.ResetTaskbar();
        }

        [DllExport]
        public static void Reload(IntPtr data, IntPtr rm, ref double maxValue)
        {
            Measure measure = (Measure)data;
            Rainmeter.API api = (Rainmeter.API)rm;

            measure.inputStr = api.ReadString("Height", "48");
            Main.ChangeTaskBarHeight(Int32.Parse(measure.inputStr));
        }

        [DllExport]
        public static double Update(IntPtr data)
        {
            Measure measure = (Measure)data;
            return 0.0;
        }

        [DllExport]
        public static void ExecuteBang(IntPtr data, [MarshalAs(UnmanagedType.LPWStr)]String args)
        {
            Measure measure = (Measure)data;
            args = args.Trim();
            if (args == "Reset")
            {
                Main.ResetTaskbar();
            } else
            {
                Main.InitCustomTaskBar();
            }
        }

        [DllExport]
        public static IntPtr GetString(IntPtr data)
        {
            Measure measure = (Measure)data;
            if (measure.buffer != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(measure.buffer);
                measure.buffer = IntPtr.Zero;
            }

            measure.buffer = Marshal.StringToHGlobalUni(measure.inputStr);
            return measure.buffer;
        }
    }
}

