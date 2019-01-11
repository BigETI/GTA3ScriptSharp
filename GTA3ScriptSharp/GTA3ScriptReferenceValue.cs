/// <summary>
/// GTA3Script sharp namespace
/// </summary>
namespace GTA3ScriptSharp
{
    /// <summary>
    /// GTA3Script reference value structure
    /// </summary>
    public struct GTA3ScriptReferenceValue
    {
        /// <summary>
        /// Reference value type
        /// </summary>
        public readonly EReferenceType Type;

        /// <summary>
        /// Value offset
        /// </summary>
        public readonly ushort Offset;

        /// <summary>
        /// Is value global
        /// </summary>
        public readonly bool IsGlobal;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="type">Reference value type</param>
        /// <param name="offset">Offset</param>
        /// <param name="isGlobal">Is global</param>
        public GTA3ScriptReferenceValue(EReferenceType type, ushort offset, bool isGlobal)
        {
            Type = type;
            Offset = offset;
            IsGlobal = isGlobal;
        }
    }
}
