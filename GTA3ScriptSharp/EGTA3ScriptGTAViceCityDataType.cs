/// <summary>
/// GTA3Script sharp namespace
/// </summary>
namespace GTA3ScriptSharp
{
    /// <summary>
    /// GTA3Script GTA Vice City data type enumerator
    /// </summary>
    public enum EGTA3ScriptGTAViceCityDataType
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
        /// Immediate 32-bit signed integer
        /// Argument length: 4
        /// 
        /// - GTA III
        /// - GTA Vice City
        /// - GTA San Andreas
        /// </summary>
        Immediate32BitInt = 0x1,

        /// <summary>
        /// Global variable
        /// Argument length: 2
        /// 
        /// - GTA III
        /// - GTA Vice City
        /// - GTA San Andreas
        /// </summary>
        GlobalVariable = 0x2,

        /// <summary>
        /// Local variable
        /// Argument length: 2
        /// 
        /// - GTA III
        /// - GTA Vice City
        /// - GTA San Andreas
        /// </summary>
        LocalVariable = 0x3,

        /// <summary>
        /// Immediate 8-bit signed integer
        /// Argument length: 1
        /// 
        /// - GTA III
        /// - GTA Vice City
        /// - GTA San Andreas
        /// </summary>
        Immediate8BitInt = 0x4,

        /// <summary>
        /// Immediate 16-bit signed integer
        /// Argument length: 2
        /// 
        /// - GTA III
        /// - GTA Vice City
        /// - GTA San Andreas
        /// </summary>
        Immediate16BitInt = 0x5,

        /// <summary>
        /// Immediate 32-bit float
        /// Argument length: 4
        /// 
        /// - GTA Vice City
        /// - GTA San Andreas
        /// </summary>
        Immediate32BitFloat = 0x6
    }
}
