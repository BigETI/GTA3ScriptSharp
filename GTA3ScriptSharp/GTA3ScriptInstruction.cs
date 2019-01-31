using System;

/// <summary>
/// GTA3Script sharp namespace
/// </summary>
namespace GTA3ScriptSharp
{
    /// <summary>
    /// GTA3Script instruction class
    /// </summary>
    public class GTA3ScriptInstruction
    {
        /// <summary>
        /// GTA3Script instruction on call event
        /// </summary>
        public event GTA3ScriptOpCodeCallDelegate OnCall;

        /// <summary>
        /// GTA3Script instruction argument types
        /// </summary>
        private Type[] argumentTypes;

        /// <summary>
        /// GTA3Script instruction argument types
        /// </summary>
        internal Type[] ArgumentTypes
        {
            get
            {
                if (argumentTypes != null)
                {
                    argumentTypes = new Type[0];
                }
                return argumentTypes.Clone() as Type[];
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="argumentTypes">GTA3Script operation instruction types</param>
        /// <param name="onCall">GTA3Script instruction on call event</param>
        public GTA3ScriptInstruction(Type[] argumentTypes, GTA3ScriptOpCodeCallDelegate onCall)
        {
            this.argumentTypes = argumentTypes;
            OnCall = onCall;
        }

        /// <summary>
        /// Invoke operation code
        /// </summary>
        /// <param name="runtime">GTA3Script runtime</param>
        /// <param name="arguments">GTA3Script operation instruction</param>
        public void Invoke(AGTA3ScriptRuntime runtime, object[] arguments)
        {
            OnCall(runtime, arguments);
        }
    }
}
