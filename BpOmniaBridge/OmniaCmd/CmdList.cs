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
        #region System Commands
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

        public bool Login(string username, string password)
        {
            var cmd = new Command("login", "System", "Login");
            string[] parNames = new string[] { "Username", "Password" };
            string[] parValues = new string[] { username, password};
            cmd.AddParams(parNames, parValues);
            cmd.Send();

            string[] resultKeys = new string[] { "Result" };
            var answer = new ReadCommands("login", "System", "Login");
            return answer.ResultValues[0] == "ACK";
        }

        public void Logout()
        {
            var cmd = new Command("logout", "System", "Logout");
            cmd.Send();
        }
        #endregion

        #region Archive Commands

        public bool ChangeSubject(Guid recordID, string[] subjNames, string[] subjValues)
        {
            var cmd = new Command("change_subj", "Archive", "ChangeSubject");
            List<string> listNames = subjNames.ToList();
            List<string> listValues = subjValues.ToList();
            listNames.Insert(0,"RecordID");
            listValues.Insert(0, recordID.ToString());
            string[] parNames = listNames.ToArray();
            string[] parValues = listValues.ToArray();
            cmd.AddParams(parNames, parValues);
            cmd.Send();

            string[] resultKeys = new string[] { "Result" };
            var answer = new ReadCommands("change_subj", "Archive", "ChangeSubject");
            return answer.ResultValues[0] == "ACK";
        }

        public string SelectCreateSubject(string[] subjNames, string[] subjValues)
        {
            var cmd = new Command("select_create_subj", "Archive", "SelectCreateSubject");
            cmd.AddParams(subjNames, subjValues);
            cmd.Send();

            var answer = new ReadCommands("select_create_subj", "Archive", "SelectCreateSubject");
            return answer.ResultValues[0];
        }

        public List<object> GetSubjectVisitList(string[] subjNames, string[] subjValues)
        {
            var cmd = new Command("list_visit_card", "Archive", "GetSubjectVisitList");
            cmd.AddParams(subjNames, subjValues);
            cmd.Send();

            var answer = new ReadCommands("list_visit_card", "Archive", "GetSubjectVisitList");
            List<object> result = new List<object> { };
            result.Add(answer.ResultKeys);
            result.Add(answer.ResultValues);
            return result;
        }



        #endregion
    }
}
