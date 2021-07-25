using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Sky_framework
{
	/// <summary>
	/// Classe permettant des interactions avec la corbeille.
	/// </summary>
	public class RecycleBin
	{
		#region Déplacements
		/// <summary>
		/// Permet de déplacer un fichier vers la corbeille.
		/// </summary>
		/// <param name="filePath">Chemin <b>complet</b> du fichier à déplacer.</param>
		/// <returns>Une instance de <see cref="RecycleBinMoveResult"/> donnant le résultat de l'opération.</returns>
		/// <exception cref="ArgumentException"><c>filePath</c> n'est pas un chemin absolu.</exception>
		/// <exception cref="FileNotFoundException">Le fichier n'existe pas.</exception>
		/// <exception cref="EntryPointNotFoundException">La méthode SHFileOperation ne peut être utilisée.</exception>
		public static RecycleBinMoveResult MoveFileToRB(string filePath)
		{
			if (!Path.IsPathRooted(filePath))
				throw new ArgumentException("Le chemin n'est pas un chemin absolu.", "filePath");

			if (!File.Exists(filePath))
				throw new FileNotFoundException("Le fichier n'existe pas.", filePath);

			if (!filePath.EndsWith("\0"))
				filePath += '\0';

			RecycleBinWin32.SHFILEOPSTRUCT fileOp = new RecycleBinWin32.SHFILEOPSTRUCT();
			fileOp.wFunc = RecycleBinWin32.FILEOP.FO_DELETE;
			fileOp.fFlags = RecycleBinWin32.FILEOP_FLAGS.FOF_ALLOWUNDO
				| RecycleBinWin32.FILEOP_FLAGS.FOF_NOCONFIRMATION
				| RecycleBinWin32.FILEOP_FLAGS.FOF_NOCONFIRMMKDIR
				| RecycleBinWin32.FILEOP_FLAGS.FOF_NOERRORUI
				| RecycleBinWin32.FILEOP_FLAGS.FOF_SILENT
				;
			fileOp.pFrom = filePath;

			int ret;

			try
			{
				ret = RecycleBinWin32.SHFileOperation(ref fileOp);
			}
			catch (EntryPointNotFoundException)
			{
				throw;
			}

			return new RecycleBinMoveResult(ret);
		}

		/// <summary>
		/// Permet de déplacer un répertoire vers la corbeille.
		/// </summary>
		/// <param name="directory">Chemin <b>complet</b> du répertoire à déplacer.</param>
		/// <returns>Une instance de <see cref="RecycleBinMoveResult"/> donnant le résultat de l'opération.</returns>
		/// <exception cref="ArgumentException"><c>directory</c> n'est pas un chemin absolu.</exception>
		/// <exception cref="DirectoryNotFoundException">Le répertoire n'existe pas.</exception>
		/// <exception cref="EntryPointNotFoundException">La méthode SHFileOperation ne peut être utilisée.</exception>
		public static RecycleBinMoveResult MoveDirectoryToRB(string directory)
		{
			if (!Path.IsPathRooted(directory))
				throw new ArgumentException("Le chemin n'est pas un chemin absolu.", "directory");

			if (!Directory.Exists(directory))
				throw new DirectoryNotFoundException("Le répertoire n'existe pas.");

			if (directory.EndsWith("\\"))
				directory = directory.Remove(directory.Length - 1, 1);

			if (!directory.EndsWith("\0"))
				directory += '\0';

			RecycleBinWin32.SHFILEOPSTRUCT fileOp = new RecycleBinWin32.SHFILEOPSTRUCT();
			fileOp.wFunc = RecycleBinWin32.FILEOP.FO_DELETE;
			fileOp.fFlags = RecycleBinWin32.FILEOP_FLAGS.FOF_ALLOWUNDO
				| RecycleBinWin32.FILEOP_FLAGS.FOF_NOCONFIRMATION
				| RecycleBinWin32.FILEOP_FLAGS.FOF_NOCONFIRMMKDIR
				| RecycleBinWin32.FILEOP_FLAGS.FOF_NOERRORUI
				| RecycleBinWin32.FILEOP_FLAGS.FOF_SILENT
				;
			fileOp.pFrom = directory;

			int ret;

			try
			{
				ret = RecycleBinWin32.SHFileOperation(ref fileOp);
			}
			catch (EntryPointNotFoundException)
			{
				throw;
			}

			return new RecycleBinMoveResult(ret);
		}
		#endregion

		#region Vider

		/// <overloads>Vide le contenu de la corbeille.</overloads>
		/// <summary>
		/// Vide tout le contenu de la corbeille.
		/// </summary>
		/// <returns>Une instance de <see cref="RecycleBinEmptyResult"/> donnant le résultat de l'opération.</returns>
		/// <exception cref="EntryPointNotFoundException">La méthode SHEmptyRecycleBin ne peut être utilisée.</exception>
		public static RecycleBinEmptyResult EmptyRB()
		{
			try
			{
				return EmptyRB(string.Empty);
			}
			catch
			{
				throw;
			}
		}

		/// <summary>
		/// Vide le contenu de la corbeille suivant le chemin de base spécifié.
		/// </summary>
		/// <param name="rootPath">
		/// <para>Chemin de la racine du lecteur dont il faut vider la corbeille.</para>
		/// <para>Ce paramètre peut être composé de sous-répertoires (c:\windows\system ..., ce qui equivaut à passer "C:\").</para>
		/// <para>Ce paramètre peut valoir <c>null</c> ou être une chaîne vide pour demander le vidage des corbeilles de tous les lecteurs, ce qui correspond à l'appel de la méthode <see cref="EmptyRB"/> sans paramètres.</para>
		/// </param>
		/// <returns>Une instance de <see cref="RecycleBinEmptyResult"/> donnant le résultat de l'opération.</returns>
		/// <exception cref="EntryPointNotFoundException">La méthode SHEmptyRecycleBin ne peut être utilisée.</exception>
		/// <exception cref="ArgumentException"><c>rootPath</c> n'est pas un chemin absolu.</exception>
		public static RecycleBinEmptyResult EmptyRB(string rootPath)
		{
			if (rootPath == null)
				rootPath = string.Empty;

			if (rootPath != string.Empty && !Path.IsPathRooted(rootPath))
				throw new ArgumentException("Le chemin n'est pas un chemin absolu.", "filePath");

			int ret;

			try
			{
				ret = RecycleBinWin32.SHEmptyRecycleBin(
					IntPtr.Zero,
					rootPath,
					RecycleBinWin32.SHERB_FLAGS.SHERB_NOCONFIRMATION
					| RecycleBinWin32.SHERB_FLAGS.SHERB_NOPROGRESSUI
					| RecycleBinWin32.SHERB_FLAGS.SHERB_NOSOUND
					);
			}
			catch (EntryPointNotFoundException)
			{
				throw;
			}

			return new RecycleBinEmptyResult(ret);
		}

		#endregion

		#region Informations

		/// <overloads>Obtient des informations sur la corbeille.</overloads>
		/// <summary>
		/// Obtient les informations (poids et nombre d'éléments) au total pour toutes les corbeilles.
		/// </summary>
		/// <returns>Une instance de <see cref="RecycleBinQueryResult"/> donnant le résultat de l'opération.</returns>
		/// <exception cref="EntryPointNotFoundException">La méthode SHQueryRecycleBin ne peut être utilisée.</exception>
		public static RecycleBinQueryResult QueryInformations()
		{
			try
			{
				return QueryInformations(string.Empty);
			}
			catch
			{
				throw;
			}
		}

		/// <summary>
		/// Obtient les informations (poids et nombre d'éléments) de la corbeille suivant le chemin de base spécifié.
		/// </summary>
		/// <param name="rootPath">
		/// <para>Chemin de la racine du lecteur dont il faut obtenir les informations.</para>
		/// <para>Ce paramètre peut être composé de sous-répertoires (c:\windows\system ..., ce qui equivaut à passer "C:\").</para>
		/// <para>Ce paramètre peut valoir <c>null</c> ou être une chaîne vide pour demander les informations totales pour les corbeilles de tous les lecteurs, ce qui correspond à l'appel de la méthode <see cref="QueryInformations"/> sans paramètres.</para>
		/// </param>
		/// <returns>Une instance de <see cref="RecycleBinQueryResult"/> donnant le résultat de l'opération.</returns>
		/// <exception cref="EntryPointNotFoundException">La méthode SHQueryRecycleBin ne peut être utilisée.</exception>
		/// <exception cref="ArgumentException"><c>rootPath</c> n'est pas un chemin absolu.</exception>
		public static RecycleBinQueryResult QueryInformations(string rootPath)
		{
			if (rootPath == null)
				rootPath = string.Empty;

			if (rootPath != string.Empty && !Path.IsPathRooted(rootPath))
				throw new ArgumentException("Le chemin n'est pas un chemin absolu.", "filePath");

			RecycleBinWin32.SHQUERYRBINFO infos = new RecycleBinWin32.SHQUERYRBINFO();
			infos.cbSize = (uint)Marshal.SizeOf(typeof(RecycleBinWin32.SHQUERYRBINFO));

			int ret;

			try
			{
				ret = RecycleBinWin32.SHQueryRecycleBin(
					rootPath,
					ref infos
					);
			}
			catch (EntryPointNotFoundException)
			{
				throw;
			}

			return new RecycleBinQueryResult(ret, infos);
		}

		#endregion
	}
}
