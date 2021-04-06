using UnityEngine;

namespace Lasp
{
    //
    // Spectrum texture baking utility
    //
    [AddComponentMenu("LASP/Utility/Custom Spectrum To Texture")]
    [RequireComponent(typeof(SpectrumAnalyzer))]
    public sealed class CustomSpectrumToTexture : MonoBehaviour
    {
        #region Editable attributes

        // X-axis log scale switch
        [SerializeField] bool _logScale = true;
        public bool logScale {
            get => _logScale;
            set => _logScale = value;
        }

        // Bake target render texture
        [SerializeField] RenderTexture _renderTexture = null;
        public RenderTexture renderTexture {
            get => _renderTexture;
            set => _renderTexture = value;
        }

        #endregion

        #region Runtime public property

        // Baked spectrum texture
        public Texture texture => _texture;

        #endregion

        #region Private members

        SpectrumAnalyzer _analyzer;
        Texture2D _texture;
        MaterialPropertyBlock _block;

        #endregion

        #region MonoBehaviour implementation

        void OnDestroy()
        {
            if (_texture != null) Destroy(_texture);
        }

        void Update()
        {
            // Spectrum analyzer component cache
            if (_analyzer == null) _analyzer = GetComponent<SpectrumAnalyzer>();

            // Refresh the temporary texture when the resolution was changed.
            if (_texture != null && _texture.width != _analyzer.resolution)
            {
                Destroy(_texture);
                _texture = null;
            }

            // Lazy initialization of the temporary texture
            // Edit: For video exports, change format to RGBA32
            if (_texture == null)
                _texture = new Texture2D(_analyzer.resolution, 1,
                                         TextureFormat.RGBA32, false)
                           { wrapMode = TextureWrapMode.Clamp };

            // Texture update
            if (_logScale)
                _texture.LoadRawTextureData(_analyzer.logSpectrumArray);
            else
                _texture.LoadRawTextureData(_analyzer.spectrumArray);

            _texture.Apply();

            // Update the external render texture.
            if (_renderTexture != null)
            {
                // Edit: Copy texture after resize by RenderTexture size
                Texture2D resized = ResizeTexture(_texture, _renderTexture.width, _renderTexture.height);
                Graphics.CopyTexture(resized, _renderTexture);
            }

            
        }

        #endregion
        
        /// <summary>
        /// Edit: Resize Texture for uNvEncoder.
        /// </summary>
        /// <param name="srcTexture"></param>
        /// <param name="newWidth"></param>
        /// <param name="newHeight"></param>
        /// <returns></returns>
        private Texture2D ResizeTexture(Texture2D srcTexture, int newWidth, int newHeight) {
            var resizedTexture = new Texture2D(newWidth, newHeight, srcTexture.format, false);
            Graphics.ConvertTexture(srcTexture, resizedTexture);
            return resizedTexture;
        }
    }
    
}
