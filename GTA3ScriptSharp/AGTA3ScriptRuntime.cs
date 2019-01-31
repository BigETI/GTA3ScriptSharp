using System;
using System.Collections.Generic;
using System.Threading;

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
        /// Keywords
        /// </summary>
        private Dictionary<string, ushort> keywords = new Dictionary<string, ushort>()
        {
            { "NOP", 0x0 },
            { "WAIT", 0x1 },
            { "GOTO", 0x2 },
            { "SHAKE_CAM", 0x3 },
            { "SET_VAR_INT", 0x4 },
            { "SET_VAR_FLOAT", 0x5 },
            { "SET_LVAR_INT", 0x6 },
            { "SET_LVAR_FLOAT", 0x7 },
            { "ADD_VAL_TO_INT_VAR", 0x8 },
            { "ADD_VAL_TO_FLOAT_VAR", 0x9 },
            { "ADD_VAL_TO_INT_LVAR", 0xA },
            { "ADD_VAL_TO_FLOAT_LVAR", 0xB },
            { "SUB_VAL_FROM_INT_VAR", 0xC },
            { "SUB_VAL_FROM_FLOAT_VAR", 0xD },
            { "SUB_VAL_FROM_INT_LVAR", 0xE },
            { "SUB_VAL_FROM_FLOAT_LVAR", 0xF },
            { "MULT_INT_VAR_BY_VAL", 0x10 },
            { "MULT_FLOAT_VAR_BY_VAL", 0x11 },
            { "MULT_INT_LVAR_BY_VAL", 0x12 },
            { "MULT_FLOAT_LVAR_BY_VAL", 0x13 },
            { "DIV_INT_VAR_BY_VAL", 0x14 },
            { "DIV_FLOAT_VAR_BY_VAL", 0x15 },
            { "DIV_INT_LVAR_BY_VAL", 0x16 },
            { "DIV_FLOAT_LVAR_BY_VAL", 0x17 },
            { "IS_INT_VAR_GREATER_THAN_NUMBER", 0x18 },
            { "IS_INT_LVAR_GREATER_THAN_NUMBER", 0x19 },
            { "IS_NUMBER_GREATER_THAN_INT_VAR", 0x1A },
            { "IS_NUMBER_GREATER_THAN_INT_LVAR", 0x1B },
            { "IS_INT_VAR_GREATER_THAN_INT_VAR", 0x1C },
            { "IS_INT_LVAR_GREATER_THAN_INT_LVAR", 0x1D },
            { "IS_INT_VAR_GREATER_THAN_INT_LVAR", 0x1E },
            { "IS_INT_LVAR_GREATER_THAN_INT_VAR", 0x1F },
            { "IS_FLOAT_VAR_GREATER_THAN_NUMBER", 0x20 },
            { "IS_FLOAT_LVAR_GREATER_THAN_NUMBER", 0x21 },
            { "IS_NUMBER_GREATER_THAN_FLOAT_VAR", 0x22 },
            { "IS_NUMBER_GREATER_THAN_FLOAT_LVAR", 0x23 },
            { "IS_FLOAT_VAR_GREATER_THAN_FLOAT_VAR", 0x24 },
            { "IS_FLOAT_LVAR_GREATER_THAN_FLOAT_LVAR", 0x25 },
            { "IS_FLOAT_VAR_GREATER_THAN_FLOAT_LVAR", 0x26 },
            { "IS_FLOAT_LVAR_GREATER_THAN_FLOAT_VAR", 0x27 },
            { "IS_INT_VAR_GREATER_OR_EQUAL_TO_NUMBER", 0x28 },
            { "IS_INT_LVAR_GREATER_OR_EQUAL_TO_NUMBER", 0x29 },
            { "IS_NUMBER_GREATER_OR_EQUAL_TO_INT_VAR", 0x2A },
            { "IS_NUMBER_GREATER_OR_EQUAL_TO_INT_LVAR", 0x2B },
            { "IS_INT_VAR_GREATER_OR_EQUAL_TO_INT_VAR", 0x2C },
            { "IS_INT_LVAR_GREATER_OR_EQUAL_TO_INT_LVAR", 0x2D },
            { "IS_INT_VAR_GREATER_OR_EQUAL_TO_INT_LVAR", 0x2E },
            { "IS_INT_LVAR_GREATER_OR_EQUAL_TO_INT_VAR", 0x2F },
            { "IS_FLOAT_VAR_GREATER_OR_EQUAL_TO_NUMBER", 0x30 },
            { "IS_FLOAT_LVAR_GREATER_OR_EQUAL_TO_NUMBER", 0x31 },
            { "IS_NUMBER_GREATER_OR_EQUAL_TO_FLOAT_VAR", 0x32 },
            { "IS_NUMBER_GREATER_OR_EQUAL_TO_FLOAT_LVAR", 0x33 },
            { "IS_FLOAT_VAR_GREATER_OR_EQUAL_TO_FLOAT_VAR", 0x34 },
            { "IS_FLOAT_LVAR_GREATER_OR_EQUAL_TO_FLOAT_LVAR", 0x35 },
            { "IS_FLOAT_VAR_GREATER_OR_EQUAL_TO_FLOAT_LVAR", 0x36 },
            { "IS_FLOAT_LVAR_GREATER_OR_EQUAL_TO_FLOAT_VAR", 0x37 },
            { "IS_INT_VAR_EQUAL_TO_NUMBER", 0x38 },
            { "IS_INT_LVAR_EQUAL_TO_NUMBER", 0x39 },
            { "IS_INT_VAR_EQUAL_TO_INT_VAR", 0x3A },
            { "IS_INT_LVAR_EQUAL_TO_INT_LVAR", 0x3B },
            { "IS_INT_VAR_EQUAL_TO_INT_LVAR", 0x3C },
            { "IS_FLOAT_VAR_EQUAL_TO_NUMBER", 0x42 },
            { "IS_FLOAT_LVAR_EQUAL_TO_NUMBER", 0x43 },
            { "IS_FLOAT_VAR_EQUAL_TO_FLOAT_VAR", 0x44 },
            { "IS_FLOAT_LVAR_EQUAL_TO_FLOAT_LVAR", 0x45 },
            { "IS_FLOAT_VAR_EQUAL_TO_FLOAT_LVAR", 0x46 },
            { "GOTO_IF_FALSE", 0x4D },
            { "TERMINATE_THIS_SCRIPT", 0x4E },
            { "MISSION_END", 0x4E }

            // TODO
        };

        /// <summary>
        /// GTA3Script instruction set
        /// </summary>
        private GTA3ScriptInstruction[] instructionSet = new GTA3ScriptInstruction[]
        {
            // 0x0
            new GTA3ScriptInstruction(new Type[0], (runtime, arguments) =>
            {
                // ...
            }),

            // 0x1
            new GTA3ScriptInstruction(new Type[] { typeof(int) }, (runtime, arguments) =>
            {
                Thread.Sleep((int)(arguments[0]));
            }),

            // 0x2
            new GTA3ScriptInstruction(new Type[] { typeof(ushort), typeof(uint) }, (runtime, arguments) =>
            {
                runtime.CurrentCommandIndex = (uint)(arguments[1]);
            }),

            // 0x3
            new GTA3ScriptInstruction(new Type[] { typeof(int) }, null),

            // 0x4
            new GTA3ScriptInstruction(new Type[] { typeof(GTA3ScriptReferenceValue), typeof(int) }, (runtime, arguments) =>
            {
                // TODO
                throw new NotImplementedException();
            }),
            
            // 0x5
            new GTA3ScriptInstruction(new Type[] { typeof(GTA3ScriptReferenceValue), typeof(float) }, (runtime, arguments) =>
            {
                // TODO
                throw new NotImplementedException();
            }),
            
            // 0x6
            new GTA3ScriptInstruction(new Type[] { typeof(GTA3ScriptReferenceValue), typeof(int) }, (runtime, arguments) =>
            {
                // TODO
                throw new NotImplementedException();
            }),
            
            // 0x7
            new GTA3ScriptInstruction(new Type[] { typeof(GTA3ScriptReferenceValue), typeof(float) }, (runtime, arguments) =>
            {
                // TODO
                throw new NotImplementedException();
            }),
            
            // 0x8
            new GTA3ScriptInstruction(new Type[] { typeof(GTA3ScriptReferenceValue), typeof(int) }, (runtime, arguments) =>
            {
                // TODO
                throw new NotImplementedException();
            }),
            
            // 0x9
            new GTA3ScriptInstruction(new Type[] { typeof(GTA3ScriptReferenceValue), typeof(float) }, (runtime, arguments) =>
            {
                // TODO
                throw new NotImplementedException();
            }),
            
            // 0xA
            new GTA3ScriptInstruction(new Type[] { typeof(GTA3ScriptReferenceValue), typeof(int) }, (runtime, arguments) =>
            {
                // TODO
                throw new NotImplementedException();
            }),
            
            // 0xB
            new GTA3ScriptInstruction(new Type[] { typeof(GTA3ScriptReferenceValue), typeof(float) }, (runtime, arguments) =>
            {
                // TODO
                throw new NotImplementedException();
            }),
            
            // 0xC
            new GTA3ScriptInstruction(new Type[] { typeof(GTA3ScriptReferenceValue), typeof(int) }, (runtime, arguments) =>
            {
                // TODO
                throw new NotImplementedException();
            }),
            
            // 0xD
            new GTA3ScriptInstruction(new Type[] { typeof(GTA3ScriptReferenceValue), typeof(float) }, (runtime, arguments) =>
            {
                // TODO
                throw new NotImplementedException();
            }),
            
            // 0xE
            new GTA3ScriptInstruction(new Type[] { typeof(GTA3ScriptReferenceValue), typeof(int) }, (runtime, arguments) =>
            {
                // TODO
                throw new NotImplementedException();
            }),
            
            // 0xF
            new GTA3ScriptInstruction(new Type[] { typeof(GTA3ScriptReferenceValue), typeof(float) }, (runtime, arguments) =>
            {
                // TODO
                throw new NotImplementedException();
            }),
            
            // 0x10
            new GTA3ScriptInstruction(new Type[] { typeof(GTA3ScriptReferenceValue), typeof(int) }, (runtime, arguments) =>
            {
                // TODO
                throw new NotImplementedException();
            }),
            
            // 0x11
            new GTA3ScriptInstruction(new Type[] { typeof(GTA3ScriptReferenceValue), typeof(float) }, (runtime, arguments) =>
            {
                // TODO
                throw new NotImplementedException();
            }),
            
            // 0x12
            new GTA3ScriptInstruction(new Type[] { typeof(GTA3ScriptReferenceValue), typeof(int) }, (runtime, arguments) =>
            {
                // TODO
                throw new NotImplementedException();
            }),
            
            // 0x13
            new GTA3ScriptInstruction(new Type[] { typeof(GTA3ScriptReferenceValue), typeof(float) }, (runtime, arguments) =>
            {
                // TODO
                throw new NotImplementedException();
            }),
            
            // 0x14
            new GTA3ScriptInstruction(new Type[] { typeof(GTA3ScriptReferenceValue), typeof(int) }, (runtime, arguments) =>
            {
                // TODO
                throw new NotImplementedException();
            }),
            
            // 0x15
            new GTA3ScriptInstruction(new Type[] { typeof(GTA3ScriptReferenceValue), typeof(float) }, (runtime, arguments) =>
            {
                // TODO
                throw new NotImplementedException();
            }),
            
            // 0x16
            new GTA3ScriptInstruction(new Type[] { typeof(GTA3ScriptReferenceValue), typeof(int) }, (runtime, arguments) =>
            {
                // TODO
                throw new NotImplementedException();
            }),
            
            // 0x17
            new GTA3ScriptInstruction(new Type[] { typeof(GTA3ScriptReferenceValue), typeof(int) }, (runtime, arguments) =>
            {
                // TODO
                throw new NotImplementedException();
            }),
            
            // 0x18
            new GTA3ScriptInstruction(new Type[] { typeof(GTA3ScriptReferenceValue), typeof(int) }, (runtime, arguments) =>
            {
                // TODO
                throw new NotImplementedException();
            }),
            
            // 0x19
            new GTA3ScriptInstruction(new Type[] { typeof(GTA3ScriptReferenceValue), typeof(int) }, (runtime, arguments) =>
            {
                // TODO
                throw new NotImplementedException();
            }),
            
            // 0x1A
            new GTA3ScriptInstruction(new Type[] { typeof(int), typeof(GTA3ScriptReferenceValue) }, (runtime, arguments) =>
            {
                // TODO
                throw new NotImplementedException();
            }),
            
            // 0x1B
            new GTA3ScriptInstruction(new Type[] { typeof(int), typeof(GTA3ScriptReferenceValue) }, (runtime, arguments) =>
            {
                // TODO
                throw new NotImplementedException();
            }),
            
            // 0x1C
            new GTA3ScriptInstruction(new Type[] { typeof(GTA3ScriptReferenceValue), typeof(GTA3ScriptReferenceValue) }, (runtime, arguments) =>
            {
                // TODO
                throw new NotImplementedException();
            }),
            
            // 0x1D
            new GTA3ScriptInstruction(new Type[] { typeof(GTA3ScriptReferenceValue), typeof(GTA3ScriptReferenceValue) }, (runtime, arguments) =>
            {
                // TODO
                throw new NotImplementedException();
            }),
            
            // 0x1E
            new GTA3ScriptInstruction(new Type[] { typeof(GTA3ScriptReferenceValue), typeof(GTA3ScriptReferenceValue) }, (runtime, arguments) =>
            {
                // TODO
                throw new NotImplementedException();
            }),
            
            // 0x1F
            new GTA3ScriptInstruction(new Type[] { typeof(GTA3ScriptReferenceValue), typeof(GTA3ScriptReferenceValue) }, (runtime, arguments) =>
            {
                // TODO
                throw new NotImplementedException();
            }),
            
            // 0x20
            new GTA3ScriptInstruction(new Type[] { typeof(GTA3ScriptReferenceValue), typeof(float) }, (runtime, arguments) =>
            {
                // TODO
                throw new NotImplementedException();
            }),
            
            // 0x21
            new GTA3ScriptInstruction(new Type[] { typeof(GTA3ScriptReferenceValue), typeof(float) }, (runtime, arguments) =>
            {
                // TODO
                throw new NotImplementedException();
            }),
            
            // 0x22
            new GTA3ScriptInstruction(new Type[] { typeof(float), typeof(GTA3ScriptReferenceValue) }, (runtime, arguments) =>
            {
                // TODO
                throw new NotImplementedException();
            }),
            
            // 0x23
            new GTA3ScriptInstruction(new Type[] { typeof(float), typeof(GTA3ScriptReferenceValue) }, (runtime, arguments) =>
            {
                // TODO
                throw new NotImplementedException();
            }),
            
            // 0x24
            new GTA3ScriptInstruction(new Type[] { typeof(GTA3ScriptReferenceValue), typeof(GTA3ScriptReferenceValue) }, (runtime, arguments) =>
            {
                // TODO
                throw new NotImplementedException();
            }),
            
            // 0x25
            new GTA3ScriptInstruction(new Type[] { typeof(GTA3ScriptReferenceValue), typeof(GTA3ScriptReferenceValue) }, (runtime, arguments) =>
            {
                // TODO
                throw new NotImplementedException();
            }),
            
            // 0x26
            new GTA3ScriptInstruction(new Type[] { typeof(GTA3ScriptReferenceValue), typeof(GTA3ScriptReferenceValue) }, (runtime, arguments) =>
            {
                // TODO
                throw new NotImplementedException();
            }),
            
            // 0x27
            new GTA3ScriptInstruction(new Type[] { typeof(GTA3ScriptReferenceValue), typeof(GTA3ScriptReferenceValue) }, (runtime, arguments) =>
            {
                // TODO
                throw new NotImplementedException();
            }),
            
            // 0x28
            new GTA3ScriptInstruction(new Type[] { typeof(GTA3ScriptReferenceValue), typeof(int) }, (runtime, arguments) =>
            {
                // TODO
                throw new NotImplementedException();
            }),
            
            // 0x29
            new GTA3ScriptInstruction(new Type[] { typeof(GTA3ScriptReferenceValue), typeof(int) }, (runtime, arguments) =>
            {
                // TODO
                throw new NotImplementedException();
            }),
            
            // 0x2A
            new GTA3ScriptInstruction(new Type[] { typeof(int), typeof(GTA3ScriptReferenceValue) }, (runtime, arguments) =>
            {
                // TODO
                throw new NotImplementedException();
            }),
            
            // 0x2B
            new GTA3ScriptInstruction(new Type[] { typeof(int), typeof(GTA3ScriptReferenceValue) }, (runtime, arguments) =>
            {
                // TODO
                throw new NotImplementedException();
            }),
            
            // 0x2C
            new GTA3ScriptInstruction(new Type[] { typeof(GTA3ScriptReferenceValue), typeof(GTA3ScriptReferenceValue) }, (runtime, arguments) =>
            {
                // TODO
                throw new NotImplementedException();
            }),
            
            // 0x2D
            new GTA3ScriptInstruction(new Type[] { typeof(GTA3ScriptReferenceValue), typeof(GTA3ScriptReferenceValue) }, (runtime, arguments) =>
            {
                // TODO
                throw new NotImplementedException();
            }),
            
            // 0x2E
            new GTA3ScriptInstruction(new Type[] { typeof(GTA3ScriptReferenceValue), typeof(GTA3ScriptReferenceValue) }, (runtime, arguments) =>
            {
                // TODO
                throw new NotImplementedException();
            }),
            
            // 0x2F
            new GTA3ScriptInstruction(new Type[] { typeof(GTA3ScriptReferenceValue), typeof(GTA3ScriptReferenceValue) }, (runtime, arguments) =>
            {
                // TODO
                throw new NotImplementedException();
            }),
            
            // 0x30
            new GTA3ScriptInstruction(new Type[] { typeof(GTA3ScriptReferenceValue), typeof(float) }, (runtime, arguments) =>
            {
                // TODO
                throw new NotImplementedException();
            }),
            
            // 0x31
            new GTA3ScriptInstruction(new Type[] { typeof(GTA3ScriptReferenceValue), typeof(float) }, (runtime, arguments) =>
            {
                // TODO
                throw new NotImplementedException();
            }),
            
            // 0x32
            new GTA3ScriptInstruction(new Type[] { typeof(float), typeof(GTA3ScriptReferenceValue) }, (runtime, arguments) =>
            {
                // TODO
                throw new NotImplementedException();
            }),
            
            // 0x33
            new GTA3ScriptInstruction(new Type[] { typeof(float), typeof(GTA3ScriptReferenceValue) }, (runtime, arguments) =>
            {
                // TODO
                throw new NotImplementedException();
            }),
            
            // 0x34
            new GTA3ScriptInstruction(new Type[] { typeof(GTA3ScriptReferenceValue), typeof(GTA3ScriptReferenceValue) }, (runtime, arguments) =>
            {
                // TODO
                throw new NotImplementedException();
            }),
            
            // 0x35
            new GTA3ScriptInstruction(new Type[] { typeof(GTA3ScriptReferenceValue), typeof(GTA3ScriptReferenceValue) }, (runtime, arguments) =>
            {
                // TODO
                throw new NotImplementedException();
            }),
            
            // 0x36
            new GTA3ScriptInstruction(new Type[] { typeof(GTA3ScriptReferenceValue), typeof(GTA3ScriptReferenceValue) }, (runtime, arguments) =>
            {
                // TODO
                throw new NotImplementedException();
            }),
            
            // 0x37
            new GTA3ScriptInstruction(new Type[] { typeof(GTA3ScriptReferenceValue), typeof(GTA3ScriptReferenceValue) }, (runtime, arguments) =>
            {
                // TODO
                throw new NotImplementedException();
            }),
            
            // 0x38
            new GTA3ScriptInstruction(new Type[] { typeof(GTA3ScriptReferenceValue), typeof(int) }, (runtime, arguments) =>
            {
                // TODO
                throw new NotImplementedException();
            }),
            
            // 0x39
            new GTA3ScriptInstruction(new Type[] { typeof(GTA3ScriptReferenceValue), typeof(int) }, (runtime, arguments) =>
            {
                // TODO
                throw new NotImplementedException();
            }),
            
            // 0x3A
            new GTA3ScriptInstruction(new Type[] { typeof(GTA3ScriptReferenceValue), typeof(GTA3ScriptReferenceValue) }, (runtime, arguments) =>
            {
                // TODO
                throw new NotImplementedException();
            }),
            
            // 0x3B
            new GTA3ScriptInstruction(new Type[] { typeof(GTA3ScriptReferenceValue), typeof(GTA3ScriptReferenceValue) }, (runtime, arguments) =>
            {
                // TODO
                throw new NotImplementedException();
            }),
            
            // 0x3C
            new GTA3ScriptInstruction(new Type[] { typeof(GTA3ScriptReferenceValue), typeof(GTA3ScriptReferenceValue) }, (runtime, arguments) =>
            {
                // TODO
                throw new NotImplementedException();
            }),

            // 0x3D
            null,

            // 0x3E
            null,

            // 0x3F
            null,

            // 0x40
            null,

            // 0x41
            null,
            
            // 0x42
            new GTA3ScriptInstruction(new Type[] { typeof(GTA3ScriptReferenceValue), typeof(float) }, (runtime, arguments) =>
            {
                // TODO
                throw new NotImplementedException();
            }),
            
            // 0x43
            new GTA3ScriptInstruction(new Type[] { typeof(GTA3ScriptReferenceValue), typeof(float) }, (runtime, arguments) =>
            {
                // TODO
                throw new NotImplementedException();
            }),
            
            // 0x44
            new GTA3ScriptInstruction(new Type[] { typeof(GTA3ScriptReferenceValue), typeof(GTA3ScriptReferenceValue) }, (runtime, arguments) =>
            {
                // TODO
                throw new NotImplementedException();
            }),
            
            // 0x45
            new GTA3ScriptInstruction(new Type[] { typeof(GTA3ScriptReferenceValue), typeof(GTA3ScriptReferenceValue) }, (runtime, arguments) =>
            {
                // TODO
                throw new NotImplementedException();
            }),
            
            // 0x46
            new GTA3ScriptInstruction(new Type[] { typeof(GTA3ScriptReferenceValue), typeof(GTA3ScriptReferenceValue) }, (runtime, arguments) =>
            {
                // TODO
                throw new NotImplementedException();
            }),

            //0x47
            null,
            
            //0x48
            null,
            
            //0x49
            null,
            
            //0x4A
            null,
            
            //0x4B
            null,
            
            //0x4C
            null,
            
            // 0x4D
            new GTA3ScriptInstruction(new Type[] { typeof(ushort), typeof(uint) }, (runtime, arguments) =>
            {
                if ((bool)(runtime.ReturnValue))
                {
                    runtime.CurrentCommandIndex = (uint)(arguments[1]);
                }
            }),
            
            // 0x4E
            new GTA3ScriptInstruction(new Type[0], (runtime, arguments) =>
            {
                runtime.KeepRunning = false;
            }),
            
            // 0x4F
            new GTA3ScriptInstruction(new Type[] { typeof(GTA3ScriptReferenceValue), typeof(GTA3ScriptReferenceValue) }, (runtime, arguments) =>
            {
                // TODO
            })

            // TODO
        };

        /*/// <summary>
        /// Thread name
        /// </summary>
        private string threadName;*/

        /// <summary>
        /// Main script commands
        /// </summary>
        private GTA3ScriptCommand[] mainScriptCommands;

        /// <summary>
        /// Mission scripts commmands
        /// </summary>
        private GTA3ScriptCommand[][] missionScriptsCommands;

        /// <summary>
        /// Keep running
        /// </summary>
        public bool KeepRunning { get; internal set; } = true;

        /// <summary>
        /// Current command index
        /// </summary>
        public uint CurrentCommandIndex { get; internal set; }

        /// <summary>
        /// Return value
        /// </summary>
        public object ReturnValue { get; internal set; }

        /// <summary>
        /// Is patchable
        /// </summary>
        public bool IsPatchable { get; private set; } = true;

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
        /// Main script commands
        /// </summary>
        private GTA3ScriptCommand[] MainScriptCommands
        {
            get
            {
                if (mainScriptCommands == null)
                {
                    mainScriptCommands = new GTA3ScriptCommand[0];
                }
                return mainScriptCommands;
            }
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        internal AGTA3ScriptRuntime()
        {
            // ...
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
        /// <param name="keywords">Keywords</param>
        /// <param name="onCall">GTA3Script instruction on call event</param>
        /// <param name="argumentTypes">GTA3Script instruction argument types</param>
        /// <exception cref="ArgumentException">Invalid argument</exception>
        public void Patch(ushort opCode, string[] keywords, GTA3ScriptOpCodeCallDelegate onCall, params Type[] argumentTypes)
        {
            Type[] argument_types = ((argumentTypes == null) ? (new Type[0]) : argumentTypes);
            if (IsPatchable && (opCode >= 0))
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
                InstructionSet[opCode] = new GTA3ScriptInstruction(argument_types, onCall);
                AddKeywords(opCode, keywords);
            }
        }

        /// <summary>
        /// Patch instruction
        /// </summary>
        /// <param name="opCode">GTA3Script operation code</param>
        /// <param name="keyword">Keyword</param>
        /// <param name="onCall">GTA3Script instruction on call event</param>
        /// <param name="argumentTypes">GTA3Script instruction argument types</param>
        /// <exception cref="ArgumentException">Invalid argument</exception>
        public void Patch(ushort opCode, string keyword, GTA3ScriptOpCodeCallDelegate onCall, params Type[] argumentTypes)
        {
            Patch(opCode, new string[] { keyword }, onCall, argumentTypes);
        }

        /// <summary>
        /// Add keyword
        /// </summary>
        /// <param name="opCode">GTA3Script operation code</param>
        /// <param name="keyword">Keyword</param>
        public void AddKeyword(ushort opCode, string keyword)
        {
            if (keyword != null)
            {
                if (!(keywords.ContainsKey(keyword)))
                {
                    keywords.Add(keyword, opCode);
                }
            }
        }

        /// <summary>
        /// Add keyword
        /// </summary>
        /// <param name="opCode">GTA3Script operation code</param>
        /// <param name="keywords">Keywords</param>
        public void AddKeywords(ushort opCode, params string[] keywords)
        {
            if (keywords != null)
            {
                foreach (string keyword in keywords)
                {
                    AddKeyword(opCode, keyword);
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
            if (KeepRunning && (CurrentCommandIndex < MainScriptCommands.Length))
            {
                ref GTA3ScriptCommand command = ref MainScriptCommands[CurrentCommandIndex];
                try
                {
                    InstructionSet[command.OpCode].Invoke(this, command.Arguments);
                    ++CurrentCommandIndex;
                    ret = true;
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e);
                    throw new GTA3ScriptExecutionException("Error executing operation code " + command.OpCode + " at command index " + CurrentCommandIndex, e);
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

        /// <summary>
        /// Load GTA3Script finished
        /// </summary>
        /// <param name="mainScriptCommands">Main script commands</param>
        /// <param name="missionScriptsCommands">Mission script commands</param>
        internal void LoadScriptFinished(GTA3ScriptCommand[] mainScriptCommands, GTA3ScriptCommand[][] missionScriptsCommands)
        {
            this.mainScriptCommands = mainScriptCommands;
            this.missionScriptsCommands = missionScriptsCommands;
            IsPatchable = false;
        }
    }
}
