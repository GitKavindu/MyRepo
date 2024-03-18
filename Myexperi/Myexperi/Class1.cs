using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myexperi
{
    internal class Class1
    {
        static String sUsername;
        static String sUserPassword;
        static String sPreferedName;
        static String sNationalID;
        static bool sIsAdmin;
        static String sEmail;
        static int sMobileNo;

        public static string adminname;

        internal static void setvalues(String Username, String UserPassword)
        {

            sUsername = Username;
            sUserPassword = UserPassword;
            
        }
        internal static void setvalues(String Username, String UserPassword, String PreferedName, String NationalID, bool IsAdmin, String Email,int MobileNo)
        {

            sUsername = Username;
            sUserPassword = UserPassword;
            sPreferedName=PreferedName;
            sNationalID= NationalID;
            sIsAdmin=IsAdmin;
            sEmail= Email;
            sMobileNo=MobileNo;
        }

        internal static String GetUsername()
        {
            return sUsername;
            
        }
        internal static String GetUserPassword()
        {
            return sUserPassword;

        }

        internal static String GetPreferedName()
        {
            return sPreferedName;

        }

        internal static String GettNationalID()
        {
            return sNationalID;

        }

        internal static String GetEmail()
        {
            return sEmail;

        }

        internal static bool GetIsAdmin()
        {
            return sIsAdmin;

        }
        internal static int GetMobileNo()
        {
            return sMobileNo;

        }


    }
}
