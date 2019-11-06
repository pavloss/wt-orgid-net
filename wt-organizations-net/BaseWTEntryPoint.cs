using System;
using Nethereum.Web3;
using Nethereum.HdWallet;
using Nethereum.Web3.Accounts;
using Nethereum.Hex.HexTypes;
using System.Numerics;

namespace WindingTreeOrganizationsNet
{
    public class BaseWTEntryPoint
    {
        protected string EndpointUrl;
        protected string PublicKey;
        protected Web3 Web3;

        protected string _privateKeyOrMnenomic;
        protected string _password;

        protected HexBigInteger Gas = new HexBigInteger(new BigInteger(4600000));
        protected HexBigInteger Value = new HexBigInteger(new BigInteger(00000000000));

        public BaseWTEntryPoint(string endpointUrl, string publicKey, string privateKeyOrMnenomic, string password = "")
        {
            EndpointUrl = endpointUrl;
            PublicKey = publicKey;
            _privateKeyOrMnenomic = privateKeyOrMnenomic;
            _password = password;

            Connect();
        }

        public void Connect()
        {   
            Account account;

            if (_privateKeyOrMnenomic.Contains(" "))
            {
                // it's a Mnenomic
                var wallet = new Wallet(_privateKeyOrMnenomic, _password);
                account = wallet.GetAccount(PublicKey);
            }
            else
            {
                // we assume it's a private key
                account = new Account(_privateKeyOrMnenomic);
            }
            //Creates the connecto to the network and gets an instance of the contract.
            try
            {
                this.Web3 = new Web3(account, EndpointUrl);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public enum SegmentIndex
    {
        HOTELS = 1,
        AIRLINES = 2
    }

    public static class SegmentEntryPoints
    {
        public static string Hotels { get { return "0x2c29c421d7fd7be4cc2bfb6d1a44426e43021914"; } }
        public static string Airlines { get { return "0x7d3Ce0d422D381971Eb2bD9615b58f2df5C9f047"; } }
    }

}
