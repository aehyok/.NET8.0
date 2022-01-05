using aehyok.Core.Data;
using System.Threading.Tasks;

namespace aehyok.Lib.Services
{
    public interface IEncryptionService: IDependency
    {
        Task<string> GetCodeByToken();

        string CreateSaltKey(int size);

        string CreatePasswordHash(string password, string saltKey, string passwordFormat);

        string CreateHash(byte[] data, string hashAlgorithm);

        string CreateHash(string source, string hashAlgorithm);

        Task<string> EncryptText(string plainText, string encryptionPrivateKey = "");

        Task<string> DecryptText(string cipherText, string encryptionPrivateKey = "");
    }
}