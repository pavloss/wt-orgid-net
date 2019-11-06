using System;
using System.Text;

namespace WindingTreeOrganizationsNet.Helper
{
    public static class KeccakDigest
    {
        public static string getHexHash(string input)
        {
            byte[] hashedBytes = getByteHash(input);
            return BitConverter.ToString(hashedBytes).Replace("-", "");
        }

        public static byte[] getByteHash(string input)
        {
            var digest = new Org.BouncyCastle.Crypto.Digests.KeccakDigest(256);
            digest.Reset();
            byte[] bytes = Encoding.UTF8.GetBytes(input);
            digest.BlockUpdate(bytes, 0, bytes.Length);
            byte[] hashedBytes = new byte[256 / 8];
            digest.DoFinal(hashedBytes, 0);

            return hashedBytes;
        }
    }
}
