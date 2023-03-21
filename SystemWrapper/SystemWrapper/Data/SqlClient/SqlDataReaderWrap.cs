using System.Data.SqlClient;
using SystemInterface.Data.SqlClient;

namespace SystemWrapper.Data.SqlClient
{
    /// <summary>
    /// Wrapper for <see cref="T:System.Data.SqlClient.SqlDataReader"/> class.
    /// </summary>
    public class SqlDataReaderWrap : ISqlDataReader
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the SqlDataReaderWrap class (requires a subsequent call to Initialize).
        /// </summary>
        public SqlDataReaderWrap()
        {
            // this constructor assumes the caller will call the Initialize method before using
        }

        /// <summary>
        /// Initializes a new instance of the SqlDataReaderWrap class.
        /// </summary>
        /// <param name="dataReader">SqlDataReader object.</param>
        public SqlDataReaderWrap(SqlDataReader dataReader)
        {
            Initialize(dataReader);
        }

        /// <summary>
        /// Initializes a new instance of the SqlDataReaderWrap class.
        /// </summary>
        /// <param name="dataReader">SqlDataReader object.</param>
        public void Initialize(SqlDataReader dataReader)
        {
            SqlDataReaderInstance = dataReader;
        }

        #endregion Constructors

        /// <inheritdoc />
        object ISqlDataReader.this[int i]
        {
            get { return SqlDataReaderInstance[i]; }
        }

        /// <inheritdoc />
        object ISqlDataReader.this[string name]
        {
            get { return SqlDataReaderInstance[name]; }
        }

        /// <inheritdoc />
        public SqlDataReader SqlDataReaderInstance { get; private set; }

        /// <inheritdoc />
        public void Close()
        {
            SqlDataReaderInstance.Close();
        }

        /// <inheritdoc />
        public bool Read()
        {
            return SqlDataReaderInstance.Read();
        }
    }
}