using System;

/// <summary>
/// GTA3Script sharp namespace
/// </summary>
namespace GTA3ScriptSharp
{
    /// <summary>
    /// GTA3Script operation code class
    /// </summary>
    public class GTA3ScriptOpCode
    {
        /// <summary>
        /// On call event
        /// </summary>
        public event GTA3ScriptOpCodeCallDelegate OnCall;

        /// <summary>
        /// GTA3Script operation code argument types
        /// </summary>
        private Type[] argTypes;

        /// <summary>
        /// GTA3Script operation code argument types
        /// </summary>
        internal Type[] ArgTypes
        {
            get
            {
                if (argTypes != null)
                {
                    argTypes = new Type[0];
                }
                return argTypes.Clone() as Type[];
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="onCall">On call event</param>
        /// <param name="argTypes">GTA3Script operation code argument types</param>
        public GTA3ScriptOpCode(GTA3ScriptOpCodeCallDelegate onCall, Type[] argTypes)
        {
            OnCall = onCall;
            this.argTypes = argTypes;
        }

        /// <summary>
        /// Invoke operation code
        /// </summary>
        /// <param name="runtime">GTA3Script runtime</param>
        /// <param name="args">GTA3Script operation code arguments</param>
        public void Invoke(AGTA3ScriptRuntime runtime, object[] args)
        {
            OnCall?.Invoke(runtime, args);
        }
    }
}
