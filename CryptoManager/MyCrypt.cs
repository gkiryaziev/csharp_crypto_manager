using System;
using System.Security.Cryptography;
using System.Text;

namespace GAKiryaziev
{
  class MyCrypt
  {
    // создаем объект для получения средств шифрования
      MD5CryptoServiceProvider md5cr = new MD5CryptoServiceProvider();

    //-------------------------------
    // получаем хеш MD5
    //-------------------------------
    public string GetHashMD5(string str)
    {
      // вычисляем хеш-представление в байтах
      byte[] byteHash = md5cr.ComputeHash(Encoding.UTF8.GetBytes(str));
	  //byte[] byteHash = md5cr.ComputeHash(Encoding.Unicode.GetBytes(str));

      string hash = string.Empty;

      // формируем строку из массива байт
      foreach(byte b in byteHash)
      {
        hash += string.Format("{0:x2}", b);
      }
      return hash;
    }
    
    //-----------------------------------
    // сверяем строку и хеш
    //-----------------------------------
    public bool VerifyHashMD5(string str, string hash)
    {
      // строка для сверки
      string hashOfInput = GetHashMD5(str);
      
      StringComparer comparer = StringComparer.OrdinalIgnoreCase;
      
      if (0 == comparer.Compare(hashOfInput, hash))
      {
        return true;
      }
      else
      {
        return false;
      }
    }

    //--------------------------------
    // XOR шифрование
    //--------------------------------     
    public string XORCrypt(string text, string key)
    {
      var result = new StringBuilder();

      for (int c = 0; c < text.Length; ++c)
        result.Append((char)((uint)text[c] ^ (uint)key[c % key.Length]));

      return result.ToString();
    }
	
	//==============================================
	// получаем пароль в виде хеша SHA1
	//==============================================
	public static string SHA1(string password)
	{
		SHA1Managed sha1 = new SHA1Managed();		
		string salt = "Pa$$w0rd";
		string final = string.Concat(password, salt);
		byte[] buffer = Encoding.UTF8.GetBytes(final);
		byte[] hash = sha1.ComputeHash(buffer);

		return BitConverter.ToString(hash).Replace("-", "").ToLower();
	}

	public static string SHA256(string password)
	{
		SHA256Managed sha256 = new SHA256Managed();
		string salt = "Pa$$w0rd";
		string final = string.Concat(password, salt);
		byte[] buffer = Encoding.UTF8.GetBytes(final);		
		byte[] hash = sha256.ComputeHash(buffer);
		
		return BitConverter.ToString(hash).Replace("-","").ToLower();
	}

	
	//==============================================
	// генерируем пароль
	//==============================================
	public static string PasswordGenerator(int passwordLength, bool strongPassword)
	{
		Random random = new Random();

		int seed = random.Next(1, int.MaxValue);

		const string allowedChars = "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm0987654321";
		const string specialChars = "~!@#$%^&*()_+-=|{}[]\\/';:><";

		char[] charsArray = new char[passwordLength];
		Random rnd = new Random(seed);

		for (int i = 0; i < passwordLength; ++i)
		{ 
			// спец. символы ?
			if (strongPassword && i % random.Next(3, passwordLength) == 0)
			{
				charsArray[i] = specialChars[rnd.Next(0, specialChars.Length)];
			}
			else
			{
				charsArray[i] = allowedChars[rnd.Next(0, allowedChars.Length)];
			}
		}

		return new string(charsArray);
	}
  }
}
