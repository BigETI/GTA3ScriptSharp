using System;

/// <summary>
/// GTA3Script sharp namespace
/// </summary>
namespace GTA3ScriptSharp
{
    /// <summary>
    /// IR2 class
    /// </summary>
    public class IR2
    {
        /// <summary>
        /// Game
        /// </summary>
        private EGame game;

        /// <summary>
        /// Lines
        /// </summary>
        private string[] lines;

        /// <summary>
        /// Lines
        /// </summary>
        private string[] Lines
        {
            get
            {
                if (lines == null)
                {
                    lines = new string[0];
                }
                return lines;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game">Game</param>
        /// <param name="lines">Lines</param>
        internal IR2(EGame game, string[] lines)
        {
            this.game = game;
            this.lines = lines;
        }

        /// <summary>
        /// Compile
        /// </summary>
        /// <returns>SCM if successful, otherwise "null"</returns>
        public SCM Compile()
        {
            // TODO
            throw new NotImplementedException();
        }
    }
}
