# Winding Tree ORG.ID .NET library

wt-orgid-net is a .NET wrapper for interfacing with the ORGID smart contracts in the Ethereum blockchain. 


## Team Members

* Pavlos Syngelakis, pavlos.syngelakis@gmail.com, @pavloss
* Marc Laros, marc@smlss.nl, @mlaros

## Repository

https://github.com/pavloss/wt-orgid-net

## Description

wt-orgid-net is a .NET wrapper for interfacing with the 0xORG smart contract interfaces in the Ethereum blockchain. It provides an easy-to-use interface for a large set of the functions described at https://debugging-tools.windingtree.com/contract-caller.

The repo consists of 2 parts:
- A .net core 3.0 library 
- A sample .net core 3.0 console app that demos the library. It features easy onboarding of an organization, adding an organization to a segment directory, and updating the JSON for an organization.

Getting it up and running:

$ git clone https://github.com/pavloss/wt-orgid-net.git
$ cd wt-orgid-net/WindingTreeOrganizationConsoleTest/
$ dotnet run

Enter your (infura) endpoint url, your private and public key, and get started with the onboarding proces.

Please make a choice
1. Add a new organization
2. Add an existing organization to a segment directory
3. Update the ORG.Json for an existing organization
9. Exit



## Problems
Starting with blockchain and windingtree can be very challenging due to the complicated terminology and interfaces. The aim was to give new developers a good starting point to build on.

## Solution
By creating a wrapper library, we are taking away the complicated work of calculating hashes, learning about ABI's and how the smart contracts are programmed. 

## We learned

## Extra resources

* link to presentation (and/or)
* link to running instance
