using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using TCAPPServer.Domain;
using TCAPPServer;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NHibernate;

namespace TCAPPTests
{
    [TestFixture]
    public class Class1
    {
        private ISessionFactory _sessionFactory;
        private Configuration _configuration;

        [TestFixtureSetUp]
        public void Setup()
        {
            _configuration = new Configuration();
            _configuration.Configure();
            _configuration.AddAssembly(typeof (Business).Assembly);
            _sessionFactory = _configuration.BuildSessionFactory();
            
            new SchemaExport(_configuration).Execute(false, true, false);
        }

        //[SetUp]
        //public void SetupContext()
        //{
        //    new SchemaExport(_configuration).Execute(false, true, false);
        //}

        [Test]
        public void CreateBusiness()
        {
            IService service = new Service();
            Business b = new Business();
            b.Id = 1;
            b.Name = "SKY";
            b.Employees = new List<Person>();
            Person p = new Person();
            p.Id = 1;
            p.Name = "Sepehr";
            b.Employees.Add(p);
            service.Add(b);

            Business sky = service.GetById(1);
            Assert.IsNotNull(sky);
        }

        [Test]
        public void GetBusiness()
        {
            IService service = new Service();
            Business sky = service.GetById(1);
            Assert.IsNotNull(sky);
        }



    }
}
