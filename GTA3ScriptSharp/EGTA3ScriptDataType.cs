using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// GTA3Script sharp namespace
/// </summary>
namespace GTA3ScriptSharp
{
    /// <summary>
    /// GTA3Script data type enumerator
    /// </summary>
    public enum EGTA3ScriptDataType
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
        /// Immediate 8-bit signed integer constant 0
        /// Argument length: 0
        /// 
        /// - GTA Liberty City Stories
        /// - GTA Vice City Stories
        /// </summary>
        Immediate8BitIntConst0 = 0x1,

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
        /// Immediate 8-bit floating-point constant 0.0
        /// Argument length: 0
        /// 
        /// - GTA Liberty City Stories
        /// - GTA Vice City Stories
        /// </summary>
        Immediate8BitFloatConst0 = 0x2,
        
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
        /// Immediate 8-bit packed floating-point
        /// Argument length: 1
        /// 
        /// - GTA Liberty City Stories
        /// - GTA Vice City Stories
        /// </summary>
        Immediate8BitPackedFloat = 0x3,

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
        /// Immediate 16-bit packed floating-point
        /// Argument length: 2
        /// 
        /// - GTA Liberty City Stories
        /// - GTA Vice City Stories
        /// </summary>
        Immediate16BitPackedFloat = 0x4,

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
        /// Immediate 24-bit packed floating-point
        /// Argument length: 3
        /// 
        /// - GTA Liberty City Stories
        /// - GTA Vice City Stories
        /// </summary>
        Immediate24BitPackedFloat = 0x5,

        /// <summary>
        /// Immediate 16-bit fixed-point
        /// Argument length: 2
        /// 
        /// - GTA III
        /// </summary>
        Immediate16BitFixedPoint = 0x6,

        /// <summary>
        /// Immediate 32-bit float
        /// Argument length: 4
        /// 
        /// - GTA Vice City
        /// - GTA San Andreas
        /// </summary>
        Immediate32BitFloat = 0x6,

        /// <summary>
        /// Immediate 32-bit signed integer
        /// Argument length: 4
        /// 
        /// - GTA Liberty City Stories
        /// - GTA Vice City Stories
        /// </summary>
        NewImmediate32BitInt = 0x6,

        /// <summary>
        /// Global array
        /// Argument length: 6
        /// 
        /// - GTA San Andreas
        /// </summary>
        GlobalArray = 0x7,

        /// <summary>
        /// Immediate 8-bit integer
        /// Argument length: 1
        /// 
        /// - GTA Liberty City Stories
        /// - GTA Vice City Stories
        /// </summary>
        NewImmediate8BitInt = 0x7,

        /// <summary>
        /// Local array
        /// Argument length: 6
        /// 
        /// - GTA San Andreas
        /// </summary>
        LocalArray = 0x8,

        /// <summary>
        /// Immediate 16-bit integer
        /// Argument length: 2
        /// 
        /// - GTA Liberty City Stories
        /// - GTA Vice City Stories
        /// </summary>
        NewImmediate16BitInt = 0x8,

        /// <summary>
        /// Immediate 8-byte string
        /// Argument length: 8
        /// 
        /// - GTA San Andreas
        /// </summary>
        Immediate8ByteString = 0x9,

        /// <summary>
        /// Immediate 32-bit floating-point
        /// Argument length: 4
        /// 
        /// - GTA Liberty City Stories
        /// - GTA Vice City Stories
        /// </summary>
        NewImmediate32BitFloat = 0x9,

        /// <summary>
        /// Global 8-byte string
        /// Argument length: 2
        /// 
        /// - GTA San Andreas
        /// </summary>
        Global8ByteString = 0xA,

        /// <summary>
        /// Immediate null terminated string
        /// Argument length: n + NUL
        /// 
        /// - GTA Liberty City Stories
        /// - GTA Vice City Stories
        /// </summary>
        ImmediateNullTerminatedString = 0xA,

        /// <summary>
        /// Local 8-byte string
        /// Argument length: 2
        /// 
        /// - GTA San Andreas
        /// </summary>
        Local8ByteString = 0xB,

        /// <summary>
        /// Global 8-byte string array
        /// Argument length: 6
        /// 
        /// - GTA San Andreas
        /// </summary>
        Global8ByteStringArray = 0xC,

        /// <summary>
        /// Local 8-byte string array
        /// Argument length: 6
        /// 
        /// - GTA San Andreas
        /// </summary>
        Local8ByteStringArray = 0xD,

        /// <summary>
        /// Immediate variable-length string
        /// Argument length: 1 + (n - 1)
        /// 
        /// - GTA San Andreas
        /// </summary>
        ImmediateVarLengthString = 0xE,

        /// <summary>
        /// Immediate 16-byte string
        /// Argument length: 16
        /// 
        /// - GTA San Andreas
        /// </summary>
        Immediate16ByteString = 0xF,

        /// <summary>
        /// Global 16-byte string
        /// Argument length: 2
        /// 
        /// - GTA San Andreas
        /// </summary>
        Global16ByteString = 0x10,

        /// <summary>
        /// Local 16-byte string
        /// Argument length: 2
        /// 
        /// - GTA San Andreas
        /// </summary>
        Local16ByteString = 0x11,

        /// <summary>
        /// Global 16-byte string array
        /// Argument length: 6
        /// 
        /// - GTA San Andreas
        /// </summary>
        Global16ByteStringArray = 0x12,

        /// <summary>
        /// Local 16-byte string array
        /// Argument length: 6
        /// 
        /// - GTA San Andreas
        /// </summary>
        Local16ByteStringArray = 0x13
    }
}
