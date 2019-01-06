using System;
using System.IO;

/// <summary>
/// GTA3Script sharp namespace
/// </summary>
namespace GTA3ScriptSharp
{
    /// <summary>
    /// GTA3Script interface
    /// </summary>
    public abstract class AGTA3Script : IDisposable
    {
        /// <summary>
        /// Game
        /// </summary>
        public readonly EGame Game;

        /// <summary>
        /// Stream
        /// </summary>
        public Stream Stream { get; private set; }

        /// <summary>
        /// Dispose stream on dispose
        /// </summary>
        private readonly bool DisposeStreamOnDispose = true;

        /// <summary>
        /// Load GTA3Script to GTA3Script runtime
        /// </summary>
        /// <param name="runtime">GTA3Script runtime</param>
        internal abstract void LoadToRuntime(AGTA3ScriptRuntime runtime);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game">Game</param>
        /// <param name="stream">Stream</param>
        internal AGTA3Script(EGame game, Stream stream)
        {
            Game = game;
            Stream = stream;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game">Game</param>
        /// <param name="stream">Stream</param>
        /// <param name="disposeStreamOnDispose">Dispose stream on dispose</param>
        internal AGTA3Script(EGame game, Stream stream, bool disposeStreamOnDispose)
        {
            Game = game;
            Stream = stream;
            DisposeStreamOnDispose = disposeStreamOnDispose;
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            if (Stream != null)
            {
                if (DisposeStreamOnDispose)
                {
                    Stream.Dispose();
                }
                Stream = null;
            }
        }
    }
}
