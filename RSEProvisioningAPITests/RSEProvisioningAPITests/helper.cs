using System.Linq;

namespace RseProvisioningApiTests
{
    public static class Helper
    {
        public static bool ArraysEqual<T>(T[] a1, T[] a2)
        {
            if (ReferenceEquals(a1, a2))
                return true;

            if (a1 == null || a2 == null)
                return false;

            if (a1.Length != a2.Length)
                return false;

            var l1 = a1.ToList();
            var l2 = a2.ToList();

            foreach (var item in l1)
            {
                if (!l2.Contains(item))
                {
                    return false;
                }
                l2.Remove(item);
            }
            if (l2.Any())
            {
                return false;
            }
            return true;
        }
    }
}
