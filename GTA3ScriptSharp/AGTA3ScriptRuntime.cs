using System;
using System.Collections.Generic;

/// <summary>
/// GTA3Script sharp namespace
/// </summary>
namespace GTA3ScriptSharp
{
    /// <summary>
    /// GTA3Script runtime abstract class
    /// </summary>
    public abstract class AGTA3ScriptRuntime
    {
        /// <summary>
        /// GTA3Script instruction set
        /// </summary>
        private GTA3ScriptInstruction[] instructionSet;

        /// <summary>
        /// Thread name
        /// </summary>
        private string threadName;

        /// <summary>
        /// Main script
        /// </summary>
        private GTA3ScriptCommand[] mainScript;

        /// <summary>
        /// Program counter
        /// </summary>
        public uint ProgramCounter { get; internal set; }

        /// <summary>
        /// Register
        /// </summary>
        public int Register { get; internal set; }

        /// <summary>
        /// Is patchable
        /// </summary>
        public bool IsPatchable { get; private set; } = true;

        /// <summary>
        /// Keywords
        /// </summary>
        private Dictionary<string, ushort> keywords = new Dictionary<string, ushort>();

        /// <summary>
        /// GTA3Script instruction set
        /// </summary>
        internal GTA3ScriptInstruction[] InstructionSet
        {
            get
            {
                if (instructionSet == null)
                {
                    instructionSet = new GTA3ScriptInstruction[0];
                }
                return instructionSet;
            }
        }

        /// <summary>
        /// GTA3Script commands
        /// </summary>
        private GTA3ScriptCommand[] Commands
        {
            get
            {
                if (mainScript == null)
                {
                    mainScript = new GTA3ScriptCommand[0];
                }
                return mainScript;
            }
        }

        /// <summary>
        /// Resize instruction set
        /// </summary>
        /// <param name="newSize">New size</param>
        private void ResizeInstructionSet(ushort newSize)
        {
            if (newSize != InstructionSet.Length)
            {
                GTA3ScriptInstruction[] instruction_set = new GTA3ScriptInstruction[newSize];
                if (newSize > 0U)
                {
                    Array.Copy(InstructionSet, instruction_set, Math.Min(InstructionSet.Length, instruction_set.Length));
                    instructionSet = instruction_set;
                }
            }
        }

        /// <summary>
        /// Patch instruction
        /// </summary>
        /// <param name="opCode">GTA3Script operation code</param>
        /// <param name="onCall">GTA3Script instruction on call event</param>
        /// <param name="argumentTypes">GTA3Script instruction argument types</param>
        /// <param name="names">Name</param>
        /// <exception cref="ArgumentException">Invalid argument</exception>
        public void Patch(ushort opCode, GTA3ScriptOpCodeCallDelegate onCall, Type[] argumentTypes, params string[] names)
        {
            Type[] argument_types = ((argumentTypes == null) ? (new Type[0]) : argumentTypes);
            string[] n = ((names == null) ? (new string[0]) : names);
            if (IsPatchable && (opCode >= 0) && (onCall != null))
            {
                if (opCode > InstructionSet.Length)
                {
                    ResizeInstructionSet((ushort)(opCode + 1));
                }
                for (int i = 0; i < argument_types.Length; i++)
                {
                    if (argument_types[i] == null)
                    {
                        throw new ArgumentException("argTypes[" + i + "] is null", "argTypes");
                    }
                }
                InstructionSet[opCode] = new GTA3ScriptInstruction(onCall, argument_types);
                foreach (string name in n)
                {
                    if (name != null)
                    {
                        if (!(keywords.ContainsKey(name)))
                        {
                            keywords.Add(name, opCode);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Execute step
        /// </summary>
        /// <exception cref="GTA3ScriptExecutionException">GTA3Script execution exception</exception>
        public bool ExecuteStep()
        {
            bool ret = false;
            if (ProgramCounter < mainScript.Length)
            {
                ref GTA3ScriptCommand command = ref mainScript[ProgramCounter];
                try
                {
                    InstructionSet[command.OpCode].Invoke(this, command.Arguments);
                    ++ProgramCounter;
                    ret = true;
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e);
                    throw new GTA3ScriptExecutionException("Error executing operation code " + command.OpCode + " at command index " + ProgramCounter, e);
                }
            }
            return ret;
        }

        /// <summary>
        /// Execute
        /// </summary>
        /// <exception cref="GTA3ScriptExecutionException">GTA3Script execution exception</exception>
        public void Execute()
        {
            while (ExecuteStep()) ;
        }

        /// <summary>
        /// Load GTA3Script
        /// </summary>
        /// <param name="script">Script</param>
        public void LoadScript(AGTA3Script script)
        {
            if (script != null)
            {
                script.LoadToRuntime(this);
            }
        }
    }
}
