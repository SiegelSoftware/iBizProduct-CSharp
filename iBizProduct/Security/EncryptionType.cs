
namespace iBizProduct.Security
{
    /// <summary>
    /// This provides a context of Supported Encryption Methods
    /// </summary>
    public enum EncryptionType
    {
        /// <summary>
        /// Use Base64 Encoding. This is the default method iBizProduct uses.
        /// </summary>
        BASE64,

        /// <summary>
        /// Use SHA Encryption.
        /// </summary>
        SHA
    }
}
