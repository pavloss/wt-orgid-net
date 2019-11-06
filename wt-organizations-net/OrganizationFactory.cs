using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
using Newtonsoft.Json.Linq;
using WindingTreeOrganizationsNet.Helper;

namespace WindingTreeOrganizationsNet
{
    public class OrganizationFactory : BaseWTEntryPoint
    {

        private const string EntryPoint = "0x8E6463ea056d812094Ed514455Ab3C88fc23D59C";
        private const string AbiPath = "WindingTreeOrganizationsNet.ABIs.OrganizationFactory.json";

        private readonly string _abi;
        private readonly Contract _contract;

        public OrganizationFactory(string endpointUrl, string publicKey, string privateKeyOrMnenomic, string password = "") :base(endpointUrl, publicKey, privateKeyOrMnenomic, password)
        {
            this._abi = JsonResourceReader.ReadJsonFromResource(AbiPath);
            _contract = Web3.Eth.GetContract(_abi, EntryPoint);
        }

        public async Task<int> GetCreatedOrganizationsLength()
        {
            return await _contract.GetFunction("getCreatedOrganizationsLength").CallAsync<int>();
        }

        public async Task<List<string>> GetCreatedOrganizations()
        {
            return await _contract.GetFunction("getCreatedOrganizations").CallAsync<List<string>>();
        }

        public async Task<TransactionReceipt> Create(string jsonUri, HexBigInteger gas = null, HexBigInteger value = null)
        {
            //download and calculate the hash from URI content
            using var client = new WebClient();
            var jsonContent = client.DownloadString(jsonUri);

            if (string.IsNullOrEmpty(jsonContent) || !jsonContent.IsValidJson())
                throw new Exception("Invalid JSON format.");

            Function of = _contract.GetFunction("create");
            return await of.SendTransactionAndWaitForReceiptAsync(this.PublicKey, gas ?? base.Gas, value ?? base.Value, null, new object[] { jsonUri, Helper.KeccakDigest.getByteHash(jsonContent) });
        }

        public async Task<TransactionReceipt> CreateAndAddToDirectory(string jsonUri, string directoryAddress, HexBigInteger gas = null, HexBigInteger value = null)
        {
            //download and calculate the hash from URI content
            using var client = new WebClient();  
            var jsonContent = client.DownloadString(jsonUri); 

            Function of = _contract.GetFunction("createAndAddToDirectory");
            return await of.SendTransactionAndWaitForReceiptAsync(this.PublicKey, gas ?? base.Gas, value ?? base.Value, null, new object[] { jsonUri, Helper.KeccakDigest.getByteHash(jsonContent), directoryAddress });
        }

    }
}
