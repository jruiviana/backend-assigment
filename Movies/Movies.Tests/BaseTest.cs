using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Movies.Core.Profiles;
using Xunit;

[assembly: CollectionBehavior(DisableTestParallelization = true)]
namespace Movie.Tests
{
    public class BaseTest
    {
        protected Mapper CreateMapper()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfiles(new List<Profile>
                {
                    new SearchProfile(),
                    new ApiKeyProfile()
                });
            });
            var mapper = new Mapper(configuration);
            return mapper;
        }
        public static void AssertWith<TExpected, TActual>(IEnumerable<TActual> actual, IEnumerable<TExpected> expected, Action<TExpected, TActual> inspector)
        {
            Assert.Collection(actual, expected.Select(e => (Action<TActual>)(a => inspector(e, a))).ToArray());
        }
    }
}
