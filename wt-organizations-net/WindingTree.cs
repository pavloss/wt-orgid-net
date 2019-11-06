using System.Threading.Tasks;
using Nethereum.Contracts;
using WindingTreeOrganizationsNet.Helper;

namespace WindingTreeOrganizationsNet
{
    public class WindingTree : BaseWTEntryPoint
    {

        private const string EntryPoint = "0xa268937c2573e2AB274BF6d96e88FfE0827F0D4D";
        private const string AbiPath = "WindingTreeOrganizationsNet.ABIs.WindingTreeEntrypoint.json";

        private readonly string _abi;
        private readonly Contract _contract;

        public WindingTree(string endpointUrl, string publicKey, string privateKeyOrMnenomic, string password = "") :base(endpointUrl, publicKey, privateKeyOrMnenomic, password)
        {
            this._abi = JsonResourceReader.ReadJsonFromResource(AbiPath);
            _contract = Web3.Eth.GetContract(_abi, EntryPoint);
        }

        public async Task<string> Segments(int index)
        {
            return await _contract.GetFunction("segments").CallAsync<string>(new object[] { index });
        }

        public async Task<string> GetOrganizationFactory()
        {
            return await _contract.GetFunction("getOrganizationFactory").CallAsync<string>();
        }

    }
}
