using System;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
using WindingTreeOrganizationsNet;
using WindingTreeOrganizationsNet.Helper;

namespace WindingTreeOrganizationConsoleTest
{
    class Program
    {

        private static string endpointUrl = string.Empty;
        private static string privateKey = string.Empty;

        private static string publicKey = string.Empty;

        static void Main(string[] args)
        {
            Console.WriteLine("Enter endpoint url:");
            endpointUrl = Console.ReadLine();
            Console.WriteLine("Enter private key or mnemonic:");
            privateKey = Console.ReadLine();
            Console.WriteLine("Enter public key:");
            publicKey = Console.ReadLine();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Please make a choice");
                Console.WriteLine("1. Add a new organization");
                Console.WriteLine("2. Add an existing organization to a segment directory");
                Console.WriteLine("3. Update the ORG.Json for an existing organization");
                Console.WriteLine("9. Exit");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddNewOrg();
                        break;

                    case "2":
                        AddOrgToSegment();
                        break;

                    case "3":
                        UpdateJsonOrg();
                        break;
                    case "9":
                        return;
                       
                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }

            }

        }

        private static void UpdateJsonOrg()
        {
            Console.WriteLine("Enter your organizations smart contract address:");
            string orgKey = Console.ReadLine();

            Organization organization = new Organization(orgKey, endpointUrl, publicKey, privateKey);

            Console.WriteLine($"Current JSON url: {organization.GetOrgJsonUri().Result}");
            Console.WriteLine($"Current JSON hash: {KeccakDigest.getHexHash(organization.GetOrgJsonHash().Result)}");


            Console.WriteLine("Enter a new ORG.Json URL");
            string jsonUrl = Console.ReadLine();

            Console.WriteLine("Updating URI and Hash ..");
            TransactionReceipt tr = organization.ChangeOrgJsonUriAndHash(jsonUrl, null, new HexBigInteger(new BigInteger(00000000000))).Result;
            Console.WriteLine(FormatTransactionReceipt(tr));
            Console.ReadLine();
        }

        private static void AddOrgToSegment()
        {
            Console.WriteLine("Enter your organizations smart contract address:");
            string orgKey = Console.ReadLine();

            Console.WriteLine("Enter the segment [hotels|airlines]:");
            string segment = Console.ReadLine();

            SegmentIndex index = 0;
            switch (segment)
            {
                case "hotels":
                    index = SegmentIndex.HOTELS;
                    break;
                case "airlines":
                    index = SegmentIndex.AIRLINES;
                    break;
                default:
                    throw new Exception("Invalid segment");
            }

            SegmentDirectory segmentDirectory = new SegmentDirectory(index, endpointUrl, publicKey, privateKey);
            TransactionReceipt tr = segmentDirectory.Add(orgKey, null, new HexBigInteger(new BigInteger(00000000000))).Result;

            Console.WriteLine(FormatTransactionReceipt(tr));
            Console.ReadKey();
        }

        static void AddNewOrg()
        {

            Console.Clear();
            Console.WriteLine("Please host this json somewhere and enter the URL:");
            string jsonUrl = Console.ReadLine();

            OrganizationFactory of = new OrganizationFactory(endpointUrl, publicKey, privateKey);
            TransactionReceipt tr = of.Create(jsonUrl,null, new HexBigInteger(new BigInteger(00000000000))).Result;

            Console.WriteLine(FormatTransactionReceipt(tr));

            if ((bool)tr.Succeeded())
            {
                Console.WriteLine($"Created organization contract address:{tr.Logs[0]["address"].ToString()}");
                Console.WriteLine($"TransactionHash: {tr.Logs[0]["blockHash"].ToString()}");
                Console.WriteLine($"Json URL: {jsonUrl}");

                Console.ReadLine();
            }
            else
            {
                Console.WriteLine($"Error creating contract. See details:");
                Console.WriteLine($"{tr.Logs}");
                Console.ReadLine();
            }
        }

        static string GenerateJson()
        {
            Console.Clear();
            Console.WriteLine("Getting information for ORG.JSON");
            Console.WriteLine("Legal entity information.");
            Console.Write("Name: ");
            string legalEntityName = Console.ReadLine();
            Console.WriteLine("Address information");
            Console.Write("Road: ");
            string legalEntityAddressRoad = Console.ReadLine();
            Console.Write("Housenumber: ");
            string legalEntityAddressHousenumber = Console.ReadLine();
            Console.Write("City: ");
            string legalEntityAddressCity = Console.ReadLine();
            Console.Write("CountryCode: ");
            string legalEntityAddressCountryCode = Console.ReadLine();

            Console.WriteLine();
            Console.WriteLine("Contact information.");
            Console.Write("Email: ");
            string contactEmail = Console.ReadLine();

            Console.WriteLine();
            Console.Write("Would you like to add a hotel or an airline");
            string rootSegment = Console.ReadLine();

            Console.WriteLine();
            Console.WriteLine($"{rootSegment} information.");
            Console.Write("latitude: ");
            string lat = Console.ReadLine();

            Console.Write("longitude: ");
            string lon = Console.ReadLine();

            Console.Write($"{rootSegment} name: ");
            string hotelname = Console.ReadLine();
            Console.Write($"{rootSegment} website: ");
            string website = Console.ReadLine();

            return "";

        }


        public static string FormatTransactionReceipt(TransactionReceipt tr)
        {
            return $"Status: {tr.Status}\n" +
                $"\tTransactionHash: {tr.TransactionHash}\n" +
                $"\tTransactionIndex: {tr.TransactionIndex}\n" +
                $"\tHasErrors: {tr.HasErrors()}";

        }
    }
}
