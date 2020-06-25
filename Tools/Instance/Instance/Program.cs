using Amazon;
using Amazon.EC2;
using Amazon.EC2.Model;
using Amazon.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Instance
{
    class Program
    {
        static void Main(string[] args)
        {
            string amiId = "ami-09194d3374023cff7";
            string securityGroupId = "sg-010be23ad158d5685";
            string keyPairName = "DevOps_Wi";
            string instanceSize = "t2.micro";

            Amazon.Util.ProfileManager.RegisterProfile("default", "MyAccessKey", "MySecretKey");
            var ec2Client = new AmazonEC2Client(RegionEndpoint.USWest2);
            string subnetId = "subnet-cc4083b4";
            InstanceNetworkInterfaceSpecification networkSpecification = new InstanceNetworkInterfaceSpecification()
            {
                DeviceIndex = 0,
                SubnetId = subnetId,
                Groups = new List<string>() { securityGroupId },
                AssociatePublicIpAddress = true
            };
            List<InstanceNetworkInterfaceSpecification> networkSpecifications = new List<InstanceNetworkInterfaceSpecification>() { networkSpecification };

            RunInstancesRequest launchRequest = new RunInstancesRequest()
            {
                ImageId = amiId,
                InstanceType = instanceSize,
                MinCount = 1,
                MaxCount = 1,
                KeyName = keyPairName,
                NetworkInterfaces = networkSpecifications
            };

            var data = ec2Client.RunInstancesAsync(launchRequest);
            while ( ! data.IsCompleted)
            {
                Console.WriteLine("Still contnues to run");
                Thread.Sleep(10000);
            }

            var result = data.Result;
            Console.WriteLine(result.ToString());
           
        }
    }
}
