/// <summary>
/// GTA3Script sharp namespace
/// </summary>
namespace GTA3ScriptSharp
{
    /// <summary>
    /// GTA3Script GTA Vice City Stories data type enumerator
    /// </summary>
    public enum EGTA3ScriptGTAViceCityStoriesDataType
    {
        /// <summary>
        /// End of argument list
        /// Argument length: 0
        /// 
        /// - GTA III
        /// - GTA Vice City
        /// - GTA San Andreas
        /// - GTA Liberty City Stories
        /// - GTA Vice City Stories
        /// </summary>
        EndOfArgumentList = 0x0,

        /// <summary>
        /// Immediate 8-bit signed integer constant 0
        /// Argument length: 0
        /// 
        /// - GTA Liberty City Stories
        /// - GTA Vice City Stories
        /// </summary>
        Immediate8BitIntConst0 = 0x1,

        /// <summary>
        /// Immediate 8-bit floating-point constant 0.0
        /// Argument length: 0
        /// 
        /// - GTA Liberty City Stories
        /// - GTA Vice City Stories
        /// </summary>
        Immediate8BitFloatConst0 = 0x2,

        /// <summary>
        /// Immediate 8-bit packed floating-point
        /// Argument length: 1
        /// 
        /// - GTA Liberty City Stories
        /// - GTA Vice City Stories
        /// </summary>
        Immediate8BitPackedFloat = 0x3,

        /// <summary>
        /// Immediate 16-bit packed floating-point
        /// Argument length: 2
        /// 
        /// - GTA Liberty City Stories
        /// - GTA Vice City Stories
        /// </summary>
        Immediate16BitPackedFloat = 0x4,

        /// <summary>
        /// Immediate 24-bit packed floating-point
        /// Argument length: 3
        /// 
        /// - GTA Liberty City Stories
        /// - GTA Vice City Stories
        /// </summary>
        Immediate24BitPackedFloat = 0x5,

        /// <summary>
        /// Immediate 32-bit signed integer
        /// Argument length: 4
        /// 
        /// - GTA Liberty City Stories
        /// - GTA Vice City Stories
        /// </summary>
        Immediate32BitInt = 0x6,

        /// <summary>
        /// Immediate 8-bit integer
        /// Argument length: 1
        /// 
        /// - GTA Liberty City Stories
        /// - GTA Vice City Stories
        /// </summary>
        Immediate8BitInt = 0x7,

        /// <summary>
        /// Immediate 16-bit integer
        /// Argument length: 2
        /// 
        /// - GTA Liberty City Stories
        /// - GTA Vice City Stories
        /// </summary>
        Immediate16BitInt = 0x8,

        /// <summary>
        /// Immediate 32-bit floating-point
        /// Argument length: 4
        /// 
        /// - GTA Liberty City Stories
        /// - GTA Vice City Stories
        /// </summary>
        Immediate32BitFloat = 0x9,

        /// <summary>
        /// Immediate null terminated string
        /// Argument length: n + NUL
        /// 
        /// - GTA Liberty City Stories
        /// - GTA Vice City Stories
        /// </summary>
        ImmediateNullTerminatedString = 0xA
    }
}
