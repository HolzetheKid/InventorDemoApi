using System;
using System.Runtime.InteropServices;

namespace InventorDemoApi.ButtonBase
{
    // Class used to convert bitmaps and icons from their .Net native types into
    // an IPictureDisp object which is what the Inventor API requires. A typical
    // usage is shown below where MyIcon is a bitmap or icon that's available
    // as a resource of the project.
    //
    // Dim smallIcon As stdole.IPictureDisp = PictureDispConverter.ToIPictureDisp(My.Resources.MyIcon)
    /// https://adndevblog.typepad.com/manufacturing/2012/06/how-to-convert-iconbitmap-to-ipicturedisp-without-visualbasiccompatibilityvb6supporticontoipicture.html
    /// https://forums.autodesk.com/t5/inventor-customization/add-icon-for-button-ribbon/m-p/9190359#M103795
    ///
    public sealed class PictureDispConverter
    {
        [DllImport("OleAut32.dll", EntryPoint = "OleCreatePictureIndirect", ExactSpelling = true, PreserveSig = false)]
        private static extern stdole.IPictureDisp OleCreatePictureIndirect([MarshalAs(UnmanagedType.AsAny)] object picdesc, ref Guid iid, [MarshalAs(UnmanagedType.Bool)] bool fOwn);

        private static Guid iPictureDispGuid = typeof(stdole.IPictureDisp).GUID;

        private sealed class PICTDESC
        {
            private PICTDESC()
            {
            }

            // Picture Types
            public const short PICTYPE_BITMAP = 1;

            public const short PICTYPE_ICON = 3;

            [StructLayout(LayoutKind.Sequential)]
            public class Icon
            {
                internal int cbSizeOfStruct = Marshal.SizeOf(typeof(Icon));
                internal int picType = PICTYPE_ICON;
                internal IntPtr hicon = IntPtr.Zero;
                internal int unused1;
                internal int unused2;

                internal Icon(System.Drawing.Icon icon)
                {
                    hicon = icon.ToBitmap().GetHicon();
                }
            }

            [StructLayout(LayoutKind.Sequential)]
            public class Bitmap
            {
                internal int cbSizeOfStruct = Marshal.SizeOf(typeof(Bitmap));
                internal int picType = PICTYPE_BITMAP;
                internal IntPtr hbitmap = IntPtr.Zero;
                internal IntPtr hpal = IntPtr.Zero;
                internal int unused;

                internal Bitmap(System.Drawing.Bitmap bitmap)
                {
                    hbitmap = bitmap.GetHbitmap();
                }
            }
        }

        public static stdole.IPictureDisp ToIPictureDisp(System.Drawing.Icon icon)
        {
            PICTDESC.Icon pictIcon = new PICTDESC.Icon(icon);
            return OleCreatePictureIndirect(pictIcon, ref iPictureDispGuid, true);
        }

        public static stdole.IPictureDisp ToIPictureDisp(System.Drawing.Bitmap bmp)
        {
            PICTDESC.Bitmap pictBmp = new PICTDESC.Bitmap(bmp);
            return OleCreatePictureIndirect(pictBmp, ref iPictureDispGuid, true);
        }
    }
}