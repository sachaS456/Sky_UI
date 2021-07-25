using System;
using System.Runtime.InteropServices;

namespace Sky_framework
{
	/// <summary>
	/// Description résumée de Win32.
	/// </summary>
	internal class RecycleBinWin32
	{
		#region API methods

		/// <summary>
		/// Copies, moves, renames, or deletes a file system object.
		/// </summary>
		/// <param name="lpFileOp">[in] Pointer to an SHFILEOPSTRUCT structure that contains information this function needs to carry out the specified operation. This parameter must contain a valid value that is not NULL. You are responsibile for validating the value. If you do not validate it, you will experience unexpected results.</param>
		/// <returns>Returns zero if successful, or nonzero otherwise.</returns>
		[DllImport("shell32.dll")]
		public static extern int SHFileOperation(
			ref SHFILEOPSTRUCT lpFileOp
			);

		/// <summary>
		/// Empties the Recycle Bin on the specified drive.
		/// </summary>
		/// <param name="hwnd">Handle to the parent window of any dialog boxes that might be displayed during the operation. This parameter can be NULL.</param>
		/// <param name="pszRootPath">Address of a null-terminated string of maximum length MAX_PATH that contains the path of the root drive on which the Recycle Bin is located. This parameter can contain the address of a string formatted with the drive, folder, and subfolder names (c:\windows\system . . .). It can also contain an empty string or NULL. If this value is an empty string or NULL, all Recycle Bins on all drives will be emptied.</param>
		/// <param name="dwFlags">Une ou plusieurs valeurs de SHERB_FLAGS.</param>
		/// <returns>Returns S_OK if successful, or an OLE-defined error value otherwise.</returns>
		[DllImport("shell32.dll")]
		public static extern int SHEmptyRecycleBin(
			IntPtr hwnd,
			string pszRootPath,
			SHERB_FLAGS dwFlags
			);

		/// <summary>
		/// Retrieves the size of the Recycle Bin, and the number of items in it, on the specified drive.
		/// </summary>
		/// <param name="pszRootPath">Address of a null-terminated string of maximum length MAX_PATH to contain the path of the root drive on which the Recycle Bin is located. This parameter can contain the address of a string formatted with the drive, folder, and subfolder names (C:\Windows\System...).</param>
		/// <param name="pSHQueryRBInfo">Address of a SHQUERYRBINFO structure that receives the Recycle Bin information. The cbSize member of the structure must be set to the size of the structure before calling this application programming interface (API).</param>
		/// <returns>Returns S_OK if successful, or an OLE-defined error value otherwise.</returns>
		[DllImport("shell32.dll")]
		public static extern int SHQueryRecycleBin(
			string pszRootPath,
			ref SHQUERYRBINFO pSHQueryRBInfo
			);

		#endregion

		#region API consts

		/// <summary>
		/// Value that indicates which operation to perform.
		/// </summary>
		public enum FILEOP : uint
		{
			/// <summary>
			/// Move the files specified in pFrom to the location specified in pTo.
			/// </summary>
			FO_MOVE = 0x0001,

			/// <summary>
			/// Copy the files specified in the pFrom member to the location specified in the pTo member.
			/// </summary>
			FO_COPY = 0x0002,

			/// <summary>
			/// Delete the files specified in pFrom.
			/// </summary>
			FO_DELETE = 0x0003,

			/// <summary>
			/// Rename the file specified in pFrom. You cannot use this flag to rename multiple files with a single function call. Use FO_MOVE instead.
			/// </summary>
			FO_RENAME = 0x0004
		}


		/// <summary>
		/// Flags that control the file operation.
		/// </summary>
		[Flags]
		public enum FILEOP_FLAGS : ushort
		{
			/// <summary>
			/// The pTo member specifies multiple destination files (one for each source file) rather than one directory where all source files are to be deposited.
			/// </summary>
			FOF_MULTIDESTFILES = 0x0001,

			/// <summary>
			/// Not used.
			/// </summary>
			FOF_CONFIRMMOUSE = 0x0002,

			/// <summary>
			/// Do not display a progress dialog box.
			/// </summary>
			FOF_SILENT = 0x0004,

			/// <summary>
			/// Give the file being operated on a new name in a move, copy, or rename operation if a file with the target name already exists.
			/// </summary>
			FOF_RENAMEONCOLLISION = 0x0008,

			/// <summary>
			/// Respond with "Yes to All" for any dialog box that is displayed.
			/// </summary>
			FOF_NOCONFIRMATION = 0x0010,

			/// <summary>
			/// If FOF_RENAMEONCOLLISION is specified and any files were renamed, assign a name mapping object containing their old and new names to the hNameMappings member.
			/// </summary>
			FOF_WANTMAPPINGHANDLE = 0x0020,

			/// <summary>
			/// Preserve undo information, if possible. Operations can be undone only from the same process that performed the original operation. If pFrom does not contain fully qualified path and file names, this flag is ignored.
			/// </summary>
			FOF_ALLOWUNDO = 0x0040,

			/// <summary>
			/// Perform the operation on files only if a wildcard file name (*.*) is specified.
			/// </summary>
			FOF_FILESONLY = 0x0080,

			/// <summary>
			/// Display a progress dialog box but do not show the file names.
			/// </summary>
			FOF_SIMPLEPROGRESS = 0x0100,

