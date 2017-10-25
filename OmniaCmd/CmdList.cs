using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BpOmniaBridge
{
    public class CommandList
    {
        #region System Commands
        // not used
        public void CloseApplication()
        {
            var cmd = new Command("close_app", "System", "CloseApplication");
            cmd.Send();
        }
        
        // not used
        public void CreateSessionKey()
        {
            var cmd = new Command("create_key", "System", "CreateSessionKey");
            cmd.Send();
        }
        
        // not used
        public void GetCurrentUser()
        {
            var cmd = new Command("current_user", "System", "GetCurrentUser");
            cmd.Send();
        }
        
        // not used
        public void GetPermissionLevel()
        {
            var cmd = new Command("perm_level", "System", "GetPermissionLevel");
            cmd.Send();
        }

        public Command Login(string username, string password)
        {
            var cmd = new Command("login", "System", "Login");
            string[] parNames = new string[] { "Username", "Password" };
            string[] parValues = new string[] { username, password};
            cmd.AddParams(parNames, parValues);
            cmd.Send();

            return cmd;
        }

        // not used
        public void Logout()
        {
            var cmd = new Command("logout", "System", "Logout");
            cmd.Send();
        }
        #endregion

        #region Archive Commands

        // not used
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

        public Command SelectCreateSubject(string[] subjNames, string[] subjValues)
        {
            var cmd = new Command("select_create_subj", "Archive", "SelectCreateSubject");
            cmd.AddParams(subjNames, subjValues);
            cmd.Send();

            return cmd;
        }

        public Command GetSubjectVisitList(string[] subjNames, string[] subjValues)
        {
            var cmd = new Command("list_visit_card", "Archive", "GetSubjectVisitList", true, 0);
            cmd.AddParams(subjNames, subjValues);
            cmd.Send();

            return cmd;
        }

        public Command CreateVisit(string[] subjNames, string[] subjValues)
        {
            var cmd = new Command("create_visit_card", "Archive", "CreateVisit", true, 0);
            cmd.AddParams(subjNames, subjValues);
            cmd.Send();

            return cmd;
        }

        public Command SelectVisit(string[] subjNames, string[] subjValues)
        {
            var cmd = new Command("select_visit_card", "Archive", "SelectVisit", true, 0);
            cmd.AddParams(subjNames, subjValues);
            cmd.Send();

            return cmd;
        }

        public Command ExportData(string[] subjNames, string[] subjValues)
        {
            var cmd = new Command("export_data", "Archive", "ExportData", true, 0);
            cmd.AddParams(subjNames, subjValues);
            cmd.Send();

            return cmd;
        }

        public Command ExportReport(string[] subjNames, string[] subjValues)
        {
            var cmd = new Command("export_report", "Archive", "ExportReport");
            cmd.AddParams(subjNames, subjValues);
            cmd.Send();

            return cmd;
        }



        #endregion
    }
}
