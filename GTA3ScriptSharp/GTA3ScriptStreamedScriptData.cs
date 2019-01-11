using System;
/// <summary>
/// GTA3Script sharp namespace
/// </summary>
namespace GTA3ScriptSharp
{
    /// <summary>
    /// GTA3Script streamed script data
    /// </summary>
    internal struct GTA3ScriptStreamedScriptData : IComparable<GTA3ScriptStreamedScriptData>
    {
        /// <summary>
        /// File name
        /// </summary>
        public readonly string FileName;

        /// <summary>
        /// File offset
        /// </summary>
        public readonly uint FileOffset;

        /// <summary>
        /// Script size
        /// </summary>
        public readonly uint ScriptSize;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fileName">File name</param>
        /// <param name="fileOffset">File offset</param>
        /// <param name="scriptSize">Script size</param>
        internal GTA3ScriptStreamedScriptData(string fileName, uint fileOffset, uint scriptSize)
        {
            FileName = fileName;
            FileOffset = fileOffset;
            ScriptSize = scriptSize;
        }

        /// <summary>
        /// Compare to
        /// </summary>
        /// <param name="other">Other</param>
        /// <returns>Comparison result</returns>
        public int CompareTo(GTA3ScriptStreamedScriptData other)
        {
            return FileOffset.CompareTo(other.FileOffset);
        }
    }
}
