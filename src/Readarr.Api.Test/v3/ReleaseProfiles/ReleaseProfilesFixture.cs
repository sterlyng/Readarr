using FluentAssertions;
using NUnit.Framework;
using Readarr.Api.V1.Profiles.Release;
using Readarr.Common.Serializer.System.Text.Json;
using Readarr.Http.REST;

namespace Readarr.Api.Test.v3.ReleaseProfiles
{
    [TestFixture]
    public class ReleaseProfilesFixture
    {
        [Test]
        public void should_deserialize_releaseprofile_v3_ignored_null()
        {
            var resource = STJson.Deserialize<ReleaseProfileResource>("{ \"ignored\": null, \"required\": null }");

            var model = resource.ToModel();

            model.Ignored.Should().BeEquivalentTo();
            model.Required.Should().BeEquivalentTo();
        }

        [Test]
        public void should_deserialize_releaseprofile_v3_ignored_string()
        {
            var resource = STJson.Deserialize<ReleaseProfileResource>("{ \"ignored\": \"testa,testb\", \"required\": \"testc,testd\" }");

            var model = resource.ToModel();

            model.Ignored.Should().BeEquivalentTo("testa", "testb");
            model.Required.Should().BeEquivalentTo("testc", "testd");
        }

        [Test]
        public void should_deserialize_releaseprofile_v3_ignored_string_array()
        {
            var resource = STJson.Deserialize<ReleaseProfileResource>("{ \"ignored\": [ \"testa\", \"testb\" ], \"required\": [ \"testc\", \"testd\" ] }");

            var model = resource.ToModel();

            model.Ignored.Should().BeEquivalentTo("testa", "testb");
            model.Required.Should().BeEquivalentTo("testc", "testd");
        }

        [Test]
        public void should_throw_with_bad_releaseprofile_v3_ignored_type()
        {
            var resource = STJson.Deserialize<ReleaseProfileResource>("{ \"ignored\": {} }");

            Assert.Throws<BadRequestException>(() => resource.ToModel());
        }
    }
}
