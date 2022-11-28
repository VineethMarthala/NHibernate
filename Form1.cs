using NHibernate;
using NHibernate.Cfg;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NHibernateTest
{
    public partial class Form1 : Form
    {
        private Configuration myConfiguration;
        private ISessionFactory mySessionFactory;
        private ISession mySession;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            myConfiguration = new Configuration();
            myConfiguration.Configure();
            mySessionFactory = myConfiguration.BuildSessionFactory();
            mySession = mySessionFactory.OpenSession();

            //Add
            using (mySession.BeginTransaction())
            {
                Employee employee = new Employee {Id=1, FirstName = "VIneeth", LastName="Reddy" };
                mySession.Save(employee);
                mySession.Transaction.Commit();
            }

            //Fetching Data From DB
            using (mySession.BeginTransaction())
            {
                ICriteria critera = mySession.CreateCriteria<Employee>();
                IList<Employee> list = critera.List<Employee>();

                

                mySession.Transaction.Commit();
            }

            using (mySession.BeginTransaction())
            {
                var employee = mySession.QueryOver<Employee>().Where(x => x.Id == 1);
                mySession.Delete(employee);
                mySession.Transaction.Commit();
            }
        }
    }
}
