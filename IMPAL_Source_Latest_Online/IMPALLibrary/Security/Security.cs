using System;
using System.Security; 
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System.Net.Mail;

namespace IMPALLibrary
{
    #region Random Password, Mail Send, Encryption And Decryption

    public class SecurityBL
    {
        #region Global Variable Declaration


        #endregion Global Variable Declaration

        #region Constructor

        public SecurityBL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #endregion Constructor

        #region Create Random Password

        public string CreateRandomPassword(int PasswordLength)
        {
            string _allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789!@#$&^*-~";
            Random randNum = new Random();
            char[] chars = new char[PasswordLength];
            int allowedCharCount = _allowedChars.Length;
            for (int i = 0; i < PasswordLength; i++)
            {
                chars[i] = _allowedChars[(int)((_allowedChars.Length) * randNum.NextDouble())];
            }
            return new string(chars);
        }

        #endregion Create Random Password

        #region Send Mail Message

        public static void SendMailMessage(string from, string to, string bcc, string cc, string subject, string body, string smtpserver)
        {
            // Instantiate a new instance of MailMessage
            MailMessage mMailMessage = new MailMessage();
            // Set the sender address of the mail message
            mMailMessage.From = new MailAddress(from);
            // Set the recepient address of the mail message
            mMailMessage.To.Add(new MailAddress(to));
            // Check if the bcc value is null or an empty string
            if ((bcc != null) && (bcc != string.Empty))
            {
                // Set the Bcc address of the mail message
                mMailMessage.Bcc.Add(new MailAddress(bcc));
            }
            // Check if the cc value is null or an empty value
            if ((cc != null) && (cc != string.Empty))
            {
                // Set the CC address of the mail message
                mMailMessage.CC.Add(new MailAddress(cc));
            }
            // Set the subject of the mail message
            mMailMessage.Subject = subject;
            // Set the body of the mail message
            mMailMessage.Body = body;
            // Secify the format of the body as HTML
            mMailMessage.IsBodyHtml = true;
            // Set the priority of the mail message to normal
            mMailMessage.Priority = MailPriority.Normal;
            // Instantiate a new instance of SmtpClient
            //SmtpClient mSmtpClient = new SmtpClient("mailchennai2.eserveglobal.com");
            SmtpClient mSmtpClient = new SmtpClient(smtpserver);
            // Send the mail message
            mSmtpClient.Send(mMailMessage);
        }

        #endregion Send Mail Message

        #region Encryption With Security Key

        public static string Encrypt(string toEncrypt, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            System.Configuration.AppSettingsReader settingsReader = new System.Configuration.AppSettingsReader();
            // Get the key from config file

            string key = (string)settingsReader.GetValue("SecurityKey",
                                                             typeof(String));
            //System.Windows.Forms.MessageBox.Show(key);
            //If hashing use get hashcode regards to your key
            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                //Always release the resources and flush data
                // of the Cryptographic service provide. Best Practice

                hashmd5.Clear();
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes.
            //We choose ECB(Electronic code Book)
            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)

            //tdes.Padding = PaddingMode.PKCS7; 
            tdes.Padding = PaddingMode.ANSIX923;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            //transform the specified region of bytes array to resultArray
            byte[] resultArray =
              cTransform.TransformFinalBlock(toEncryptArray, 0,
              toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor
            tdes.Clear();
            //Return the encrypted data into unreadable string format
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        #endregion Encryption With Security Key

        #region Decryption With Security Key

        public static string Decrypt(string cipherString, bool useHashing)
        {
            byte[] keyArray;
            //get the byte code of the string

            byte[] toEncryptArray = Convert.FromBase64String(cipherString);

            System.Configuration.AppSettingsReader settingsReader = new System.Configuration.AppSettingsReader();
            //Get your key from config file to open the lock!
            string key = (string)settingsReader.GetValue("SecurityKey", typeof(String));

            if (useHashing)
            {
                //if hashing was used get the hash code with regards to your key
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                //release any resource held by the MD5CryptoServiceProvider

                hashmd5.Clear();
            }
            else
            {
                //if hashing was not implemented get the byte code of the key
                keyArray = UTF8Encoding.UTF8.GetBytes(key);
            }

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes. 
            //We choose ECB(Electronic code Book)

            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)
            //tdes.Padding = PaddingMode.PKCS7;
            tdes.Padding = PaddingMode.ANSIX923;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(
                                 toEncryptArray, 0, toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor                
            tdes.Clear();
            //return the Clear decrypted TEXT
            return UTF8Encoding.UTF8.GetString(resultArray);
        }

        #endregion Decryption With Security Key


    }

