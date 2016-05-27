using mx.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstimationConsole.Commands
{
    public class ListGroup : CommandBase
    {
        public ListGroup(ProjectModuleBase parent) : base("ls", parent)
        {

        }

        protected override void OnExecute(string[] args)
        {
            if (args == null)
                return;

            if (this.ProjectRef == null || this.ProjectRef.WorkingGroup == null)
            {
                Console.WriteLine("error");
                return;
            }

            try
            {
                var sortedList = this.ProjectRef.WorkingGroup.Items.OrderBy(x => x.Name);
                foreach (ActivityObjectBase item in sortedList)
                {
                    if (item is Group)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                    Console.WriteLine(item.Name);
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}
