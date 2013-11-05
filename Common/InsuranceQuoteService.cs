using System;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace Common
{

    [ServiceContract]
    public interface IInsuranceQuoteService
    {
        [OperationContract]
        Policy[] FindPolicies(FindPolicyRequest req);
    }

    [DataContract]
    public class FindPolicyRequest
    {
        [DataMember]
        public string PersonSSN { get; set; }

        [DataMember]
        public DateTime DateOfBirth { get; set; }

        [DataMember]
        public string Gender { get; set; }

        [DataMember]
        public string PolicyType { get; set; }

    }

   

    [DataContract]
    public class Policy
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public String PolicyName { get; set; }


        [DataMember]
        public decimal BasePrice { get; set; }
    }
}