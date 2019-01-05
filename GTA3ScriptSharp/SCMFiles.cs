using System;
using System.Reflection;

/// <summary>
/// GTA3Script sharp namespace
/// </summary>
namespace GTA3ScriptSharp
{
    /// <summary>
    /// SCM files class
    /// </summary>
    public class SCMFiles : IDisposable
    {
        /// <summary>
        /// Game
        /// </summary>
        private EGame game;

        /// <summary>
        /// SCMs
        /// </summary>
        private SCM[] scms;

        /// <summary>
        /// SCMs
        /// </summary>
        public SCM[] SCMs
        {
            get
            {
                return ((scms == null) ? scms : (new SCM[0]));
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game">Game</param>
        /// <param name="scms">SCMs</param>
        internal SCMFiles(EGame game, SCM[] scms)
        {
            this.game = game;
            this.scms = scms;
        }

        /// <summary>
        /// Interpret all scripts
        /// </summary>
        /// <returns>GTA3Script runtimes if successful, otherwise "null"</returns>
        public AGTA3ScriptRuntime[] InterpretAll()
        {
            // TODO
            throw new NotImplementedException();
        }

        /// <summary>
        /// Compile all scripts to assembly
        /// </summary>
        /// <returns>Assembly if successful, otherwise "null"</returns>
        public Assembly CompileAllToAssembly()
        {
            // TODO
            throw new NotImplementedException();
        }

        /// <summary>
        /// Compile all scripts to IR2
        /// </summary>
        /// <returns>IR2s if successful, otherwise "null"</returns>
        public IR2[] CompileAllToIR2()
        {
            // TODO
            throw new NotImplementedException();
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            if (scms != null)
            {
                foreach (SCM scm in scms)
                {
                    if (scm != null)
                    {
                        scm.Dispose();
                    }
                }
                scms = null;
            }
        }
    }
}
