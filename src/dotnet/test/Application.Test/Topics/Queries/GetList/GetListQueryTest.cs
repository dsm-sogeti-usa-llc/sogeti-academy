using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Sogeti.Academy.Application.Storage;
using Sogeti.Academy.Application.Topics.Models;
using Sogeti.Academy.Application.Topics.Queries.GetList;
using Sogeti.Academy.Application.Topics.Storage;
using Xunit;

namespace Sogeti.Academy.Application.Test.Topics.Queries.GetList
{
    public class GetListQueryTest
    {
        private readonly Mock<IDocumentCollection<Topic>> _topicCollectionMock;
        private readonly GetListQuery _getListQuery;

        public GetListQueryTest()
        {
            _topicCollectionMock = new Mock<IDocumentCollection<Topic>>();
            var topicsContextMock = new Mock<ITopicsContext>();
            topicsContextMock.Setup(s => s.GetCollection<Topic>()).Returns(_topicCollectionMock.Object);

            _getListQuery = new GetListQuery(topicsContextMock.Object);
        }

        [Fact]
        public async Task Execute_ShouldGetAllTopicsInCollection()
        {
            var topics = new List<Topic>
            {
                new Topic(),
                new Topic(),
                new Topic()
            };
            _topicCollectionMock.Setup(s => s.GetAllAsync()).ReturnsAsync(topics);

            var viewModel = await _getListQuery.Execute();
            Assert.Equal(3, viewModel.Topics.Length);
        }

        [Fact]
        public async Task Execute_ShouldMapEachTopicToViewModel()
        {
            var topics = new List<Topic>
            {
                new Topic
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "React"
                }
            };
            _topicCollectionMock.Setup(s => s.GetAllAsync()).ReturnsAsync(topics);

            var viewModel = (await _getListQuery.Execute()).Topics[0];
            Assert.Equal(topics[0].Id, viewModel.Id);
            Assert.Equal(topics[0].Name, viewModel.Name);
        }

        [Fact]
        public async Task Execute_ShouldCountDistinctVotes()
        {
            var topics = new List<Topic>
            {
                new Topic
                {
                    Votes = new List<Vote>
                    {
                        new Vote {Email = "bob"},
                        new Vote {Email = "Bill"},
                        new Vote {Email = "bob"},
                        new Vote {Email = "Jake"},
                        new Vote {Email = "Jake"},
                    }
                }
            };
            _topicCollectionMock.Setup(s => s.GetAllAsync()).ReturnsAsync(topics);

            var viewModel = (await _getListQuery.Execute()).Topics[0];
            Assert.Equal(3, viewModel.Votes);
        }

        [Fact]
        public async Task Exectue_ShouldOrderTopicsByVotes()
        {
            var topics = new List<Topic>
            {
                new Topic
                {
                    Name = "React",
                    Votes = new List<Vote>
                    {
                        new Vote {Email = "Bob"}
                    }
                },
                new Topic
                {
                    Name = "TDD",
                    Votes = new List<Vote>
                    {
                        new Vote {Email = "Bill"},
                        new Vote {Email = "John"},
                        new Vote {Email = "Jill"}
                    }
                },
                new Topic
                {
                    Name = "Bogus",
                    Votes = new List<Vote>
                    {
                        new Vote {Email = "Jake"},
                        new Vote {Email = "Bob"},
                    }
                }
            };
            _topicCollectionMock.Setup(s => s.GetAllAsync()).ReturnsAsync(topics);

            var viewModel = await _getListQuery.Execute();
            Assert.Equal("TDD", viewModel.Topics[0].Name);
            Assert.Equal("Bogus", viewModel.Topics[1].Name);
            Assert.Equal("React", viewModel.Topics[2].Name);
        }
    }
}
