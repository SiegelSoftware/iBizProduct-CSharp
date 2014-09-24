
namespace iBizProduct.Security
{
    /// <summary>
    /// This provides a context of Supported Encryption Methods
    /// </summary>
    public enum EncryptionType
    {
        /// <summary>
        /// No Encryption Used.
        /// </summary>
        None = 0,

        /// <summary>
        /// Use Base64 Encoding. This is the default method iBizProduct uses.
        /// </summary>
        Base64 = 1,

        /// <summary>
        /// Use Laravel compatible encryption. This is based off of the 4.2 library at
        /// https://github.com/laravel/framework/blob/4.2/src/Illuminate/Encryption/Encrypter.php
        /// </summary>
        Laravel = 2,

        /// <summary>
        /// Use Rijndael 128 Bit Encryption
        /// </summary>
        Rijndael128 = 3
    }
}
