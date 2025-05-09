// SPDX-FileCopyrightText: 2025 Unity Technologies and the glTFast authors
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Threading.Tasks;

namespace GLTFast.Loading
{
    /// <summary>
    /// Provides a mechanism for loading external resources from a URI
    /// </summary>
    public interface IDownloadProvider
    {
        /// <summary>
        /// Sends a URI request
        /// </summary>
        /// <param name="url">URI to request</param>
        /// <returns>Object representing the request</returns>
        Task<IDownload> Request(Uri url);

        /// <summary>
        /// Sends a URI request to load a texture
        /// </summary>
        /// <param name="url">URI to request</param>
        /// <param name="nonReadable">If true, resulting texture is not CPU readable (uses less memory)</param>
        /// <returns>Object representing the request</returns>
        Task<ITextureDownload> RequestTexture(Uri url, bool nonReadable);
    }
}
