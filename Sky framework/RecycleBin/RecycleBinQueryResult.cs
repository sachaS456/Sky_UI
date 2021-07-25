using System;

namespace Sky_framework
{
	/// <summary>
	/// Fournit les résultats d'une demande d'informations.
	/// <seealso cref="RecycleBin">RecycleBin Class</seealso>
	/// </summary>
	public class RecycleBinQueryResult
	{
		/// <summary>
		/// Initialise une nouvelle instance de <see cref="RecycleBinQueryResult"/>.
		/// </summary>
		/// <param name="ret">Code retourné par SHQueryRecycleBin.</param>
		/// <param name="infos">Structure <see cref="RecycleBinWin32.SHQUERYRBINFO"/> utilisée avec SHQueryRecycleBin.</param>
		internal RecycleBinQueryResult(int ret, RecycleBinWin32.SHQUERYRBINFO infos)
		{
			if ( ret == RecycleBinWin32.S_OK )
			{
				m_success = true;
				m_rbSize = infos.i64Size;
				m_rbNumItems = infos.i64NumItems;
			}
			
			m_OLEErrorCode = ret;
		}

		#region Champs

		private bool m_success;
		private int m_OLEErrorCode;
		private ulong m_rbSize;
		private ulong m_rbNumItems;
		
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
		/// Obtient le code retourné par la méthode SHQueryRecycleBin.
		/// </summary>
		/// <value>Un "OLE-defined error value" si <c>Success</c> vaut <c>false</c>, sinon 0.</value>
		public int OLEErrorCode
		{
			get
			{
				return m_OLEErrorCode;
			}
		}

		/// <summary>
		/// Obtient la taille en octets de la totalité des éléments présents dans la corbeille pour laquelle les informations ont été demandées.
		/// </summary>
		public ulong RBSize
		{
			get
			{
				return m_rbSize;
			}
		}

		/// <summary>
		/// Obtient le nombre total d'éléments présents dans la corbeille pour laquelle les informations ont été demandées.
		/// </summary>
		public ulong RBNumItems
		{
			get
			{
				return m_rbNumItems;
			}
		}

		#endregion
	}
}