    #endregion Random Password, Mail Send, Encryption And Decryption

    #region Encryption And Decryption Using Salt

    public class RijndaelSimple
    {
        /// <summary>
        /// Encrypts specified plaintext using Rijndael symmetric key algorithm
        /// and returns a base64-encoded result.
        /// </summary>
        /// <param name="plainText">
        /// Plaintext value to be encrypted.
        /// </param>
        /// <param name="passPhrase">
        /// Passphrase from which a pseudo-random password will be derived. The
        /// derived password will be used to generate the encryption key.
        /// Passphrase can be any string. In this example we assume that this
        /// passphrase is an ASCII string.
        /// </param>
        /// <param name="saltValue">
        /// Salt value used along with passphrase to generate password. Salt can
        /// be any string. In this example we assume that salt is an ASCII string.
        /// </param>
        /// <param name="hashAlgorithm">
        /// Hash algorithm used to generate password. Allowed values are: "MD5" and
        /// "SHA1". SHA1 hashes are a bit slower, but more secure than MD5 hashes.
        /// </param>
        /// <param name="passwordIterations">
        /// Number of iterations used to generate password. One or two iterations
        /// should be enough.
        /// </param>
        /// <param name="initVector">
        /// Initialization vector (or IV). This value is required to encrypt the
        /// first block of plaintext data. For RijndaelManaged class IV must be 
        /// exactly 16 ASCII characters long.
        /// </param>
        /// <param name="keySize">
        /// Size of encryption key in bits. Allowed values are: 128, 192, and 256. 
        /// Longer keys are more secure than shorter keys.
        /// </param>
        /// <returns>
        /// Encrypted value formatted as a base64-encoded string.
        /// </returns>
        public static string Encrypt(string plainText,
                                     string passPhrase,
                                     string saltValue,
                                     string hashAlgorithm,
                                     int passwordIterations,
                                     string initVector,
                                     int keySize)
        {
            // Convert strings into byte arrays.
            // Let us assume that strings only contain ASCII codes.
            // If strings include Unicode characters, use Unicode, UTF7, or UTF8 
            // encoding.
            byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
            byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);

            // Convert our plaintext into a byte array.
            // Let us assume that plaintext contains UTF8-encoded characters.
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            // First, we must create a password, from which the key will be derived.
            // This password will be generated from the specified passphrase and 
            // salt value. The password will be created using the specified hash 
            // algorithm. Password creation can be done in several iterations.
            PasswordDeriveBytes password = new PasswordDeriveBytes(
                                                            passPhrase,
                                                            saltValueBytes,
                                                            hashAlgorithm,
                                                            passwordIterations);

            // Use the password to generate pseudo-random bytes for the encryption
            // key. Specify the size of the key in bytes (instead of bits).
            byte[] keyBytes = password.GetBytes(keySize / 8);

            // Create uninitialized Rijndael encryption object.
            RijndaelManaged symmetricKey = new RijndaelManaged();

            // It is reasonable to set encryption mode to Cipher Block Chaining
            // (CBC). Use default options for other symmetric key parameters.
            symmetricKey.Mode = CipherMode.CBC;

            // Generate encryptor from the existing key bytes and initialization 
            // vector. Key size will be defined based on the number of the key 
            // bytes.
            ICryptoTransform encryptor = symmetricKey.CreateEncryptor(
                                                             keyBytes,
                                                             initVectorBytes);

            // Define memory stream which will be used to hold encrypted data.
            MemoryStream memoryStream = new MemoryStream();

            // Define cryptographic stream (always use Write mode for encryption).
            CryptoStream cryptoStream = new CryptoStream(memoryStream,
                                                         encryptor,
                                                         CryptoStreamMode.Write);
            // Start encrypting.
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);

            // Finish encrypting.
            cryptoStream.FlushFinalBlock();

            // Convert our encrypted data from a memory stream into a byte array.
            byte[] cipherTextBytes = memoryStream.ToArray();

            // Close both streams.
            memoryStream.Close();
            cryptoStream.Close();

            // Convert encrypted data into a base64-encoded string.
            string cipherText = Convert.ToBase64String(cipherTextBytes);

            // Return encrypted string.
            return cipherText;
        }

