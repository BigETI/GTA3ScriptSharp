/// <summary>
/// GTA3Script sharp namespace
/// </summary>
namespace GTA3ScriptSharp
{
    /// <summary>
    /// GTA3Script command class
    /// </summary>
    public struct GTA3ScriptCommand
    {
        /// <summary>
        /// GTA3Script operation code
        /// </summary>
        public readonly ushort OpCode;

        /// <summary>
        /// GTA3Script operation code arguments
        /// </summary>
        public readonly object[] Arguments;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="opCode">Operation code</param>
        /// <param name="arguments">Arguments</param>
        public GTA3ScriptCommand(ushort opCode, object[] arguments)
        {
            OpCode = opCode;
            Arguments = arguments;
        }
    }
}
