/*--------------------------------------------------------------------------------------------------------------------
 Copyright (C) 2021 Himber Sacha

 This program is free software: you can redistribute it and/or modify
 it under the +terms of the GNU General Public License as published by
 the Free Software Foundation, either version 2 of the License, or
 any later version.

 This program is distributed in the hope that it will be useful,
 but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 GNU General Public License for more details.

 You should have received a copy of the GNU General Public License
 along with this program.  If not, see https://www.gnu.org/licenses/gpl-2.0.html. 

--------------------------------------------------------------------------------------------------------------------*/

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
