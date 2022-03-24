using System;
using Xunit;

namespace GradeBook.Tests
{

    public delegate string WriteLogDelegate(string message);

    public class TypeTests
    {
        int count = 0; 

        [Fact]
        public void WriteLogDelegateCanPointToMethod()
        {
            WriteLogDelegate log;

            log = ReturnMessage;

            var result = log("Hello!");

            Assert.Equal("Hello!", result);
        }

        [Fact]
        public void WriteLogDelegateCanPointToMethodCount()
        {
            WriteLogDelegate log = ReturnMessage;
            log += ReturnMessage;
            log += incrementCount;

            var result = log("Hello!");
            Assert.Equal(3, count);
        }

        string ReturnMessage(string message)
        {
            count++;
            return message;
        }        

        string incrementCount(string message)
        {
            count++;
            return message.ToLower();
        }     

        [Fact]
        public void GetBookReturnsDifferentObjects()
        {
            var book1 = GetBook("Book 1");
            var book2 = GetBook("Book 2");

            Assert.Equal("Book 1", book1.Name);
            Assert.Equal("Book 2", book2.Name);

            Assert.NotSame(book1, book2);
        }

		[Fact]
        public void TwoVarsCanReferenceSameObject()
        {
            var book1 = GetBook("Book 1");
            
            //This does not make a copy. It copies the value, 
            //which is a pointer to the memory address of book 1
            var book2 = book1;

            Assert.Equal("Book 1", book1.Name);
            Assert.Equal("Book 1", book2.Name);

            Assert.Same(book1, book2);
            Assert.True(Object.ReferenceEquals(book1, book2));
        }

        [Fact]
        public void CanSetNameFromReference()
        {
            var book1 = GetBook("Book 1");
            SetName(book1, "New Name");

            Assert.Equal("New Name", book1.Name);
        }

        [Fact]
        public void CSharpIsPassByValue()
        {
            var book1 = GetBook("Book 1");
            GetBookSetName(book1, "New Name");

            Assert.Equal("Book 1", book1.Name);
        }

        [Fact]
        public void CSharpIsPassByReference()
        {
            var book1 = GetBook("Book 1");
            GetBookSetNameRef(ref book1, "New Name");

            Assert.Equal("New Name", book1.Name);
        }

        private InMemoryBook GetBook(string name)
		{
			return new InMemoryBook(name);
		}

		private void SetName(InMemoryBook book, string name)
		{
			book.Name = name;
		}

		private void GetBookSetName(InMemoryBook book, string name)
		{
			book = new InMemoryBook(name);
		}

		private void GetBookSetNameRef(ref InMemoryBook book, string name)
		{
			book = new InMemoryBook(name);
		}




        [Fact]
        public void ValueTypePassByValue()
        {
            var x = getInt();
            setInt(x);

            Assert.Equal(3, x);
        }

        [Fact]
        public void ValueTypePassByReference()
        {
            var x = getInt();
            setIntRef(ref x);

            Assert.Equal(42, x);
        }

		private int getInt()
        {
            return 3;
        }

		private void setIntRef(ref int number)
		{
			number = 42;
		}

		private void setInt(int number)
		{
			number = 42;
		}








        [Fact]
        public void StringsBehaveLikeValueType()
        {
            string name = "Victor";
            makeUpperCase(name);
        
            Assert.Equal("Victor", name);
        }

		private void makeUpperCase(string parameter)
		{
			parameter.ToUpper();
		}


        [Fact]
        public void StringsBehaveLikeReferenceType()
        {
            string name = "Victor";
            var upper = getUpperCase(name);
        
            Assert.Equal("Victor", name);
            Assert.Equal("VICTOR", upper);
        }

		private string getUpperCase(string parameter)
		{
			return parameter.ToUpper();
		}
	}
}