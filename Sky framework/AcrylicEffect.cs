using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;

namespace Sky_framework
{
    public static class AcrylicEffect
    {
        [ObsoleteAttribute("This method is obsolete.", false)]
        public static void EnableAcrylic(IWin32Window window, Color blurColor)
        {
            if (window is null) throw new ArgumentNullException(nameof(window));

            Win32.AccentPolicy accentPolicy = new Win32.AccentPolicy
            {
                AccentState = Win32.ACCENT.ENABLE_ACRYLICBLURBEHIND,
                GradientColor = ToAbgr(blurColor)
            };

            unsafe
            {
                Win32.SetWindowCompositionAttribute(
                    new HandleRef(window, window.Handle),
                    new Win32.WindowCompositionAttributeData
                    {
                        Attribute = Win32.WCA.ACCENT_POLICY,
                        Data = &accentPolicy,
                        DataLength = Marshal.SizeOf<Win32.AccentPolicy>()
                    });
            }
        }

        private static uint ToAbgr(Color color)
        {
            return ((uint)color.A << 24)
                | ((uint)color.B << 16)
                | ((uint)color.G << 8)
                | color.R;
        }
    }
}
