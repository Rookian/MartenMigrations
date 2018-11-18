using System;
using System.Threading.Tasks;

namespace CLIGlobalTool
{
	[Migration("Hey!")]
    public class Migration_11182018130641_Migration : Migration
    {
        public Task Migrate()
        {
			throw new NotImplementedException();
        }
    }
}