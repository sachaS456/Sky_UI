using System;

namespace Sky_framework
{
	/// <summary>
	/// Fournit les résultats d'une opération de déplacement.
	/// <seealso cref="RecycleBin">RecycleBin Class</seealso>
	/// </summary>
	public class RecycleBinMoveResult
	{
		/// <summary>
		/// Initialise une nouvelle instance de <see cref="RecycleBinMoveResult"/>.
		/// </summary>
		/// <param name="ret">Code retourné par SHFileOperation.</param>
		public RecycleBinMoveResult(int ret)
		{
			if (ret == 0)
				m_success = true;

			if (ret != 0)
				m_w32Exception = new System.ComponentModel.Win32Exception(ret);
			else
				m_w32Exception = null;
		}

		#region Champs

		private bool m_success;
		private System.ComponentModel.Win32Exception m_w32Exception;

		#endregion

		#region Propriétés

		/// <summary>
		/// Obtient le résultat de l'opération.
		/// </summary>
		/// <value><c>true</c> est cas de réussite, sinon <c>false</c>.</value>
		public bool Success
		{
			get
			{
				return m_success;
			}
		}

		/// <summary>
		/// Exception Win32 éventuellement fournie.
		/// </summary>
		/// <value><c>null</c> si l'opération est une réussite.</value>
		public System.ComponentModel.Win32Exception W32Exception
		{
			get
			{
				return m_w32Exception;
			}
		}

		#endregion
	}
}
