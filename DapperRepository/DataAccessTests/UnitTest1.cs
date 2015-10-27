using System.Collections.Generic;
using DataAccessLayer;
using DataAccessLayer.repository;
using DataAccessModels;
using NUnit.Framework;

namespace DataAccessTests
{
    [TestFixture]
    public class UnitTest1
    {
        [Test]
        public void TestUpdateWorks()
        {
            Repository repository = new Repository(new SqlConnectionDev());
            Club club = new Club();
            club.clubId = 1;
            club.Name = "Stirling Kyokushin";

            bool result = repository.Update(club);
            Assert.That(result);
        }

        [Test]
        public void TestCreateWorks()
        {
            Repository repository = new Repository(new SqlConnectionDev());
            Club club = new Club();
            club.Name = "Stirling Kyokushin";
            bool result = repository.Create(club);

            Assert.That(result);
        }

        [Test]
        public void TestReadWorks()
        {
            Repository repository = new Repository(new SqlConnectionDev());
            Club club = new Club();
            club.clubId = 2;
            var result = repository.Read<Club>(club);

            Assert.IsNotNull(result);
        }

        [Test]
        public void TestDeleteWorks()
        {
            Repository repository = new Repository(new SqlConnectionDev());
            Club club = new Club();
            club.clubId = 1;
            bool result = repository.Delete(club);

            Assert.IsNotNull(result);
        }
    }
}