using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Sockets;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace OverpricedInsuranceCo
{
    class Program
    {
        private static ServiceHost _host;

        static void Main(string[] args)
        {
            if ((args.Length > 0 && args[0] == "seed"))
            {

                //create me 20 policies where the minimm age is 25

                //    var m25Policies = from i in Enumerable.Range(1, 20)
                //        select new InsurancePolicy()
                //        {
                //            PolicyName = "Minimum 25 Policy " + i,
                //            BasePrice = 10M + Convert.ToDecimal(i/10),
                //            MaximumAge = 25

                //        };

                //  //create me 15 policies wehre the minimum age is 30
                //var m30Policies = from i in Enumerable.Range(1, 15)
                //        select new InsurancePolicy()
                //        {
                //            PolicyName = "Minimum 30 Policy " + i,
                //            BasePrice = 10M + Convert.ToDecimal(i/10),
                //            MaximumAge = 30

                //        };

                //and last but not least a bunch of legacy data with no age limit
                var noAgePolicies = from i in Enumerable.Range(1, 10000000)
                    select new InsurancePolicy()
                    {
                        PolicyName = "No Age Policy " + i,
                        BasePrice = 10M + Convert.ToDecimal(i/10),

                    };

                var policiesToInsert = noAgePolicies;

                var seeder = new DataSeeder();
                seeder.InsertRows();


            }
            else
            {
                _host = new ServiceHost(typeof (InsuranceQuoteService), new Uri("http://localhost:8088/ins"));
                _host.AddDefaultEndpoints();
                _host.Open();
                Console.WriteLine("Listening");
                Console.ReadLine();
            }
        }
    }

    public class InsuranceQuoteService : IInsuranceQuoteService
    {
        public Policy[] FindPolicies(FindPolicyRequest req)
        {
            Console.WriteLine("Request Receieved");
            using (var db = new InsuranceDBContext())
            {
              
                var policies = db.InsurancePolicies.AsQueryable();
               
                if (req.PolicyType == "Single" || req.PolicyType == "Married" || req.PolicyType == "Family")
                {
                    policies = policies.Where(x => x.PolicyType == req.PolicyType);
                }


               List<Policy> returnList = new List<Policy>();

                foreach (var p in policies)
                {
                    var policy = new Policy()
                    {
                        PolicyName = p.PolicyName,
                        ID = p.ID
                    };

                    policy.BasePrice = CalculateBasePrice(p, req);
                    returnList.Add(policy);
                }

                
                return returnList.ToArray();

            }
        }

        private decimal CalculateBasePrice(InsurancePolicy insurancePolicy, FindPolicyRequest req)
        {
            var basePrice = insurancePolicy.BasePrice;
              var ageOfApplicant = DateTime.Now.Year - req.DateOfBirth.Year;
            if (ageOfApplicant < 20)
            {
                basePrice += 2M;
            } else if (ageOfApplicant >= 20 && ageOfApplicant < 30)
            {
                basePrice += 5M;
            }
            else
            {
                basePrice += fib((ageOfApplicant)) + 10M;

            }

            return basePrice;
        }
        public static int fib(int n)
        {
            if (n == 0 || n == 1)
                return n;
            else
                return (fib(n - 1) + fib(n - 2));
        }
        public static int Fibonacci(int n)
        {
            int a = 0;
            int b = 1;
            // In N steps compute Fibonacci sequence iteratively.
            for (int i = 0; i < n; i++)
            {
                int temp = a;
                a = b;
                b = temp + b;
            }
            return a;
        }
    }


    public class DataSeeder
    {
        public void InsertRows()
        {
            List<Task> tt = new List<Task>();
            for (int i = 1; i <= 1000; i++)
            {
                using (var db = new InsuranceDBContext())
                {
                    for (int j = 1; j <= 1000; j++)
                    {
                        var ip = new InsurancePolicy()
                        {
                            PolicyName = "No Age Policy X1 " + i * j,
                            BasePrice = 10M + Convert.ToDecimal((i * j) / 100),

                        };
                        db.InsurancePolicies.Add(ip);
                    }
                    Console.WriteLine("saving group " + i);
                    db.SaveChanges();
                    Console.WriteLine("saved group " + i);
                }
            }

            //Console.WriteLine("All groups done");

            //Parallel.For(0, 1000, (i, si) =>
            //{
            //    using (var db = new InsuranceDBContext())
            //    {
            //        Parallel.For(0, 1000, (j, sj) =>
            //        {
            //            var ip = new InsurancePolicy()
            //            {
            //                PolicyName = "No Age Policy " + i*j,
            //                BasePrice = 10M + Convert.ToDecimal((i*j)/100),

            //            };
            //            db.InsurancePolicies.Add(ip);
            //        });

            //        Console.WriteLine("Saving group " + i);
            //        db.SaveChanges();
            //        Console.WriteLine("Saved group " + i);
            //        //var finished =
            //        //    db.SaveChangesAsync()
            //        //        .ContinueWith(t => Console.WriteLine("Inserted " + t.Result + " rows in group " + i));
            //        //tt.Add(finished);
            //    }
            //});

            //Task.WaitAll(tt.ToArray());
        }
    }

    public class InsuranceDBContext : DbContext
    {
        public DbSet<InsurancePolicy>  InsurancePolicies { get; set; }
    }

    public class InsurancePolicy
    {
        public int ID { get; set; }
        public string PolicyName { get; set; }
        public decimal BasePrice { get; set; }
        public char? GenderSpecific { get; set; }

        public int? MaximumAge { get; set; }
        public string PolicyType { get; set; }
    }
}
