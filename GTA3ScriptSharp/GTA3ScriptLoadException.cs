using System;
using System.Runtime.Serialization;

/// <summary>
/// GTA3Script sharp namespace
/// </summary>
namespace GTA3ScriptSharp
{
    /// <summary>
    /// GTA3Script load exception
    /// </summary>
    public class GTA3ScriptLoadException : Exception
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public GTA3ScriptLoadException() : base()
        {
            // ...
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Message</param>
        public GTA3ScriptLoadException(string message) : base(message)
        {
            // ...
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="innerException">Inner exception</param>
        public GTA3ScriptLoadException(string message, Exception innerException) : base(message, innerException)
        {
            // ...
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="info">Serialization information</param>
        /// <param name="context">Streaming context</param>
        protected GTA3ScriptLoadException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            // ...
        }
    }
}