        /// <summary>
        /// Decrypts specified ciphertext using Rijndael symmetric key algorithm.
        /// </summary>
        /// <param name="cipherText">
        /// Base64-formatted ciphertext value.
        /// </param>
        /// <param name="passPhrase">
        /// Passphrase from which a pseudo-random password will be derived. The
        /// derived password will be used to generate the encryption key.
        /// Passphrase can be any string. In this example we assume that this
        /// passphrase is an ASCII string.
        /// </param>
        /// <param name="saltValue">
        /// Salt value used along with passphrase to generate password. Salt can
        /// be any string. In this example we assume that salt is an ASCII string.
        /// </param>
        /// <param name="hashAlgorithm">
        /// Hash algorithm used to generate password. Allowed values are: "MD5" and
        /// "SHA1". SHA1 hashes are a bit slower, but more secure than MD5 hashes.
        /// </param>
        /// <param name="passwordIterations">
        /// Number of iterations used to generate password. One or two iterations
        /// should be enough.
        /// </param>
        /// <param name="initVector">
        /// Initialization vector (or IV). This value is required to encrypt the
        /// first block of plaintext data. For RijndaelManaged class IV must be
        /// exactly 16 ASCII characters long.
        /// </param>
        /// <param name="keySize">
        /// Size of encryption key in bits. Allowed values are: 128, 192, and 256.
        /// Longer keys are more secure than shorter keys.
        /// </param>
        /// <returns>
        /// Decrypted string value.
        /// </returns>
        /// <remarks>
        /// Most of the logic in this function is similar to the Encrypt
        /// logic. In order for decryption to work, all parameters of this function
        /// - except cipherText value - must match the corresponding parameters of
        /// the Encrypt function which was called to generate the
        /// ciphertext.
        /// </remarks>
        public static string Decrypt(string cipherText,
                                     string passPhrase,
                                     string saltValue,
                                     string hashAlgorithm,
                                     int passwordIterations,
                                     string initVector,
                                     int keySize)
        {
            // Convert strings defining encryption key characteristics into byte
            // arrays. Let us assume that strings only contain ASCII codes.
            // If strings include Unicode characters, use Unicode, UTF7, or UTF8
            // encoding.
            byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
            byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);

            // Convert our ciphertext into a byte array.
            byte[] cipherTextBytes = Convert.FromBase64String(cipherText);

            // First, we must create a password, from which the key will be 
            // derived. This password will be generated from the specified 
            // passphrase and salt value. The password will be created using
            // the specified hash algorithm. Password creation can be done in
            // several iterations.
            PasswordDeriveBytes password = new PasswordDeriveBytes(
                                                            passPhrase,
                                                            saltValueBytes,
                                                            hashAlgorithm,
                                                            passwordIterations);

            // Use the password to generate pseudo-random bytes for the encryption
            // key. Specify the size of the key in bytes (instead of bits).
            byte[] keyBytes = password.GetBytes(keySize / 8);

            // Create uninitialized Rijndael encryption object.
            RijndaelManaged symmetricKey = new RijndaelManaged();

            // It is reasonable to set encryption mode to Cipher Block Chaining
            // (CBC). Use default options for other symmetric key parameters.
            symmetricKey.Mode = CipherMode.CBC;

            // Generate decryptor from the existing key bytes and initialization 
            // vector. Key size will be defined based on the number of the key 
            // bytes.
            ICryptoTransform decryptor = symmetricKey.CreateDecryptor(
                                                             keyBytes,
                                                             initVectorBytes);

            // Define memory stream which will be used to hold encrypted data.
            MemoryStream memoryStream = new MemoryStream(cipherTextBytes);

            // Define cryptographic stream (always use Read mode for encryption).
            CryptoStream cryptoStream = new CryptoStream(memoryStream,
                                                          decryptor,
                                                          CryptoStreamMode.Read);

