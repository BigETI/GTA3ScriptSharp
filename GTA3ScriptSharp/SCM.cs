using System;
using System.IO;
using System.Reflection;

/// <summary>
/// GTA3Script sharp namespace
/// </summary>
namespace GTA3ScriptSharp
{
    /// <summary>
    /// SCM class
    /// </summary>
    public class SCM : IDisposable
    {
        /// <summary>
        /// Game
        /// </summary>
        private EGame game;

        /// <summary>
        /// Stream
        /// </summary>
        private Stream stream;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game">Game</param>
        /// <param name="stream">Stream</param>
        internal SCM(EGame game, Stream stream)
        {
            this.game = game;
            this.stream = stream;
        }

        /// <summary>
        /// Interpret script
        /// </summary>
        /// <returns></returns>
        public static AGTA3ScriptRuntime Interpret()
        {
            // TODO
            throw new NotImplementedException();
        }

        /// <summary>
        /// Compile script to assembly
        /// </summary>
        /// <returns>Assembly if successful, otherwise "null"</returns>
        public static Assembly CompileToAssembly()
        {
            // TODO
            throw new NotImplementedException();
        }

        /// <summary>
        /// Compile to IR2
        /// </summary>
        /// <returns>IR2 if successful, otherwise "null"</returns>
        public static IR2 CompileToIR2()
        {
            // TODO
            throw new NotImplementedException();
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            if (stream != null)
            {
                stream.Dispose();
                stream = null;
            }
        }
    }
}
