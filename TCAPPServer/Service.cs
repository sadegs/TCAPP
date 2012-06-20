using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using NHibernate.Cfg;
using TCAPPServer.Domain;

namespace TCAPPServer
{
    public class Service : IService
    {

        public void Add(Business business)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(business);
                    transaction.Commit();
                }
        }

        public void Update(Business business)
        {
            throw new NotImplementedException();
        }

        public void Remove(Business business)
        {
            throw new NotImplementedException();   
        }

        public Business GetById(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                Business business = session.Get<Business>(id);
                return business;
            }
        }

        public List<Business> GetAll()
        {

            throw new NotImplementedException();
        }
    }

    public interface IService
    {
        void Add(Business business);
        void Update(Business business);
        void Remove(Business business);
        Business GetById(int id);
        List<Business> GetAll();        
    }

    public static class NHibernateHelper
    {

        private static ISessionFactory _sessionFactory;

        private static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                {
                    var configuration = new Configuration();
                    configuration.Configure();
                    configuration.AddAssembly(typeof(Business).Assembly);
                    _sessionFactory = configuration.BuildSessionFactory();
                }
                return _sessionFactory;
            }
        }

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }
    }
    
}