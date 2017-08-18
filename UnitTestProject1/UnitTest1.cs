// Copy the files to the build target directory as part of the build process.
// Option 1: If they are specific to one test project, include them as content files in the Visual Studio test project.
// Select them in Solution Explorer and set the Copy to Output property to Copy if Newer.
// Option 2: define a post-build task to copy the files into the build output directory. For example:
// xcopy /Y /S "$(SolutionDir)SharedFiles\*" "$(TargetDir)"  
// In a C# project, Open the project properties of your test project, open the Build Events page, use post-build event command line
// 2. Use DeploymentItemAttribute on test methods or test classes to specify the files and folders, copied from the build output directory to the deployment directory.
// [DeploymentItem("source", "targetFolder")]
// string testData = System.IO.File.ReadAllText(@"targetFolder\source


using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        private TestContext context;

        public TestContext TestContext
        {
            get { return context; }
            set { context = value; }
        }

        //[DataSource(@"Provider=Microsoft.SqlServerCe.Client.4.0; Data Source=C:\AccessDB\TestDataSource.mdb;", "Numbers")]
        //[DataSource("Dsn=Excel Files;dbq=C:\\AccessDB\\data.xlsx;defaultdir=.; driverid=790;maxbuffersize=2048;pagetimeout=5", "System.Data.Odbc")]
        [DataSource(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\AccessDB\data.xlsx;Extended Properties='Excel 12.0;HDR=YES;IMEX=1;';")]
        [TestMethod()]
        public void AddIntegers_FromDataSourceTest()
        {
            // Access the data  
            int x = Convert.ToInt32(TestContext.DataRow["Val1"]);
            int y = Convert.ToInt32(TestContext.DataRow["Val2"]);

            Assert.AreEqual(context.DataRow["Val1"], context.DataRow["Val2"]);
        }

        [TestMethod()]
        //[DeploymentItem("C:\\AccessDB\\TestDataSource.mdb")] //MyTestProject\\
        [DataSource("MyJetDataSource")]
        public void MyTestMethod()
        {
            int a = Int32.Parse(context.DataRow["Arg1"].ToString());
            int b = Int32.Parse(context.DataRow["Arg2"].ToString());
            Assert.AreNotEqual(a, b, "A value was equal.");
        }

        [TestMethod()]
        //[DeploymentItem("MyTestProject\\data.xlsx")]
        [DataSource("MyExcelDataSource")]
        //[DataSource(dataProvider, connectionString, tableName, dataAccessMethod)]
        //<add name="MyExcelDataSource" connectionString="MyExcelConn" dataTableName="Sheet1$" dataAccessMethod="Sequential"/>
        //[DataSource("MyExcelDataSource", "MyExcelConn", "Sheet1$", DataAccessMethod.Sequential)]
        //[DataSource(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\AccessDB\data.xlsx;Extended Properties='Excel 12.0;HDR=YES;IMEX=1;';")]
        //[DataSource("CompanyAddressInfo_DataSource")]
        //[DeploymentItem("CoreUnitTests\\CompanyUnitTests\\CompanyTestData.xlsx")]
        public void MyTestMethod2()
        {
            Assert.AreEqual(context.DataRow["Val1"], context.DataRow["Val2"]);
        }
    }
}