			/// <summary>
			/// Do not confirm the creation of a new directory if the operation requires one to be created.
			/// </summary>
			FOF_NOCONFIRMMKDIR = 0x0200,

			/// <summary>
			/// Do not display a user interface if an error occurs.
			/// </summary>
			FOF_NOERRORUI = 0x0400,

			/// <summary>
			/// Version 4.71. Do not copy the security attributes of the file.
			/// </summary>
			FOF_NOCOPYSECURITYATTRIBS = 0x0800,

			/// <summary>
			/// Only operate in the local directory. Don't operate recursively into subdirectories.
			/// </summary>
			FOF_NORECURSION = 0x1000,

			/// <summary>
			/// Version 5.0. Do not move connected files as a group. Only move the specified files.
			/// </summary>
			FOF_NO_CONNECTED_ELEMENTS = 0x2000,

			/// <summary>
			/// Version 5.0. Send a warning if a file is being destroyed during a delete operation rather than recycled. This flag partially overrides FOF_NOCONFIRMATION.
			/// </summary>
			FOF_WANTNUKEWARNING = 0x4000,

			/// <summary>
			/// Treat reparse points as objects, not containers. You must set _WIN32_WINNT to 5.01 or later to use this flag. See Shell and Common Controls Versions for further discussion of versioning.
			/// </summary>
			FOF_NORECURSEREPARSE = 0x8000
		}


		/// <summary>
		/// 
		/// </summary>
		[Flags]
		public enum SHERB_FLAGS : uint
		{
			/// <summary>
			/// No dialog box confirming the deletion of the objects will be displayed.
			/// </summary>
			SHERB_NOCONFIRMATION = 0x00000001,

			/// <summary>
			/// No dialog box indicating the progress will be displayed.
			/// </summary>
			SHERB_NOPROGRESSUI = 0x00000002,

			/// <summary>
			/// No sound will be played when the operation is complete.
			/// </summary>
			SHERB_NOSOUND = 0x00000004
		}


		public const int S_OK = 0x00000000;

		#endregion

		#region API structs

		/// <summary>
		/// Contains information that the SHFileOperation function uses to perform file operations.
		/// </summary>
		[StructLayout(LayoutKind.Sequential)]
		public struct SHFILEOPSTRUCT
		{
			/// <summary>
			/// Window handle to the dialog box to display information about the status of the file operation.
			/// </summary>
			public IntPtr hwnd;

			/// <summary>
			/// Value that indicates which operation to perform.
			/// </summary>
			public FILEOP wFunc;

			/// <summary>
			/// Address of a buffer to specify one or more source file names. These names must be fully qualified paths. Standard Microsoft® MS-DOS® wild cards, such as "*", are permitted in the file-name position. Although this member is declared as a null-terminated string, it is used as a buffer to hold multiple file names. Each file name must be terminated by a single NULL character. An additional NULL character must be appended to the end of the final name to indicate the end of pFrom.
			/// </summary>
			[MarshalAs(UnmanagedType.LPStr)]
			public string pFrom;

			/// <summary>
			/// Address of a buffer to contain the name of the destination file or directory. This parameter must be set to NULL if it is not used. Like pFrom, the pTo member is also a double-null terminated string and is handled in much the same way. However, pTo must meet the following specifications. 
			/// - Wildcard characters are not supported.
			/// - Copy and Move operations can specify destination directories that do not exist and the system will attempt to create them. The system normally displays a dialog box to ask the user if they want to create the new directory. To suppress this dialog box and have the directories created silently, set the FOF_NOCONFIRMMKDIR flag in fFlags.
			/// - For Copy and Move operations, the buffer can contain multiple destination file names if the fFlags member specifies FOF_MULTIDESTFILES.
			/// - Pack multiple names into the string in the same way as for pFrom.
			/// - Use only fully-qualified paths. Using relative paths will have unpredictable results.
			/// </summary>
			[MarshalAs(UnmanagedType.LPStr)]
			public string pTo;

			/// <summary>
			/// Flags that control the file operation.
			/// </summary>
			public FILEOP_FLAGS fFlags;

			/// <summary>
			/// Value that receives TRUE if the user aborted any file operations before they were completed, or FALSE otherwise.
			/// </summary>
			public bool fAnyOperationsAborted;

			/// <summary>
			/// A handle to a name mapping object containing the old and new names of the renamed files. This member is used only if the fFlags member includes the FOF_WANTMAPPINGHANDLE flag. See Remarks for more details.
			/// </summary>
			public IntPtr hNameMappings;

			/// <summary>
			/// Address of a string to use as the title of a progress dialog box. This member is used only if fFlags includes the FOF_SIMPLEPROGRESS flag.
			/// </summary>
			[MarshalAs(UnmanagedType.LPStr)]
			public System.String lpszProgressTitle;
		}


		/// <summary>
		/// Contains the size and item count information retrieved by the SHQueryRecycleBin function.
		/// </summary>
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		public struct SHQUERYRBINFO
		{
			/// <summary>
			/// Size of the structure, in bytes. This member must be filled in prior to calling the function.
			/// </summary>
			public uint cbSize;

			/// <summary>
			/// Total size of all the objects in the specified Recycle Bin, in bytes.
			/// </summary>
			public ulong i64Size;

			/// <summary>
			/// Total number of items in the specified Recycle Bin.
			/// </summary>
			public ulong i64NumItems;
		}

		#endregion
	}
}
