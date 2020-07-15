using Bogus;
using System;
using System.Collections.Generic;
using Veritaware.Toolkits.LightVM.WpfMock.IServices;

namespace Veritaware.Toolkits.LightVM.WpfMock.MockServices
{
    class MockNamesService : INamesService
    {
        private readonly Faker _faker;

        public MockNamesService()
        {
            _faker = new Faker("en");
        }

        public string Get() => _faker.Name.FullName();
        public ICollection<string> Get(int count)
        {
            var names = new List<string>();
            for(var i = 0; i < count; i++)
            {
                names.Add(_faker.Name.FullName());
            }
            return names;
        }
    }
}
