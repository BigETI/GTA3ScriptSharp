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
        /// On call event
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
        /// <param name="onCall">On call event</param>
        /// <param name="argumentTypes">GTA3Script operation instruction types</param>
        public GTA3ScriptInstruction(GTA3ScriptOpCodeCallDelegate onCall, Type[] argumentTypes)
        {
            OnCall = onCall;
            this.argumentTypes = argumentTypes;
        }

        /// <summary>
        /// Invoke operation code
        /// </summary>
        /// <param name="runtime">GTA3Script runtime</param>
        /// <param name="arguments">GTA3Script operation instruction</param>
        public void Invoke(AGTA3ScriptRuntime runtime, object[] arguments)
        {
            OnCall?.Invoke(runtime, arguments);
        }
    }
}
