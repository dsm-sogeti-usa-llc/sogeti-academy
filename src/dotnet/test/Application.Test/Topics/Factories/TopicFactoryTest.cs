using System;
using Sogeti.Academy.Application.Topics.Factories;
using Xunit;

namespace Sogeti.Academy.Application.Test.Topics.Factories
{
    public class TopicFactoryTest
    {
        private readonly TopicFactory _topicFactory;

        public TopicFactoryTest()
        {
            _topicFactory = new TopicFactory();
        }

        [Fact]
        public void Create_ShouldInitializeIdWithNewGuid()
        {
            var topic = _topicFactory.Create(null);
            Assert.NotEqual(Guid.Empty, Guid.Parse(topic.Id));
        }

        [Fact]
        public void Create_ShouldSetTopicName()
        {
            var topic = _topicFactory.Create("React");
            Assert.Equal("React", topic.Name);
        }
    }
}