            // Since at this point we don't know what the size of decrypted data
            // will be, allocate the buffer long enough to hold ciphertext;
            // plaintext is never longer than ciphertext.
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];

            // Start decrypting.
            int decryptedByteCount = cryptoStream.Read(plainTextBytes,
                                                       0,
                                                       plainTextBytes.Length);

            // Close both streams.
            memoryStream.Close();
            cryptoStream.Close();

            // Convert decrypted data into a string. 
            // Let us assume that the original plaintext string was UTF8-encoded.
            string plainText = Encoding.UTF8.GetString(plainTextBytes,
                                                       0,
                                                       decryptedByteCount);

            // Return decrypted string.   
            return plainText;
        }
    }

    #endregion Encryption And Decryption Using Salt

    #region Asymetric Encryption And Decryption

    public class BAGCryptography
    {
        public static RSACryptoServiceProvider rsa;

        #region Assign Parameter

        public static void AssignParameter()
        {
            const int PROVIDER_RSA_FULL = 1;
            const string CONTAINER_NAME = "BAGContainer";
            CspParameters cspParams;
            cspParams = new CspParameters(PROVIDER_RSA_FULL);
            cspParams.KeyContainerName = CONTAINER_NAME;
            cspParams.Flags = CspProviderFlags.UseMachineKeyStore;
            cspParams.ProviderName = "Microsoft Strong Cryptographic Provider";
            rsa = new RSACryptoServiceProvider(cspParams);
        }

        #endregion Assign Parameter

        #region Encryption Using Public Key

        public static string EncryptData(string data2Encrypt)
        {
            AssignParameter();
            StreamReader reader = new StreamReader(@"C:\Inetpub\wwwroot\HCLBPOCryptography\publickey.xml");
            string publicOnlyKeyXML = reader.ReadToEnd(); rsa.FromXmlString(publicOnlyKeyXML);
            reader.Close();
            //read plaintext, encrypt it to ciphertext	
            byte[] plainbytes = System.Text.Encoding.UTF8.GetBytes(data2Encrypt);
            byte[] cipherbytes = rsa.Encrypt(plainbytes, false);
            return Convert.ToBase64String(cipherbytes);
        }

        #endregion Encryption Using Public Key

        #region Public And Private Key Generation

        public static void AssignNewKey()
        {
            AssignParameter();
            //provide public and private RSA params	
            StreamWriter writer = new StreamWriter(@"C:\Inetpub\wwwroot\HCLBPOCryptography\privatekey.xml"); 
            string publicPrivateKeyXML = rsa.ToXmlString(true); 
            writer.Write(publicPrivateKeyXML);
            writer.Close();	//provide public only RSA params	
            writer = new StreamWriter(@"C:\Inetpub\wwwroot\HCLBPOCryptography\publickey.xml");
            string publicOnlyKeyXML = rsa.ToXmlString(false);
            writer.Write(publicOnlyKeyXML);
            writer.Close();
        }

        #endregion Public And Private Key Generation

        #region Decryption Using Private Key

        public static string DecryptData(string data2Decrypt)
        {
            AssignParameter();
            byte[] getpassword = Convert.FromBase64String(data2Decrypt);
            StreamReader reader = new StreamReader(@"C:\Inetpub\wwwroot\HCLBPOCryptography\privatekey.xml");
            string publicPrivateKeyXML = reader.ReadToEnd();
            rsa.FromXmlString(publicPrivateKeyXML);
            reader.Close();
            //read ciphertext, decrypt it to plaintext	
            byte[] plain = rsa.Decrypt(getpassword, false);
            return System.Text.Encoding.UTF8.GetString(plain);
        }

        #endregion Decryption Psing Private Key
    }

    #endregion Asymetric Encryption And Decryption

    public sealed class PasswordHash
    {
        HashAlgorithm hashProvider;
        int saltLength;

        /// <summary>
        /// The constructor takes a hashAlgorithm as a parameter.
        /// </summary>
        /// <param name="hashAlgorithm">
        /// A <see cref="hashAlgorithm"/> hashAlgorihm which is derived from hashAlgorithm. C# provides
        /// the following classes: SHA1Managed,SHA256Managed, SHA384Managed, SHA512Managed and MD5CryptoServiceProvider
        /// </param>
        public PasswordHash(HashAlgorithm hashAlgorithm, int saltLength)
        {
            this.hashProvider = hashAlgorithm;
            this.saltLength = saltLength;
        }

        internal static string CreateSalt(int size)
        {
            // Generate a cryptographic random number using the cryptographic
            // service provider
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[size];
            rng.GetBytes(buff);
            // Return a Base64 string representation of the random number
            return Convert.ToBase64String(buff);
        }
        /// <summary>
        /// Default constructor which initialises the saltedhash with the SHA256Managed algorithm
        /// and a salt of 4 bytes ( or 4*8 = 32 bits)
        /// </summary>
        public PasswordHash()
            : this(new SHA256Managed(), 4)
        {
        }

        /// <summary>
        /// The actual hash calculation is shared by both GethashAndsalt and the Verifyhash functions
        /// </summary>
        /// <param name="data">A byte array of the data to hash</param>
        /// <param name="salt">A byte array of the salt to add to the hash</param>
        /// <returns>A byte array with the calculated hash</returns>
        internal byte[] ComputeHash(byte[] data, byte[] salt)
        {
            // Allocate memory to store both the data and salt together
            byte[] dataAndSalt = new byte[data.Length + saltLength];

            // Copy both the data and salt into the new array
            Array.Copy(data, dataAndSalt, data.Length);
            Array.Copy(salt, 0, dataAndSalt, data.Length, saltLength);

            // Calculate the hash
            // Compute hash value of our plain text with appended salt.
            return hashProvider.ComputeHash(dataAndSalt);
        }

        /// <summary>
        /// Given a data block this routine returns both a hash and a salt
        /// </summary>
        /// <param name="data">
        /// A <see cref="System.Byte"/>byte array containing the data from which to derive the salt
        /// </param>
        /// <param name="hash">
        /// A <see cref="System.Byte"/>byte array which will contain the hash calculated
        /// </param>
        /// <param name="salt">
        /// A <see cref="System.Byte"/>byte array which will contain the salt generated
        /// </param>
        public void GetHashAndSalt(byte[] data, out byte[] hash, out byte[] salt)
        {
            // Strong runtime pseudo-random number generator, on Windows uses CryptAPI
            // on Unix /dev/urandom
            RNGCryptoServiceProvider random = new RNGCryptoServiceProvider();
            
            // Allocate memory for the salt
            salt = new byte[saltLength];



            // Create a random salt
            random.GetNonZeroBytes(salt);


            // Compute hash value of our data with the salt.
            hash = ComputeHash(data, salt);
           
        }

        /// <summary>
        /// The routine provides a wrapper around the GethashAndsalt function providing conversion
        /// from the required byte arrays to strings. Both the hash and salt are returned as Base-64 encoded strings.
        /// </summary>
        /// <param name="data">
        /// A <see cref="System.String"/> string containing the data to hash
        /// </param>
        /// <param name="hash">
        /// A <see cref="System.String"/> base64 encoded string containing the generated hash
        /// </param>
        /// <param name="salt">
        /// A <see cref="System.String"/> base64 encoded string containing the generated salt
        /// </param>
        public void GetHashAndSaltString(string data, out string hash, out string salt)
        {
            byte[] hashOut;
            byte[] saltOut;

            // Obtain the hash and salt for the given string
            GetHashAndSalt(Encoding.UTF8.GetBytes(data), out hashOut, out saltOut);

            // Transform the byte[] to Base-64 encoded strings
            hash = Convert.ToBase64String(hashOut);
            salt = Convert.ToBase64String(saltOut);
        }

        /// <summary>
        /// This routine verifies whether the data generates the same hash as we had stored previously
        /// </summary>
        /// <param name="data">The data to verify </param>
        /// <param name="hash">The hash we had stored previously</param>
        /// <param name="salt">The salt we had stored previously</param>
        /// <returns>True on a succesfull match</returns>
        public bool VerifyHash(byte[] data, byte[] hash, byte[] salt)
        {
            byte[] Newhash = ComputeHash(data, salt);

            //  No easy array comparison in C# -- we do the legwork
            if (Newhash.Length != hash.Length) return false;

            for (int Lp = 0; Lp < hash.Length; Lp++)
                if (!hash[Lp].Equals(Newhash[Lp]))
                    return false;

            return true;
        }

        /// <summary>
        /// This routine provides a wrapper around Verifyhash converting the strings containing the
        /// data, hash and salt into byte arrays before calling Verifyhash.
        /// </summary>
        /// <param name="data">A UTF-8 encoded string containing the data to verify</param>
        /// <param name="hash">A base-64 encoded string containing the previously stored hash</param>
        /// <param name="salt">A base-64 encoded string containing the previously stored salt</param>
        /// <returns></returns>
        internal bool VerifyHashString(string data, string hash, string salt)
        {
            byte[] hashToVerify = Convert.FromBase64String(hash);
            byte[] saltToVerify = Convert.FromBase64String(salt);
            byte[] dataToVerify = Encoding.UTF8.GetBytes(data);
            return VerifyHash(dataToVerify, hashToVerify, saltToVerify);
        }

    }
}
