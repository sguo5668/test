using PagedList.Core;
using System.Collections.Generic;
using System.Linq;

namespace DIA.Web
{
    public class TestService
    {
        private IList<TestModel> sampleData;

        public TestService()
        {
            this.sampleData = new List<TestModel>();

            for (var i = 1; i <= 500; i++)
            {
                this.sampleData.Add(
                    new TestModel()
                    {
                        Name = "Test " + i
                    });
            }
        }

        public IPagedList<TestModel> GetTests(int pageNumber, int pageSize)
        {
            var tests = this.sampleData.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            return new StaticPagedList<TestModel>(tests, pageNumber, pageSize, this.sampleData.Count);
        }
    }
}
