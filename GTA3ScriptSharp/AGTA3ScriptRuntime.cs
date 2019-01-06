using System;

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
        /// Commands
        /// </summary>
        private GTA3ScriptCommand[] commands;

        /// <summary>
        /// Program counter
        /// </summary>
        public uint ProgramCounter { get; private set; }

        /// <summary>
        /// Is patchable
        /// </summary>
        public bool IsPatchable { get; private set; } = true;

        /// <summary>
        /// GTA3Script instruction set
        /// </summary>
        private GTA3ScriptInstruction[] InstructionSet
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
                if (commands == null)
                {
                    commands = new GTA3ScriptCommand[0];
                }
                return commands;
            }
        }

        /// <summary>
        /// Resize instruction set
        /// </summary>
        /// <param name="newSize">New size</param>
        private void ResizeInstructionSet(uint newSize)
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
        /// <exception cref="ArgumentException">Invalid argument</exception>
        public void Patch(int opCode, GTA3ScriptOpCodeCallDelegate onCall, Type[] argumentTypes)
        {
            if (IsPatchable && (opCode >= 0) && (onCall != null) && (argumentTypes != null))
            {
                if (opCode > InstructionSet.Length)
                {
                    ResizeInstructionSet((uint)opCode + 1U);
                }
                for (int i = 0; i < argumentTypes.Length; i++)
                {
                    if (argumentTypes[i] == null)
                    {
                        throw new ArgumentException("argTypes[" + i + "] is null", "argTypes");
                    }
                }
                InstructionSet[opCode] = new GTA3ScriptInstruction(onCall, argumentTypes);
            }
        }

        /// <summary>
        /// Execute step
        /// </summary>
        /// <exception cref="GTA3ScriptExecutionException">GTA3Script execution exception</exception>
        public bool ExecuteStep()
        {
            bool ret = false;
            if (ProgramCounter < commands.Length)
            {
                ref GTA3ScriptCommand command = ref commands[ProgramCounter];
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
