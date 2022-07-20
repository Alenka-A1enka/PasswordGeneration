using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace pass_generation
{
    public class GetPasswordData
    {
        /// <summary>
        /// По очереди вызывает все необходимые методы: приведение длины пароля к стандартной, 
        /// приведение длины соля к стандартной, генерация пароля, генерация соли, хеширование
        /// </summary>
        /// <param name="pass_length"></param>
        /// <param name="salt_length"></param>
        /// <param name="passData"></param>
        public static void Get(int pass_length, int salt_length, PasswordData passData)
        {
            pass_length = GetCorrectPassWordLength(pass_length);
            salt_length = GetCorrectSaltLength(salt_length);
            SetNumericPass(pass_length, passData);
            SetSalt(salt_length, passData);
            GetHash(passData);
        }
        static int GetCorrectPassWordLength(int pass_length)
        {
            //коррекция длины пароля
            if (pass_length < 4) return 4;
            if (pass_length > 8) return 8;
            if (pass_length % 2 == 1) return pass_length + 1;
            return pass_length;
        }
        static int GetCorrectSaltLength(int salt_length)
        {
            //коррекция длины соли
            if (salt_length <= 0) return 1;
            return salt_length;
        }
        static void SetNumericPass(int pass_length, PasswordData passData)
        {
            passData.Password = "";

            Random r = new Random();
            int[] mas = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            for (int i = 0; i < pass_length; i++)
            {
                //генерация рандомного индекса, не превышающего размер массива mas
                int index = Convert.ToInt32(r.NextDouble() * mas.Length) % mas.Length;
                passData.Password += mas[index].ToString();
            }
        }
        static void SetSalt(int salt_length, PasswordData passData)
        {
            passData.Salt = "";

            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            byte[] mas = new byte[salt_length];

            //генерация соли в байтовом массиве заданного размера
            crypto.GetBytes(mas);

            try
            {
                //перевод байтового массива в строку
                string value = "";
                foreach(byte b in mas)
                {
                    value += b.ToString();
                }
                passData.Salt = value.Substring(0, salt_length);
            }
            catch(ArgumentOutOfRangeException e)
            {
                //заданная длина соли не удовлетворяет требованиям
                passData.Salt = "";
            }
        }
        /// <summary>
        /// Хеширует пароль и соль
        /// </summary>
        /// <param name="passData"></param>
        static void GetHash(PasswordData passData)
        {
            passData.Hash = "";
            string text = passData.Password + passData.Salt;

            //генерация хеша на основе пароля и хеша, метод sha1
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(text));
                var sb = new StringBuilder(hash.Length * 2);

                foreach (byte b in hash)
                {
                    sb.Append(b.ToString("X2"));
                }

                passData.Hash = sb.ToString();
            }
        }
    }
}
