using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileDataSearch.Model
{
    public class MaharasthraNew
    {
        public int Id { get; set; }
        public string Customer_Name { get; set; }
        public string Father_Name { get; set; }
        public string BirthDate { get; set; }
        public string Local_Address { get; set; }
        public string Permanent_Address { get; set; }
        public string Local_PinCode { get; set; }
        public string Permanent_PinCode { get; set; }
        public string Gender { get; set; }
        public string VotingCardNo { get; set; }
        public string AdharCardNo { get; set; }
        public string PanCard { get; set; }
        public string MobileNo { get; set; }
        public string AltMobile_Number { get; set; }
        public string Profession { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Operator { get; set; }
        public string Catagory { get; set; }
        public string IsActive { get; set; }
        public string CustomerDate { get; set; }
        public string Source_of_Data { get; set; }
    }

    public static class Extensions
    {
        public static List<List<T>> partition<T>(this List<T> values, int chunkSize)
        {
            return values.Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / chunkSize)
                .Select(x => x.Select(v => v.Value).ToList())
                .ToList();
        }

        private static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto,
        };

        public static byte[] Serialize<T>(this T source)
        {
            var asString = JsonConvert.SerializeObject(source, SerializerSettings);
            return Encoding.Unicode.GetBytes(asString);
        }

        public static T Deserialize<T>(this byte[] source)
        {
            var asString = Encoding.Unicode.GetString(source);
            return JsonConvert.DeserializeObject<T>(asString);
        }

    }
}
