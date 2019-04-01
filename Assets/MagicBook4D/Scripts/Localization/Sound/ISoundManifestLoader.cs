using Loxodon.Framework.Asynchronous;

namespace Firecoals.AssetBundles.Sound
{
    public interface ISoundManifestLoader
    {
        /// <summary>
        /// Synchronously load a sound manifest
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        SoundManifest LoadSync(string path);

        /// <summary>
        /// Asynchronously load a sound manifest
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        IAsyncResult<SoundManifest> LoadAsync(string path);
    }
}

