/*--------------------------------------------------------------------------------------------------------------------
 Copyright (C) 2022 Himber Sacha

 This program is free software: you can redistribute it and/or modify
 it under the +terms of the GNU General Public License as published by
 the Free Software Foundation, either version 3 of the License, or
 any later version.

 This program is distributed in the hope that it will be useful,
 but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 GNU General Public License for more details.

 You should have received a copy of the GNU General Public License
 along with this program.  If not, see https://www.gnu.org/licenses/gpl-3.0.html. 

--------------------------------------------------------------------------------------------------------------------*/

using System;
using System.Runtime.InteropServices;

namespace Sky_UI
{
    public static class Win32
    {
        [DllImport("Gdi32.dll", EntryPoint = "DeleteObject")]
        public static extern bool DeleteObject(IntPtr hObject);

        /// <summary>
        ///
        /// </summary>
        /// <param name="nLeftRect"> x-coordinate of upper-left corner </param>
        /// <param name="nTopRect"> y-coordinate of upper-left corner </param>
        /// <param name="nRightRect"> x-coordinate of lower-right corner </param>
        /// <param name="nBottomRect"> y-coordinate of lower-right corner </param>
        /// <param name="nWidthEllipse"> height of ellipse </param>
        /// <param name="nHeightEllipse"> width of ellipse </param>
        /// <returns></returns>
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        public static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        [DllImport("user32.dll")]
        public static extern int SetWindowCompositionAttribute(HandleRef hWnd, in WindowCompositionAttributeData data);

        [DllImport("user32.dll")]
        public static extern int SystemParametersInfo(int uiAction, int uiParam, string pvParam, int fWinIni);

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        public extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        public extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        // ReSharper disable InconsistentNaming, UnusedMember.Local, NotAccessedField.Local
#pragma warning disable 649

        public unsafe struct WindowCompositionAttributeData
        {
            internal WCA Attribute;
            internal void* Data;
            internal int DataLength;
        }

        public enum WCA
        {
            ACCENT_POLICY = 19
        }

        public enum ACCENT
        {
            DISABLED = 0,
            ENABLE_GRADIENT = 1,
            ENABLE_TRANSPARENTGRADIENT = 2,
            ENABLE_BLURBEHIND = 3,
            ENABLE_ACRYLICBLURBEHIND = 4,
            INVALID_STATE = 5
        }

        public struct AccentPolicy
        {
            internal ACCENT AccentState;
            internal uint AccentFlags;
            internal uint GradientColor;
            internal uint AnimationId;
        }
    }
}
