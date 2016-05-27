using mx.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstimationConsole.Commands
{
    public class ChangeGroup : CommandBase
    {
        public ChangeGroup(ProjectModuleBase parent) : base("cd", parent)
        {

        }

        protected override void OnExecute(string[] args)
        {
            if (args == null)
                return;

            if (args.Length != 2)
            {
                Console.WriteLine("Invalid Arguments");
                return;
            }

            try
            {
                if (string.Compare(args[1], "..") == 0)
                {
                    this.ProjectRef.WorkingGroup = this.MoveUp(this.ProjectRef.WorkingGroup);
                }
                else if (string.Compare(args[1], "/") == 0)
                {
                    this.ProjectRef.WorkingGroup = this.MoveRoot();
                }
                else
                {
                    this.ProjectRef.WorkingGroup = this.ChangeDirectory(this.ProjectRef.WorkingGroup, args[1]);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private Group MoveRoot()
        {
            return this.ProjectRef.ActivityRoot;
        }

        private Group MoveUp(Group workingGroup)
        {
            if (workingGroup != null)
            {
                if (workingGroup.Parent != null)
                {
                    return workingGroup.Parent;
                }
            }

            return workingGroup;
        }

        private Group ChangeDirectory(Group workingGroup, string path)
        {
            if (path.StartsWith("/"))
                workingGroup = this.ProjectRef.ActivityRoot;

            string[] args = path.Split('/', '\\');

            foreach (string arg in args)
            {
                workingGroup = workingGroup.FindItemByName(arg) as Group;
                if (workingGroup == null)
                {
                    throw new Exception(string.Format("Invalid Directory '{0}'", arg));
                }
            }

            return workingGroup;
        }
    }
}
