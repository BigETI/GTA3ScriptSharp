using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

/// <summary>
/// GTA3Script sharp namespace
/// </summary>
namespace GTA3ScriptSharp
{
    /// <summary>
    /// SCM class
    /// </summary>
    public class SCM : AGTA3Script
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game">Game</param>
        /// <param name="stream">Stream</param>
        internal SCM(EGame game, Stream stream) : base(game, stream)
        {
            // ...
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game">Game</param>
        /// <param name="stream">Stream</param>
        /// <param name="disposeStreamOnDispose">Dispose stream on dispose</param>
        internal SCM(EGame game, Stream stream, bool disposeStreamOnDispose) : base(game, stream, disposeStreamOnDispose)
        {
            // ...
        }

        /// <summary>
        /// CHeck SCM version
        /// </summary>
        /// <param name="version">Version</param>
        /// <returns></returns>
        private bool CheckVersion(byte[] version)
        {
            bool ret = false;
            if (version.Length == 3)
            {
                if ((version[0] == 0x2) && (version[1] == 0x0) && (version[2] == (((Game == EGame.GTALibertyCityStories) || (Game == EGame.GTAViceCityStories)) ? 6 : 1)))
                {
                    ret = true;
                }
            }
            return ret;
        }

        /// <summary>
        /// Get null terminated byte string length
        /// </summary>
        /// <param name="bytes">Bytes</param>
        /// <returns>Number of characters in byte string</returns>
        private static int GetNullTerminatedByteStringLength(byte[] bytes)
        {
            int ret = 0;
            if (bytes != null)
            {
                ret = bytes.Length;
                for (int i = 0; i < bytes.Length; i++)
                {
                    if (bytes[i] == 0)
                    {
                        ret = i;
                        break;
                    }
                }
            }
            return ret;
        }

        /// <summary>
        /// Read next header chunk offset
        /// </summary>
        /// <param name="reader">Binary reader</param>
        /// <param name="version">Version</param>
        /// <returns>Next header chunk offset</returns>
        private long ReadNextHeaderChunkOffset(BinaryReader reader, byte[] version)
        {
            if (Stream.Read(version, 0, version.Length) != version.Length)
            {
                throw new GTA3ScriptLoadException("This is not a SCM stream");
            }
            if (!(CheckVersion(version)))
            {
                throw new GTA3ScriptLoadException("This is not a SCM stream");
            }
            return reader.ReadUInt32();
        }

        /// <summary>
        /// Read global variable space
        /// </summary>
        /// <param name="reader">Binary reader</param>
        /// <param name="version">Version</param>
        /// <returns>Next header chunk offset</returns>
        private long ReadGlobalVariableSpace(BinaryReader reader, byte[] version)
        {
            long ret = ReadNextHeaderChunkOffset(reader, version);
            char game = reader.ReadChar();
            switch (game)
            {
                case 'l':
                    if (Game != EGame.GTAIII)
                    {
                        throw new GTA3ScriptLoadException("Target game is GTA III");
                    }
                    break;
                case 'm':
                    if (Game != EGame.GTAViceCity)
                    {
                        throw new GTA3ScriptLoadException("Target game is GTA Vice City");
                    }
                    break;
                case 's':
                    if (Game != EGame.GTASanAndreas)
                    {
                        throw new GTA3ScriptLoadException("Target game is GTA San Andreas");
                    }
                    break;
                default:
                    throw new GTA3ScriptLoadException("Unknown target game specifier \"" + game + "\"");
            }
            return ret;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="version"></param>
        /// <returns>Next header chunk offset</returns>
        private long ReadAlignment(BinaryReader reader, byte[] version)
        {
            long ret = ReadNextHeaderChunkOffset(reader, version);
            byte alignment = reader.ReadByte();
            if (alignment != 0)
            {
                throw new GTA3ScriptLoadException("Alignment is not zero");
            }
            return ret;
        }

        /// <summary>
        /// Read used objects
        /// </summary>
        /// <param name="reader">Binary reader</param>
        /// <param name="usedObjects">Used objects</param>
        private void ReadUsedObjects(BinaryReader reader, out string[] usedObjects)
        {
            uint num_objects = reader.ReadUInt32();
            byte[] data = new byte[24];
            usedObjects = new string[num_objects];
            for (int i = 0; i < usedObjects.Length; i++)
            {
                if (Stream.Read(data, 0, data.Length) != data.Length)
                {
                    throw new GTA3ScriptLoadException("This is not a SCM stream");
                }
                int len = GetNullTerminatedByteStringLength(data);
                usedObjects[i] = ((len > 0) ? Encoding.UTF8.GetString(data, 0, len) : "");
            }
        }

        /// <summary>
        /// Read sizes
        /// </summary>
        /// <param name="reader">Binary reader</param>
        /// <param name="version">Version</param>
        /// <param name="mainScriptSize">Main script size</param>
        /// <param name="largestMissionScriptSize">Largest mission script size</param>
        /// <param name="numMissions">Number of missions</param>
        /// <param name="numExclusiveMissions">Number of exclusive missions</param>
        /// <returns>Next header chunk offset</returns>
        private long ReadSizes(BinaryReader reader, byte[] version, out uint mainScriptSize, out uint largestMissionScriptSize, out ushort numMissions, out ushort numExclusiveMissions)
        {
            long ret = ReadNextHeaderChunkOffset(reader, version);
            mainScriptSize = reader.ReadUInt32();
            largestMissionScriptSize = reader.ReadUInt32();
            numMissions = reader.ReadUInt16();
            numExclusiveMissions = reader.ReadUInt16();
            return ret;
        }

        /// <summary>
        /// Read mission offsets
        /// </summary>
        /// <param name="reader">Binary reader</param>
        /// <param name="missionOffsets">Mission offsets</param>
        /// <param name="numMissions">Number of missions</param>
        private void ReadMissionOffsets(BinaryReader reader, out uint[] missionOffsets, ushort numMissions)
        {
            List<uint> mission_offsets = new List<uint>();
            for (uint i = 0U; i != numMissions; i++)
            {
                mission_offsets.Add(reader.ReadUInt32());
            }
            mission_offsets.Sort();
            missionOffsets = mission_offsets.ToArray();
            mission_offsets.Clear();
        }

        /// <summary>
        /// Read main script
        /// </summary>
        /// <param name="mainScriptSize">Main script size</param>
        /// <param name="mainScriptData">Main script data</param>
        private void ReadMainScript(uint mainScriptSize, out byte[] mainScriptData)
        {
            mainScriptData = new byte[mainScriptSize];
            if (Stream.Read(mainScriptData, 0, mainScriptData.Length) != mainScriptData.Length)
            {
                throw new GTA3ScriptLoadException("This is not a SCM stream");
            }
        }

        /// <summary>
        /// Read mission scripts
        /// </summary>
        /// <param name="missionOffsets">Mission offsets</param>
        /// <param name="endStreamPosition">End of stream position</param>
        /// <param name="missions">Mission</param>
        private void ReadMissionScripts(uint[] missionOffsets, long endStreamPosition, out byte[][] missions)
        {
            missions = new byte[missionOffsets.Length][];
            for (int i = 0; i < missions.Length; i++)
            {
                byte[] mission = new byte[((i < (missions.Length - 1)) ? missionOffsets[i + 1] : ((uint)(endStreamPosition))) - missionOffsets[i]];
                if (Stream.Read(mission, 0, mission.Length) != mission.Length)
                {
                    throw new GTA3ScriptLoadException("This is not a SCM stream");
                }
                missions[i] = mission;
            }
        }

        /// <summary>
        /// Read script commands
        /// </summary>
        /// <param name="runtime">GTA3Script runtime</param>
        /// <param name="script">Script</param>
        /// <returns>GTA3Script commands</returns>
        private GTA3ScriptCommand[] ReadScriptCommands(AGTA3ScriptRuntime runtime, byte[] script)
        {
            GTA3ScriptCommand[] ret = null;
            List<GTA3ScriptCommand> script_commands = new List<GTA3ScriptCommand>();
            using (MemoryStream script_stream = new MemoryStream(script))
            {
                using (BinaryReader main_script_reader = new BinaryReader(script_stream))
                {
                    Dictionary<long, uint> command_labels = new Dictionary<long, uint>();
                    Dictionary<long, List<uint>> resolve_labels = new Dictionary<long, List<uint>>();
                    script_stream.Seek(0L, SeekOrigin.Begin);
                    long script_stream_len = script_stream.Length;
                    uint command_index = 0U;
                    while (script_stream.Position < script_stream_len)
                    {
                        long command_position = script_stream.Position;
                        ushort op_code = main_script_reader.ReadUInt16();
                        if (op_code < runtime.InstructionSet.Length)
                        {
                            GTA3ScriptInstruction instruction = runtime.InstructionSet[op_code];
                            Type[] argument_types = instruction.ArgumentTypes;
                            object[] arguments = new object[argument_types.Length];
                            if (op_code == 0x2)
                            {
                                if (argument_types.Length == 2)
                                {
                                    if (argument_types[0].IsAssignableFrom(typeof(ushort)))
                                    {
                                        if (argument_types[1].IsAssignableFrom(typeof(uint)))
                                        {
                                            ushort label_id = main_script_reader.ReadUInt16();
                                            long position = main_script_reader.ReadUInt32();
                                            if (command_labels.ContainsKey(position))
                                            {
                                                arguments[0] = label_id;
                                                arguments[1] = command_labels[position];
                                                script_commands.Add(new GTA3ScriptCommand(op_code, arguments));
                                            }
                                            else
                                            {
                                                List<uint> list = null;
                                                if (resolve_labels.ContainsKey(position))
                                                {
                                                    list = resolve_labels[position];
                                                }
                                                else
                                                {
                                                    list = new List<uint>();
                                                    resolve_labels.Add(position, list);
                                                }
                                                list.Add(command_index);
                                                arguments[0] = label_id;
                                                arguments[1] = 0U;
                                                script_commands.Add(new GTA3ScriptCommand(op_code, arguments));
                                            }
                                        }
                                        else
                                        {
                                            throw new GTA3ScriptLoadException("Can not assign " + typeof(int) + " to " + argument_types[1].FullName + " at operation code " + op_code + " at command index " + command_index);
                                        }
                                    }
                                    else
                                    {
                                        throw new GTA3ScriptLoadException("Can not assign " + typeof(ushort) + " to " + argument_types[0].FullName + " at operation code " + op_code + " at command index " + command_index);
                                    }
                                }
                                else
                                {
                                    throw new GTA3ScriptLoadException("Invalid GOTO argument count at command index " + command_index);
                                }
                            }
                            else
                            {
                                for (int i = 0; i < argument_types.Length; i++)
                                {
                                    Type type = argument_types[i];
                                    byte data_type = main_script_reader.ReadByte();
                                    switch (Game)
                                    {
                                        case EGame.GTAIII:
                                            switch ((EGTA3ScriptGTAIIIDataType)data_type)
                                            {
                                                case EGTA3ScriptGTAIIIDataType.EndOfArgumentList:
                                                    // TODO
                                                    // Test
                                                    throw new GTA3ScriptLoadException("Can not assign " + ((EGTA3ScriptGTAIIIDataType)data_type) + " to " + type.FullName + " at operation code " + op_code + " at command index " + command_index);
                                                //break;
                                                case EGTA3ScriptGTAIIIDataType.Immediate32BitInt:
                                                    if (type.IsAssignableFrom(typeof(int)))
                                                    {
                                                        arguments[i] = main_script_reader.ReadInt32();
                                                    }
                                                    else
                                                    {
                                                        throw new GTA3ScriptLoadException("Can not assign " + ((EGTA3ScriptGTAIIIDataType)data_type) + " to " + type.FullName + " at operation code " + op_code + " at command index " + command_index);
                                                    }
                                                    break;
                                                case EGTA3ScriptGTAIIIDataType.GlobalVariable:
                                                    if (type.IsAssignableFrom(typeof(GTA3ScriptReferenceValue)))
                                                    {
                                                        arguments[i] = new GTA3ScriptReferenceValue(EReferenceType.IntFloat, main_script_reader.ReadUInt16(), true);
                                                    }
                                                    else
                                                    {
                                                        throw new GTA3ScriptLoadException("Can not assign " + ((EGTA3ScriptGTAIIIDataType)data_type) + " to " + type.FullName + " at operation code " + op_code + " at command index " + command_index);
                                                    }
                                                    break;
                                                case EGTA3ScriptGTAIIIDataType.LocalVariable:
                                                    if (type.IsAssignableFrom(typeof(GTA3ScriptReferenceValue)))
                                                    {
                                                        arguments[i] = new GTA3ScriptReferenceValue(EReferenceType.IntFloat, main_script_reader.ReadUInt16(), false);
                                                    }
                                                    else
                                                    {
                                                        throw new GTA3ScriptLoadException("Can not assign " + ((EGTA3ScriptGTAIIIDataType)data_type) + " to " + type.FullName + " at operation code " + op_code + " at command index " + command_index);
                                                    }
                                                    break;
                                                case EGTA3ScriptGTAIIIDataType.Immediate8BitInt:
                                                    if (type.IsAssignableFrom(typeof(sbyte)))
                                                    {
                                                        arguments[i] = main_script_reader.ReadSByte();
                                                    }
                                                    else
                                                    {
                                                        throw new GTA3ScriptLoadException("Can not assign " + ((EGTA3ScriptGTAIIIDataType)data_type) + " to " + type.FullName + " at operation code " + op_code + " at command index " + command_index);
                                                    }
                                                    break;
                                                case EGTA3ScriptGTAIIIDataType.Immediate16BitInt:
                                                    if (type.IsAssignableFrom(typeof(short)))
                                                    {
                                                        arguments[i] = main_script_reader.ReadInt16();
                                                    }
                                                    else
                                                    {
                                                        throw new GTA3ScriptLoadException("Can not assign " + ((EGTA3ScriptGTAIIIDataType)data_type) + " to " + type.FullName + " at operation code " + op_code + " at command index " + command_index);
                                                    }
                                                    break;
                                                case EGTA3ScriptGTAIIIDataType.Immediate16BitFixedPoint:
                                                    if (type.IsAssignableFrom(typeof(float)) || type.IsAssignableFrom(typeof(double)))
                                                    {
                                                        short temp = main_script_reader.ReadInt16();
                                                        arguments[i] = temp / 16.0f;
                                                    }
                                                    else
                                                    {
                                                        throw new GTA3ScriptLoadException("Can not assign " + ((EGTA3ScriptGTAIIIDataType)data_type) + " to " + type.FullName + " at operation code " + op_code + " at command index " + command_index);
                                                    }
                                                    break;
                                                default:
                                                    throw new GTA3ScriptLoadException("Invalid data type specifier " + data_type + " for " + type.FullName + " at operation code " + op_code + " at command index " + command_index);
                                            }
                                            break;
                                        case EGame.GTAViceCity:
                                            switch ((EGTA3ScriptGTAViceCityDataType)data_type)
                                            {
                                                case EGTA3ScriptGTAViceCityDataType.EndOfArgumentList:
                                                    // TODO
                                                    // Test
                                                    throw new GTA3ScriptLoadException("Can not assign " + ((EGTA3ScriptGTAViceCityDataType)data_type) + " to " + type.FullName + " at operation code " + op_code + " at command index " + command_index);
                                                //break;
                                                case EGTA3ScriptGTAViceCityDataType.Immediate8BitInt:
                                                    if (type.IsAssignableFrom(typeof(sbyte)))
                                                    {
                                                        arguments[i] = main_script_reader.ReadSByte();
                                                    }
                                                    else
                                                    {
                                                        throw new GTA3ScriptLoadException("Can not assign " + ((EGTA3ScriptGTAViceCityDataType)data_type) + " to " + type.FullName + " at operation code " + op_code + " at command index " + command_index);
                                                    }
                                                    break;
                                                case EGTA3ScriptGTAViceCityDataType.Immediate16BitInt:
                                                    if (type.IsAssignableFrom(typeof(short)))
                                                    {
                                                        arguments[i] = main_script_reader.ReadInt16();
                                                    }
                                                    else
                                                    {
                                                        throw new GTA3ScriptLoadException("Can not assign " + ((EGTA3ScriptGTAViceCityDataType)data_type) + " to " + type.FullName + " at operation code " + op_code + " at command index " + command_index);
                                                    }
                                                    break;
                                                case EGTA3ScriptGTAViceCityDataType.Immediate32BitInt:
                                                    if (type.IsAssignableFrom(typeof(int)))
                                                    {
                                                        arguments[i] = main_script_reader.ReadInt32();
                                                    }
                                                    else
                                                    {
                                                        throw new GTA3ScriptLoadException("Can not assign " + ((EGTA3ScriptGTAViceCityDataType)data_type) + " to " + type.FullName + " at operation code " + op_code + " at command index " + command_index);
                                                    }
                                                    break;
                                                case EGTA3ScriptGTAViceCityDataType.Immediate32BitFloat:
                                                    if (type.IsAssignableFrom(typeof(float)) || type.IsAssignableFrom(typeof(double)))
                                                    {
                                                        arguments[i] = main_script_reader.ReadSingle();
                                                    }
                                                    else
                                                    {
                                                        throw new GTA3ScriptLoadException("Can not assign " + ((EGTA3ScriptGTAViceCityDataType)data_type) + " to " + type.FullName + " at operation code " + op_code + " at command index " + command_index);
                                                    }
                                                    break;
                                                case EGTA3ScriptGTAViceCityDataType.GlobalVariable:
                                                    if (type.IsAssignableFrom(typeof(GTA3ScriptReferenceValue)))
                                                    {
                                                        arguments[i] = new GTA3ScriptReferenceValue(EReferenceType.IntFloat, main_script_reader.ReadUInt16(), true);
                                                    }
                                                    else
                                                    {
                                                        throw new GTA3ScriptLoadException("Can not assign " + ((EGTA3ScriptGTAViceCityDataType)data_type) + " to " + type.FullName + " at operation code " + op_code + " at command index " + command_index);
                                                    }
                                                    break;
                                                case EGTA3ScriptGTAViceCityDataType.LocalVariable:
                                                    if (type.IsAssignableFrom(typeof(GTA3ScriptReferenceValue)))
                                                    {
                                                        arguments[i] = new GTA3ScriptReferenceValue(EReferenceType.IntFloat, main_script_reader.ReadUInt16(), false);
                                                    }
                                                    else
                                                    {
                                                        throw new GTA3ScriptLoadException("Can not assign " + ((EGTA3ScriptGTAViceCityDataType)data_type) + " to " + type.FullName + " at operation code " + op_code + " at command index " + command_index);
                                                    }
                                                    break;
                                                default:
                                                    throw new GTA3ScriptLoadException("Invalid data type specifier " + data_type + " for " + type.FullName + " at operation code " + op_code + " at command index " + command_index);
                                            }
                                            break;
                                        case EGame.GTASanAndreas:
                                            switch ((EGTA3ScriptGTASanAndreasDataType)data_type)
                                            {
                                                case EGTA3ScriptGTASanAndreasDataType.EndOfArgumentList:
                                                    // TODO
                                                    // Test
                                                    throw new GTA3ScriptLoadException("Can not assign " + ((EGTA3ScriptGTASanAndreasDataType)data_type) + " to " + type.FullName + " at operation code " + op_code + " at command index " + command_index);
                                                //break;
                                                case EGTA3ScriptGTASanAndreasDataType.Immediate32BitInt:
                                                    if (type.IsAssignableFrom(typeof(int)))
                                                    {
                                                        arguments[i] = main_script_reader.ReadInt32();
                                                    }
                                                    else
                                                    {
                                                        throw new GTA3ScriptLoadException("Can not assign " + ((EGTA3ScriptGTASanAndreasDataType)data_type) + " to " + type.FullName + " at operation code " + op_code + " at command index " + command_index);
                                                    }
                                                    break;
                                                case EGTA3ScriptGTASanAndreasDataType.GlobalVariable:
                                                    if (type.IsAssignableFrom(typeof(GTA3ScriptReferenceValue)))
                                                    {
                                                        arguments[i] = new GTA3ScriptReferenceValue(EReferenceType.IntFloat, main_script_reader.ReadUInt16(), true);
                                                    }
                                                    else
                                                    {
                                                        throw new GTA3ScriptLoadException("Can not assign " + ((EGTA3ScriptGTASanAndreasDataType)data_type) + " to " + type.FullName + " at operation code " + op_code + " at command index " + command_index);
                                                    }
                                                    break;
                                                case EGTA3ScriptGTASanAndreasDataType.LocalVariable:
                                                    if (type.IsAssignableFrom(typeof(GTA3ScriptReferenceValue)))
                                                    {
                                                        arguments[i] = new GTA3ScriptReferenceValue(EReferenceType.IntFloat, main_script_reader.ReadUInt16(), false);
                                                    }
                                                    else
                                                    {
                                                        throw new GTA3ScriptLoadException("Can not assign " + ((EGTA3ScriptGTASanAndreasDataType)data_type) + " to " + type.FullName + " at operation code " + op_code + " at command index " + command_index);
                                                    }
                                                    break;
                                                case EGTA3ScriptGTASanAndreasDataType.Immediate8BitInt:
                                                    if (type.IsAssignableFrom(typeof(sbyte)))
                                                    {
                                                        arguments[i] = main_script_reader.ReadSByte();
                                                    }
                                                    else
                                                    {
                                                        throw new GTA3ScriptLoadException("Can not assign " + ((EGTA3ScriptGTASanAndreasDataType)data_type) + " to " + type.FullName + " at operation code " + op_code + " at command index " + command_index);
                                                    }
                                                    break;
                                                case EGTA3ScriptGTASanAndreasDataType.Immediate16BitInt:
                                                    if (type.IsAssignableFrom(typeof(short)))
                                                    {
                                                        arguments[i] = main_script_reader.ReadInt16();
                                                    }
                                                    else
                                                    {
                                                        throw new GTA3ScriptLoadException("Can not assign " + ((EGTA3ScriptGTASanAndreasDataType)data_type) + " to " + type.FullName + " at operation code " + op_code + " at command index " + command_index);
                                                    }
                                                    break;
                                                case EGTA3ScriptGTASanAndreasDataType.Immediate32BitFloat:
                                                    if (type.IsAssignableFrom(typeof(float)) || type.IsAssignableFrom(typeof(double)))
                                                    {
                                                        arguments[i] = main_script_reader.ReadSingle();
                                                    }
                                                    else
                                                    {
                                                        throw new GTA3ScriptLoadException("Can not assign " + ((EGTA3ScriptGTASanAndreasDataType)data_type) + " to " + type.FullName + " at operation code " + op_code + " at command index " + command_index);
                                                    }
                                                    break;
                                                case EGTA3ScriptGTASanAndreasDataType.GlobalArray:
                                                    if (type.IsAssignableFrom(typeof(GTA3ScriptReferenceArray)))
                                                    {
                                                        ushort offset = main_script_reader.ReadUInt16();
                                                        ushort index_variable = main_script_reader.ReadUInt16();
                                                        byte size = main_script_reader.ReadByte();
                                                        byte properties = main_script_reader.ReadByte();
                                                        arguments[i] = new GTA3ScriptReferenceArray(EReferenceType.IntFloat, offset, true, index_variable, size, (byte)(properties & 0x7F), (properties & 0x80) == 0x80);
                                                    }
                                                    else
                                                    {
                                                        throw new GTA3ScriptLoadException("Can not assign " + ((EGTA3ScriptGTASanAndreasDataType)data_type) + " to " + type.FullName + " at operation code " + op_code + " at command index " + command_index);
                                                    }
                                                    break;
                                                case EGTA3ScriptGTASanAndreasDataType.LocalArray:
                                                    if (type.IsAssignableFrom(typeof(GTA3ScriptReferenceArray)))
                                                    {
                                                        ushort offset = main_script_reader.ReadUInt16();
                                                        ushort index_variable = main_script_reader.ReadUInt16();
                                                        byte size = main_script_reader.ReadByte();
                                                        byte properties = main_script_reader.ReadByte();
                                                        arguments[i] = new GTA3ScriptReferenceArray(EReferenceType.IntFloat, offset, false, index_variable, size, (byte)(properties & 0x7F), (properties & 0x80) == 0x80);
                                                    }
                                                    else
                                                    {
                                                        throw new GTA3ScriptLoadException("Can not assign " + ((EGTA3ScriptGTASanAndreasDataType)data_type) + " to " + type.FullName + " at operation code " + op_code + " at command index " + command_index);
                                                    }
                                                    break;
                                                case EGTA3ScriptGTASanAndreasDataType.Immediate8ByteString:
                                                    if (type.IsAssignableFrom(typeof(string)))
                                                    {
                                                        byte[] bytes = main_script_reader.ReadBytes(8);
                                                        int len = GetNullTerminatedByteStringLength(bytes);
                                                        arguments[i] = ((len > 0) ? Encoding.UTF8.GetString(bytes, 0, len) : "");
                                                    }
                                                    else
                                                    {
                                                        throw new GTA3ScriptLoadException("Can not assign " + ((EGTA3ScriptGTASanAndreasDataType)data_type) + " to " + type.FullName + " at operation code " + op_code + " at command index " + command_index);
                                                    }
                                                    break;
                                                case EGTA3ScriptGTASanAndreasDataType.Global8ByteString:
                                                    if (type.IsAssignableFrom(typeof(GTA3ScriptReferenceValue)))
                                                    {
                                                        arguments[i] = new GTA3ScriptReferenceValue(EReferenceType.ByteString8, main_script_reader.ReadUInt16(), true);
                                                    }
                                                    else
                                                    {
                                                        throw new GTA3ScriptLoadException("Can not assign " + ((EGTA3ScriptGTASanAndreasDataType)data_type) + " to " + type.FullName + " at operation code " + op_code + " at command index " + command_index);
                                                    }
                                                    break;
                                                case EGTA3ScriptGTASanAndreasDataType.Local8ByteString:
                                                    if (type.IsAssignableFrom(typeof(GTA3ScriptReferenceValue)))
                                                    {
                                                        arguments[i] = new GTA3ScriptReferenceValue(EReferenceType.ByteString8, main_script_reader.ReadUInt16(), false);
                                                    }
                                                    else
                                                    {
                                                        throw new GTA3ScriptLoadException("Can not assign " + ((EGTA3ScriptGTASanAndreasDataType)data_type) + " to " + type.FullName + " at operation code " + op_code + " at command index " + command_index);
                                                    }
                                                    break;
                                                case EGTA3ScriptGTASanAndreasDataType.Global8ByteStringArray:
                                                    if (type.IsAssignableFrom(typeof(GTA3ScriptReferenceArray)))
                                                    {
                                                        ushort offset = main_script_reader.ReadUInt16();
                                                        ushort index_variable = main_script_reader.ReadUInt16();
                                                        byte size = main_script_reader.ReadByte();
                                                        byte properties = main_script_reader.ReadByte();
                                                        arguments[i] = new GTA3ScriptReferenceArray(EReferenceType.ByteString8, offset, true, index_variable, size, (byte)(properties & 0x7F), (properties & 0x80) == 0x80);
                                                    }
                                                    else
                                                    {
                                                        throw new GTA3ScriptLoadException("Can not assign " + ((EGTA3ScriptGTASanAndreasDataType)data_type) + " to " + type.FullName + " at operation code " + op_code + " at command index " + command_index);
                                                    }
                                                    break;
                                                case EGTA3ScriptGTASanAndreasDataType.Local8ByteStringArray:
                                                    if (type.IsAssignableFrom(typeof(GTA3ScriptReferenceArray)))
                                                    {
                                                        ushort offset = main_script_reader.ReadUInt16();
                                                        ushort index_variable = main_script_reader.ReadUInt16();
                                                        byte size = main_script_reader.ReadByte();
                                                        byte properties = main_script_reader.ReadByte();
                                                        arguments[i] = new GTA3ScriptReferenceArray(EReferenceType.ByteString8, offset, false, index_variable, size, (byte)(properties & 0x7F), (properties & 0x80) == 0x80);
                                                    }
                                                    else
                                                    {
                                                        throw new GTA3ScriptLoadException("Can not assign " + ((EGTA3ScriptGTASanAndreasDataType)data_type) + " to " + type.FullName + " at operation code " + op_code + " at command index " + command_index);
                                                    }
                                                    break;
                                                case EGTA3ScriptGTASanAndreasDataType.ImmediateVarLengthString:
                                                    if (type.IsAssignableFrom(typeof(string)))
                                                    {
                                                        byte len = main_script_reader.ReadByte();
                                                        byte[] bytes = main_script_reader.ReadBytes(len);
                                                        arguments[i] = ((len > 0) ? Encoding.UTF8.GetString(bytes) : "");
                                                    }
                                                    else
                                                    {
                                                        throw new GTA3ScriptLoadException("Can not assign " + ((EGTA3ScriptGTASanAndreasDataType)data_type) + " to " + type.FullName + " at operation code " + op_code + " at command index " + command_index);
                                                    }
                                                    break;
                                                case EGTA3ScriptGTASanAndreasDataType.Immediate16ByteString:
                                                    if (type.IsAssignableFrom(typeof(string)))
                                                    {
                                                        byte[] bytes = main_script_reader.ReadBytes(16);
                                                        int len = GetNullTerminatedByteStringLength(bytes);
                                                        arguments[i] = ((len > 0) ? Encoding.UTF8.GetString(bytes, 0, len) : "");
                                                    }
                                                    else
                                                    {
                                                        throw new GTA3ScriptLoadException("Can not assign " + ((EGTA3ScriptGTASanAndreasDataType)data_type) + " to " + type.FullName + " at operation code " + op_code + " at command index " + command_index);
                                                    }
                                                    break;
                                                case EGTA3ScriptGTASanAndreasDataType.Global16ByteString:
                                                    if (type.IsAssignableFrom(typeof(GTA3ScriptReferenceValue)))
                                                    {
                                                        arguments[i] = new GTA3ScriptReferenceValue(EReferenceType.ByteString16, main_script_reader.ReadUInt16(), true);
                                                    }
                                                    else
                                                    {
                                                        throw new GTA3ScriptLoadException("Can not assign " + ((EGTA3ScriptGTASanAndreasDataType)data_type) + " to " + type.FullName + " at operation code " + op_code + " at command index " + command_index);
                                                    }
                                                    break;
                                                case EGTA3ScriptGTASanAndreasDataType.Local16ByteString:
                                                    if (type.IsAssignableFrom(typeof(GTA3ScriptReferenceValue)))
                                                    {
                                                        arguments[i] = new GTA3ScriptReferenceValue(EReferenceType.ByteString16, main_script_reader.ReadUInt16(), false);
                                                    }
                                                    else
                                                    {
                                                        throw new GTA3ScriptLoadException("Can not assign " + ((EGTA3ScriptGTASanAndreasDataType)data_type) + " to " + type.FullName + " at operation code " + op_code + " at command index " + command_index);
                                                    }
                                                    break;
                                                case EGTA3ScriptGTASanAndreasDataType.Global16ByteStringArray:
                                                    if (type.IsAssignableFrom(typeof(GTA3ScriptReferenceArray)))
                                                    {
                                                        ushort offset = main_script_reader.ReadUInt16();
                                                        ushort index_variable = main_script_reader.ReadUInt16();
                                                        byte size = main_script_reader.ReadByte();
                                                        byte properties = main_script_reader.ReadByte();
                                                        arguments[i] = new GTA3ScriptReferenceArray(EReferenceType.ByteString16, offset, true, index_variable, size, (byte)(properties & 0x7F), (properties & 0x80) == 0x80);
                                                    }
                                                    else
                                                    {
                                                        throw new GTA3ScriptLoadException("Can not assign " + ((EGTA3ScriptGTASanAndreasDataType)data_type) + " to " + type.FullName + " at operation code " + op_code + " at command index " + command_index);
                                                    }
                                                    break;
                                                case EGTA3ScriptGTASanAndreasDataType.Local16ByteStringArray:
                                                    if (type.IsAssignableFrom(typeof(GTA3ScriptReferenceArray)))
                                                    {
                                                        ushort offset = main_script_reader.ReadUInt16();
                                                        ushort index_variable = main_script_reader.ReadUInt16();
                                                        byte size = main_script_reader.ReadByte();
                                                        byte properties = main_script_reader.ReadByte();
                                                        arguments[i] = new GTA3ScriptReferenceArray(EReferenceType.ByteString16, offset, false, index_variable, size, (byte)(properties & 0x7F), (properties & 0x80) == 0x80);
                                                    }
                                                    else
                                                    {
                                                        throw new GTA3ScriptLoadException("Can not assign " + ((EGTA3ScriptGTASanAndreasDataType)data_type) + " to " + type.FullName + " at operation code " + op_code + " at command index " + command_index);
                                                    }
                                                    break;
                                                default:
                                                    throw new GTA3ScriptLoadException("Invalid data type specifier " + data_type + " for " + type.FullName + " at operation code " + op_code + " at command index " + command_index);
                                            }
                                            break;
                                        case EGame.GTALibertyCityStories:
                                            switch ((EGTA3ScriptGTALibertyCityStoriesDataType)data_type)
                                            {
                                                case EGTA3ScriptGTALibertyCityStoriesDataType.EndOfArgumentList:
                                                    // TODO
                                                    // Test
                                                    throw new GTA3ScriptLoadException("Can not assign " + ((EGTA3ScriptGTALibertyCityStoriesDataType)data_type) + " to " + type.FullName + " at operation code " + op_code + " at command index " + command_index);
                                                //break;
                                                case EGTA3ScriptGTALibertyCityStoriesDataType.Immediate8BitIntConst0:
                                                    if (type.IsAssignableFrom(typeof(sbyte)))
                                                    {
                                                        arguments[i] = (sbyte)0;
                                                    }
                                                    else
                                                    {
                                                        throw new GTA3ScriptLoadException("Can not assign " + ((EGTA3ScriptGTALibertyCityStoriesDataType)data_type) + " to " + type.FullName + " at operation code " + op_code + " at command index " + command_index);
                                                    }
                                                    break;
                                                case EGTA3ScriptGTALibertyCityStoriesDataType.Immediate8BitFloatConst0:
                                                    if (type.IsAssignableFrom(typeof(float)))
                                                    {
                                                        arguments[i] = 0.0f;
                                                    }
                                                    else
                                                    {
                                                        throw new GTA3ScriptLoadException("Can not assign " + ((EGTA3ScriptGTALibertyCityStoriesDataType)data_type) + " to " + type.FullName + " at operation code " + op_code + " at command index " + command_index);
                                                    }
                                                    break;
                                                case EGTA3ScriptGTALibertyCityStoriesDataType.Immediate8BitPackedFloat:
                                                    if (type.IsAssignableFrom(typeof(float)))
                                                    {
                                                        arguments[i] = BitConverter.ToSingle(BitConverter.GetBytes((uint)(main_script_reader.ReadByte() << 24)), 0);
                                                    }
                                                    else
                                                    {
                                                        throw new GTA3ScriptLoadException("Can not assign " + ((EGTA3ScriptGTALibertyCityStoriesDataType)data_type) + " to " + type.FullName + " at operation code " + op_code + " at command index " + command_index);
                                                    }
                                                    break;
                                                case EGTA3ScriptGTALibertyCityStoriesDataType.Immediate16BitPackedFloat:
                                                    if (type.IsAssignableFrom(typeof(float)))
                                                    {
                                                        arguments[i] = BitConverter.ToSingle(BitConverter.GetBytes((uint)(main_script_reader.ReadUInt16() << 16)), 0);
                                                    }
                                                    else
                                                    {
                                                        throw new GTA3ScriptLoadException("Can not assign " + ((EGTA3ScriptGTALibertyCityStoriesDataType)data_type) + " to " + type.FullName + " at operation code " + op_code + " at command index " + command_index);
                                                    }
                                                    break;
                                                case EGTA3ScriptGTALibertyCityStoriesDataType.Immediate24BitPackedFloat:
                                                    if (type.IsAssignableFrom(typeof(float)))
                                                    {
                                                        uint unpacked_float = main_script_reader.ReadUInt16();
                                                        unpacked_float |= (uint)(main_script_reader.ReadByte() << 16);
                                                        arguments[i] = BitConverter.ToSingle(BitConverter.GetBytes(unpacked_float), 0);
                                                    }
                                                    else
                                                    {
                                                        throw new GTA3ScriptLoadException("Can not assign " + ((EGTA3ScriptGTALibertyCityStoriesDataType)data_type) + " to " + type.FullName + " at operation code " + op_code + " at command index " + command_index);
                                                    }
                                                    break;
                                                case EGTA3ScriptGTALibertyCityStoriesDataType.Immediate32BitInt:
                                                    if (type.IsAssignableFrom(typeof(int)))
                                                    {
                                                        arguments[i] = main_script_reader.ReadInt32();
                                                    }
                                                    else
                                                    {
                                                        throw new GTA3ScriptLoadException("Can not assign " + ((EGTA3ScriptGTALibertyCityStoriesDataType)data_type) + " to " + type.FullName + " at operation code " + op_code + " at command index " + command_index);
                                                    }
                                                    break;
                                                case EGTA3ScriptGTALibertyCityStoriesDataType.Immediate8BitInt:
                                                    if (type.IsAssignableFrom(typeof(sbyte)))
                                                    {
                                                        arguments[i] = main_script_reader.ReadSByte();
                                                    }
                                                    else
                                                    {
                                                        throw new GTA3ScriptLoadException("Can not assign " + ((EGTA3ScriptGTALibertyCityStoriesDataType)data_type) + " to " + type.FullName + " at operation code " + op_code + " at command index " + command_index);
                                                    }
                                                    break;
                                                case EGTA3ScriptGTALibertyCityStoriesDataType.Immediate16BitInt:
                                                    if (type.IsAssignableFrom(typeof(short)))
                                                    {
                                                        arguments[i] = main_script_reader.ReadInt16();
                                                    }
                                                    else
                                                    {
                                                        throw new GTA3ScriptLoadException("Can not assign " + ((EGTA3ScriptGTALibertyCityStoriesDataType)data_type) + " to " + type.FullName + " at operation code " + op_code + " at command index " + command_index);
                                                    }
                                                    break;
                                                case EGTA3ScriptGTALibertyCityStoriesDataType.Immediate32BitFloat:
                                                    if (type.IsAssignableFrom(typeof(float)))
                                                    {
                                                        arguments[i] = main_script_reader.ReadSingle();
                                                    }
                                                    else
                                                    {
                                                        throw new GTA3ScriptLoadException("Can not assign " + ((EGTA3ScriptGTALibertyCityStoriesDataType)data_type) + " to " + type.FullName + " at operation code " + op_code + " at command index " + command_index);
                                                    }
                                                    break;
                                                default:
                                                    throw new GTA3ScriptLoadException("Invalid data type specifier " + data_type + " for " + type.FullName + " at operation code " + op_code + " at command index " + command_index);
                                            }
                                            break;
                                        case EGame.GTAViceCityStories:
                                            switch ((EGTA3ScriptGTAViceCityStoriesDataType)data_type)
                                            {
                                                case EGTA3ScriptGTAViceCityStoriesDataType.EndOfArgumentList:
                                                    // TODO
                                                    // Test
                                                    throw new GTA3ScriptLoadException("Can not assign " + ((EGTA3ScriptGTAViceCityStoriesDataType)data_type) + " to " + type.FullName + " at operation code " + op_code + " at command index " + command_index);
                                                //break;
                                                case EGTA3ScriptGTAViceCityStoriesDataType.Immediate8BitIntConst0:
                                                    if (type.IsAssignableFrom(typeof(sbyte)))
                                                    {
                                                        arguments[i] = (sbyte)0;
                                                    }
                                                    else
                                                    {
                                                        throw new GTA3ScriptLoadException("Can not assign " + ((EGTA3ScriptGTAViceCityStoriesDataType)data_type) + " to " + type.FullName + " at operation code " + op_code + " at command index " + command_index);
                                                    }
                                                    break;
                                                case EGTA3ScriptGTAViceCityStoriesDataType.Immediate8BitFloatConst0:
                                                    if (type.IsAssignableFrom(typeof(float)))
                                                    {
                                                        arguments[i] = 0.0f;
                                                    }
                                                    else
                                                    {
                                                        throw new GTA3ScriptLoadException("Can not assign " + ((EGTA3ScriptGTAViceCityStoriesDataType)data_type) + " to " + type.FullName + " at operation code " + op_code + " at command index " + command_index);
                                                    }
                                                    break;
                                                case EGTA3ScriptGTAViceCityStoriesDataType.Immediate8BitPackedFloat:
                                                    if (type.IsAssignableFrom(typeof(float)))
                                                    {
                                                        arguments[i] = BitConverter.ToSingle(BitConverter.GetBytes((uint)(main_script_reader.ReadByte() << 24)), 0);
                                                    }
                                                    else
                                                    {
                                                        throw new GTA3ScriptLoadException("Can not assign " + ((EGTA3ScriptGTAViceCityStoriesDataType)data_type) + " to " + type.FullName + " at operation code " + op_code + " at command index " + command_index);
                                                    }
                                                    break;
                                                case EGTA3ScriptGTAViceCityStoriesDataType.Immediate16BitPackedFloat:
                                                    if (type.IsAssignableFrom(typeof(float)))
                                                    {
                                                        arguments[i] = BitConverter.ToSingle(BitConverter.GetBytes((uint)(main_script_reader.ReadUInt16() << 16)), 0);
                                                    }
                                                    else
                                                    {
                                                        throw new GTA3ScriptLoadException("Can not assign " + ((EGTA3ScriptGTAViceCityStoriesDataType)data_type) + " to " + type.FullName + " at operation code " + op_code + " at command index " + command_index);
                                                    }
                                                    break;
                                                case EGTA3ScriptGTAViceCityStoriesDataType.Immediate24BitPackedFloat:
                                                    if (type.IsAssignableFrom(typeof(float)))
                                                    {
                                                        uint unpacked_float = main_script_reader.ReadUInt16();
                                                        unpacked_float |= (uint)(main_script_reader.ReadByte() << 16);
                                                        arguments[i] = BitConverter.ToSingle(BitConverter.GetBytes(unpacked_float), 0);
                                                    }
                                                    else
                                                    {
                                                        throw new GTA3ScriptLoadException("Can not assign " + ((EGTA3ScriptGTAViceCityStoriesDataType)data_type) + " to " + type.FullName + " at operation code " + op_code + " at command index " + command_index);
                                                    }
                                                    break;
                                                case EGTA3ScriptGTAViceCityStoriesDataType.Immediate32BitInt:
                                                    if (type.IsAssignableFrom(typeof(int)))
                                                    {
                                                        arguments[i] = main_script_reader.ReadInt32();
                                                    }
                                                    else
                                                    {
                                                        throw new GTA3ScriptLoadException("Can not assign " + ((EGTA3ScriptGTAViceCityStoriesDataType)data_type) + " to " + type.FullName + " at operation code " + op_code + " at command index " + command_index);
                                                    }
                                                    break;
                                                case EGTA3ScriptGTAViceCityStoriesDataType.Immediate8BitInt:
                                                    if (type.IsAssignableFrom(typeof(sbyte)))
                                                    {
                                                        arguments[i] = main_script_reader.ReadSByte();
                                                    }
                                                    else
                                                    {
                                                        throw new GTA3ScriptLoadException("Can not assign " + ((EGTA3ScriptGTAViceCityStoriesDataType)data_type) + " to " + type.FullName + " at operation code " + op_code + " at command index " + command_index);
                                                    }
                                                    break;
                                                case EGTA3ScriptGTAViceCityStoriesDataType.Immediate16BitInt:
                                                    if (type.IsAssignableFrom(typeof(short)))
                                                    {
                                                        arguments[i] = main_script_reader.ReadInt16();
                                                    }
                                                    else
                                                    {
                                                        throw new GTA3ScriptLoadException("Can not assign " + ((EGTA3ScriptGTAViceCityStoriesDataType)data_type) + " to " + type.FullName + " at operation code " + op_code + " at command index " + command_index);
                                                    }
                                                    break;
                                                case EGTA3ScriptGTAViceCityStoriesDataType.Immediate32BitFloat:
                                                    if (type.IsAssignableFrom(typeof(float)))
                                                    {
                                                        arguments[i] = main_script_reader.ReadSingle();
                                                    }
                                                    else
                                                    {
                                                        throw new GTA3ScriptLoadException("Can not assign " + ((EGTA3ScriptGTAViceCityStoriesDataType)data_type) + " to " + type.FullName + " at operation code " + op_code + " at command index " + command_index);
                                                    }
                                                    break;
                                                case EGTA3ScriptGTAViceCityStoriesDataType.ImmediateNullTerminatedString:
                                                    if (type.IsAssignableFrom(typeof(string)))
                                                    {
                                                        List<byte> bytes = new List<byte>();
                                                        byte b;
                                                        while ((b = main_script_reader.ReadByte()) != 0)
                                                        {
                                                            bytes.Add(b);
                                                        }
                                                        arguments[i] = Encoding.UTF8.GetString(bytes.ToArray());
                                                        bytes.Clear();
                                                    }
                                                    else
                                                    {
                                                        throw new GTA3ScriptLoadException("Can not assign " + ((EGTA3ScriptGTAViceCityStoriesDataType)data_type) + " to " + type.FullName + " at operation code " + op_code + " at command index " + command_index);
                                                    }
                                                    break;
                                                default:
                                                    throw new GTA3ScriptLoadException("Invalid data type specifier " + data_type + " for " + type.FullName + " at operation code " + op_code + " at command index " + command_index);
                                            }
                                            break;
                                    }
                                }
                                script_commands.Add(new GTA3ScriptCommand(op_code, arguments));
                            }
                            if (resolve_labels.ContainsKey(command_position))
                            {
                                List<uint> list = resolve_labels[command_position];
                                foreach (uint resolve_label in list)
                                {
                                    GTA3ScriptCommand command = script_commands[(int)resolve_label];
                                    if (command.OpCode == 0x2)
                                    {
                                        arguments = command.Arguments.Clone() as object[];
                                        arguments[1] = command_index;
                                        script_commands[(int)resolve_label] = new GTA3ScriptCommand(command.OpCode, arguments);
                                    }
                                    instruction = runtime.InstructionSet[command.OpCode];
                                    argument_types = instruction.ArgumentTypes;
                                }
                                list.Clear();
                                resolve_labels.Remove(command_position);
                            }
                        }
                        else
                        {
                            throw new GTA3ScriptLoadException("Invalid operation code " + op_code);
                        }
                        ++command_index;
                    }
                }
            }
            ret = script_commands.ToArray();
            script_commands.Clear();
            return ret;
        }

        /// <summary>
        /// Load GTA3Script to GTA3Script runtime
        /// </summary>
        /// <param name="runtime">GTA3Script runtime</param>
        /// <exception cref="GTA3ScriptLoadException">GTA3Script load exception</exception>
        internal override void LoadToRuntime(AGTA3ScriptRuntime runtime)
        {
            try
            {
                if ((runtime != null) && (Stream != null))
                {
                    if (!(Stream.CanRead) || !(Stream.CanSeek))
                    {
                        throw new GTA3ScriptLoadException("SCM stream is not readable or seekable");
                    }
                    BinaryReader reader = new BinaryReader(Stream);
                    byte[] version = new byte[3];
                    long next_header_chunk;
                    string[] used_objects;
                    byte[] main_script;
                    byte[][] mission_scripts;
                    uint main_script_size;
                    uint largest_mission_script_size;
                    ushort num_mission_scripts;
                    ushort num_exclusive_mission_scripts;
                    uint[] mission_script_offsets;
                    uint largest_num_mission_script_local_vars;
                    uint largest_streamed_script_size;
                    uint num_streamed_scripts;
                    uint aaa_file_offset;
                    uint size_global_var_space;
                    uint streamed_script_checksum;
                    byte[][] streamed_scripts;
                    uint num_save_variables;
                    ushort[] save_variables;
                    ushort num_true_globals;
                    ushort most_globals;
                    GTA3ScriptStreamedScriptData[] streamed_script_data;
                    GTA3ScriptCommand[] main_script_commands;
                    GTA3ScriptCommand[][] mission_scripts_commands;
                    switch (Game)
                    {
                        case EGame.GTAIII:
                        case EGame.GTAViceCity:
                            next_header_chunk = ReadGlobalVariableSpace(reader, version);
                            Stream.Seek(next_header_chunk, SeekOrigin.Begin);
                            next_header_chunk = ReadAlignment(reader, version);
                            ReadUsedObjects(reader, out used_objects);
                            Stream.Seek(next_header_chunk, SeekOrigin.Begin);
                            next_header_chunk = ReadSizes(reader, version, out main_script_size, out largest_mission_script_size, out num_mission_scripts, out num_exclusive_mission_scripts);
                            ReadMissionOffsets(reader, out mission_script_offsets, num_mission_scripts);
                            Stream.Seek(next_header_chunk, SeekOrigin.Begin);
                            ReadMainScript(main_script_size, out main_script);
                            ReadMissionScripts(mission_script_offsets, Stream.Length, out mission_scripts);
                            main_script_commands = ReadScriptCommands(runtime, main_script);
                            mission_scripts_commands = new GTA3ScriptCommand[mission_scripts.Length][];
                            for (int i = 0; i < mission_scripts.Length; i++)
                            {
                                mission_scripts_commands[i] = ReadScriptCommands(runtime, mission_scripts[i]);
                            }
                            // TODO
                            break;
                        case EGame.GTASanAndreas:
                            next_header_chunk = ReadGlobalVariableSpace(reader, version);
                            Stream.Seek(next_header_chunk, SeekOrigin.Begin);
                            next_header_chunk = ReadAlignment(reader, version);
                            ReadUsedObjects(reader, out used_objects);
                            Stream.Seek(next_header_chunk, SeekOrigin.Begin);
                            next_header_chunk = ReadSizes(reader, version, out main_script_size, out largest_mission_script_size, out num_mission_scripts, out num_exclusive_mission_scripts);
                            largest_num_mission_script_local_vars = reader.ReadUInt32();
                            ReadMissionOffsets(reader, out mission_script_offsets, num_mission_scripts);
                            Stream.Seek(next_header_chunk, SeekOrigin.Begin);
                            next_header_chunk = ReadNextHeaderChunkOffset(reader, version);
                            largest_streamed_script_size = reader.ReadUInt32();
                            num_streamed_scripts = reader.ReadUInt32();
                            byte[] data = new byte[24];
                            List<GTA3ScriptStreamedScriptData> streamed_script_data_list = new List<GTA3ScriptStreamedScriptData>();
                            for (uint i = 0U; i != num_streamed_scripts; i++)
                            {
                                if (Stream.Read(data, 0, data.Length) != data.Length)
                                {
                                    throw new GTA3ScriptLoadException("This is not a SCM stream");
                                }
                                int len = GetNullTerminatedByteStringLength(data);
                                string file_name = ((len > 0) ? Encoding.UTF8.GetString(data, 0, len) : "");
                                uint file_offset = reader.ReadUInt32();
                                uint script_size = reader.ReadUInt32();
                                streamed_script_data_list.Add(new GTA3ScriptStreamedScriptData(file_name, file_offset, script_size));
                            }
                            streamed_script_data_list.Sort();
                            streamed_script_data = streamed_script_data_list.ToArray();
                            streamed_script_data_list.Clear();
                            Stream.Seek(next_header_chunk, SeekOrigin.Begin);
                            next_header_chunk = ReadNextHeaderChunkOffset(reader, version);
                            aaa_file_offset = reader.ReadUInt32();
                            Stream.Seek(next_header_chunk, SeekOrigin.Begin);
                            size_global_var_space = reader.ReadUInt32();
                            streamed_script_checksum = reader.ReadUInt32();
                            ReadMainScript(main_script_size, out main_script);
                            ReadMissionScripts(mission_script_offsets, (streamed_script_data.Length > 0) ? streamed_script_data[0].FileOffset : Stream.Length, out mission_scripts);
                            streamed_scripts = new byte[num_streamed_scripts][];
                            for (int i = 0; i < mission_scripts.Length; i++)
                            {
                                byte[] streamed_script = new byte[((i < (streamed_scripts.Length - 1)) ? streamed_script_data[i + 1].FileOffset : ((uint)(Stream.Length))) - streamed_script_data[i].FileOffset];
                                if (Stream.Read(streamed_script, 0, streamed_script.Length) != streamed_script.Length)
                                {
                                    throw new GTA3ScriptLoadException("This is not a SCM stream");
                                }
                                streamed_scripts[i] = streamed_script;
                            }
                            main_script_commands = ReadScriptCommands(runtime, main_script);
                            mission_scripts_commands = new GTA3ScriptCommand[mission_scripts.Length][];
                            for (int i = 0; i < mission_scripts.Length; i++)
                            {
                                mission_scripts_commands[i] = ReadScriptCommands(runtime, mission_scripts[i]);
                            }
                            // TODO
                            break;
                        case EGame.GTALibertyCityStories:
                        case EGame.GTAViceCityStories:
                            main_script_size = reader.ReadUInt32();
                            largest_mission_script_size = reader.ReadUInt32();
                            next_header_chunk = ReadNextHeaderChunkOffset(reader, version);
                            Stream.Seek(next_header_chunk, SeekOrigin.Begin);
                            num_save_variables = reader.ReadUInt32();
                            save_variables = new ushort[num_save_variables];
                            for (int i = 0; i < save_variables.Length; i++)
                            {
                                save_variables[i] = reader.ReadUInt16();
                            }
                            ReadUsedObjects(reader, out used_objects);
                            Stream.Seek(next_header_chunk, SeekOrigin.Begin);
                            next_header_chunk = ReadNextHeaderChunkOffset(reader, version);
                            num_true_globals = reader.ReadUInt16();
                            most_globals = reader.ReadUInt16();
                            largest_mission_script_size = reader.ReadUInt32();
                            num_mission_scripts = reader.ReadUInt16();
                            num_exclusive_mission_scripts = reader.ReadUInt16();
                            // TODO

                            // Test
                            mission_script_offsets = new uint[0];

                            Stream.Seek(next_header_chunk, SeekOrigin.Begin);
                            ReadMainScript(main_script_size, out main_script);
                            ReadMissionScripts(mission_script_offsets, Stream.Length, out mission_scripts);
                            mission_scripts_commands = new GTA3ScriptCommand[mission_scripts.Length][];
                            for (int i = 0; i < mission_scripts.Length; i++)
                            {
                                mission_scripts_commands[i] = ReadScriptCommands(runtime, mission_scripts[i]);
                            }
                            // TODO
                            break;
                    }
                    // TODO
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                throw new GTA3ScriptLoadException("SCM stream can't be loaded", e);
            }
        }
    }
}
