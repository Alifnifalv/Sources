namespace Eduegate.Integrations.Engine
{
    public class ArgParameter
    {
        public ArgParameter()
        {

        }

        public bool MigrateStudentProfile { get; set; }
        public string ServerPath { get; set; }
        public string LocalPath { get; set; }

        public bool MigrateTopics { get; set; }
    }
}
