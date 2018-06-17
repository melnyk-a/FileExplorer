using System.Runtime.InteropServices;

namespace FileExplorer
{
    internal sealed class NativeMethods
    {
        [DllImport("user32")]
        public static extern void SetProcessDPIAware();
    }
}