using System.Text;

namespace ANIMALITOS_PHARMA_API.Accessors.Util.Helper
{
    public class Helper
    {
        private static Random random = new Random((int)DateTime.Now.Ticks);
        public string GenerateAlphanumericString(int lengthString)
        {
            const string pool = "abcdefghijklmnopqrstuvwxyz0123456789";
            var builder = new StringBuilder();

            for (var i = 0; i < lengthString; i++)
            {
                var c = pool[random.Next(0, pool.Length)];
                builder.Append(c);
            }

            return builder.ToString();
        }

        public string GenerateRandomString(int lengthString)
        {
            Guid myGuid = Guid.NewGuid();
            string randomString = myGuid.ToString().Replace("-", string.Empty).Substring(0, lengthString);

            return randomString;
        }

        public int GenerateRandomNumeric(int lengthString)
        {
            string stringNumeric = "";
            for (int i = 0; i < lengthString; i++)
            {
                stringNumeric += random.Next().ToString();
            }
            return Convert.ToInt32(stringNumeric);
        }
    }
}