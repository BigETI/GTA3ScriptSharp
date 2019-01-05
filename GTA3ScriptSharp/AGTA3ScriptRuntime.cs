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
        /// GTA3Script operation codes
        /// </summary>
        private GTA3ScriptOpCode[] opCodes;

        /// <summary>
        /// GTA3Script operation codes
        /// </summary>
        private GTA3ScriptOpCode[] OpCodes
        {
            get
            {
                if (opCodes != null)
                {
                    // TODO
                    opCodes = new GTA3ScriptOpCode[0];
                }
                return opCodes;
            }
        }

        /// <summary>
        /// Patch operation code
        /// </summary>
        /// <param name="opCode">GTA3Script operation code</param>
        /// <param name="onCall">GTA3Script operation code on call event</param>
        /// <param name="argTypes">GTA3Script operation code argument types</param>
        /// <exception cref="ArgumentException">Invalid argument</exception>
        public void Patch(int opCode, GTA3ScriptOpCodeCallDelegate onCall, Type[] argTypes)
        {
            if ((opCode >= 0) && (opCode < OpCodes.Length) && (onCall != null) && (argTypes != null))
            {
                for (int i = 0; i < argTypes.Length; i++)
                {
                    if (argTypes[i] == null)
                    {
                        throw new ArgumentException("argTypes[" + i + "] is null", "argTypes");
                    }
                }
                OpCodes[opCode] = new GTA3ScriptOpCode(onCall, argTypes);
            }
        }

        /// <summary>
        /// Patch operation code
        /// </summary>
        /// <param name="opCode">GTA3Script operation code</param>
        /// <param name="onCall">GTA3Script operation code on call event</param>
        /// <param name="argTypes">GTA3Script operation code argument types</param>
        public void Patch(EGTA3ScriptOpCode opCode, GTA3ScriptOpCodeCallDelegate onCall, Type[] argTypes)
        {
            Patch((int)opCode, onCall, argTypes);
        }

        /// <summary>
        /// Invoke GTA3Script operation code
        /// </summary>
        /// <param name="opCode">GTA3Script operation code</param>
        /// <param name="args">GTA3Script operation code arguments</param>
        /// <exception cref="ArgumentException">Invalid argument</exception>
        internal void Invoke(int opCode, object[] args)
        {
            object[] arguments = ((args == null) ? new object[0] : args);
            if ((opCode >= 0) && (opCode < OpCodes.Length))
            {
                GTA3ScriptOpCode op_code = OpCodes[opCode];
                if (op_code != null)
                {
                    if (op_code.ArgTypes.Length == args.Length)
                    {
                        for (int i = 0; i < args.Length; i++)
                        {
                            object arg = args[i];
                            if (arg != null)
                            {
                                if (arg.GetType() != op_code.ArgTypes[i])
                                {
                                    throw new ArgumentException("Invalid argument type " + arg.GetType().FullName + " for " + op_code.ArgTypes[i].FullName + " at index " + i + " operation code " + opCode, "args");
                                }
                            }
                            else
                            {
                                throw new ArgumentException("Invalid null type for " + op_code.ArgTypes[i].FullName + " at index " + i + " operation code " + opCode, "args");
                            }
                        }
                        op_code.Invoke(this, args);
                    }
                    else
                    {
                        throw new ArgumentException("Arguments length doesn't equal supported argument types", "args");
                    }
                }
                else
                {
                    throw new ArgumentException("Operation code " + opCode + " has not been implemented yet", "opCode");
                }
            }
            else
            {
                throw new ArgumentException("Invalid operation code " + opCode, "opCode");
            }
        }

        /// <summary>
        /// Invoke GTA3Script operation code
        /// </summary>
        /// <param name="opCode">Operation code</param>
        /// <exception cref="ArgumentException">Invalid argument</exception>
        internal void Invoke(int opCode)
        {
            Invoke(opCode, null);
        }
    }
}
