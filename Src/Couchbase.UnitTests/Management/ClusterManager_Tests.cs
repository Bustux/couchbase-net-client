﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Couchbase.Configuration.Client;
using Couchbase.Configuration.Server;
using Couchbase.Management;
using Couchbase.Views;
using Moq;
using NUnit.Framework;

namespace Couchbase.UnitTests.Management
{
    [TestFixture]
    public class ClusterManagerTests
    {
        private ClientConfiguration _clientConfiguration;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _clientConfiguration = new ClientConfiguration()
            {
                BucketConfigs = new Dictionary<string, BucketConfiguration>()
                {
                    { "test", new BucketConfiguration() }
                }
            };
        }

        #region CreateBucket

        [Test]
        public void CreateBucket_FlushEnabledTrue_SendsWithCorrectParameter()
        {
            // Arrange

            var mockServerConfig = new Mock<IServerConfig>();

            var managerMock = new Mock<ClusterManager>(_clientConfiguration, mockServerConfig.Object,
                new JsonDataMapper(_clientConfiguration), new HttpClient(), "username", "password");
            managerMock
                .Setup(x => x.PostFormDataAsync(It.IsAny<Uri>(), It.Is<Dictionary<string, string>>(p => p["flushEnabled"] == "1")))
                .Returns(Task.FromResult((IResult) new DefaultResult(true, "success", null)));

            // Act

            managerMock.Object.CreateBucket("test", flushEnabled: true);

            // Assert

            managerMock.Verify(
                x => x.PostFormDataAsync(It.IsAny<Uri>(), It.Is<Dictionary<string, string>>(p => p["flushEnabled"] == "1")),
                Times.Once);
        }

        [Test]
        public void CreateBucket_FlushEnabledFalse_SendsWithCorrectParameter()
        {
            // Arrange

            var mockServerConfig = new Mock<IServerConfig>();

            var managerMock = new Mock<ClusterManager>(_clientConfiguration, mockServerConfig.Object,
                new JsonDataMapper(_clientConfiguration), new HttpClient(), "username", "password");
            managerMock
                .Setup(x => x.PostFormDataAsync(It.IsAny<Uri>(), It.Is<Dictionary<string, string>>(p => p["flushEnabled"] == "0")))
                .Returns(Task.FromResult((IResult)new DefaultResult(true, "success", null)));

            // Act

            managerMock.Object.CreateBucket("test", flushEnabled: false);

            // Assert

            managerMock.Verify(
                x => x.PostFormDataAsync(It.IsAny<Uri>(), It.Is<Dictionary<string, string>>(p => p["flushEnabled"] == "0")),
                Times.Once);
        }

        [Test]
        public void CreateBucket_IndexReplicasTrue_SendsWithCorrectParameter()
        {
            // Arrange

            var mockServerConfig = new Mock<IServerConfig>();

            var managerMock = new Mock<ClusterManager>(_clientConfiguration, mockServerConfig.Object,
                new JsonDataMapper(_clientConfiguration), new HttpClient(), "username", "password");
            managerMock
                .Setup(x => x.PostFormDataAsync(It.IsAny<Uri>(), It.Is<Dictionary<string, string>>(p => p["replicaIndex"] == "1")))
                .Returns(Task.FromResult((IResult)new DefaultResult(true, "success", null)));

            // Act

            managerMock.Object.CreateBucket("test", indexReplicas: true);

            // Assert

            managerMock.Verify(
                x => x.PostFormDataAsync(It.IsAny<Uri>(), It.Is<Dictionary<string, string>>(p => p["replicaIndex"] == "1")),
                Times.Once);
        }

        [Test]
        public void CreateBucket_IndexReplicasFalse_SendsWithCorrectParameter()
        {
            // Arrange

            var mockServerConfig = new Mock<IServerConfig>();

            var managerMock = new Mock<ClusterManager>(_clientConfiguration, mockServerConfig.Object,
                new JsonDataMapper(_clientConfiguration), new HttpClient(), "username", "password");
            managerMock
                .Setup(x => x.PostFormDataAsync(It.IsAny<Uri>(), It.Is<Dictionary<string, string>>(p => p["replicaIndex"] == "0")))
                .Returns(Task.FromResult((IResult)new DefaultResult(true, "success", null)));

            // Act

            managerMock.Object.CreateBucket("test", indexReplicas: false);

            // Assert

            managerMock.Verify(
                x => x.PostFormDataAsync(It.IsAny<Uri>(), It.Is<Dictionary<string, string>>(p => p["replicaIndex"] == "0")),
                Times.Once);
        }

        #endregion
    }
}