using LinqToDB;
using LinqToDB.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebAddressbookTests
{
    public class AddressBookDB : LinqToDB.Data.DataConnection
    {
        public AddressBookDB() : base(ProviderName.MySql, 
            @"server=localhost; database=addressbook; port=3306; Uid=root; Pwd=; 
                charset=utf8; Allow Zero Datetime=true") { }

        public ITable<GroupData> Groups { get { return this.GetTable<GroupData>(); } }

        public ITable<ContactData> Contacts { get { return this.GetTable<ContactData>(); } }

        public ITable<GroupContactRelation> GCR { get { return this.GetTable<GroupContactRelation>(); } }
    }
}
