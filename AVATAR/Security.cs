using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AVATAR
{
    public class Security
    {
        public void Encrypt_Data(string file_to_encrypt)
        {
            string PlainText=File.ReadAllText(file_to_encrypt);
            string Password= "4NtaYw9qTxrQKCRpIR6dvB6NqabIsBhISRkA2vOd/SLNqDQEOgHoF3kuX2";
            string Salt = "Kosher";
            string HashAlgorithm = "SHA1";
            int PasswordIterations = 2;
            string InitialVector = "OFRna73m*aze01xY";
            int KeySize = 256;
            if (string.IsNullOrEmpty(PlainText))
            {

            }
            byte[] InitialVectorBytes = Encoding.ASCII.GetBytes(InitialVector);
            byte[] SaltValueBytes = Encoding.ASCII.GetBytes(Salt);
            byte[] PlainTextBytes = Encoding.UTF8.GetBytes(PlainText);
            PasswordDeriveBytes DerivedPassword = new PasswordDeriveBytes(Password, SaltValueBytes, HashAlgorithm, PasswordIterations);
            byte[] KeyBytes = DerivedPassword.GetBytes(KeySize / 8);
            RijndaelManaged SymmetricKey = new RijndaelManaged();
            SymmetricKey.Mode = CipherMode.CBC;
            byte[] CipherTextBytes = null;
            using (ICryptoTransform Encryptor = SymmetricKey.CreateEncryptor(KeyBytes, InitialVectorBytes))
            {
                using (MemoryStream MemStream = new MemoryStream())
                {
                    using (CryptoStream CryptoStream = new CryptoStream(MemStream, Encryptor, CryptoStreamMode.Write))
                    {
                        CryptoStream.Write(PlainTextBytes, 0, PlainTextBytes.Length);
                        CryptoStream.FlushFinalBlock();
                        CipherTextBytes = MemStream.ToArray();
                        MemStream.Close();
                        CryptoStream.Close();
                    }
                }
            }
            SymmetricKey.Clear();

            string encrypted_data = Convert.ToBase64String(CipherTextBytes);
            File.WriteAllText(file_to_encrypt, encrypted_data);
        }
        public void Decrypt_Data(string file_to_decrypt)
        {
            string CipherText= File.ReadAllText(file_to_decrypt);
            string Password= "4NtaYw9qTxrQKCRpIR6dvB6NqabIsBhISRkA2vOd/SLNqDQEOgHoF3kuX2";
            string Salt = "Kosher";
            string HashAlgorithm = "SHA1";
            int PasswordIterations = 2;
            string InitialVector = "OFRna73m*aze01xY";
            int KeySize = 256;
            if (string.IsNullOrEmpty(CipherText))
            {
                
            }
            byte[] InitialVectorBytes = Encoding.ASCII.GetBytes(InitialVector);
            byte[] SaltValueBytes = Encoding.ASCII.GetBytes(Salt);
            byte[] CipherTextBytes = Convert.FromBase64String(CipherText);
            PasswordDeriveBytes DerivedPassword = new PasswordDeriveBytes(Password, SaltValueBytes, HashAlgorithm, PasswordIterations);
            byte[] KeyBytes = DerivedPassword.GetBytes(KeySize / 8);
            RijndaelManaged SymmetricKey = new RijndaelManaged();
            SymmetricKey.Mode = CipherMode.CBC;
            byte[] PlainTextBytes = new byte[CipherTextBytes.Length];
            int ByteCount = 0;
            using (ICryptoTransform Decryptor = SymmetricKey.CreateDecryptor(KeyBytes, InitialVectorBytes))
            {
                using (MemoryStream MemStream = new MemoryStream(CipherTextBytes))
                {
                    using (CryptoStream CryptoStream = new CryptoStream(MemStream, Decryptor, CryptoStreamMode.Read))
                    {
                        ByteCount = CryptoStream.Read(PlainTextBytes, 0, PlainTextBytes.Length);
                        MemStream.Close();
                        CryptoStream.Close();
                    }
                }
            }

            SymmetricKey.Clear();

            string decrypted_data = Encoding.UTF8.GetString(PlainTextBytes, 0, ByteCount);
            File.WriteAllText(file_to_decrypt, decrypted_data);
        }

        public string Random_Security_Key()
        {
            string input = "abcdefghijklmnopqrstuvwxyz0123456789";

            Random rand = new Random();
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < 12; i++)
            {
                ch = input[rand.Next(0, input.Length)];
                builder.Append(ch);
            }
            return builder.ToString();
        }
    }
}
