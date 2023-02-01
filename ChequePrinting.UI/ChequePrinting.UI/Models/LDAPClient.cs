using System;
using System.Collections.Generic;
using System.DirectoryServices.Protocols;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Web;

namespace ChequePrinting.UI.Models
{
    public class LDAPClient
    {
        private readonly LdapConnection connection;
        public LDAPClient(string userName, string domain, string password, string url)
        {
            var credentials = new NetworkCredential(userName, password, domain);
            var serverId = new LdapDirectoryIdentifier(url);
            connection = new LdapConnection(serverId, credentials);
            try
            {
                connection.Bind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool ValidateUser(string userName, string password)
        {
            var sha1 = new SHA1Managed();
            var digest = Convert.ToBase64String(sha1.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)));
            var request = new CompareRequest(string.Format("CN=Authorizer,OU=eNotify,OU=Groups,DC=scb,DC=com,DC=np", userName),
               password, "{SHA}" + digest);
            var response = (CompareResponse)connection.SendRequest(request);
            return response.ResultCode == ResultCode.CompareTrue;
        }
        public bool ValidateUserByBind(string userName, string password)
        {
            bool result = true;
            var credentials = new NetworkCredential(userName, password);
            var serverId = new LdapDirectoryIdentifier(connection.SessionOptions.HostName);

            var conn = new LdapConnection(serverId, credentials);
            try
            {
                conn.Bind();
            }
            catch (Exception)
            {
                result = false;
            }
            conn.Dispose();
            return result;
        }
    }
}