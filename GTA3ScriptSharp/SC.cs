using System;
using System.IO;

/// <summary>
/// GTA3Script sharp namespace
/// </summary>
namespace GTA3ScriptSharp
{
    /// <summary>
    /// SC class
    /// </summary>
    public class SC : AGTA3Script
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game">Game</param>
        /// <param name="stream">Stream</param>
        internal SC(EGame game, Stream stream) : base(game, stream)
        {
            // ...
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game">Game</param>
        /// <param name="stream">Stream</param>
        /// <param name="disposeStreamOnDispose">Dispose stream on dispose</param>
        internal SC(EGame game, Stream stream, bool disposeStreamOnDispose) : base(game, stream, disposeStreamOnDispose)
        {
            // ...
        }

        /// <summary>
        /// Load GTA3Script to GTA3Script runtime
        /// </summary>
        /// <param name="runtime">GTA3Script Runtime</param>
        internal override void LoadToRuntime(AGTA3ScriptRuntime runtime)
        {
            throw new NotImplementedException();
        }
    }
}
