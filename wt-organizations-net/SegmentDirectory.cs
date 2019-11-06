using System.Collections.Generic;
using System.Threading.Tasks;
using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
using WindingTreeOrganizationsNet.Helper;

namespace WindingTreeOrganizationsNet
{
    public class SegmentDirectory : BaseWTEntryPoint
    {
        private readonly string _entryPoint;
        private const string AbiPath = "WindingTreeOrganizationsNet.ABIs.SegmentDirectory.json";

        private readonly string _abi;
        private readonly Contract _contract;

        public SegmentDirectory(SegmentIndex segment,string endpointUrl, string publicKey, string privateKeyOrMnenomic, string password = "") :base(endpointUrl, publicKey, privateKeyOrMnenomic, password)
        {
            switch (segment)
            {
                case SegmentIndex.AIRLINES:
                    _entryPoint = SegmentEntryPoints.Airlines;
                    break;
                case SegmentIndex.HOTELS:
                    _entryPoint = SegmentEntryPoints.Hotels;
                    break;
                default:
                    break;
            }
            this._abi = JsonResourceReader.ReadJsonFromResource(AbiPath);
            _contract = Web3.Eth.GetContract(_abi, _entryPoint);
        }

        public async Task<TransactionReceipt> Add(string address, HexBigInteger gas = null, HexBigInteger value = null)
        {
            Function of = _contract.GetFunction("add");
            return await of.SendTransactionAndWaitForReceiptAsync(this.PublicKey, gas ?? base.Gas, value ?? base.Value, null, new object[] { address });
        }

        public async Task<TransactionReceipt> Remove(string address, HexBigInteger gas = null, HexBigInteger value = null)
        {
            Function of = _contract.GetFunction("remove");
            return await of.SendTransactionAndWaitForReceiptAsync(this.PublicKey, gas ?? base.Gas, value ?? base.Value, null, new object[] { address });
        }

        public async Task<List<string>> GetOrganizations()
        {
            return await _contract.GetFunction("getOrganizations").CallAsync<List<string>>();
        }

        public async Task<int> OrganizationsIndex(string address)
        {
            return await _contract.GetFunction("organizationsIndex").CallAsync<int>(new object[] { address });
        }

    }
}
