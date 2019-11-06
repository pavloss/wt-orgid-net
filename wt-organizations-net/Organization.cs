using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
using WindingTreeOrganizationsNet.Helper;

namespace WindingTreeOrganizationsNet
{
    public class Organization : BaseWTEntryPoint
    {

       
        private const string AbiPath = "WindingTreeOrganizationsNet.ABIs.Organization.json";

        private readonly string _abi;
        private readonly Contract _contract;

        public Organization(string organizationContractAddress, string endpointUrl, string publicKey, string privateKeyOrMnenomic, string password = "") :base(endpointUrl, publicKey, privateKeyOrMnenomic, password)
        {
            this._abi = JsonResourceReader.ReadJsonFromResource(AbiPath);
            _contract = Web3.Eth.GetContract(_abi, organizationContractAddress);
        }

        public async Task<string> GetOrgJsonUri()
        {
            return await _contract.GetFunction("getOrgJsonUri").CallAsync<string>();
        }

        public async Task<string> GetOrgJsonHash()
        {
            return await _contract.GetFunction("getOrgJsonHash").CallAsync<string>();
        }

        public async Task<List<string>> GetAssociatedKeys()
        {
            return await _contract.GetFunction("getAssociatedKeys").CallAsync<List<string>>();
        }

        public async Task<TransactionReceipt> AddAssociatedKey(string key, HexBigInteger gas = null, HexBigInteger value = null)
        {
            Function of = _contract.GetFunction("addAssociatedKey");
            return await of.SendTransactionAndWaitForReceiptAsync(this.PublicKey, gas ?? base.Gas , value ?? base.Value, null, new object[] { key });
        }

        public async Task<TransactionReceipt> RemoveAssociatedKey(string key, HexBigInteger gas = null, HexBigInteger value = null)
        {
            Function of = _contract.GetFunction("removeAssociatedKey");
            return await of.SendTransactionAndWaitForReceiptAsync(this.PublicKey, gas ?? base.Gas, value ?? base.Value, null, new object[] { key });
        }

        public async Task<TransactionReceipt> ChangeOrgJsonUri(string jsonUri, HexBigInteger gas = null, HexBigInteger value = null)
        {
            Function of = _contract.GetFunction("changeOrgJsonUri");
            return await of.SendTransactionAndWaitForReceiptAsync(this.PublicKey, gas ?? base.Gas, value ?? base.Value, null, new object[] { jsonUri });
        }

        public async Task<TransactionReceipt> ChangeOrgJsonHash(string jsonHash, HexBigInteger gas = null, HexBigInteger value = null)
        {
            Function of = _contract.GetFunction("changeOrgJsonHash");
            return await of.SendTransactionAndWaitForReceiptAsync(this.PublicKey, gas ?? base.Gas, value ?? base.Value, null, new object[] { jsonHash });
        }

        public async Task<TransactionReceipt> ChangeOrgJsonUriAndHash(string jsonUri, HexBigInteger gas = null, HexBigInteger value = null)
        {
            using var client = new WebClient();
            var jsonContent = client.DownloadString(jsonUri);

            Function of = _contract.GetFunction("changeOrgJsonUriAndHash");
            return await of.SendTransactionAndWaitForReceiptAsync(this.PublicKey, gas ?? base.Gas, value ?? base.Value, null, new object[] { jsonUri, KeccakDigest.getByteHash(jsonContent) });
        }

        public async Task<TransactionReceipt> TransferOwnership(string newOwner, HexBigInteger gas = null, HexBigInteger value = null)
        {
            Function of = _contract.GetFunction("transferOwnership");
            return await of.SendTransactionAndWaitForReceiptAsync(this.PublicKey, gas ?? base.Gas, value ?? base.Value, null, new object[] { newOwner });
        }



    }
}
