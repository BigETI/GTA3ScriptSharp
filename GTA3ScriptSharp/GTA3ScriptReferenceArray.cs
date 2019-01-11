/// <summary>
/// GTA3Script sharp namespace
/// </summary>
namespace GTA3ScriptSharp
{
    /// <summary>
    /// GTA3Script reference array structure
    /// </summary>
    public struct GTA3ScriptReferenceArray
    {
        /// <summary>
        /// Reference array type
        /// </summary>
        public readonly EReferenceType Type;

        /// <summary>
        /// Array offset
        /// </summary>
        public readonly ushort Offset;

        /// <summary>
        /// Is array global
        /// </summary>
        public readonly bool IsGlobal;

        /// <summary>
        /// Array index variable
        /// </summary>
        public readonly ushort IndexVariable;

        /// <summary>
        /// Array size
        /// </summary>
        public readonly byte Size;

        /// <summary>
        /// Array element type
        /// </summary>
        public readonly byte ElementType;

        /// <summary>
        /// Is array index global variable
        /// </summary>
        public readonly bool IsIndexGlobalVariable;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="type">Reference array type</param>
        /// <param name="offset">Array offset</param>
        /// <param name="isGlobal">Is array global</param>
        /// <param name="indexVariable">Array index variable</param>
        /// <param name="size">Array size</param>
        /// <param name="elementType">Array element type</param>
        /// <param name="isIndexGlobalVariable">Is array index global variable</param>
        public GTA3ScriptReferenceArray(EReferenceType type, ushort offset, bool isGlobal, ushort indexVariable, byte size, byte elementType, bool isIndexGlobalVariable)
        {
            Type = type;
            Offset = offset;
            IsGlobal = isGlobal;
            IndexVariable = indexVariable;
            Size = size;
            ElementType = elementType;
            IsIndexGlobalVariable = isIndexGlobalVariable;
        }
    }
}
