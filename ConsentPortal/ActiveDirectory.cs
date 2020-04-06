using System;
using System.DirectoryServices;

namespace ConsentPortal.Pages
{ 
    internal class ActiveDirectory
    {
        internal bool Authenticate(string domain, string userName, string password)
        {
            string domainAndUsername = domain + "\\" + userName;

            try
            {
                using (DirectoryEntry entry = new DirectoryEntry("LDAP://172.27.4.83", domainAndUsername, password))
                {
                    using DirectorySearcher search = new DirectorySearcher(entry)
                    {
                        Filter = "(SAMAccountName=" + userName + ")"
                    };

                    search.PropertiesToLoad.Add("dn");
                    search.PropertiesToLoad.Add("cn");
                    search.PropertiesToLoad.Add("SAMAccountName");
                    SearchResult result = search.FindOne();

                    if (result == null)
                        return false;
                }
                return true;
            }
            catch (DirectoryServicesCOMException ex)
            {
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}