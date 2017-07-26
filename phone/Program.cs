using System;
using System.Net;
using System.DirectoryServices;
using System.DirectoryServices.Protocols;
using System.Security.Permissions;

namespace ConnectLDAP
{
    [DirectoryServicesPermission(SecurityAction.Demand, Unrestricted = true)]

    public class LDAPConnect
    {
        // static variables used throughout the example
        static LdapConnection ldapConnection;
        static string ldapServer;
        static NetworkCredential credential;
        static string targetOU; // dn of an OU. eg: "OU=sample,DC=fabrikam,DC=com"

        public static void Main(string[] args)
        {
            try
            {
                GetParameters(args);  // Get the Command Line parameters

                // Create the new LDAP connection
                ldapConnection = new LdapConnection(ldapServer);
                ldapConnection.Credential = credential;
                Console.WriteLine("LdapConnection is created successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine("\r\nUnexpected exception occurred:\r\n\t" + e.GetType() + ":" + e.Message);
            }
        }

        static void GetParameters(string[] args)
        {
            // When running: ConnectLDAP.exe <ldapServer> <user> <pwd> <domain> <targetOU>

            if (args.Length != 5)
            {
                Console.WriteLine("Usage: ConnectLDAP.exe <ldapServer> <user> <pwd> <domain> <targetOU>");
                Environment.Exit(-1);// return an error code of -1
            }

            // test arguments to ensure they are valid and secure

            // initialize variables
            ldapServer = args[0];
            credential = new NetworkCredential(args[1], args[2], args[3]);
            targetOU = args[4];
        }
    }
}