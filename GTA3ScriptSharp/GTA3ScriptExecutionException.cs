using System;
using System.Runtime.Serialization;

/// <summary>
/// GTA3Script sharp namespace
/// </summary>
namespace GTA3ScriptSharp
{
    /// <summary>
    /// GTA3Script execution exception
    /// </summary>
    public class GTA3ScriptExecutionException : Exception
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public GTA3ScriptExecutionException() : base()
        {
            // ...
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Message</param>
        public GTA3ScriptExecutionException(string message) : base(message)
        {
            // ...
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="innerException">Inner exception</param>
        public GTA3ScriptExecutionException(string message, Exception innerException) : base(message, innerException)
        {
            // ...
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="info">Serialization information</param>
        /// <param name="context">Streaming context</param>
        protected GTA3ScriptExecutionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            // ...
        }
    }
}
