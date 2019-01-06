using System;

/// <summary>
/// GTA3Script sharp namespace
/// </summary>
namespace GTA3ScriptSharp
{
    /// <summary>
    /// GTA3Script files class
    /// </summary>
    public class GTA3ScriptFiles : IDisposable
    {
        /// <summary>
        /// Game
        /// </summary>
        private EGame game;

        /// <summary>
        /// Scripts
        /// </summary>
        private AGTA3Script[] scripts;

        /// <summary>
        /// Scripts
        /// </summary>
        public AGTA3Script[] Scripts
        {
            get
            {
                return ((scripts == null) ? scripts : (new AGTA3Script[0]));
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game">Game</param>
        /// <param name="scripts">Scripts</param>
        internal GTA3ScriptFiles(EGame game, AGTA3Script[] scripts)
        {
            this.game = game;
            this.scripts = scripts;
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            if (scripts != null)
            {
                foreach (AGTA3Script script in scripts)
                {
                    if (script != null)
                    {
                        script.Dispose();
                    }
                }
                scripts = null;
            }
        }
    }
}
