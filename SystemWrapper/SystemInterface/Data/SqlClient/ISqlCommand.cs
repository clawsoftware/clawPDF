using System.Data.SqlClient;

namespace SystemInterface.Data.SqlClient
{
    /// <summary>
    /// Wrapper for <see cref="T:System.Data.SqlClient.SqlCommand"/> class.
    /// </summary>
    public interface ISqlCommand
    {
        // Properties

        /// <summary>
        /// Gets <see cref="T:System.Data.SqlClient.SqlCommand"/> object.
        /// </summary>
        SqlCommand SqlCommandInstance { get; }

        // Methods

        /// <summary>
        /// Sends the CommandText to the Connection and builds a SqlDataReader.
        /// </summary>
        /// <returns>A ISqlDataReaderWrap object. </returns>
        ISqlDataReader ExecuteReader();

        /// <summary>
        /// Initializes a new instance of the SqlCommandWrap class.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Initializes a new instance of the SqlCommandWrap class.
        /// </summary>
        /// <param name="command">SqlCommand object.</param>
        void Initialize(SqlCommand command);

        /// <summary>
        /// Initializes a new instance of the SqlCommandWrap class with the text of the query.
        /// </summary>
        /// <param name="cmdText">The text of the query.</param>
        void Initialize(string cmdText);

        /// <summary>
        /// Initializes a new instance of the SqlCommandWrap class with the text of the query and a ISqlConnectionWrap.
        /// </summary>
        /// <param name="cmdText">The text of the query.</param>
        /// <param name="connection">A ISqlConnectionWrap that represents the connection to an instance of SQL Server.</param>
        void Initialize(string cmdText, ISqlConnection connection);

        /*

            // Events
    [ResCategory("DataCategory_StatementCompleted"), ResDescription("DbCommand_StatementCompleted")]
    public event StatementCompletedEventHandler StatementCompleted;

    // Methods
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading=true)]
    public IAsyncResult BeginExecuteNonQuery();
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading=true)]
    public IAsyncResult BeginExecuteNonQuery(AsyncCallback callback, object stateObject);
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading=true)]
    public IAsyncResult BeginExecuteReader();
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading=true)]
    public IAsyncResult BeginExecuteReader(CommandBehavior behavior);
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading=true)]
    public IAsyncResult BeginExecuteReader(AsyncCallback callback, object stateObject);
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading=true)]
    public IAsyncResult BeginExecuteReader(AsyncCallback callback, object stateObject, CommandBehavior behavior);
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading=true)]
    public IAsyncResult BeginExecuteXmlReader();
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading=true)]
    public IAsyncResult BeginExecuteXmlReader(AsyncCallback callback, object stateObject);
    public override void Cancel();
    public SqlCommand Clone();
    public SqlParameter CreateParameter();
    public int EndExecuteNonQuery(IAsyncResult asyncResult);
    public SqlDataReader EndExecuteReader(IAsyncResult asyncResult);
    public XmlReader EndExecuteXmlReader(IAsyncResult asyncResult);
    public override int ExecuteNonQuery();
    public SqlDataReader ExecuteReader(CommandBehavior behavior);
    public override object ExecuteScalar();
    public XmlReader ExecuteXmlReader();
    public override void Prepare();
    public void ResetCommandTimeout();
    object ICloneable.Clone();

    // Properties
    public override string CommandText { get; set; }
    public override int CommandTimeout { get; set; }
    public override CommandType CommandType { get; set; }
    public SqlConnection Connection { get; set; }
    public override bool DesignTimeVisible { get; set; }
    public SqlNotificationRequest Notification { get; set; }
    public bool NotificationAutoEnlist { get; set; }
    public SqlParameterCollection Parameters { get; }
    public SqlTransaction Transaction { get; set; }
    public override UpdateRowSource UpdatedRowSource { get; set; }
*/
    }
}
