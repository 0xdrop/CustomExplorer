using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Rainmeter;

namespace CustomExplorerLibrary
{
    public class Main
    {
        public static int TaskbarHeight = 48; //Default Height to 48

        public static void ChangeTaskBarHeight(int DesiredHeight)
        {
            TaskbarHeight = DesiredHeight;
            InitCustomTaskBar();
        }
        public static void ResetTaskbar()
        {
            WindowsTasks.RestoreDesktopArea();
            WindowsTasks.ShowTaskBar();
        }
        public static void InitCustomTaskBar() {
            WindowsTasks.HideTaskBar();
            WindowsTasks.MakeNewDesktopArea();
        }
    }
}
