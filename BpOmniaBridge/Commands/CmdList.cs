using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BpOmniaBridge.CommandUtility;
using BpOmniaBridge.Utility;

namespace BpOmniaBridge.CommandList
{
    public class CommandList
    {
        public void CloseApplication()
        {
            var cmd = new Command("close_app", "System", "CloseApplication");
            cmd.Send();
        }

        public void CreateSessionKey()
        {
            var cmd = new Command("create_key", "System", "CreateSessionKey");
            cmd.Send();
        }

        public void GetCurrentUser()
        {
            var cmd = new Command("current_user", "System", "GetCurrentUser");
            cmd.Send();
        }

        public void GetPermissionLevel()
        {
            var cmd = new Command("perm_level", "System", "GetPermissionLevel");
            cmd.Send();
        }

        public void Login(string username, string password)
        {
            var cmd = new Command("login", "System", "Login");
            string[] parNames = new string[] { "Username", "Password" };
            string[] parValues = new string[] { username, password};
            cmd.AddParams(parNames, parValues);
            cmd.Send();
        }

        public void Logout()
        {
            var cmd = new Command("logout", "System", "Logout");
            cmd.Send();
        }
    }
}
