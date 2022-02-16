using System;

namespace MahdeFooald.Common
{
    public static class ExceptionMessages
    {
        public static string NUllArgumentException(Type argumenttype, object argumentname)
                                => $"{argumenttype.GetType().Name} passed null to the {nameof(argumentname)} Method";


        public static string NUllArgumentException(Type[] argumenttypes, object[] argumentnames)
        {
            var result = string.Empty;

            for (int i = 0; i < argumenttypes.Length; i++)
                result += $"{argumenttypes[i].GetType().Name} passed null to the {argumentnames[i]} Method \n";


            return result;
        }
    }
}
