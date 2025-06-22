using ANIMALITOS_PHARMA_API.Accessors;
using ANIMALITOS_PHARMA_API.Accessors.Util.Helper;
using ANIMALITOS_PHARMA_API.Accessors.Util.StatusEnumerable;
using ANIMALITOS_PHARMA_API.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ANIMALITOS_PHARMA_API.Test.AccessorTest
{
    [TestClass]
    public class AccessorTest
    {
        private readonly AnimalitosClient accessor = new();
        private readonly Helper Helper = new();

        [TestMethod]
        public void CrudTest()
        {
            int id = CreateAddressBook();
            ReadAddressBook(id);
            UpdateAddressBook(id);
            DeleteAddressBook(id);
        }

        public int CreateAddressBook()
        {
            var addressBook = new AddressBook
            {
                Direction = "AddressBookDirection",
                Phone = "AddressBookPhone",
                Email = "AddressBookEmail",
                Rfc = "AddressBookRfc",
                StatusId = (int)ObjectStatus.UNIT_TEST_CREATE
            };

            addressBook = accessor.CreateAddressBook(addressBook);
            return addressBook.Id;
        }
        public void ReadAddressBook(int id)
        {
            var addressBook = accessor.GetAddressBook(id);

            Assert.AreEqual("AddressBookDirection", addressBook.Direction);
            Assert.AreEqual("AddressBookPhone", addressBook.Phone);
            Assert.AreEqual("AddressBookEmail", addressBook.Email);
            Assert.AreEqual("AddressBookRfc", addressBook.Rfc);
            Assert.AreEqual((int)ObjectStatus.UNIT_TEST_CREATE, addressBook.StatusId);
        }
        public void UpdateAddressBook(int id)
        {
            var addressBook = new AddressBook
            {
                Id = id,
                Direction = "AddressBookDirectionUpdate",
                Phone = "AddressBookPhoneUpdate",
                Email = "AddressBookEmailUpdate",
                Rfc = "AddressBookRfcUpdate",
                StatusId = (int)ObjectStatus.UNIT_TEST_CREATE
            };
            var addressBookUpdate = accessor.UpdateAddressBook(addressBook);

            Assert.AreEqual("AddressBookDirectionUpdate", addressBookUpdate.Direction);
            Assert.AreEqual("AddressBookPhoneUpdate", addressBookUpdate.Phone);
            Assert.AreEqual("AddressBookEmailUpdate", addressBookUpdate.Email);
            Assert.AreEqual("AddressBookRfcUpdate", addressBookUpdate.Rfc);
            Assert.AreEqual((int)ObjectStatus.UNIT_TEST_CREATE, addressBookUpdate.StatusId);
        }
        public void DeleteAddressBook(int id)
        {
            var addressBook = new AddressBook
            {
                Id = id
            };

            accessor.DeleteAddressBook(addressBook, true);
            var addressBookDelete = accessor.GetAddressBook(id);

            Assert.IsNull(addressBookDelete);
        }
    }
}