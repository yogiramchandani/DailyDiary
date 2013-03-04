using Xunit;

namespace DailyDiaryApi.AcceptanceTests
{
    public class UseDatabaseAttribute : BeforeAfterTestAttribute
    {
        public override void Before(System.Reflection.MethodInfo methodUnderTest)
        {
            new BootStrap().InstallDatabase();
            base.Before(methodUnderTest);
        }

        public override void After(System.Reflection.MethodInfo methodUnderTest)
        {
            new BootStrap().UninstallDatabase();
            base.After(methodUnderTest);
        }
    }
}