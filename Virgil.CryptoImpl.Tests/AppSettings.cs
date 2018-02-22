using System.Configuration;

namespace Virgil.CryptoImpl.Tests
{
    public class AppSettings
    {
        public static string PrivateKeySTC31_1 = ConfigurationManager.AppSettings["test:PrivateKeySTC31_1"];
        public static string PrivateKeySTC31_2 = ConfigurationManager.AppSettings["test:PrivateKeySTC31_2"];
        public static string PublicKeySTC32 = ConfigurationManager.AppSettings["test:PublicKeySTC32"];
    }
}
